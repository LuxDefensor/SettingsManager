using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Settings
{
    public class SettingsManager
    {
        private FileManager f;
        private Dictionary<string, string> settings;

        public SettingsManager(string fileName, string[] defaultSettings)
            : this(fileName)
        {
            f.WriteAll(defaultSettings);
            if (defaultSettings.Length > 0)
                f.WriteAll(defaultSettings);
        }

        public SettingsManager(string fileName, Dictionary<string, string> defaultSettings)
            : this(fileName, defaultSettings.Select(kvp => kvp.Key + "=" + kvp.Value).ToArray())
        {
        }

        public SettingsManager(string fileName)
        {
            f = new FileManager(fileName);
            settings = f.ReadAll().ToDictionary<string, string, string>(s => s.Substring(0, s.IndexOf('=')).Trim(),
                                         s => s.Substring(s.IndexOf('=') + 1).Trim());
        }

        public SettingsManager() : this("settings.ini")
        {

        }

        public string this[string i]
        {
            get
            {
                return settings[i];
            }
            set
            {
                if (settings.ContainsKey(i))
                    settings[i] = value;
                else
                    settings.Add(i, value);
                f.WriteAll(settings);
            }
        }

        public void ChangeSettings(Dictionary<string, string> newSettings)
        {
            foreach (var kvp in newSettings)
            {
                if (settings.ContainsKey(kvp.Key))
                    settings[kvp.Key] = kvp.Value;
                else
                    settings.Add(kvp.Key, kvp.Value);
            }
            f.WriteAll(settings);
        }

        public void ChangeSettings(string[] newSettings)
        {
            ChangeSettings(newSettings.ToDictionary<string, string, string>(s => s.Substring(0, s.IndexOf('=')).Trim(),
                                                         s => s.Substring(s.IndexOf('=') + 1).Trim()));
        }

        public bool HasKey(string key)
        {
            return settings.ContainsKey(key);
        }
                
    }
}
