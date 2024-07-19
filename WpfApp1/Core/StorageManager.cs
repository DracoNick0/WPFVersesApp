using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Core
{
    class StorageManager
    {
        readonly Dictionary<string, string> loadedVerses = new Dictionary<string, string>();

        public StorageManager()
        {
            AppDomain.CurrentDomain.ProcessExit += SaveToFile;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            string versesTextFile = @"..\..\Storage\AllSavedVersesData.txt";

            if (File.Exists(versesTextFile))
            {
                using (StreamReader sr = new StreamReader(versesTextFile))
                {
                    string line;
                    string reference;
                    string details;
                    int indexOfDelimiter;

                    while ((line = sr.ReadLine()) != null)
                    {
                        // Find delimiter in line
                        indexOfDelimiter = line.IndexOf('|');

                        // Split the line into two parts
                        reference = line.Substring(0, indexOfDelimiter);
                        details = line.Substring(indexOfDelimiter);

                        // Save substring and delimiter in dictionary
                        this.loadedVerses[reference] = details;
                    }
                }
            }
        }

        private void SaveToFile(object sender, EventArgs e)
        {
            string versesTextFile = @"..\..\Storage\AllSavedVersesData.txt";

            if (File.Exists(versesTextFile))
            {
                using (StreamWriter sw = new StreamWriter(versesTextFile))
                {
                    foreach (string key in this.loadedVerses.Keys)
                    {
                        sw.WriteLine(key + loadedVerses[key]);
                    }
                }
            }
        }
    }
}
