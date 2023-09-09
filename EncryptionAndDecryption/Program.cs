// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

//Console.WriteLine("Hello, World!");
//var request = "{\"emailAddress\":\"ebelebeemmanuel@yahoo.com\",\"phoneNumber\":\"09054423120\",\"password\":\"Test@123\",\"profileTypeId\":1}";
//var request2 = "{\"EmailAddress\":\"ebelebeemmanuel@yahoo.com\",\"PhoneNumber\":\"09054423120\",\"Password\":\"Test@123\",\"ProfileTypeId\":1}";

//physical card request payload
//var request = "{\"userId\":\"atolagbemuiz@gmail.com\",\"paymentExternalReference\":\"P-RiIO8lwo7DDy\",\"email\":\"atolagberianat@gmail.com\",\"amount\":5000,\"product\":110228,\"deliveryAddress\":\"Awoyaya Lagos\",\"recipientFirstName\":\"Rianat\",\"recipientLastName\":\"Atolagbe\",\"recipientPhoneNumber\":\"070809921000\"}";

//var request = "{\"userId\":\"atolagbemuiz@gmail.com\",\"amount\":20000,\"cardCategory\":3,\"design\":\"linear-gradient(277.4deg,#4DA6AE 0.433%, #4B7EA5 46.77%,#494899 90.1%)\",\"recipientFirstName\":\"atolagbemuiz@gmail.com\",\"recipientLastName\":\"07080992100\",\"recipientEmailAddress\":\"atolagbemuiz@gmail.com\"}";
//var request = "{\"userId\":\"atolagbemuiz@gmail.com\",\"amount\":20000,\"cardCategory\":3,\"design\":\"linear-gradient(127deg, rgba(0,255,0,.8), rgba(0,255,0,0))\",\"recipientFirstName\":\"atolagbemuiz@gmail.com\",\"recipientLastName\":\"07080992100\",\"recipientEmailAddress\":\"atolagbemuiz@gmail.com\"}";

//virtual card request payload
//var request = "{\"userId\":\"cholas.bassey@gmail.com\",\"amount\":5000,\"cardCategory\":1,\"design\":\"linear-gradient(127deg, rgba(0,255,0,.8), rgba(0,255,0,0))\",\"recipientFirstName\":\"Nicholas\",\"recipientLastName\":\"Bassey\",\"recipientEmailAddress\":\"atolagbemuiz@gmail.com\", \"PaymentExternalReference\":\"V-nwiXRgkiNiJC\"}";

//var request = "[{\"userId\":\"nickyawat@yahoo.com\",\"CheckOutItemId\":3212,\"recipientFirstName\":\"Muiz\",\"recipientLastName\":\"Atolagbe\",\"recipientEmailAddress\":\"muiz.atolagbe@sterling.ng\",\"phoneNumber\":\"09054044447\",\"cardCategory\":1,\"design\":\"linear - gradient(127deg, rgba(0, 255, 0, .8), rgba(0, 255, 0, 0))\",\"amount\":5000,\"message\":\"Happy Birthday\",\"paymentExternalReference\":\"BV-BmwjUgfES3oL\"}, {\"userId\":\"nickyawat@yahoo.com\",\"CheckOutItemId\":3212,\"recipientFirstName\":\"Muiz\",\"recipientLastName\":\"Atolagbe\",\"recipientEmailAddress\":\"atolagbemuiz@gmail.com\",\"phoneNumber\":\"09054044447\",\"cardCategory\":1,\"design\":\"linear - gradient(127deg, rgba(0, 255, 0, .8), rgba(0, 255, 0, 0))\",\"amount\":5000,\"message\":\"Happy Birthday\",\"paymentExternalReference\":\"BV-BmwjUgfES3oL\"}]";


//var request = "{\"pageNumber\":1,\"pageSize\":20}";
var request = "{\"email\": \"atolagbemuiz@gmail.com\"}";

//var response = "ae1b776c1558b6aae3d0ba87dafac941a4df4ef0f4f0e496ca6934d9b78c772f3293f75b5ba5bbde8ec2190122b3aee4d01e78553327246f7a495387be04593a8bdecf0ec230e9b9fa0012e3ee6bd2fe";
var encryteddata = Encrypt(request);
//var decryptData = Decrypt(response);
//var decryptdata = Decrypt(encryteddata);    
Console.WriteLine($"{encryteddata}");
//Console.WriteLine($"{decryptData}");
Console.ReadLine(); 

