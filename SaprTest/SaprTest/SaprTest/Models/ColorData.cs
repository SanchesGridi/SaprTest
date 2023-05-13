using Prism.Mvvm;
using System.Windows.Media;

namespace SaprTest.Models;

public class ColorData : BindableBase
{
    private Brush _brush;
    public Brush Brush
    {
        get => _brush;
        set => SetProperty(ref _brush, value);
    }

    private bool _used;
    public bool Used
    {
        get => _used;
        set => SetProperty(ref _used, value);
    }

    public ColorData(Color color, bool used)
    {
        Brush = new SolidColorBrush(color);
        Used = used;
    }

    public Color GetColor() => ((SolidColorBrush)Brush).Color;
}
