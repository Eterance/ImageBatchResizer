
using ImageBatchResizer.Views.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageBatchResizer.Services
{
    internal class WindowsSettingsService
    {
        private Dictionary<string, string> stringSettings;
        private Dictionary<string, int> intSettings;
        private Dictionary<string, bool> boolSettings;
        private Dictionary<string, double> doubleSettings;
        private string intSettingsFileName = "intSettings.ini";
        private string stringSettingsFileName = "stringSettings.ini";
        private string boolSettingsFileName = "boolSettings.ini";
        private string doubleSettingsFileName = "doubleSettings.ini";
        private string rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string _directoryPath = "";


        public WindowsSettingsService(string directoryPath)
        {
            intSettings = DeserializeInt(Path.Combine(rootFolder, directoryPath, intSettingsFileName));
            stringSettings = DeserializeString(Path.Combine(rootFolder, directoryPath, stringSettingsFileName));
            boolSettings = DeserializeBool(Path.Combine(rootFolder, directoryPath, boolSettingsFileName));
            doubleSettings = DeserializeDouble(Path.Combine(rootFolder, directoryPath, doubleSettingsFileName));
            _directoryPath = directoryPath;
        }

        public int Get(string key, int defaultValue)
        {
            bool success = intSettings.TryGetValue(key, out int value);
            if (success)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public string Get(string key, string defaultValue)
        {
            bool success = stringSettings.TryGetValue(key, out string? value);
            if (success)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public bool Get(string key, bool defaultValue)
        {
            bool success = boolSettings.TryGetValue(key, out bool value);
            if (success)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
        public double Get(string key, double defaultValue)
        {
            bool success = doubleSettings.TryGetValue(key, out double value);
            if (success)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public bool IsContainsKey(string key)
        {
            return stringSettings.ContainsKey(key) || intSettings.ContainsKey(key);
        }

        public void Set(string key, int value)
        {
            intSettings[key] = value;
            Serialize(intSettings, Path.Combine(rootFolder, _directoryPath, intSettingsFileName));
        }

        public void Set(string key, string value)
        {
            stringSettings[key] = value;
            Serialize(stringSettings, Path.Combine(rootFolder, _directoryPath, stringSettingsFileName));
        }

        public void Set(string key, bool value)
        {
            boolSettings[key] = value;
            Serialize(boolSettings, Path.Combine(rootFolder, _directoryPath, boolSettingsFileName));
        }
        public void Set(string key, double value)
        {
            doubleSettings[key] = value;
            Serialize(doubleSettings, Path.Combine(rootFolder, _directoryPath, doubleSettingsFileName));
        }

        private void Serialize<T>(Dictionary<string, T> dict, string dictFileName)
        {
            /*// Create a DataContractSerializer instance for the dictionary type
            DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<int, string>));

            // Open a file stream to save the serialized data
            using (FileStream stream = new FileStream(dictFileName, FileMode.Create))
            {
                // Create an XmlWriter to write the serialized data to the file stream
                XmlWriter writer = XmlWriter.Create(stream);

                // Serialize the dictionary to the XmlWriter
                serializer.WriteObject(writer, dict);
            }*/
            // 获取文件夹路径
            string folderPath = Path.GetDirectoryName(dictFileName);
            // 逐级检查并创建文件夹
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // 检查并创建文件
            if (!File.Exists(dictFileName))
            {
                File.Create(dictFileName).Close();
            }
            using (StreamWriter sw = new StreamWriter(dictFileName))
            {
                foreach (KeyValuePair<string, T> kvp in dict)
                {
                    sw.WriteLine("{0}={1}", kvp.Key, kvp.Value);
                }
            }
        }

        private Dictionary<string, int> DeserializeInt(string dictFileName)
        {
            if (File.Exists(dictFileName) == false)
            {
                return new Dictionary<string, int>();
            }
            Dictionary<string, int> dict = new();
            using (StreamReader sr = new StreamReader(dictFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('=');
                    dict[keyValue[0]] = int.Parse(keyValue[1]);
                }
            }
            return dict;
        }

        private Dictionary<string, string> DeserializeString(string dictFileName)
        {
            if (File.Exists(dictFileName) == false)
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> dict = new();
            using (StreamReader sr = new StreamReader(dictFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('=');
                    dict[keyValue[0]] = keyValue[1];
                }
            }
            return dict;
        }
        private Dictionary<string, bool> DeserializeBool(string dictFileName)
        {
            if (File.Exists(dictFileName) == false)
            {
                return new Dictionary<string, bool>();
            }
            Dictionary<string, bool> dict = new();
            using (StreamReader sr = new StreamReader(dictFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('=');
                    dict[keyValue[0]] = bool.Parse(keyValue[1]);
                }
            }
            return dict;
        }
        private Dictionary<string, double> DeserializeDouble(string dictFileName)
        {
            if (File.Exists(dictFileName) == false)
            {
                return new Dictionary<string, double>();
            }
            Dictionary<string, double> dict = new();
            using (StreamReader sr = new StreamReader(dictFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('=');
                    dict[keyValue[0]] = double.Parse(keyValue[1]);
                }
            }
            return dict;
        }

        private T? XmlDeserialize<T>(string dictFileName)
        {
            if (File.Exists(dictFileName) == false)
            {
                return default(T);
            }
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using var fs = new FileStream(dictFileName, FileMode.Open);
            T result = (T)mySerializer.Deserialize(fs);
            if (result == null)
            {
                result = default;
            }
            return result;
        }

        private void XmlSerialize<T>(T value, string dictFileName)
        {
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(dictFileName);
            mySerializer.Serialize(myWriter, value);
            myWriter.Close();
        }
    }
}
