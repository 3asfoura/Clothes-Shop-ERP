using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Offline activation: no internet and no license server involved.
    //
    // How it works, in plain terms:
    //  1. Each PC has a "Machine ID" computed from a couple of hardware serial
    //     numbers (motherboard + processor), so it's specific to that PC.
    //  2. A "License Key" is just that Machine ID plus an optional expiry date,
    //     signed with a secret password (HMAC-SHA256) that only lives in this
    //     source code. Anyone with a Machine ID and that secret can produce a
    //     valid key for it - but nobody without the secret can forge one.
    //  3. GenerateLicenseKey() is the "vendor" side: it makes keys. It's wired
    //     up behind the hidden Ctrl+Alt+G shortcut on the login screen (see
    //     FrmLogin.cs) so only whoever is selling/installing the software uses
    //     it - shops themselves only ever see Activate().
    //  4. ValidateLicenseKey() / IsActivated() is the "client" side: it checks
    //     a key is genuine and not expired, and remembers it in a local file.
    //
    // Caveat to be upfront about: the secret below lives in the compiled app,
    // so a determined person could eventually extract it and forge keys. For
    // a small business tool sold on trust this is a normal, accepted trade-off
    // (the same one most small offline-activated software makes) - it stops
    // casual copying, not a dedicated attacker. If that ever needs to be
    // stronger, the fix is moving key generation to a small separate tool the
    // vendor keeps privately, so the secret never ships inside the app at all.
    public static class LicenseManager
    {
        private const string Secret = "ClothesShopERP-2026-ChangeThisSecretBeforeRealDistribution";
        private static readonly string LicenseFilePath =
            Path.Combine(Application.StartupPath, "license.dat");

        /// <summary>A short, stable code identifying this PC. Shown to the shop owner to send to the vendor.</summary>
        public static string GetMachineId()
        {
            string raw;
            try
            {
                raw = ReadWmiValue("Win32_BaseBoard", "SerialNumber")
                    + "|" + ReadWmiValue("Win32_Processor", "ProcessorId");
            }
            catch
            {
                // WMI can be blocked in some locked-down/virtualized environments -
                // fall back to something still reasonably machine-specific.
                raw = Environment.MachineName + "|" + Environment.UserDomainName;
            }

            byte[] hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(raw));
            // Short, readable, groups of 5: xxxxx-xxxxx-xxxxx-xxxxx
            string code = Convert.ToBase64String(hash).Replace("+", "").Replace("/", "").Replace("=", "").ToUpper();
            code = code.Substring(0, Math.Min(20, code.Length));
            return string.Join("-", Enumerable.Range(0, code.Length / 5).Select(i => code.Substring(i * 5, 5)));
        }

        private static string ReadWmiValue(string wmiClass, string property)
        {
            using (var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wmiClass}"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    var value = obj[property]?.ToString();
                    if (!string.IsNullOrWhiteSpace(value)) return value;
                }
            }
            return "";
        }

        /// <summary>Vendor-only: produces a license key for a given Machine ID, optionally expiring on a date.</summary>
        public static string GenerateLicenseKey(string machineId, DateTime? expiryDate)
        {
            string payload = machineId.Trim().ToUpper() + "|" + (expiryDate.HasValue ? expiryDate.Value.ToString("yyyyMMdd") : "NOEXPIRY");
            string signature = ComputeSignature(payload);
            string combined = payload + "|" + signature;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(combined));
        }

        /// <summary>Checks a license key is genuinely signed for this machine and not expired.</summary>
        public static bool ValidateLicenseKey(string machineId, string licenseKey, out DateTime? expiryDate, out string error)
        {
            expiryDate = null;
            error = null;
            try
            {
                string combined = Encoding.UTF8.GetString(Convert.FromBase64String(licenseKey.Trim()));
                string[] parts = combined.Split('|');
                if (parts.Length != 3) { error = "Invalid license key format."; return false; }

                string keyMachineId = parts[0];
                string expiryPart = parts[1];
                string signature = parts[2];

                string payload = keyMachineId + "|" + expiryPart;
                if (ComputeSignature(payload) != signature) { error = "This license key is not valid."; return false; }

                if (!string.Equals(keyMachineId, machineId.Trim().ToUpper(), StringComparison.OrdinalIgnoreCase))
                {
                    error = "This license key was issued for a different computer.";
                    return false;
                }

                if (expiryPart != "NOEXPIRY")
                {
                    DateTime exp = DateTime.ParseExact(expiryPart, "yyyyMMdd", null);
                    expiryDate = exp;
                    if (DateTime.Today > exp) { error = "This license has expired."; return false; }
                }

                return true;
            }
            catch
            {
                error = "Invalid license key.";
                return false;
            }
        }

        private static string ComputeSignature(string payload)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                return Convert.ToBase64String(hash);
            }
        }

        public static bool IsActivated()
        {
            if (!File.Exists(LicenseFilePath)) return false;
            try
            {
                string licenseKey = File.ReadAllText(LicenseFilePath).Trim();
                return ValidateLicenseKey(GetMachineId(), licenseKey, out _, out _);
            }
            catch
            {
                return false;
            }
        }

        public static void SaveActivation(string licenseKey)
        {
            File.WriteAllText(LicenseFilePath, licenseKey.Trim());
        }
    }
}
