﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SaprTest.Core.Services.Interfaces;

public interface IViewHelperService
{
    void ScrollConsole(DependencyObject window, string consoleName);

    Rect AddRectangle(DependencyObject window, string canvasName,
        (double X, double Y, double Width, double Height, Color Color) settings);

    Rect? DrawOuterRectangle(DependencyObject window, string canvasName, List<Color> colors);

    void ClearCanvas(DependencyObject window, string canvasName);
}
