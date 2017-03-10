using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickQuizlet.Entity
{
    class HistorySet
    {
        public string setId { get; set; }
        public string setTitle { get; set; }
    }

    class Setting
    {
        public string clientId { get; set; }
        public string currentSetId { get; set; }
        public string userFollow { get; set; }
        public int timeSetting { get; set; }
        //public List<HistorySet> history { get; set; }
    }
}
