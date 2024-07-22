using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class VerseViewModel
    {
        Verse verse;
        public VerseViewModel(Verse verse)
        {
            this.verse = verse;
        }
    }
}