static string Decrypt(string ciphertext)
{
    string roundtrip = string.Empty;
    //APIResponse<string> response = new APIResponse<string>();
    try
    {  // Create a new instance of the Aes    
       // class.  This generates a new key and initialization                
       // vector (IV).                
        using (Aes myAes = Aes.Create())
        {
            var EMPKey = "zMdRgUkXp2s5v8y/B?O(H+MbPeShZxCe";
            var EmpIV = "qThPmYq8s6v9y$G&";
            var GiftCardKey = "wHdRgUkXp2s5v8y/B?O(H+MbPeShZxCg";
            var GiftCardIV = "tThPmYq9s6v9y$G$";
            myAes.Key = System.Text.Encoding.UTF8.GetBytes(EMPKey);
            myAes.IV = System.Text.Encoding.UTF8.GetBytes(EmpIV);
            string newres = ciphertext.Replace("\"", "");
            //string newresult = newre.Replace("{}", "");
            // Decrypt the bytes to a string.
            roundtrip = DecryptStringFromBytes_Aes(newres, myAes.Key, myAes.IV);
            if (!string.IsNullOrEmpty(roundtrip))
            {
                return roundtrip;

            }

        }

    }
    catch (Exception ex)
    {

        //_logger.Error(ex);
    }
    return roundtrip;
}
static string DecryptStringFromBytes_Aes(string cipherText, byte[] Key, byte[] IV)
{             // Check arguments.            
    if (cipherText == null || cipherText.Length <= 0)
        throw new ArgumentNullException("cipherText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("AppId");

    // Declare the string used to hold            
    // the decrypted text.            
    string plaintext = null;

    // Create an Aes object            
    // with the specified key and IV.            
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = Key;
        aesAlg.IV = IV;
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        byte[] cipherbytes = HexToByteArray(cipherText);

        // Create the streams used for decryption.                
        using (MemoryStream msDecrypt = new MemoryStream(cipherbytes))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted bytes from the decrypting stream                            
                    // and place them in a string.                            
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }

    }

    return plaintext;

}
static byte[] HexToByteArray(string hex)
{
    return Enumerable.Range(0, hex.Length)
      .Where(x => x % 2 == 0)
      .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
      .ToArray();
}
string ByteArrayToString(byte[] ba)
{
    StringBuilder hex = new StringBuilder(ba.Length * 2);
    foreach (byte b in ba) hex.AppendFormat("{0:x2}", b); return hex.ToString();
}
string Encrypt(string plaintext)
{
    try
    {
        using (Aes myAes = Aes.Create())
        {
            var EMPKey = "zMdRgUkXp2s5v8y/B?O(H+MbPeShZxCe";
            var EmpIV = "qThPmYq8s6v9y$G&";
            var GiftCardKey = "wHdRgUkXp2s5v8y/B?O(H+MbPeShZxCg";
            var GiftCardIV = "tThPmYq9s6v9y$G$";
            myAes.Key = System.Text.Encoding.UTF8.GetBytes(GiftCardKey);
            myAes.IV = System.Text.Encoding.UTF8.GetBytes(GiftCardIV);
            byte[] encrypted = EncryptStringToBytes_Aes(plaintext, myAes.Key, myAes.IV);

            string ciphertext = ByteArrayToString(encrypted);

            return ciphertext;

            // Encrypt the string to an array of bytes.                    
            //byte[] encrypted = EncryptStringToBytes_Aes(plaintext, myAes.Key, myAes.IV);

            //string ciphertext = ByteArrayToString(encrypted);

            //return ciphertext;
        }
    }
    catch (Exception ex)
    {

       
        return "Error occured";
    }
}
static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
{             // Check arguments.            
    if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException("plainText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");
    byte[] encrypted;
    // with the specified key and IV.            
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = Key;
        aesAlg.IV = IV;
        // Create an encryptor to perform the stream transform.                
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        // Create the streams used for encryption.                
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.    
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
    }
    // Return the encrypted bytes from the memory stream.            
    return encrypted;

}
static CryptoStream EncryptStream(Stream responseStream)
{
    Aes aes = GetEncryptionAlgorithm();


    ToBase64Transform base64Transform = new();
    CryptoStream base64EncodedStream = new(responseStream, base64Transform, CryptoStreamMode.Write);
    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
    CryptoStream cryptoStream = new(base64EncodedStream, encryptor, CryptoStreamMode.Write);

    return cryptoStream;
}
static Aes GetEncryptionAlgorithm()
{
    var key = File.ReadAllText("Security/Secret.txt");
    var IV = File.ReadAllText("Security/IV.txt");

    Aes aes = Aes.Create();
    aes.Key = Encoding.UTF8.GetBytes(key);
    aes.IV = Encoding.UTF8.GetBytes(IV);

    if (aes.Key == null || aes.Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (aes.IV == null || aes.IV.Length <= 0)
        throw new ArgumentNullException("IV");

    return aes;
}
