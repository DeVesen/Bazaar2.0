using MudBlazor;

namespace SmartBazaar.Client.Services;

public class SnackBarService(ISnackbar mudSnackBar)
{
    private const int DefaultVisibleStateDuration = 2500;


    public void AddInfo(string message)
    {
        AddInfo(message, Guid.NewGuid().ToString());
    }

    public void AddInfo(string message, string key)
    {
        AddInfo(message, key, ElementViewPositions.BottomRight);
    }

    public void AddInfo(string message, string key, ElementViewPositions position)
    {
        AddSnackBar(Severity.Info, message, key, position);
    }


    private void AddSnackBar(Severity severity, string message, string key, ElementViewPositions position)
    {
        mudSnackBar.Configuration.PositionClass = GetPositionDetails(position);
        mudSnackBar.Add(
            message,
            severity,
            config =>
            {
                config.VisibleStateDuration = DefaultVisibleStateDuration;
            },
            key);
    }


    private static string GetPositionDetails(ElementViewPositions position)
        => position switch
        {
            ElementViewPositions.TopStart => Defaults.Classes.Position.TopStart,
            ElementViewPositions.TopLeft => Defaults.Classes.Position.TopLeft,
            ElementViewPositions.TopCenter => Defaults.Classes.Position.TopCenter,
            ElementViewPositions.TopRight => Defaults.Classes.Position.TopRight,
            ElementViewPositions.TopEnd => Defaults.Classes.Position.TopEnd,
            ElementViewPositions.BottomStart => Defaults.Classes.Position.BottomStart,
            ElementViewPositions.BottomLeft => Defaults.Classes.Position.BottomLeft,
            ElementViewPositions.BottomCenter => Defaults.Classes.Position.BottomCenter,
            ElementViewPositions.BottomRight => Defaults.Classes.Position.BottomRight,
            ElementViewPositions.BottomEnd => Defaults.Classes.Position.BottomEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
}