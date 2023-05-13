using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Media;

namespace SaprTest.Models;

public class InputData : BindableBase
{
    private readonly Dictionary<Color, bool> _colors;

    private string _topLeftX = "0";
    public string TopLeftX
    {
        get => _topLeftX;
        set => SetProperty(ref _topLeftX, value);
    }

    private string _topLeftY = "0";
    public string TopLeftY
    {
        get => _topLeftY;
        set => SetProperty(ref _topLeftY, value);
    }

    private string _height = "100";
    public string Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    private string _width = "100";
    public string Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    private string _offset = "0";
    public string Offset
    {
        get => _offset;
        set => SetProperty(ref _offset, value);
    }

    private string _count = "1";
    public string Count
    {
        get => _count;
        set => SetProperty(ref _count, value);
    }

    private SolidColorBrush _selectedColorBrush;
    public SolidColorBrush SelectedColorBrush
    {
        get => _selectedColorBrush ??= Brushes.Green;
        set => SetProperty(ref _selectedColorBrush, value);
    }

    public InputData() => _colors = new();

    public void AddColor()
    {
        var color = SelectedColorBrush.Color;
        if (!_colors.ContainsKey(color))
        {
            _colors.Add(color, true);
        }
    }

    public void SetColors((Color Key, bool Value)[] values)
    {
        ClearColors();
        for (var index = 0; index < values.Length; index++)
        {
            _colors.Add(values[index].Key, values[index].Value);
        }
    }

    public void ClearColors() => _colors.Clear();

    public Dictionary<Color, bool> GetColors() => _colors;
}
