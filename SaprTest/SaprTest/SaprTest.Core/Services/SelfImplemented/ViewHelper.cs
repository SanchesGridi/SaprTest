using SaprTest.Core.Services.Interfaces;
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

    public void AddRectangle(DependencyObject window, string canvasName,
        (double X, double Y, double Width, double Height, Color Color) settings)
    {
        var canvas = _viewProvider.GetView<Canvas>(window, canvasName);
        var path = new Path
        {
            Fill = new SolidColorBrush(settings.Color),
            Stroke = Brushes.Black,
            Data = new RectangleGeometry
            {
                Rect = new Rect
                {
                    Location = new Point(settings.X, settings.Y),
                    Size = new Size(settings.Width, settings.Height)
                }
            }
        };
        canvas.Children.Add(path);
    }
}
