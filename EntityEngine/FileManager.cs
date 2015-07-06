using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

using Newtonsoft.Json;

namespace EntityEngine
{
    public static class FileManager
    {
        public struct ObjFile
        {
            public string file;
            public string code;
            public string assemblyName;
            public string className;
        }

        public static Type GetTypeProperty(object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName).GetType();
        }

        public static void SetProperty(object instance, string propertyName, object newValue)
        {
            instance.GetType().GetProperty(propertyName).SetValue(instance, newValue, null);
        }

        public static Object LoadObjFromFile(params Object[] args)
        {
            // Pretend as if we just loaded a file
            ObjFile t = new ObjFile()
            {
                file = "",
//                code = @"{
//    '__class__' : 'EntityEngine.Components.WinSystem',
//    'WinConditions' : {
//        '__class__' : 'List<EntityEngine.Components.WinSystem.WinCondition>',
//        '__islist__' : true,
//        '__list_vars__' : [
//            {
//            }
//        ]
//    }
//}",
                code = "{\"WinConditionInternal\":0}",
                assemblyName = "EntityEngine",
                className = "EntityEngine.Components.WinSystem"
            };

            //var type = Type.GetType(t.className);
            //var instance = System.Activator.CreateInstance(type, args);

            //Newtonsoft.Json.Linq.JObject json =
            //    (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(t.code, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects } );

            //string k = JsonConvert.SerializeObject(test);

            var code = t.code;
            var type = Type.GetType(t.className);
            var i = JsonConvert.DeserializeObject(code, type);
            i.GetType().GetMethod("initFromSerial").Invoke(i,  args);

            return i;
        }
    }

    [Obsolete]
    class RSAMod
    {
        private static string _privateKey;
        private static string _publicKey;
        private static UnicodeEncoding _UFTencoder = new UnicodeEncoding();

        public static void RSA()
        {
            var rsa = new RSACryptoServiceProvider();
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);

            var text = "Test1";
            Console.WriteLine("RSA // Text to encrypt: " + text);
            var enc = Encrypt(text);
            Console.WriteLine("RSA // Encrypted Text: " + enc);
            var dec = Decrypt(enc);
            Console.WriteLine("RSA // Decrypted Text: " + dec);

            // Attempt to encrypt our python init code...
            string fileText;
            string filePath = "winConditions/__init__.py";
            string encoded;
            string decoded;

            using (StreamReader sr = new StreamReader(filePath))
            {
                fileText = sr.ReadToEnd();
            }

            Console.WriteLine("Text to encrypt:\n");
            foreach (string line in fileText.Split('\n'))
                Console.WriteLine("    " + line);

            encoded = Encrypt(fileText);
            Console.WriteLine("Encrypted text:\n");
            foreach (string line in encoded.Split('\n'))
                Console.WriteLine("    " + line);

            decoded = Decrypt(fileText);
            Console.WriteLine("Decrypted text:\n");
            foreach (string line in decoded.Split('\n'))
                Console.WriteLine("    " + line);
        }

        // Decrypt takes an ascii encoded string, and returns the original unicode message
        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_privateKey);
            byte[] byteToDecrypt = new byte[data.Length];
            for (int c = 0; c < data.Length; c++)
            {
                byteToDecrypt[c] = (byte)data[c];
            }
            byte[] decryptedByte = rsa.Decrypt(byteToDecrypt, false);
            string decryptedString = _UFTencoder.GetString(decryptedByte);

            return decryptedString;
        }

        // Encrypt takes a unicode string, and returns an ascii encoded representation
        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            byte[] dataToEncrypt = _UFTencoder.GetBytes(data);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);
            string encryptedString = "";
            foreach (byte b in encryptedByteArray)
            {
                encryptedString += (char)b;
            }

            return encryptedString;
        }

        // We can also accept bytes directly
        public static byte[] Decrypt(byte[] byteToDecrypt)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_privateKey);
            byte[] decryptedByte = rsa.Decrypt(byteToDecrypt, false);

            return decryptedByte;
        }

        public static byte[] Encrypt(byte[] dataToEncrypt)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);

            return encryptedByteArray;
        }
    }
}
