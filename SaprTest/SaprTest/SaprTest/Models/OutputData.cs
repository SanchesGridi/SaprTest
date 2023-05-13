using Prism.Mvvm;
using System;
using System.Text;
using System.Windows;

namespace SaprTest.Models;

public class OutputData : BindableBase
{
    private string _message;
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public OutputData(Rect r, bool outer = false)
    {
        Message = new StringBuilder()
            .AppendLine($"New rectangle: {DateTime.Now:dd:MM:yy -- HH:mm:ss}")
            .AppendLine($"Rectangle type: [{(outer ? "OUTER" : "INNER")}]")
            .AppendLine($"P1: ({r.TopLeft.X},{r.TopLeft.Y})")
            .AppendLine($"P2: ({r.BottomLeft.X},{r.BottomLeft.Y})")
            .AppendLine($"P3: ({r.BottomRight.X},{r.BottomRight.Y})")
            .AppendLine($"P4: ({r.TopRight.X},{r.TopRight.Y})")
            .Append(new string('=', 10))
            .ToString();
    }
}
