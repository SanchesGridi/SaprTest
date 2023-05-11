using Prism.Events;
using SaprTest.Core.Events;
using SaprTest.Core.Exceptions;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Utils;
using SaprTest.Modules.Exceptions.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SaprTest.Modules.Exceptions.ViewModels;

public class ConsoleControlViewModel : ViewModelBase
{
    private ObservableCollection<EntryModel> _exceptions;
    public ObservableCollection<EntryModel> Exceptions
    {
        get => _exceptions ??= new();
        set => SetProperty(ref _exceptions, value);
    }

    public ConsoleControlViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator) : base(viewHelper, eventAggregator)
    {
        _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(async ex =>
        {
            var entry = new EntryModel();
            entry.SetException(ex);
            if (ex.GetType() == typeof(ExceptionWithHint))
            {
                entry.Brush = Brushes.Green;
            }
            await DispatchAsync(() =>
            {
                Exceptions.AddRange(entry.GetInners());
                _viewHelper.ScrollConsole(_application.MainWindow, ViewNames.ExceptionsConsole);
            });
        });
    }
}
