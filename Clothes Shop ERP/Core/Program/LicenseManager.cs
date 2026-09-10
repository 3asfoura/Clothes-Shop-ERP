using DeviceId;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Offline activation: a license key is a Machine ID + optional expiry, signed with Secret (HMAC-SHA256).
    public static class LicenseManager
    {
        private const string Secret = "Belnix-2026-ChangeThisSecretBeforeRealDistribution";
        private static readonly string LicenseFilePath = ResolveLicenseFilePath();

        // Migrates an old exe-folder license.dat into Sett.AppDataFolder so activation isn't lost.
        private static string ResolveLicenseFilePath()
        {
            string newPath = Path.Combine(Sett.AppDataFolder, "license.dat");
            string oldPath = Path.Combine(Application.StartupPath, "license.dat");
            try
            {
                if (!File.Exists(newPath) && File.Exists(oldPath))
                    File.Copy(oldPath, newPath);
            }
            catch { }
            return newPath;
        }

        /// <summary>A short, stable code identifying this PC. Shown to the shop owner to send to the vendor.</summary>
        public static string GetMachineId()
        {
            return new DeviceIdBuilder()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .AddSystemDriveSerialNumber()
                .ToString()
                .ToUpper();
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
            return IsActivated(out _);
        }

        /// <summary>Like IsActivated(), but also reports the license's expiry date (null = no expiry).</summary>
        public static bool IsActivated(out DateTime? expiryDate)
        {
            expiryDate = null;
            if (!File.Exists(LicenseFilePath)) return false;
            try
            {
                string licenseKey = File.ReadAllText(LicenseFilePath).Trim();
                return ValidateLicenseKey(GetMachineId(), licenseKey, out expiryDate, out _);
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
