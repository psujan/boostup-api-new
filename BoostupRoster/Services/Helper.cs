using System.Security.Cryptography;

namespace Boostup.API.Services
{
    public static class Helper
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$&*%abcdefghijklmnopqrstuvwxyz0123456789";
            using var rng = RandomNumberGenerator.Create();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[GetRandomNumber(rng, chars.Length)]).ToArray());
        }

        public static int GetRandomNumber(RandomNumberGenerator rng, int max)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0) & int.MaxValue % max;
        }

    }
}
