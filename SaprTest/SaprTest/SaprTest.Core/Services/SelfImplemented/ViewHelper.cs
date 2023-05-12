using SaprTest.Core.Services.Interfaces;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

    public Rect AddRectangle(DependencyObject window, string canvasName,
        (double X, double Y, double Width, double Height, Color Color) settings)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        var rectangle = new Rect
        {
            Location = new Point(settings.X, settings.Y),
            Size = new Size(settings.Width, settings.Height)
        };
        var path = new Path
        {
            Fill = new SolidColorBrush(settings.Color),
            Stroke = Brushes.Black,
            Data = new RectangleGeometry(rectangle)
        };
        canvas.Children.Add(path);

        return rectangle;
    }

    public void DrawOuterRectangle(DependencyObject window, string canvasName)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        if (canvas?.Children?.Count > 0)
        {
            var topLeftX = double.MaxValue;
            var topLeftY = double.MaxValue;
            var bottomRightX = double.MinValue;
            var bottomRightY = double.MinValue;

            for (var index = 0; index < canvas.Children.Count; index++)
            {
                var path = canvas.Children[index] as Path;
                var geometry = path.Data as RectangleGeometry;

                if (geometry.Rect.TopLeft.X < topLeftX)
                {
                    topLeftX = geometry.Rect.TopLeft.X;
                }
                if (geometry.Rect.TopLeft.Y < topLeftY)
                {
                    topLeftY = geometry.Rect.TopLeft.Y;
                }
                if (geometry.Rect.BottomRight.X > bottomRightX)
                {
                    bottomRightX = geometry.Rect.BottomRight.X;
                }
                if (geometry.Rect.BottomRight.Y > bottomRightY)
                {
                    bottomRightY = geometry.Rect.BottomRight.Y;
                }
            }

            var width = bottomRightX - topLeftX;
            var height = bottomRightY - topLeftY;
            var rectangle = new Rect
            {
                Location = new Point(topLeftX, topLeftY),
                Size = new Size(width, height)
            };
            var outerPath = new Path
            {
                Opacity = 0.5,
                Stroke = Brushes.Black,
                Data = new RectangleGeometry(rectangle)
            };
            canvas.Children.Add(outerPath);
        }
    }

    public void ClearCanvas(DependencyObject window, string canvasName)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        if (canvas?.Children?.Count > 0)
        {
            canvas.Children.Clear();
        }
    }
}
