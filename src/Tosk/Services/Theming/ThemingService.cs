using Microsoft.JSInterop;
using Tosk.Enums;

namespace Tosk.Services.Theming
{
    public class ThemingService(IJSRuntime JS) : IThemingService
    {
        private const string ThemeKey = "theme";
        private const string JSThemeChangerFunctionName = "setBootstrapTheme";

        public Theme Theme { get; private set; }

        public async Task InitializeAsync()
        {
            var value = Preferences.Get(ThemeKey, default(Theme).ToString());
            Theme = Enum.Parse<Theme>(value);
            await ChangeTheme();
        }

        public async Task ToggleAsync()
        {
            Theme = Theme == Theme.Light 
                ? Theme.Dark 
                : Theme.Light;
            Preferences.Set(ThemeKey, Theme.ToString());
            await ChangeTheme();
        }

        private ValueTask ChangeTheme() => JS.InvokeVoidAsync(JSThemeChangerFunctionName, Theme.ToString());

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
