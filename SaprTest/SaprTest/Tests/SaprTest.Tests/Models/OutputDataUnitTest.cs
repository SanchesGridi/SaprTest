using Moq;
using SaprTest.Models;
using SaprTest.Tests.Models.Base;
using System;
using System.Windows;
using Xunit;

namespace SaprTest.Tests.Models;

public class OutputDataUnitTest : ModelUnitTest
{
    [Fact]
    public override void AllProperties_PropertyChangedEvent_Called()
    {
        var target = new OutputData(It.IsAny<Rect>(), It.IsAny<bool>());

        Assert.PropertyChanged(target, nameof(target.Message), () => target.Message = "Hello world");
    }

    [Fact]
    public void RectangleType_ShouldBe_Outer()
    {
        var target = new OutputData(It.IsAny<Rect>(), true);

        var actual = target.Message.Contains("outer", StringComparison.OrdinalIgnoreCase);

        Assert.True(actual);
    }
}
