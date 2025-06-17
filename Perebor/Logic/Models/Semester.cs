using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perebor.Logic.Models
{
    internal class Semester : IEnumerable<Subject>
    {
        public int Number { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public Semester() { }
  

        public Semester(int number)
        {
            Number = number;
        }

        // Добавляем возможность перечисления через foreach и LINQ
        public IEnumerator<Subject> GetEnumerator()
        {
            return Subjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
