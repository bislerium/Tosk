using Tosk.Theming.Enums;

namespace Tosk.Theming.Services;

public interface IThemingService : IDisposable
{
    Theme Theme { get; }
    Task InitializeAsync();
    Task ToggleAsync();
}
