using Architecture_Reminder.Views.Authentication;
using Architecture_Reminder.Views;
using System;

namespace Architecture_Reminder.Tools
{
    internal enum ModesEnum
    {
        SignIn,
        SignUp,
        Main
    }

    internal class NavigationModel
    {
        private readonly IContentWindow _contentWindow;
        private SignInView _signInView;
        private SignUpView _signUpView;
        private MainView _mainView;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch(mode)
            {
                case ModesEnum.SignIn:
                    _contentWindow.ContentControl.Content = _signInView ?? (_signInView = new SignInView());
                    Logger.Log("Navigate to SignIn");
                    break;
                case ModesEnum.SignUp:
                    _contentWindow.ContentControl.Content = _signUpView ?? (_signUpView = new SignUpView());
                    Logger.Log("Navigate to SignUp");
                    break;
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = (_mainView = new MainView());
                    Logger.Log("Navigate to Main");
                    //_contentWindow.ContentControl.Content = _mainView ?? (_mainView = new MainView());
                    break;
                default:
                    Logger.Log("Fail to navigate: ArgumentOutOfRangeException");
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
