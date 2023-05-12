using Prism.Commands;
using Prism.Events;
using SaprTest.Core.Mvvm.Models;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Utils;
using System;
using System.Windows.Media;
using Path = System.Windows.Shapes.Path;
using Polygon = System.Windows.Shapes.Polygon;
using Visibility = System.Windows.Visibility;

namespace SaprTest.ViewModels;

public class TestCanvasWindowViewModel : ViewModelBase
{
    private enum RectangleMode : byte
    {
        Path,
        Polygon
    }

    private readonly IViewProvider _viewProvider;

    // public DelegateCommand SwitchRectangleCommand { get; }
    public DelegateCommand ShowRectanglePointsCommand { get; }
    public DelegateCommand ShowRectangleAreaCommand { get; }
    public DelegateCommand AddPathRectangleCommand { get; }
    public DelegateCommand AddPolygonRectangleCommand { get; }

    public TestCanvasWindowViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator,
        IViewProvider viewProvider) : base(viewHelper, eventAggregator)
    {
        _viewProvider = viewProvider;

        // SwitchRectangleCommand = new DelegateCommand(SwitchRectangleCommandExecute);
        ShowRectanglePointsCommand = new DelegateCommand(ShowRectanglePointsCommandExecute);
        ShowRectangleAreaCommand = new DelegateCommand(ShowRectangleAreaCommandExecute);
        AddPathRectangleCommand = new DelegateCommand(AddPathRectangleCommandExecute);
        AddPolygonRectangleCommand = new DelegateCommand(AddPolygonRectangleCommandExecute);
    }

    // private void SwitchRectangleCommandExecute()
    // {
    //     var path = _viewProvider.GetView<Path>(_application.MainWindow, "_path_rectangle_static_");
    //     if (path.Visibility == Visibility.Hidden) {
    //         path.Visibility = Visibility.Visible;
    //     } else {
    //         path.Visibility = Visibility.Hidden;
    //     }
    // }

    private void ShowRectanglePointsCommandExecute()
    {
        var path = _viewProvider.GetView<Path>(_application.MainWindow, "_path_rectangle_1_");
        if (path.Data is RectangleGeometry geometry)
        {
            var rectangle = geometry.Rect;
            var output = new System.Text.StringBuilder()
                .AppendLine($"P1: ({rectangle.TopLeft.X},{rectangle.TopLeft.Y})")
                .AppendLine($"P2: ({rectangle.BottomLeft.X},{rectangle.BottomLeft.Y})")
                .AppendLine($"P3: ({rectangle.BottomRight.X},{rectangle.BottomRight.Y})")
                .AppendLine($"P4: ({rectangle.TopRight.X},{rectangle.TopRight.Y})")
                .ToString();
            System.Diagnostics.Debug.WriteLine(output);
        }
    }

    private void ShowRectangleAreaCommandExecute()
    {
        var path = _viewProvider.GetView<Path>(_application.MainWindow, "_path_rectangle_1_");
        if (path != null)
        {
            System.Diagnostics.Debug.WriteLine($"Rectangle_Path area: [{path.Data.GetArea()}]");
        }
        // var polygon = _viewProvider.GetView<Polygon>(_application.MainWindow, "_polygon_rectangle_1_");
        // if (polygon != null)
        // {
        //     System.Diagnostics.Debug.WriteLine($"Rectangle_Polygon area: [{polygon.RenderedGeometry.GetArea()}]");
        // }
    }

    private void AddPathRectangleCommandExecute()
    {
        var modes = new[] { "line_segments", "rectangle_geometry" };
        var currentMode = modes[1];

        _viewHelper.AddPathRectangle(
            _application.MainWindow,
            ViewNames.RectanglesCanvas,
            CreateRectangle(RectangleMode.Path),
            currentMode
        );
    }

    private void AddPolygonRectangleCommandExecute()
    {
        _viewHelper.AddPolygonRectangle(
            _application.MainWindow,
            ViewNames.RectanglesCanvas,
            CreateRectangle(RectangleMode.Polygon)
        );
    }

    private static Rectangle CreateRectangle(RectangleMode mode)
    {
        var topLeftX = 100d;
        var topLeftY = 100d;
        var height = 35d;
        var width = 200d;
        var color = Colors.Green;
        var id = mode switch
        {
            RectangleMode.Path => RectangleIds.NewPathId(),
            RectangleMode.Polygon => RectangleIds.NewPolygonId(),
            _ => throw new ArgumentException($"Not allowed enum type, value: {(byte)mode}", nameof(mode))
        };

        return new Rectangle(id, topLeftX, topLeftY, height, width, color);
    }
}
