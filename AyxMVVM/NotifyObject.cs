/*
Author:durow
Date:2015.10.11
implement INotifyPropertyChanged interface
if property changed,can use RaisePropertyChanged method to Notify property changed
*/

using System.ComponentModel;

namespace AyxMVVM
{
    public class ObserveObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise notify when property value changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// if property value changed set property value and raise notify
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="propertyName">property name</param>
        /// <param name="oldValue">property old value</param>
        /// <param name="newValue">property new value</param>
        protected virtual void SetAndNotifyIfChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue == null && newValue == null) return;
            if (oldValue != null && oldValue.Equals(newValue)) return;
            if (newValue != null && newValue.Equals(oldValue)) return;
            oldValue = newValue;
            RaisePropertyChanged(propertyName);
        }
    }
}
