using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Platform;
using Perebor.Logic.Models;

namespace Perebor.Logic
{
    /// <summary>
    /// Проверятель условий.
    /// </summary>
    internal static class Validator
    {
        /// <summary>
        /// Максимальная трудоемкость.
        /// </summary>
        private const int MaxCreditsPerSemester = 33;
        /// <summary>
        /// Минимальная трудоемкость трудоемкость.
        /// </summary>
        private const int MinCreditsPerSemester = 27;
        /// <summary>
        /// Максимум зачетов в год
        /// </summary>
        private const int MaxSubjectsPerYear = 12;
        /// <summary>
        /// Максимум экзамено в сессию.
        /// </summary>
        private const int MaxExamsPerSemester = 5;
        /// <summary>
        /// Максимальная разница в трудоемкости за семестр.
        /// </summary>
        private const int MaximumDifferenceInLaborIntensity = 2;
        public static bool IsValid(List<Semester> semesters)
        {
            const double MinCreditsPerSemester = 27;
            const double MaxCreditsPerSemester = 33;
            const int MaxSubjectsPerYear = 12;
            const int MaxExamsPerSemester = 5;
            const int MaximumDifferenceInLaborIntensity = 2;

            /* // Проверка 1: Суммарная трудоёмкость в каждом семестре
            foreach (var semester in semesters)
            {
                double totalCredits = semester.Subjects?.Sum(s => s.Credits) ?? 0;

                if (totalCredits < MinCreditsPerSemester || totalCredits > MaxCreditsPerSemester)
                {
                    return false;
                }
            }

            // Проверка 2: Количество дисциплин в каждом зачётном году (по 2 семестра) не более 12
            for (int i = 0; i < semesters.Count; i += 2)
            {
                if (i + 1 >= semesters.Count)
                {
                    continue;
                }

                var semesterPair = semesters.Skip(i).Take(2);
                int subjectCount = 0;

                foreach (var semester in semesterPair)
                {
                    if (semester.Subjects != null)
                    {
                        subjectCount += semester.Subjects.Count(s => 
                            s.ExamType == "Зачет" || s.ExamType == "Экзамен");
                    }
                }

                if (subjectCount > MaxSubjectsPerYear)
                {
                    return false;
                }
            }

            // Проверка 3: Количество экзаменов в каждом семестре не более 5
            foreach (var semester in semesters)
            {
                int examCount = semester.Subjects?.Count(s => s.ExamType == "Экзамен") ?? 0;

                if (examCount > MaxExamsPerSemester)
                {
                    return false;
                }
            }

            // Проверка 4: Все практики находятся в разных семестрах
            foreach (var semester in semesters)
            {
                int practiceCount = semester.Subjects?.Count(s => s.IsPractice) ?? 0;

                if (practiceCount > 1)
                {
                    return false;
                }
            }

            // Проверка 5: Максимальная разница в трудоёмкости за семестр
            if (semesters.Count > 1)
            {
                var creditSums = semesters
                    .Select(s => s.Subjects?.Sum(subj => subj.Credits) ?? 0)
                    .ToList();

                double minCredits = creditSums.Min();
                double maxCredits = creditSums.Max();

                if (maxCredits - minCredits > MaximumDifferenceInLaborIntensity)
                {
                    return false;
                }
            } */

            // Проверка 6: Порядок зависимостей (Prerequisites)
            Dictionary<int, int> subjectIdToSemesterNumber = new Dictionary<int, int>();

            foreach (var semester in semesters)
            {
                foreach (var subject in semester.Subjects)
                {
                    if (subjectIdToSemesterNumber.ContainsKey(subject.Id))
                    {
                        return false;
                    }

                    subjectIdToSemesterNumber[subject.Id] = semester.Number;
                }
            }

            foreach (var semester in semesters)
            {
                foreach (var subject in semester.Subjects)
                {
                    if (subject.Prerequisites != null)
                    {
                        foreach (var prerequisiteId in subject.Prerequisites)
                        {
                            if (!subjectIdToSemesterNumber.TryGetValue(prerequisiteId, out int prerequisiteSemesterNumber))
                            {
                                return false;
                            }

                            if (prerequisiteSemesterNumber >= semester.Number)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static double GetSignificanceCoefficient1(List<Semester> planList)
        {
            return 0;
        }

        public static double GetSignificanceCoefficient2(List<Semester> planList)
        {
            return 0;
        }
    }
}
