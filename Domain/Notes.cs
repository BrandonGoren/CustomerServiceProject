using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notes
    {
        public Notes(int id, string content)
        {
            this.Id = id;
            this.Content = content;
            this.Date = DateTime.Now;
        }

        public Notes()
        { }

        public int Id { get; private set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Agent Author { get; set; }
    }
}
