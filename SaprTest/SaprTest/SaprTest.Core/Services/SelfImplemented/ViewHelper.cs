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

    public void AddPolygonRectangle(DependencyObject window, string canvasName, CustomRectangle rectangle)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        var polygon = CreatePolygon(rectangle);
        canvas.Children.Add(polygon);
    }

    public void AddPathRectangle(DependencyObject window, string canvasName, CustomRectangle rectangle, string mode)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        var path = null as Path;
        if (mode == "line_segments")
        {
            path = CreatePath(rectangle);
        }
        else if (mode == "rectangle_geometry")
        {
            path = new Path
            {
                Name = $"_path_rectangle_{rectangle.Id}_",
                Fill = new SolidColorBrush(rectangle.Color),
                Stroke = Brushes.Black,
                Data = new RectangleGeometry
                {
                    Rect = new Rect
                    {
                        Location = new Point(rectangle.TopLeft.X, rectangle.TopLeft.Y),
                        Size = new Size(rectangle.Width, rectangle.Height)
                    }
                }
            };
        }
        canvas.Children.Add(path);
    }

    private static Polygon CreatePolygon(CustomRectangle rectangle)
    {
        return new Polygon
        {
            Name = $"_polygon_rectangle_{rectangle.Id}_",
            Fill = new SolidColorBrush(rectangle.Color),
            Stroke = Brushes.Black,
            Points =
            {
                new Point(rectangle.TopLeft.X, rectangle.TopLeft.Y),
                new Point(rectangle.BottomLeft.X, rectangle.BottomLeft.Y),
                new Point(rectangle.BottomRight.X, rectangle.BottomRight.Y),
                new Point(rectangle.TopRight.X, rectangle.TopRight.Y)
            }
        };
    }

    private static Path CreatePath(CustomRectangle rectangle)
    {
        return new Path
        {
            Name = $"_path_rectangle_{rectangle.Id}_",
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
