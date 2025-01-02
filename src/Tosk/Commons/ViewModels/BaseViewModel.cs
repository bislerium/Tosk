using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tosk.Commons.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Subscribe(PropertyChangedEventHandler handler) => PropertyChanged += handler;
        public void Unsubscribe(PropertyChangedEventHandler handler) => PropertyChanged -= handler;
        protected async Task OnPropertyChanged([CallerMemberName] string? name = null)
        {
            await ReloadAsync();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        protected virtual Task ReloadAsync() => Task.CompletedTask;
    }
}