using System;
using System.Security.Cryptography;
using System.Text;

namespace OnaySistemi
{
    public static class SifreServisi
    {
        public static string SifreHashle(string sifre)
        {
            if (string.IsNullOrWhiteSpace(sifre))
                throw new ArgumentException("Þifre boþ olamaz.");

            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] sifreBytes = Encoding.UTF8.GetBytes(sifre);
            byte[] sifreSaltli = new byte[sifreBytes.Length + salt.Length];
            
            Buffer.BlockCopy(salt, 0, sifreSaltli, 0, salt.Length);
            Buffer.BlockCopy(sifreBytes, 0, sifreSaltli, salt.Length, sifreBytes.Length);

            byte[] hash;
            using (var sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(sifreSaltli);
            }

            byte[] saltVeHash = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, saltVeHash, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, saltVeHash, salt.Length, hash.Length);

            return Convert.ToBase64String(saltVeHash);
        }

        public static bool SifreDoðrula(string girilenSifre, string dbKayitliHashli)
        {
            if (string.IsNullOrWhiteSpace(girilenSifre) || string.IsNullOrWhiteSpace(dbKayitliHashli))
                return false;

            try
            {
                byte[] saltVeHash = Convert.FromBase64String(dbKayitliHashli);
                
                byte[] salt = new byte[16];
                Buffer.BlockCopy(saltVeHash, 0, salt, 0, 16);
                
                byte[] kaydedilenHash = new byte[saltVeHash.Length - 16];
                Buffer.BlockCopy(saltVeHash, 16, kaydedilenHash, 0, kaydedilenHash.Length);

                byte[] sifreBytes = Encoding.UTF8.GetBytes(girilenSifre);
                byte[] sifreSaltli = new byte[sifreBytes.Length + salt.Length];
                
                Buffer.BlockCopy(salt, 0, sifreSaltli, 0, salt.Length);
                Buffer.BlockCopy(sifreBytes, 0, sifreSaltli, salt.Length, sifreBytes.Length);

                byte[] hesaplananHash;
                using (var sha256 = SHA256.Create())
                {
                    hesaplananHash = sha256.ComputeHash(sifreSaltli);
                }

                return KarsilastirByteArrayler(hesaplananHash, kaydedilenHash);
            }
            catch
            {
                return false;
            }
        }

        private static bool KarsilastirByteArrayler(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int fark = 0;
            for (int i = 0; i < a.Length; i++)
            {
                fark |= a[i] ^ b[i];
            }

            return fark == 0;
        }

        public static (bool Gecerli, string Mesaj) SifreGeçerliliðiKontrol(string sifre)
        {
            if (string.IsNullOrWhiteSpace(sifre))
                return (false, "Þifre boþ olamaz.");

            if (sifre.Length < 4)
                return (false, "Þifre en az 4 karakter olmalýdýr.");

            return (true, "Þifre geçerli.");
        }
    }
}
