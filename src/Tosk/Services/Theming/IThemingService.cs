using Tosk.Enums;

namespace Tosk.Services.Theming;

internal interface IThemingService : IDisposable
{
    Theme Theme { get; }
    Task InitializeAsync();
    Task ToggleAsync();
}
