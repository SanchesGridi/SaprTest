using Prism.Commands;
using Prism.Services.Dialogs;
using SaprTest.Core.Exceptions;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Utils;
using SaprTest.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace SaprTest.ViewModels;

public class ColorsDialogControlViewModel : DialogViewModel
{
    private ObservableCollection<ColorData> _colors;
    public ObservableCollection<ColorData> Colors
    {
        get => _colors ??= new();
        set => SetProperty(ref _colors, value);
    }

    public DelegateCommand SaveCommand { get; }

    public ColorsDialogControlViewModel() => SaveCommand = new DelegateCommand(SaveCommandExecute);

    public override void OnDialogOpened(IDialogParameters parameters)
    {
        base.OnDialogOpened(parameters);
        var colors = parameters.GetValue<Dictionary<Color, bool>>(Keys.ColorsKey);
        foreach (var color in colors)
        {
            Colors.Add(new(color.Key, color.Value));
        }
    }

    private void SaveCommandExecute()
    {
        if (Colors.Count > 0 && Colors.All(x => !x.Used))
        {
            CloseCommandExecute();
            throw new ExceptionWithHint("At least one color should be in use");
        }
        else if (Colors.Count == 0)
        {
            CloseCommandExecute();
        }
        else
        {
            var result = new DialogResult(ButtonResult.OK);
            var colors = new (Color color, bool used)[Colors.Count];
            for (var index = 0; index < Colors.Count; index++)
            {
                colors[index] = (Colors[index].GetColor(), Colors[index].Used);
            }
            result.Parameters.Add(Keys.ColorsKey, colors);
            OnRequestClose(result);
        }
    }
}
