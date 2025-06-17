using Perebor.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Perebor.Logic.Save
{
    public static class SubjectManager
    {
        /// <summary>
        /// Сохраняет список предметов в JSON-файл
        /// </summary>
        public static void SaveSubjectsToJson(List<Subject> subjects, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(subjects, options);
            File.WriteAllText(filePath, jsonString);
            
        }

        /// <summary>
        /// Загружает список предметов из JSON-файла
        /// </summary>
        public static List<Subject> LoadSubjectsFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Subject>();
            }

            string jsonString = File.ReadAllText(filePath);
            var loadedSubjects = JsonSerializer.Deserialize<List<Subject>>(jsonString);

            if (loadedSubjects == null)
            { 
                return new List<Subject>();
            }

            return loadedSubjects;            
        }
    }
}
