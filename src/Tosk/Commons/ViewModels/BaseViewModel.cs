using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tosk.Commons.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies listeners of a property change.
        /// Optionally evaluates a predicate to determine whether the event should be raised.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Defaults to the caller member name.</param>
        /// <param name="predicate">An optional asynchronous predicate to determine whether to notify.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected async Task NotifyPropertyChangedAsync(
            [CallerMemberName] string? propertyName = null,
            Func<Task<bool>>? predicate = null)
        {
            if (predicate == null || await predicate())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}