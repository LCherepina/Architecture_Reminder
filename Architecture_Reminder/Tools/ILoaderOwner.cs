
using System.ComponentModel;
using System.Windows;


namespace Architecture_Reminder.Tools
{
    internal interface ILoaderOwner: INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
       
    }
}
