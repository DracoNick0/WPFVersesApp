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

        // Shows the last increment for dueDate, must increase before finding next dueDate.
        public int dueDateIncrement;

        // Calculated at class initialization. The amount of days the current due date was away from the previous completion.
        public int daysTillDue;

        public Verse(string tReference, string tVersion, string tPassage, DateTime tDueDate, int tDueDateIncrement)
        {
            this.reference = tReference;
            this.version = tVersion;
            this.passage = tPassage;
            this.dueDateIncrement = tDueDateIncrement;
            this.dueDate = tDueDate;

            updateDaysTillDue();
        }

        public Verse(string tReference, string tVersion, string tPassage)
        {
            this.reference = tReference;
            this.version = tVersion;
            this.passage = tPassage;
            this.dueDateIncrement = 1;
            calculateDueDate(this.dueDateIncrement);

            updateDaysTillDue();
        }

        public void updateDaysTillDue()
        {
            this.daysTillDue = calculateDaysTillDue(this.dueDate);
        }

        public void verseCompletion()
        {
            // Increase dueDateIncrement
            this.dueDateIncrement = (int)Math.Ceiling(this.dueDateIncrement * 1.5);

            // Set new dueDate
            this.dueDate = calculateDueDate(this.dueDateIncrement);
        }

        private DateTime calculateDueDate(double dueDateIncrement)
        {
            return DateTime.Now.AddDays(dueDateIncrement);
        }

        private int calculateDaysTillDue(DateTime tDueDate)
        {
            TimeSpan dayDifference = tDueDate - DateTime.Now;
            return dayDifference.Days;
        }
    }
}
