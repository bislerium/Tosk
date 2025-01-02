using Microsoft.Extensions.Logging;
using Tosk.SQLite;
using Tosk.TodoTask.Repositories.InMemory;
using Tosk.TodoTask.Repositories.SQLite;
using Tosk.TodoTask.Services;
using Tosk.TodoTask.ViewModel;

namespace Tosk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSQLiteDbContext();

            builder.Services
                .AddInMemoryTodoRepository()
                .AddSqliteTodoRepository()
                .AddSingleton<ITodoService, TodoService>()
                .AddTodoVM();

            return builder.Build();
        }
    }
}
