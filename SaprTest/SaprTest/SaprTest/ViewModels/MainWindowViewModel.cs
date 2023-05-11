using Prism.Commands;
using Prism.Events;
using SaprTest.Core.Events;
using SaprTest.Core.Exceptions;
using SaprTest.Core.Mvvm.ViewModels;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Services.SelfImplemented;
using SaprTest.Core.Mvvm.Models;
using System;
using System.Windows.Media;
using SaprTest.Models;
using SaprTest.Core.Utils;

namespace SaprTest.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISolidColorBrushDialog _brushDialog;

    private InputData _input;
    public InputData Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public DelegateCommand SelectColorCommand { get; }
    public DelegateCommand AddRectangleCommand { get; }

    public MainWindowViewModel(
        ViewHelper viewHelper,
        IEventAggregator eventAggregator,
        ISolidColorBrushDialog brushDialog) : base(viewHelper, eventAggregator)
    {
        _brushDialog = brushDialog;

        SelectColorCommand = new DelegateCommand(SelectColorCommandExecute);
        AddRectangleCommand = new DelegateCommand(AddRectangleCommandExecute);
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
            // need implementation
            _viewHelper.AddNativeRectangle(_application.MainWindow, ViewNames.RectanglesCanvas, CreateRectangle());
        }
        catch (Exception ex)
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
    }

    private Rectangle CreateRectangle()
    {
        var topLeftX = ToDouble(Input.TopLeftX, "Top Left X param");
        var topLeftY = ToDouble(Input.TopLeftY, "Top Left Y param");
        var height = ToDouble(Input.Height, "Height");
        var width = ToDouble(Input.Width, "Width");

        var topLeftPoint = new Point(topLeftX, topLeftY);
        var bottomLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y + height);
        var bottomRightPoint = new Point(bottomLeftPoint.X + width, bottomLeftPoint.Y);
        var topRightPoint = new Point(bottomRightPoint.X, bottomRightPoint.Y - height);

        return new Rectangle(
            topLeftPoint,
            bottomLeftPoint,
            bottomRightPoint,
            topRightPoint,
            Input.SelectedColorBrush.Color,
            RectangleIds.GetNew()
        );
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
}
