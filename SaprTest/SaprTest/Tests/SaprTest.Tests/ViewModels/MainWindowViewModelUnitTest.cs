using Moq;
using Prism.Events;
using Prism.Services.Dialogs;
using SaprTest.Core.Services.Interfaces;
using SaprTest.Tests.Infrastructure.Models;
using SaprTest.ViewModels;
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
}
