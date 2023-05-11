using Prism.Mvvm;
using System.Windows.Media;

namespace SaprTest.Models;

public class InputData : BindableBase
{
    private string _topLeftX;
    public string TopLeftX
    {
        get => _topLeftX;
        set => SetProperty(ref _topLeftX, value);
    }

    private string _topLeftY;
    public string TopLeftY
    {
        get => _topLeftY;
        set => SetProperty(ref _topLeftY, value);
    }

    private string _height;
    public string Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    private string _width;
    public string Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    private SolidColorBrush _selectedColorBrush;
    public SolidColorBrush SelectedColorBrush
    {
        get => _selectedColorBrush ??= Brushes.Green;
        set => SetProperty(ref _selectedColorBrush, value);
    }
}
