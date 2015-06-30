using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notes
    {
        public Notes(string note, Agent author = null)
        {
            this.Content = note;
            this.Date = DateTime.Now;
            this.Author = author;
        }

        public string Content { get; set; }
        public DateTime Date { get; }
        public Agent Author { get; set; }
    }
}
