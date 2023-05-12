using Prism.Commands;
using Prism.Events;
using SaprTest.Core.Events;
using SaprTest.Core.Exceptions;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Utils;
using SaprTest.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SaprTest.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISolidColorBrushDialog _brushDialog;

    private InputData _input;
    public InputData Input
    {
        get => _input ??= new();
        set => SetProperty(ref _input, value);
    }

    private ObservableCollection<OutputData> _outputs;
    public ObservableCollection<OutputData> Outputs
    {
        get => _outputs ??= new();
        set => SetProperty(ref _outputs, value);
    }

    public DelegateCommand SelectColorCommand { get; }
    public DelegateCommand AddRectangleCommand { get; }
    public DelegateCommand AddStaticRectanglesCommand { get; }
    public DelegateCommand ClearCanvasCommand { get; }
    public DelegateCommand DrawOuterRectangleCommand { get; }

    public MainWindowViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator,
        ISolidColorBrushDialog brushDialog) : base(viewHelper, eventAggregator)
    {
        _brushDialog = brushDialog;

        SelectColorCommand = new DelegateCommand(SelectColorCommandExecute);
        AddRectangleCommand = new DelegateCommand(AddRectangleCommandExecute);
        AddStaticRectanglesCommand = new DelegateCommand(AddStaticRectanglesCommandExecute);
        ClearCanvasCommand = new DelegateCommand(ClearCanvasCommandExecute);
        DrawOuterRectangleCommand = new DelegateCommand(DrawOuterRectangleCommandExecute);
    }

    private void SelectColorCommandExecute()
    {
        Input.SelectedColorBrush = _brushDialog.ShowDialog() ?
            _brushDialog.GetBrush() :
            Brushes.Green;
    }

    private void AddRectangleCommandExecute()
    {
        try
        {
            var topLeftX = ToDouble(Input.TopLeftX, "Top Left X param");
            var topLeftY = ToDouble(Input.TopLeftY, "Top Left Y param");
            var width = ToDouble(Input.Width, "Width");
            var height = ToDouble(Input.Height, "Height");

            AddRectangle(topLeftX, topLeftY, width, height);

            _viewHelper.ScrollConsole(_application.MainWindow, ViewNames.OutputConsole);
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private void AddStaticRectanglesCommandExecute()
    {
        try
        {
            var width = 100d;
            var height = 50d;
            var points = CreateStaticStartPoints();
            if (double.TryParse(Input.Width, out var w) && double.TryParse(Input.Height, out var h))
            {
                width = w; height = h;
            }
            for (int index = 0; index < points.Length; index++)
            {
                AddRectangle(points[index].X, points[index].Y, width, height);
            }
            _viewHelper.ScrollConsole(_application.MainWindow, ViewNames.OutputConsole);
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private void DrawOuterRectangleCommandExecute()
    {
        try
        {
            _viewHelper.DrawOuterRectangle(_application.MainWindow, ViewNames.RectanglesCanvas);
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private void AddRectangle(double x, double y, double w, double h)
    {
        var rectangle = _viewHelper.AddRectangle(
            _application.MainWindow,
            ViewNames.RectanglesCanvas,
            (x, y, w, h, Input.SelectedColorBrush.Color)
        );
        Outputs.Add(new OutputData(rectangle));
    }

    private void ClearCanvasCommandExecute()
    {
        _viewHelper.ClearCanvas(_application.MainWindow, ViewNames.RectanglesCanvas);
    }

    private static double ToDouble(string input, string inputName)
    {
        try
        {
            return double.Parse(input);
        }
        catch (Exception ex)
        {
            throw new ExceptionWithHint($"Value: [{input}]; Name: [{inputName}]", ex);
        }
    }

    private static (double X, double Y)[] CreateStaticStartPoints()
    {
        var currentX = 50d;
        var currentY = 50d;
        var points = new (double x, double y)[5];
        for (var index = 0; index < points.Length; index++)
        {
            points[index].x = currentX;
            points[index].y = currentY;
            currentX += 25d;
            currentY += 25d;
        }
        return points;
    }
}
