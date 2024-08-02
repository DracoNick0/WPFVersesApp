using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class TapToRevealViewModel
    {
        private List<string> fullPassage;

        private string revealedPassage;
        public string RevealedPassage
        {
            get { return revealedPassage; }
        }


        public TapToRevealViewModel(Verse verse)
        {
            char[] delimiters = new char[] { ',', '.', ';', ':' };
            int prevDelimiter = 0;

            for (int i = 0; i < verse.passage.Length; i++)
            {
                foreach (char delimiter in delimiters)
                {
                    if (verse.passage[i] == delimiter)
                    {
                        fullPassage.Add(verse.passage.Substring(prevDelimiter, i - prevDelimiter + 1));
                    }
                }
            }
        }

    }
}
