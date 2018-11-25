using Architecture_Reminder.ViewModels.Authentification;
using System;


namespace Architecture_Reminder.Views.Authentication
{
    /// <summary>
    /// Interaction logic for SighnInView.xaml
    /// </summary>
    internal partial class SignInView
    {
        internal SignInView()
        {
            InitializeComponent();
            var signInViewModel = new SignInViewModel();
            DataContext = signInViewModel;
        }
    }
}
