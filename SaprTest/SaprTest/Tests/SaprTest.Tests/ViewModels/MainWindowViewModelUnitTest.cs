using Moq;
using Prism.Events;
using Prism.Services.Dialogs;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Core.Settings;
using SaprTest.Core.Utils;
using SaprTest.Tests.Infrastructure.Models;
using SaprTest.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Xunit;

namespace SaprTest.Tests.ViewModels;

public class MainWindowViewModelUnitTest
{
    private readonly Mock<IViewHelperService> _viewHelperServiceMock;
    private readonly Mock<IEventAggregator> _eventAggregatorMock;
    private readonly Mock<IDialogService> _dialogServiceMock;
    private readonly Mock<ISolidColorBrushDialog> _brushDialogMock;
    private readonly MainWindowViewModel _target;

    public MainWindowViewModelUnitTest()
    {
        _viewHelperServiceMock = new Mock<IViewHelperService>();
        _eventAggregatorMock = new Mock<IEventAggregator>();
        _dialogServiceMock = new Mock<IDialogService>();
        _brushDialogMock = new Mock<ISolidColorBrushDialog>();
        _target = new MainWindowViewModel(
            _viewHelperServiceMock.Object, _eventAggregatorMock.Object, _dialogServiceMock.Object, _brushDialogMock.Object
        );
    }

    [Fact]
    public void SelectColorCommand_SetDefaultGreenColor_WhenShowDialogMethodReturnFalse()
    {
        _brushDialogMock.Setup(x => x.ShowDialog()).Returns(false);
        _target.Input.SelectedColorBrush = Brushes.Transparent;

        _target.SelectColorCommand.Execute();

        Assert.Equal(Brushes.Green, _target.Input.SelectedColorBrush);
    }

    [Theory]
    [ClassData(typeof(BrushTheoryData))]
    public void SelectColorCommand_SetRightColor_WhenShowDialogMethodReturnTrue(SolidColorBrush toReturn, SolidColorBrush expected)
    {
        _brushDialogMock.Setup(x => x.GetBrush()).Returns(toReturn);
        _brushDialogMock.Setup(x => x.ShowDialog()).Returns(true);

        _target.SelectColorCommand.Execute();

        Assert.Equal(expected, _target.Input.SelectedColorBrush);
    }

    [Theory]
    [ClassData(typeof(AddRectanglesTheoryData))]
    public void AddRectanglesCommand_AddExpectedRectangles_Count(
        (double X, double Y, double Width, double Height, double Offset, int Count) settings)
    {
        SetupTargetInput(settings);

        _target.AddRectanglesCommand.Execute();

        var currentX = settings.X;
        var currentY = settings.Y;
        for (int index = 0; index < settings.Count; index++)
        {
            _viewHelperServiceMock.Verify(
                x => x.AddRectangle(
                    null, ViewNames.RectanglesCanvas, new RectangleSettings(currentX, currentY, settings.Width, settings.Height, Colors.Green)
                ), Times.Once()
            );
            currentX += settings.Offset;
            currentY += settings.Offset;
        }
        _viewHelperServiceMock.Verify(x => x.ScrollConsole(null, ViewNames.OutputConsole), Times.Once());
    }

    [Fact]
    public void ClearCanvasCommand_ClearAllInputColors_And_ClearCanvasMethodCalled()
    {
        var result = SetupTargetInputAndGetInitializationResult();

        _target.ClearCanvasCommand.Execute();

        Assert.True(result);
        Assert.Empty(_target.Input.GetColors());
        _viewHelperServiceMock.Verify(x => x.ClearCanvas(null, ViewNames.RectanglesCanvas), Times.Once());
    }

    [Fact]
    public void DrawOuterRectangleCommand_DrawRectangleSuccessfully_IfInputHasMoreThanOneColor()
    {
        var result = SetupTargetInputAndGetInitializationResult();
        var colors = _target.Input.GetColors().Keys.ToList();
        _viewHelperServiceMock.Setup(x => x.DrawOuterRectangle(null, ViewNames.RectanglesCanvas, colors))
            .Returns(new Rect());

        _target.DrawOuterRectangleCommand.Execute();

        Assert.True(result);
        _viewHelperServiceMock.Verify(
            x => x.DrawOuterRectangle(null, ViewNames.RectanglesCanvas, colors), Times.Once()
        );
        _viewHelperServiceMock.Verify(x => x.ScrollConsole(null, ViewNames.OutputConsole), Times.Once());
    }

    [Fact]
    public void ChooseColorsCommand_ShowDialogMethod_Called()
    {
        var parameters = new DialogParameters
        {
            { Keys.TitleKey, "Choose Colors" },
            { Keys.ColorsKey, _target.Input.GetColors() }
        };

        _target.ChooseColorsCommand.Execute();

        _dialogServiceMock.Verify(x => x.ShowDialog(Dialogs.ColorsDialog, parameters, GetCallbackMethod()), Times.Once());
    }

    private void SetupTargetInput((double X, double Y, double Width, double Height, double Offset, int Count) settings)
    {
        _target.Input.TopLeftX = settings.X.ToString();
        _target.Input.TopLeftY = settings.Y.ToString();
        _target.Input.Height = settings.Height.ToString();
        _target.Input.Width = settings.Width.ToString();
        _target.Input.Offset = settings.Offset.ToString();
        _target.Input.Count = settings.Count.ToString();
    }

    private bool SetupTargetInputAndGetInitializationResult()
    {
        var colors = new[] { Colors.Red, Colors.Orange, Colors.Blue, Colors.Olive, Colors.Yellow };
        for (var index = 0; index < colors.Length; index++)
        {
            _target.Input.SelectedColorBrush = new SolidColorBrush(colors[index]);
            _target.Input.AddColor();
        }
        return colors.Length == _target.Input.GetColors().Count;
    }

    private Action<IDialogResult> GetCallbackMethod()
    {
        var method = _target.GetType().GetTypeInfo().GetDeclaredMethod("ChooseColorsDialogCallback");
        return (Action<IDialogResult>)method.CreateDelegate(typeof(Action<IDialogResult>), _target);
    }
}
