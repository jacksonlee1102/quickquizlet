using System;
namespace QuickQuizlet.Entity
{
    class Term
    {
        public Int64 id { get; set; }
        public string term { get; set; }
        public string definition { get; set; }
        public string image { get; set; }
        public int rank { get; set; }
    }
}
