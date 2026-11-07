using System.Security.Cryptography;
using System.Text;

namespace Project_IT_STEP.Services;

public class PasswordHasher
{
    public static string Hash(string password) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}