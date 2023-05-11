using Prism.Commands;
using Prism.Events;
using SaprTest.Core.Mvvm.Models;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Utils;
using System.Windows.Media;
using Path = System.Windows.Shapes.Path;
using Visibility = System.Windows.Visibility;

namespace SaprTest.ViewModels;

public class TestCanvasWindowViewModel : ViewModelBase
{
    private readonly IViewProvider _viewProvider;

    public DelegateCommand SwitchRectangleCommand { get; }
    public DelegateCommand AddPathRectangleCommand { get; }
    public DelegateCommand AddNativeRectangleCommand { get; }

    public TestCanvasWindowViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator,
        IViewProvider viewProvider) : base(viewHelper, eventAggregator)
    {
        _viewProvider = viewProvider;

        SwitchRectangleCommand = new DelegateCommand(SwitchRectangleCommandExecute);
        AddPathRectangleCommand = new DelegateCommand(AddPathRectangleCommandExecute);
        AddNativeRectangleCommand = new DelegateCommand(AddNativeRectangleCommandExecute);
    }

    private void SwitchRectangleCommandExecute()
    {
        var path = _viewProvider.GetView<Path>(_application.MainWindow, "_rectangle_");
        if (path.Visibility == Visibility.Hidden) {
            path.Visibility = Visibility.Visible;
        } else {
            path.Visibility = Visibility.Hidden;
        }
    }

    private void AddPathRectangleCommandExecute()
    {
        _viewHelper.AddPathRectangle(_application.MainWindow, ViewNames.RectanglesCanvas, CreateRectangle());
    }

    private void AddNativeRectangleCommandExecute()
    {
        _viewHelper.AddNativeRectangle(_application.MainWindow, ViewNames.RectanglesCanvas, CreateRectangle());
    }

    private static Rectangle CreateRectangle()
    {
        var topLeftX = 100d;
        var topLeftY = 100d;
        var height = 35d;
        var width = 200d;

        var topLeftPoint = new Point(topLeftX, topLeftY);
        var bottomLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y + height);
        var bottomRightPoint = new Point(bottomLeftPoint.X + width, bottomLeftPoint.Y);
        var topRightPoint = new Point(bottomRightPoint.X, bottomRightPoint.Y - height);

        return new Rectangle(topLeftPoint, bottomLeftPoint, bottomRightPoint, topRightPoint, Colors.Green, RectangleIds.GetNew());
    }
}
