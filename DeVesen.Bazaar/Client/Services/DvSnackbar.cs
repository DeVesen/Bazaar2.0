using MudBlazor;

namespace DeVesen.Bazaar.Client.Services
{
    public class SnackBarService
    {
        private readonly ISnackbar _mudSnackBar;

        public string PositionTopStart => Defaults.Classes.Position.TopStart;
        public string PositionTopLeft => Defaults.Classes.Position.TopLeft;
        public string PositionTopCenter => Defaults.Classes.Position.TopCenter;
        public string PositionTopRight => Defaults.Classes.Position.TopRight;
        public string PositionTopEnd => Defaults.Classes.Position.TopEnd;

        public string PositionBottomStart => Defaults.Classes.Position.BottomStart;
        public string PositionBottomLeft => Defaults.Classes.Position.BottomLeft;
        public string PositionBottomCenter => Defaults.Classes.Position.BottomCenter;
        public string PositionBottomRight => Defaults.Classes.Position.BottomRight;
        public string PositionBottomEnd => Defaults.Classes.Position.BottomEnd;

        public SnackBarService(ISnackbar mudSnackBar)
        {
            _mudSnackBar = mudSnackBar;
        }

        public void AddInfo(string message)
        {
            AddInfo(message, Guid.NewGuid().ToString());
        }

        public void AddInfo(string message, string key)
        {
            AddInfo(message, key, Defaults.Classes.Position.BottomRight);
        }


        public void AddInfo(string message, string key, string position)
        {
            _mudSnackBar.Configuration.PositionClass = position;
            _mudSnackBar.Add(
                message,
                Severity.Info,
                config =>
                {
                    config.VisibleStateDuration = 2500;
                },
                key);
        }

        public void AddError(string message)
        {
            AddError(message, Guid.NewGuid().ToString());
        }

        public void AddError(string message, string key)
        {
            AddError(message, key, Defaults.Classes.Position.BottomRight);
        }

        public void AddError(string message, string key, string position)
        {
            _mudSnackBar.Configuration.PositionClass = position;
            _mudSnackBar.Add(
                message,
                Severity.Error,
                config =>
                {
                    config.VisibleStateDuration = 2500;
                },
                key);
        }

        public void ClearAll()
        {
            _mudSnackBar.Clear();
        }

        public void RemoveByKey(string key)
        {
            _mudSnackBar.RemoveByKey(key);
        }
    }
}
