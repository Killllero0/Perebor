using Perebor.Logic.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Perebor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _resultText = "Результат будет здесь";
    public string ResultText
    {
        get => _resultText;
        set => this.RaiseAndSetIfChanged(ref _resultText, value);
    }

    public ReactiveCommand<Unit, Unit> GenerateCommand { get; }

    public MainViewModel()
    {
        GenerateCommand = ReactiveCommand.CreateFromTask(Generate);
    }

    private async Task Generate()
    {
        ResultText = "Генерация планов...";

        var subjects = new List<Subject>
        {
            new Subject { Id = 1, Name = "Математический анализ 1", ExamType = "Экзамен", Credits = 5.5 },
            new Subject { Id = 2, Name = "Математический анализ 2", ExamType = "Экзамен", Credits = 5.5, Prerequisites = new List<int> { 1 } },
            new Subject { Id = 17, Name = "Дискретная математика", ExamType = "Экзамен", Credits = 3, Prerequisites = new List<int> { 1 } },
            new Subject { Id = 19, Name = "Теория вероятностей", ExamType = "Экзамен", Credits = 3, Prerequisites = new List<int> { 2 } }
        };

        var plans = await PlanGenerator.GenerateValidPlansAsync(subjects);

        if (plans.Count > 2)
        {
            var firstPlan = plans[2];
            string result = "Первый валидный план:\n";
            foreach (var semester in firstPlan)
            {
                result += $"Семестр {semester.Number}:\n";
                foreach (var subject in semester.Subjects)
                {
                    result += $" - {subject.Name} ({subject.ExamType}, {subject.Credits} з.е.)\n";
                }
            }
            ResultText = result;
        }
        else
        {
            ResultText = "Нет валидных планов.";
        }
    }
}
