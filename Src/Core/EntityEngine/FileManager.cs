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
        public static Dictionary<string, EntityEngine.Components.Mesh3D> Mesh = 
            new Dictionary<string, EntityEngine.Components.Mesh3D>();
        public static Dictionary<string, EntityEngine.Components.Shader> Shader =
            new Dictionary<string, EntityEngine.Components.Shader>();
        public static Dictionary<Guid, dynamic> guidObjects =
            new Dictionary<Guid, dynamic>();

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

        public static void SetProperty(object instance, string propertyName, object newValue, Type type = null)
        {
            if (type == null)
                type = instance.GetType();
            type.GetProperty(propertyName).SetValue(instance, newValue, null);
        }

        //FileManager.LoadAllEntities(Path.Combine("Maps", "Test", "ObjDefs", "Entities"), sys);

        // Attempt to serialize all entities
        //FileManager.SaveEntity("", sys.GetComponentSystem<TagComponent, TagSystem>()
        //    .getTaggedEntity("win"));
        //var x = 0;
        //foreach (var entity in sys.GetComponentSystem<GroupComponent, GroupSystem>().getTaggedEntities("save"))
        //   FileManager.SaveEntity(Path.Combine("Maps", "Test", "ObjDefs", "Entities", String.Format("entity_{0}.js", x++)), entity);
        public static void SaveEntity(string filePath, Entity e)
        {
            var js = new Dictionary<string, Type>();
            js.Add(JsonConvert.SerializeObject(e), typeof(Entity));
            foreach (var com in e.GetAllComponents())
            {
                try
                {
                    js.Add(JsonConvert.SerializeObject(com), com.GetType());
                }
                catch
                {
                    throw new JsonSerializationException(String.Format("Error serializing '{0}', please check for any dangling references", com.GetType()));
                }
            }

            var jsList = js.ToList();
            string json = "{";
            json += "\"json\": " + jsList[0].Key + ",";
            json += "\"assemblyName\": \"" + jsList[0].Value.Assembly.ToString() + "\", ";
            json += "\"className\": \"" + jsList[0].Value.FullName.ToString() + "\", ";
            json += "\"components\": [";
            jsList.Remove(jsList[0]);
            foreach (var j in jsList)
            {
                json += "{";
                json += "\"json\": " + j.Key + ",";
                json += "\"assemblyName\": \"" + j.Value.Assembly.ToString() + "\", ";
                json += "\"className\": \"" + j.Value.FullName.ToString() + "\"";
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            json += "}";

            using (var sWriter = new StreamWriter(filePath, false))
            {
                sWriter.Write(json);
            }
        }

        // Maybe use a Component return type instead...?
        public static KeyValuePair<dynamic, dynamic> LoadComponent(string filePath, SystemManager sys)
        {
            return new KeyValuePair<dynamic, dynamic>();
        }
        public static KeyValuePair<dynamic, dynamic> LoadComponentS(string contents, SystemManager sys)
        {
            var json = (Newtonsoft.Json.Linq.JObject)
                JsonConvert.DeserializeObject(contents, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            var comJson = json["json"].ToString();
            var guid = Guid.Parse(json["json"]["guid"].ToString());
            var assemblyName = json["assemblyName"].ToString();
            var className = json["className"].ToString();

            var comType = Type.GetType(className + ", " + assemblyName);
            var comSysType = Type.GetType(
                className.Substring(
                    0, className.IndexOf(
                        className.Split('.')[className.Split('.').Length - 1]
                    )
                ) + 
                className.Split('.')[className.Split('.').Length - 1].Replace("Component", "System") +
                ", " + assemblyName);

            dynamic comObj = JsonConvert.DeserializeObject(comJson, comType);
            //sys.GetComponentSystem<Com?, Sys?>.AddComponent(comObj)
            dynamic comSys = typeof(SystemManager).GetMethod("GetComponentSystem")
                .MakeGenericMethod(comType, comSysType)
                .Invoke(sys, null);

            SetProperty(comObj, "guid", guid, typeof(Component));

            FileManager.guidObjects.Add(guid, comObj);

            return new KeyValuePair<dynamic, dynamic>(comObj, comSys);
        }

        public static Entity LoadEntity(string filePath, SystemManager sys)
        {
            string contents;
            using (var sReader = new StreamReader(filePath))
            {
                contents = sReader.ReadToEnd();
            }

            var json = (Newtonsoft.Json.Linq.JObject)
                JsonConvert.DeserializeObject(contents, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            var entityJson = json["json"].ToString();
            var guid = Guid.Parse(json["json"]["guid"].ToString());
            var assemblyName = json["assemblyName"].ToString();
            var className = json["className"].ToString();
            var type = Type.GetType(className);
            var componentCode = json["components"].ToList();

            var entityObj = JsonConvert.DeserializeObject<Entity>(entityJson);
            SetProperty(entityObj, "guid", guid);

            var coms = new List<KeyValuePair<dynamic, dynamic>>();
            foreach (var comCode in componentCode)
                coms.Add(LoadComponentS(comCode.ToString(), sys));

            foreach (var com in coms)
            {
                com.Key.SetEntity(entityObj);
                com.Value.AddComponent(com.Key);
            }

            FileManager.guidObjects.Add(guid, (dynamic)entityObj);

            return entityObj;
        }

        public static void LoadAllEntities(string directory, SystemManager sys, bool resolveRefs = true)
        {
            foreach (var filePath in Directory.GetFiles(directory))
                sys.AddEntity(LoadEntity(filePath, sys));

            if (resolveRefs)
                LoadResolveReferences(sys);
        }

        public static void LoadResolveReferences(SystemManager sys)
        {
            // First, we need to do loading of the render component mesh/shaders
            // I know.. we're really really not following module stuff here, but the
            // render component is vital to the engine
            // TODO: fix the shader/mesh load stuff?
            RenderSystem renSys = sys.GetComponentSystem<RenderComponent, RenderSystem>();
            foreach (dynamic dynCom in FileManager.guidObjects.Values)
            {
                try
                {
                    // Component
                    Component com = (Component)dynCom;
                    if (com.GetType() == typeof(RenderComponent))
                    {
                        RenderComponent renCom = (RenderComponent)com;
                        //renCom.mesh = LoadMeshFromFile(renSys.Device, renCom.mesh.filePath);
                        var shaderGuid = renCom.shader.guid;
                        //renCom.shader =
                        //    new Shader(renSys.Device, renCom.shader.filePath, renCom.shader.shaderVars, renCom.shader.shaderLevel);
                        SetProperty(renCom.shader, "guid", shaderGuid);
                    }
                }
                catch
                {
                    // Entity
                }
            }
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
                var contents = sReader.ReadToEnd().Replace("\r\n", "\n").Replace('\r', '\n');
                var type = contents.Split('\n')[0];
                contents = contents.Substring(contents.IndexOf('\n') + 1);
                Guid id;
                if (!Guid.TryParse(contents.Split('\n')[0].Replace("{", "").Replace("}", ""), out id))
                    id = Guid.NewGuid();
                else
                    contents = contents.Substring(contents.IndexOf('\n') + 1);
                contents = contents.Replace("f", "").Replace(",", "");
                EntityEngine.Components.VertexStructures.Types T =
                    EntityEngine.Components.VertexStructures.Types.None;
                Enum.TryParse(type, out T);

                dynamic vertices;
                var indices = new List<short>();

                switch (T)
                {
                    case (EntityEngine.Components.VertexStructures.Types.Pos):
                        vertices = new List<EntityEngine.Components.VertexStructures.Pos>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.Pos()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2]))
                            });
                        }
                        break;

                    case (EntityEngine.Components.VertexStructures.Types.Textured):
                        vertices = new List<EntityEngine.Components.VertexStructures.Textured>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.Textured()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                tex = new SharpDX.Vector2(float.Parse(floats[3]), float.Parse(floats[4]))
                            });
                        }
                        break;

                    case (EntityEngine.Components.VertexStructures.Types.Normal):
                        vertices = new List<EntityEngine.Components.VertexStructures.Normal>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.Normal()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                uv = new SharpDX.Vector3(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]))
                            });
                        }
                        break;

                    case (EntityEngine.Components.VertexStructures.Types.Color):
                        vertices = new List<EntityEngine.Components.VertexStructures.Color>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.Color()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                col = new SharpDX.Vector4(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]), float.Parse(floats[6]))
                            });
                        }
                        break;

                    case (EntityEngine.Components.VertexStructures.Types.TexturedNormal):
                        vertices = new List<EntityEngine.Components.VertexStructures.TexturedNormal>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.TexturedNormal()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                tex = new SharpDX.Vector2(float.Parse(floats[3]), float.Parse(floats[4])),
                                uv = new SharpDX.Vector3(float.Parse(floats[5]), float.Parse(floats[6]), float.Parse(floats[7]))
                            });
                        }
                        break;

                    case (EntityEngine.Components.VertexStructures.Types.ColorNormal):
                        vertices = new List<EntityEngine.Components.VertexStructures.ColorNormal>();
                        foreach (string line in contents.Split('\n'))
                        {
                            if (line == "")
                            {
                                break;
                            }
                            var floats = line.Split(' ');
                            vertices.Add(new Components.VertexStructures.ColorNormal()
                            {
                                pos = new SharpDX.Vector3(float.Parse(floats[0]), float.Parse(floats[1]), float.Parse(floats[2])),
                                col = new SharpDX.Vector4(float.Parse(floats[3]), float.Parse(floats[4]), float.Parse(floats[5]), float.Parse(floats[6])),
                                uv = new SharpDX.Vector3(float.Parse(floats[7]), float.Parse(floats[8]), float.Parse(floats[9]))
                            });
                        }
                        break;

                    default:
                        throw new Exception("Type: '" + type + "' is not recognized!");
                }

                foreach (string s in contents.Split('\n')[contents.Split('\n').Length - 1].Split(' '))
                {
                    short u;
                    if (short.TryParse(s, out u))
                        indices.Add(u);
                    else
                        throw new Exception("Error loading indices from last line: '" + file + "'");
                }

                Mesh3D mesh;
                if (indices.Count == 0)
                    mesh = new Mesh3D(device, vertices.ToArray(), filePath: file);
                else
                    mesh = new Mesh3D(device, vertices.ToArray(), indices.ToArray(), file);
                SetProperty(mesh, "guid", id);
                return mesh;
            }
        }

        [Obsolete]
        public static void LoadAllMesh(string dir, SharpDX.Direct3D10.Device device, string ext=".mesh")
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file) == ext)
                {
                    Mesh[Path.GetFileNameWithoutExtension(file)] =
                        LoadMeshFromFile(device, file);
                }
            }
        }

        [Obsolete]
        public static void LoadAllShader(string dir, SharpDX.Direct3D10.Device device, string ext = ".fx")
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file) == ext)
                {
                    //Shader[Path.GetFileNameWithoutExtension(file)] =
                    //    LoadMeshFromFile(device, file);
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
