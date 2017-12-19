using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Settings
{
    internal class FileManager
    {
        private string fileName;

        public FileManager() : this("settings.ini")
        {

        }
        public FileManager(string fileName)
        {
            this.fileName = fileName;
            if (!File.Exists(fileName))
            {
                FileStream f = File.Create(fileName);
                f.Close();
            }
        }

        public void WriteAll(string[] newSettings)
        {
            string[] existing = File.ReadAllLines(fileName);
            foreach (string s in newSettings)
            {
                bool found = false;
                for (int i = 0; i < existing.Length; i++)
                {
                    if (existing[i].Substring(0, existing[i].IndexOf('=')).Trim() ==
                        s.Substring(0, s.IndexOf('=')).Trim())
                    {
                        existing[i] = s;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    string[] newArray = new string[existing.Length + 1];
                    existing.CopyTo(newArray, 0);
                    newArray[existing.Length] = s;
                    existing = newArray;
                }
            }
            File.WriteAllLines(fileName, existing);
        }

        public void WriteAll(Dictionary<string, string> newSettings)
        {
            WriteAll(newSettings.Select(kvp => kvp.Key + "=" + kvp.Value).ToArray());
        }

        public string[] ReadAll()
        {
            return File.ReadAllLines(fileName);
        }
    }
}
