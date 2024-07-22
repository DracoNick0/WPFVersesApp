using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WpfApp1.Core
{
    class Verse
    {
        public string reference;
        public string version;
        public string passage;
        // The date when the verse is due.
        public DateTime dueDate;
        // If days to the next due date increase after each completion.
        public bool dueDateIncrement;
        // Calculated at class initialization. The amount of days the current due date was away from the previous completion.
        public int daysTillDue;

        public Verse(string reference, string version, string passage, string dueDate, string dueDateIncrement)
        {
            this.dueDate = stringToDateTime(dueDate);
            calculateDaysTillDue();
        }

        private DateTime stringToDateTime(string date)
        {

            return DateTime.Now;
        }

        private void calculateDaysTillDue()
        {
            TimeSpan dayDifference = this.dueDate - DateTime.Now;
            this.daysTillDue = dayDifference.Days;
        }
    }
}
