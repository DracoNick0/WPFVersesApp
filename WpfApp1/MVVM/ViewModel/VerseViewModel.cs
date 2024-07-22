using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.MVVM.ViewModel
{
    class VerseViewModel
    {
        private string reference;

        public VerseViewModel(string verse)
        {
            this.reference = verse;
        }
    }
}
