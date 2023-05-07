using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dotnet_YouTubeAPI.Core
{
    /// <summary>
    /// A base class for objects that support property change notification through the INotifyPropertyChanged interface.
    /// </summary>
    class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="name">The name of the property that has changed. This parameter is optional and can be automatically populated using the CallerMemberName attribute.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
