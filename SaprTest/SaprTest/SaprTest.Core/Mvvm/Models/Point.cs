using Prism.Mvvm;

namespace SaprTest.Core.Mvvm.Models;

public class Point : BindableBase
{
    private double _x;
    public double X
    {
        get => _x;
        set => SetProperty(ref _x, value);
    }

    private double _y;
    public double Y
    {
        get => _y;
        set => SetProperty(ref _y, value);
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}
