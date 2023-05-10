using SaprTest.Core.Mvvm.ViewModels;

namespace SaprTest.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _content = "Hello World";
    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public MainWindowViewModel()
    {

    }
}
