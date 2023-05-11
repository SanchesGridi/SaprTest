using Prism.Mvvm;
using System;
using System.Windows.Media;

namespace SaprTest.Core.Mvvm.Models;

public sealed class Rectangle : BindableBase
{
    private readonly int _id;

    private Point _topLeft;
    public Point TopLeft
    {
        get => _topLeft;
        set => SetProperty(ref _topLeft, value);
    }

    private Point _bottomLeft;
    public Point BottomLeft
    {
        get => _bottomLeft;
        set => SetProperty(ref _bottomLeft, value);
    }

    private Point _bottomRight;
    public Point BottomRight
    {
        get => _bottomRight;
        set => SetProperty(ref _bottomRight, value);
    }

    private Point _topRight;
    public Point TopRight
    {
        get => _topRight;
        set => SetProperty(ref _topRight, value);
    }

    private Color _color;
    public Color Color
    {
        get => _color;
        set => SetProperty(ref _color, value);
    }

    public int Id => _id;

    public Rectangle(Point topLeft, Point bottomLeft, Point bottomRight, Point topRight, Color color, int id)
    {
        _id = id;

        TopLeft = topLeft;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        TopRight = topRight;
        Color = color;

        ValidateRectangle();
    }

    private void ValidateRectangle()
    {
        var leftHeight = BottomLeft.Y - TopLeft.Y;
        var rightHeight = BottomRight.Y - TopRight.Y;
        var topWidth = TopRight.X - TopLeft.X;
        var bottomWidth = BottomRight.X - BottomLeft.X;
        if (leftHeight != rightHeight || topWidth != bottomWidth)
        {
            throw new Exception("The current shape instance is not a rectangle.");
        }
    }
}
