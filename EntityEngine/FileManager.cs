using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

using Newtonsoft.Json;

using EntityFramework;
using EntityEngine.Components;
using EntityFramework.Components;

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

        public static dynamic LoadObjFromFile(string filePath, params object[] args)
        {
            ObjFile t;
            using (var sReader = new StreamReader(filePath))
            {
                try
                {
                    t = new ObjFile();
                    t.file = filePath;
                    var contents = sReader.ReadToEnd();
                    var json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(contents, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
                    t.code = json["json"].ToString();
                    t.assemblyName = json["assemblyName"].ToString();
                    t.className = json["className"].ToString();
                }
                catch
                {
                    t.assemblyName = "";
                    throw;
                }
            }
            var code = t.code;
            var type = Type.GetType(t.className);
            var jsonObj = JsonConvert.DeserializeObject(code, type);
            jsonObj.GetType().GetMethod("initFromSerial").Invoke(jsonObj, args);

            return jsonObj;
        }

        public static EntityEngine.Components.Mesh3D LoadMeshFromFile(SharpDX.Direct3D10.Device device, string file)
        {
            using (var sReader = new StreamReader(file))
            {
                var contents = sReader.ReadToEnd().Replace("\r\n", "\n").Replace('\r', '\n').Replace("f", "").Replace(",", "");
                var type = contents.Split('\n')[0];
                EntityEngine.Components.VertexStructures.Types T =
                    EntityEngine.Components.VertexStructures.Types.None;
                Enum.TryParse(type, out T);

                switch (T)
                {
                    case (EntityEngine.Components.VertexStructures.Types.Pos):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.Pos>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.Pos()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    case (EntityEngine.Components.VertexStructures.Types.Textured):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.Textured>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.Textured()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                        tex = new SharpDX.Vector2(float.Parse(floats[3]), float.Parse(floats[4]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    case (EntityEngine.Components.VertexStructures.Types.Normal):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.Normal>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.Normal()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                        uv = new SharpDX.Vector3(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    case (EntityEngine.Components.VertexStructures.Types.Color):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.Color>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.Color()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                        col = new SharpDX.Vector4(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]), float.Parse(floats[6]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    case (EntityEngine.Components.VertexStructures.Types.TexturedNormal):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.TexturedNormal>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.TexturedNormal()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                        tex = new SharpDX.Vector2(float.Parse(floats[3]), float.Parse(floats[4])),
                                        uv = new SharpDX.Vector3(float.Parse(floats[5]), float.Parse(floats[6]), float.Parse(floats[7]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    case (EntityEngine.Components.VertexStructures.Types.ColorNormal):
                        {
                            var vertices = new List<EntityEngine.Components.VertexStructures.ColorNormal>();
                            var indices = new List<short>();
                            bool isVertex = true;
                            foreach (string line in contents.Replace(type + '\n', "").Split('\n'))
                            {
                                if (line == "")
                                {
                                    isVertex = false;
                                    continue;
                                }
                                if (isVertex)
                                {
                                    var floats = line.Split(' ');
                                    vertices.Add(new Components.VertexStructures.ColorNormal()
                                    {
                                        pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                        col = new SharpDX.Vector4(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]), float.Parse(floats[6])),
                                        uv = new SharpDX.Vector3(float.Parse(floats[7]), float.Parse(floats[8]), float.Parse(floats[9]))
                                    });
                                }
                                else
                                {
                                    foreach (string s in line.Split(' '))
                                    {
                                        short u;
                                        if (short.TryParse(s, out u))
                                            indices.Add(u);
                                        else
                                            throw new Exception("Error loading indices from last line: '" + file + "'");
                                    }
                                }
                            }
                            if (indices.Count == 0)
                                return new Mesh3D(device, vertices.ToArray());
                            else
                                return new Mesh3D(device, vertices.ToArray(), indices.ToArray());
                        }

                    default:
                        throw new Exception("Type: '" + type + "' is not recognized!");
                }
            }
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
