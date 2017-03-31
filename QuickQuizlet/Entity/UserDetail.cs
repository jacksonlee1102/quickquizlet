using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickQuizlet.Entity
{
    class UserDetail
    {
        public string username { get; set; }
        public string account_type { get; set; }
        public string profile_image { get; set; }
        public int id { get; set; }
        public object statistics { get; set; }
        public List<SetDetail> sets { get; set; }
        public object favorite_sets { get; set; }
        public object studied { get; set; }
        public object groups { get; set; }
    }
}
