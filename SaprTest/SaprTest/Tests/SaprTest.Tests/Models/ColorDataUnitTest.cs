using Moq;
using SaprTest.Models;
using SaprTest.Tests.Models.Base;
using System.Windows.Media;
using Xunit;

namespace SaprTest.Tests.Models;

public class ColorDataUnitTest : ModelUnitTest
{
    [Fact]
    public override void AllProperties_PropertyChangedEvent_Called()
    {
        var target = new ColorData(Colors.Black, It.IsAny<bool>());

        Assert.PropertyChanged(target, nameof(target.Brush), () => target.Brush = Brushes.White);
        Assert.PropertyChanged(target, nameof(target.Used), () => target.Used = true);
    }

    [Fact]
    public void GetColor_ExpectedColor_IsBlack()
    {
        var target = new ColorData(Colors.Black, It.IsAny<bool>());

        var actual = target.GetColor();

        Assert.Equal(Colors.Black, actual);
    }
}
