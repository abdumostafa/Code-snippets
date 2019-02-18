using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
  /// <summary>Can be used to simply encrypt/decrypt texts.</summary>
  public class SimpleStringCipher
  {
    /// <summary>
    /// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
    /// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    /// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    /// </summary>
    public byte[] InitVectorBytes;
    /// <summary>
    /// This constant is used to determine the keysize of the encryption algorithm.
    /// </summary>
    public const int Keysize = 256;

    public static SimpleStringCipher Instance { get; }

    /// <summary>
    /// Default password to encrypt/decrypt texts.
    /// It's recommented to set to another value for security.
    /// Default value: "gsKnGZ041HLL4IM8"
    /// </summary>
    public static string DefaultPassPhrase { get; set; }

    /// <summary>
    /// Default value: Encoding.ASCII.GetBytes("jkE49230Tf093b42")
    /// </summary>
    public static byte[] DefaultInitVectorBytes { get; set; }

    /// <summary>Default value: Encoding.ASCII.GetBytes("hgt!16kl")</summary>
    public static byte[] DefaultSalt { get; set; }

    static SimpleStringCipher()
    {
      SimpleStringCipher.DefaultPassPhrase = "gsKnGZ041HLL4IM8";
      SimpleStringCipher.DefaultInitVectorBytes = Encoding.ASCII.GetBytes("jkE49230Tf093b42");
      SimpleStringCipher.DefaultSalt = Encoding.ASCII.GetBytes("hgt!16kl");
      SimpleStringCipher.Instance = new SimpleStringCipher();
    }

    public SimpleStringCipher()
    {
      this.InitVectorBytes = SimpleStringCipher.DefaultInitVectorBytes;
    }

    public string Encrypt(string plainText, string passPhrase = null, byte[] salt = null)
    {
      if (plainText == null)
        return (string) null;
      if (passPhrase == null)
        passPhrase = SimpleStringCipher.DefaultPassPhrase;
      if (salt == null)
        salt = SimpleStringCipher.DefaultSalt;
      byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
      using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt))
      {
        byte[] bytes2 = rfc2898DeriveBytes.GetBytes(32);
        using (Aes aes = Aes.Create())
        {
          aes.Mode = CipherMode.CBC;
          using (ICryptoTransform encryptor = aes.CreateEncryptor(bytes2, this.InitVectorBytes))
          {
            using (MemoryStream memoryStream = new MemoryStream())
            {
              using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
              {
                cryptoStream.Write(bytes1, 0, bytes1.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
              }
            }
          }
        }
      }
    }

    public string Decrypt(string cipherText, string passPhrase = null, byte[] salt = null)
    {
      if (string.IsNullOrEmpty(cipherText))
        return (string) null;
      if (passPhrase == null)
        passPhrase = SimpleStringCipher.DefaultPassPhrase;
      if (salt == null)
        salt = SimpleStringCipher.DefaultSalt;
      byte[] buffer = Convert.FromBase64String(cipherText);
      using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt))
      {
        byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
        using (Aes aes = Aes.Create())
        {
          aes.Mode = CipherMode.CBC;
          using (ICryptoTransform decryptor = aes.CreateDecryptor(bytes, this.InitVectorBytes))
          {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
              using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
              {
                byte[] numArray = new byte[buffer.Length];
                int count = cryptoStream.Read(numArray, 0, numArray.Length);
                return Encoding.UTF8.GetString(numArray, 0, count);
              }
            }
          }
        }
      }
    }
  }

