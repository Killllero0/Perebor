using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Perebor.Logic.Models;

namespace Perebor.Logic.Save
{
    public static class ValidPlanManager
    {
        /// <summary>
        /// Сохраняет список валидных учебных планов в JSON-файл
        /// </summary>
        internal static void SaveValidPlansToJson(List<ValidPlan> validPlans, string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(validPlans, options);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Сохранено {validPlans.Count} валидных планов в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в JSON: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает валидные планы из JSON-файла в память
        /// </summary>
        /// <param name="filePath">Путь к JSON-файлу</param>
        /// <returns>Список загруженных валидных планов</returns>
        internal static List<ValidPlan> LoadValidPlansFromJson(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                    return new List<ValidPlan>();
                }

                string jsonString = File.ReadAllText(filePath);
                var loadedPlans = JsonSerializer.Deserialize<List<ValidPlan>>(jsonString);

                if (loadedPlans == null)
                {
                    Console.WriteLine("Ошибка десериализации JSON.");
                    return new List<ValidPlan>();
                }

                Console.WriteLine($"Успешно загружено {loadedPlans.Count} валидных планов.");
                return loadedPlans;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке из JSON: {ex.Message}");
                return new List<ValidPlan>();
            }
        }
    }
}
