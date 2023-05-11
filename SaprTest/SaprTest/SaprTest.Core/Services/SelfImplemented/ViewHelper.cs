using SaprTest.Core.Services.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CustomPoint = SaprTest.Core.Mvvm.Models.Point;
using CustomRectangle = SaprTest.Core.Mvvm.Models.Rectangle;

namespace SaprTest.Core.Services.SelfImplemented;

public sealed class ViewHelper
{
    private readonly IViewProvider _viewProvider;

    public ViewHelper(IViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public void ScrollConsole(DependencyObject window, string consoleName)
    {
        var console = _viewProvider.GetView<ListBox>(window, consoleName);
        var lastIndex = console.Items.Count - 1;
        var lastChild = console.Items[lastIndex];

        console.ScrollIntoView(lastChild);
    }

    public void AddPathRectangle(DependencyObject window, string canvasName, CustomRectangle rectangle)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        var path = CreatePath(rectangle);
        canvas.Children.Add(path);
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"Rectangle area: [{path.Data.GetArea()}]");
#endif
    }

    public void AddNativeRectangle(DependencyObject window, string canvasName, CustomRectangle rectangle)
    {

    }

    private static Path CreatePath(CustomRectangle rectangle)
    {
        return new Path
        {
            Name = $"_rectangle_{rectangle.Id}_",
            Fill = new SolidColorBrush(rectangle.Color),
            Stroke = Brushes.Black,
            Data = new PathGeometry(new[]
            {
                new PathFigure(
                    start: new Point(rectangle.TopLeft.X, rectangle.TopLeft.Y),
                    segments: CreateSegments(new[] { rectangle.BottomLeft, rectangle.BottomRight, rectangle.TopRight }),
                    closed: true
                )
            })
        };
    }

    private static PathSegment[] CreateSegments(CustomPoint[] points)
    {
        var segments = new LineSegment[points.Length];
        for (var index = 0; index < points.Length; index++)
        {
            var point = new Point(points[index].X, points[index].Y);
            segments[index] = new LineSegment { Point = point };
        }
        return segments;
    }
}
