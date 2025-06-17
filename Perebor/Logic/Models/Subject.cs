using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perebor.Logic.Models
{
    /// <summary>
    /// Предмет.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// ID.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Возможные семестры дисциплины.
        /// </summary>
        public List<int>? PossibleSemesters { get; set; } = null;
        /// <summary>
        /// Предшествующие предметы (по ID)
        /// </summary>
        public List<int>? Prerequisites { get; set; } = null;
        /// <summary>
        /// Практика?.
        /// </summary>
        public bool IsPractice { get; set; } = false;
        /// <summary>
        /// Вид контроля.
        /// </summary>
        public required string ExamType { get; set; } 
        /// <summary>
        /// Зачетные единицы.
        /// </summary>
        public double Credits { get; set; }
    }
}
