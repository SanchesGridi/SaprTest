using SaprTest.Models;
using SaprTest.Tests.Models.Base;
using System.Windows.Media;
using Xunit;

namespace SaprTest.Tests.Models;

public class InputDataUnitTest : ModelUnitTest
{
    [Fact]
    public override void AllProperties_PropertyChangedEvent_Called()
    {
        var target = new InputData();
        var value = "1000";

        Assert.PropertyChanged(target, nameof(target.TopLeftX), () => target.TopLeftX = value);
        Assert.PropertyChanged(target, nameof(target.TopLeftY), () => target.TopLeftY = value);
        Assert.PropertyChanged(target, nameof(target.Height), () => target.Height = value);
        Assert.PropertyChanged(target, nameof(target.Width), () => target.Width = value);
        Assert.PropertyChanged(target, nameof(target.Offset), () => target.Offset = value);
        Assert.PropertyChanged(target, nameof(target.Count), () => target.Count = value);
    }

    [Fact]
    public void AddColors_RepeatedColors_Ignores()
    {
        var target = new InputData
        {
            SelectedColorBrush = new SolidColorBrush(Colors.Black)
        };

        for (int i = 0; i < 10; i++)
        {
            target.AddColor();
        }

        Assert.Single(target.GetColors());
    }
}