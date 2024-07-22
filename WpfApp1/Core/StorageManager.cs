using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WpfApp1.Core
{
    class StorageManager
    {
        public readonly Dictionary<string, Verse> loadedVerses = new Dictionary<string, Verse>();

        public StorageManager()
        {
            AppDomain.CurrentDomain.ProcessExit += SaveToFile;
            LoadFromFile();
        }

        public bool AddVerse(string reference, string version, string passage)
        {

            if (loadedVerses.ContainsKey(reference))
            {
                // Verse already exists
                return false;
            }
            else
            {
                // Create new verse
                Verse newVerse = new Verse(reference, version, passage);

                // Add verse into dictionary
                loadedVerses[reference] = newVerse;

                return true;
            }
        }

        public void DeleteVerse(string reference)
        {
            if (loadedVerses.ContainsKey(reference))
            {
                loadedVerses.Remove(reference);
            }
        }

        private void LoadFromFile()
        {
            string versesTextFile = @"..\..\Storage\AllSavedVersesData.txt";

            if (File.Exists(versesTextFile))
            {
                using (StreamReader sr = new StreamReader(versesTextFile))
                {
                    string line;
                    List<string> details;
                    Verse verse;
                    DateTime dueDate;
                    int dueDateIncrement;

                    while ((line = sr.ReadLine()) != null)
                    {
                        // Parse all detail.
                        // reference, version, passage, dueDate, dueDateIncrement
                        details = new List<string>(line.Split('|'));

                        // string to DateTime
                        dueDate = DateTime.Parse(details[3]);

                        // string to int
                        dueDateIncrement = int.Parse(details[4]);

                        // Create new verse
                        verse = new Verse(details[0], details[1], details[2], dueDate, dueDateIncrement);

                        // Load verse into dictionary
                        this.loadedVerses[details[0]] = verse;
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
                    Verse tVerse;

                    foreach (string key in this.loadedVerses.Keys)
                    {
                        tVerse = this.loadedVerses[key];

                        sw.WriteLine(key + "|" + tVerse.version + "|" + tVerse.passage + "|" + tVerse.dueDate + "|" + tVerse.dueDateIncrement);
                    }
                }
            }
        }
    }
}
