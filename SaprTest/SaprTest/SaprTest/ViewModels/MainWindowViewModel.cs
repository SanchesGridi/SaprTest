using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using SaprTest.Core.Events;
using SaprTest.Core.Exceptions;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Utils;
using SaprTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

using static SaprTest.Core.Utils.Convert;

namespace SaprTest.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
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
    public DelegateCommand AddRectanglesCommand { get; }
    public DelegateCommand ClearCanvasCommand { get; }
    public DelegateCommand DrawOuterRectangleCommand { get; }
    public DelegateCommand ChooseColorsCommand { get; }

    public MainWindowViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator,
        IDialogService dialogService,
        ISolidColorBrushDialog brushDialog) : base(viewHelper, eventAggregator)
    {
        _dialogService = dialogService;
        _brushDialog = brushDialog;

        SelectColorCommand = new DelegateCommand(SelectColorCommandExecute);
        AddRectanglesCommand = new DelegateCommand(AddRectanglesCommandExecute);
        ClearCanvasCommand = new DelegateCommand(ClearCanvasCommandExecute);
        DrawOuterRectangleCommand = new DelegateCommand(DrawOuterRectangleCommandExecute);
        ChooseColorsCommand = new DelegateCommand(ChooseColorsCommandExecute);
    }

    private void SelectColorCommandExecute()
    {
        Input.SelectedColorBrush = _brushDialog.ShowDialog() ?
            _brushDialog.GetBrush() :
            Brushes.Green;
    }

    private void AddRectanglesCommandExecute()
    {
        try
        {
            var topLeftX = ToDouble(Input.TopLeftX, "Top Left X param");
            var topLeftY = ToDouble(Input.TopLeftY, "Top Left Y param");
            if (topLeftX < 0 || topLeftY < 0)
            {
                throw new InvalidOperationException("Point(X,Y) should be at least equal [0]");
            }
            var width = ToDouble(Input.Width, "Width");
            var height = ToDouble(Input.Height, "Height");
            if (width <= 0 || height <= 0)
            {
                throw new InvalidOperationException("Width and Height must be greater than [0]");
            }
            var count = ToInt(Input.Count, "Rectangles Count");
            if (count < 1)
            {
                throw new InvalidOperationException("Count should be at least equal [1]");
            }
            var offset = ToDouble(Input.Offset, "Offset");
            if (offset < 0)
            {
                throw new NotImplementedException(":) need additional logic");
            }
            var points = CreateStartPoints(count, topLeftX, topLeftY, offset);
            for (int index = 0; index < points.Length; index++)
            {
                AddRectangle(points[index].X, points[index].Y, width, height);
            }
            Input.AddColor();
            _viewHelper.ScrollConsole(_application.MainWindow, ViewNames.OutputConsole);
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private void ClearCanvasCommandExecute()
    {
        _viewHelper.ClearCanvas(_application.MainWindow, ViewNames.RectanglesCanvas);
        Input.ClearColors();
    }

    private void DrawOuterRectangleCommandExecute()
    {
        try
        {
            var appliedColors = new List<Color>();
            foreach (var item in Input.GetColors())
            {
                if (item.Value)
                {
                    appliedColors.Add(item.Key);
                }
            }
            if (appliedColors.Count > 0)
            {
                var rectangle = _viewHelper.DrawOuterRectangle(
                    _application.MainWindow, ViewNames.RectanglesCanvas, appliedColors
                );
                if (rectangle != null)
                {
                    Outputs.Add(new(rectangle.Value, true));
                    _viewHelper.ScrollConsole(_application.MainWindow, ViewNames.OutputConsole);
                }
            }
            else
            {
                throw new ExceptionWithHint("Add at least one rectangle");
            }
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private void ChooseColorsCommandExecute()
    {
        try
        {
            var colorsKey = Keys.ColorsKey;
            var parameters = new DialogParameters
            {
                { Keys.TitleKey, "Choose Colors" },
                { colorsKey, Input.GetColors() }
            };
            _dialogService.ShowDialog(Dialogs.ColorsDialog, parameters, x =>
            {
                if (x.Result == ButtonResult.OK)
                {
                    var colors = x.Parameters.GetValue<(Color, bool)[]>(colorsKey);
                    Input.SetColors(colors);
                }
            });
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

    private static (double X, double Y)[] CreateStartPoints(
        int count, double currentX, double currentY, double offset)
    {
        var points = new (double x, double y)[count];
        for (var index = 0; index < points.Length; index++)
        {
            points[index].x = currentX;
            points[index].y = currentY;
     
            currentX += offset;
            currentY += offset;
        }
        return points;
    }
}
