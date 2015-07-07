using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notes
    {
        public Notes(int id, string content, DateTime date)
        {
            this.Id = id;
            this.Content = content;
            this.Date = date;
        }

        public int Id { get; private set; }
        public string Content { get; set; }
        public DateTime Date { get; }
        public Agent Author { get; set; }
    }
}
