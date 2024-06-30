using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork04._01.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public DateTime DateTime { get; set; }
        public int PersonId { get; set; }

        public Question Question { get; set; }
    }
}
