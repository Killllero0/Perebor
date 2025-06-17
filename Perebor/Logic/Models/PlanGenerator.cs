using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perebor.Logic.Models
{
    internal class PlanGenerator
    {
        // <summary>
        /// Генерирует и проверяет все возможные распределения предметов по семестрам
        /// </summary>
        public static List<ValidPlan> GenerateValidPlans(List<Subject> subjects)
        {
            var validPlans = new ConcurrentBag<ValidPlan>();

            // Генерируем и проверяем все возможные распределения предметов по семестрам
            GenerateAndValidate(subjects, 0, new Semester[8], validPlans);

            return validPlans.ToList();
        }

        private static void GenerateAndValidate(
            List<Subject> subjects,
            int index,
            Semester[] currentPlan,
            ConcurrentBag<ValidPlan> validPlans)
        {
            if (index >= subjects.Count)
            {
                // Все предметы распределены, проверяем план
                var planList = currentPlan
                    .Select(s => new Semester(s.Number) { Subjects = s.Subjects.ToList() })
                    .ToList();

                if (Validator.IsValid(planList))
                {
                    double coeff1 = Validator.GetSignificanceCoefficient1(planList);
                    double coeff2 = Validator.GetSignificanceCoefficient2(planList);

                    validPlans.Add(new ValidPlan(planList, coeff1, coeff2));
                }

                return;
            }

            for (int i = 0; i < currentPlan.Length; i++)
            {
                // Делаем копию текущего состояния перед рекурсивным вызовом
                var newPlan = new Semester[currentPlan.Length];
                for (int j = 0; j < currentPlan.Length; j++)
                {
                    newPlan[j] = new Semester(currentPlan[j].Number)
                    {
                        Subjects = currentPlan[j].Subjects.ToList()
                    };
                }

                // Добавляем предмет в i-ый семестр
                newPlan[i].Subjects.Add(subjects[index]);

                // Рекурсивно продолжаем
                GenerateAndValidate(subjects, index + 1, newPlan, validPlans);
            }
        }
        public static async Task<List<List<Semester>>> GenerateValidPlansAsync(List<Subject> subjects)
        {
            var validPlans = new List<List<Semester>>();

            void Generate(int index, List<Semester> currentPlan)
            {
                if (index >= subjects.Count)
                {
                    var planCopy = currentPlan.Select(s => new Semester
                    {
                        Number = s.Number,
                        Subjects = s.Subjects.ToList()
                    }).ToList();

                    if (Validator.IsValid(planCopy))
                    {
                        validPlans.Add(planCopy);
                    }
                    return;
                }

                var subject = subjects[index];
                for (int i = 0; i < currentPlan.Count; i++)
                {
                    currentPlan[i].Subjects.Add(subject);
                    Generate(index + 1, currentPlan);
                    currentPlan[i].Subjects.RemoveAt(currentPlan[i].Subjects.Count - 1);
                }
            }

            var initialPlan = Enumerable.Range(1, 8)
                .Select(n => new Semester { Number = n })
                .ToList();

            await Task.Run(() => Generate(0, initialPlan));

            return validPlans;
        }
    }
}
