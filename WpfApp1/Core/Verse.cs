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
        public int dueDateIncrement;
        // Calculated at class initialization. The amount of days the current due date was away from the previous completion.
        public int daysTillDue;

        public Verse(string tReference, string tVersion, string tPassage, DateTime tDueDate, int tDueDateIncrement)
        {
            this.reference = tReference;
            this.version = tVersion;
            this.passage = tPassage;
            this.dueDate = tDueDate;
            this.dueDateIncrement = tDueDateIncrement;

            this.daysTillDue = calculateDaysTillDue(this.dueDate);
        }

        /*
        private DateTime stringToDateTime(string dueDateString)
        {
            return DateTime.Parse(dueDateString);
        }
        */
        private int calculateDaysTillDue(DateTime tDueDate)
        {
            TimeSpan dayDifference = tDueDate - DateTime.Now;
            return dayDifference.Days;
        }
    }
}
