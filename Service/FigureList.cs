using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class FigureList
    {
        public Dictionary<string, Drawing.figureConstructor> figuresList = new();

        private readonly string configFileName = "dlls.conf";

        public void ReadDLLs()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;

            string[] dllFiles = Directory.GetFiles(folder, "*.dll");

            Dictionary<string, bool> config = new();

            if (File.Exists(configFileName))
            {
                foreach (var line in File.ReadAllLines(configFileName))
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#") || !trimmed.Contains("="))
                        continue;

                    var parts = trimmed.Split('=', 2, StringSplitOptions.TrimEntries);
                    if (parts.Length == 2 && bool.TryParse(parts[1], out bool flag))
                    {
                        config[parts[0]] = flag;
                    }
                }
            }

            bool updated = false;
            foreach (string dllPath in dllFiles)
            {
                string dllName = Path.GetFileName(dllPath);
                if (!config.ContainsKey(dllName))
                {
                    config[dllName] = true;
                    updated = true;
                }
            }

            if (updated)
            {
                using StreamWriter sw = new(configFileName, false);
                foreach (var kv in config)
                {
                    if (kv.Key == "lab2.dll" || kv.Key == "lab.dll")
                        continue;
                    sw.WriteLine($"{kv.Key} = {kv.Value.ToString().ToLower()}");
                }
            }

            foreach (var kv in config)
            {
                if (!kv.Value) continue;

                string dllFullPath = Path.Combine(folder, kv.Key);
                if (!File.Exists(dllFullPath) || kv.Key == "lab2.dll" || kv.Key == "lab.dll")
                    continue;

                try
                {
                    Assembly asm = Assembly.LoadFrom(dllFullPath);

                    foreach (var type in asm.GetTypes())
                    {
                        if (!typeof(Figure).IsAssignableFrom(type))
                            continue;

                        string figureName = null;
                        var nameField = type.GetField("name", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                        if (nameField != null && nameField.FieldType == typeof(string))
                            figureName = nameField.GetValue(null) as string;

                        if (string.IsNullOrEmpty(figureName))
                        {
                            var nameProp = type.GetProperty("name", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                            if (nameProp != null && nameProp.PropertyType == typeof(string))
                                figureName = nameProp.GetValue(null) as string;
                        }

                        if (string.IsNullOrEmpty(figureName))
                            continue; 

                        ConstructorInfo ctor = type.GetConstructor(new Type[] { typeof(System.Drawing.Point), typeof(System.Drawing.Point) });
                        if (ctor == null)
                            continue;

                        Drawing.figureConstructor constructorDelegate = (x, y) =>
                        {
                            return (Figure)ctor.Invoke(new object[] { x, y });
                        };

                        if (!figuresList.ContainsKey(figureName))
                            figuresList.Add(figureName, constructorDelegate);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки {kv.Key}: {ex.Message}");
                }
            }
        }
    }
}
