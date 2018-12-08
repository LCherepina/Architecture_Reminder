using Architecture_Reminder.DBModels;
using Architecture_Reminder.Managers;
using Architecture_Reminder.Tools;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Architecture_Reminder.ViewModels.Authentification
{
    class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        #endregion

        #region Commands
        private ICommand _closeCommand;
        private ICommand _signInCommand;
        private ICommand _signUpCommand;
        #endregion

        #region Properties
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        #endregion

        #region Commands
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute));
            }
        }
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute, SignInCanExecute));
            }
        }
        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute));
            }
        }


        #endregion

        #region ConstructorAndInit
        internal SignInViewModel()
        {
        }
        #endregion

        private void CloseExecute(object obj)
        {
            StationManager.CloseApp();
        }
        private bool SignInCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }

        private async void SignInExecute(object obj)

        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(100);
                User currentUser;

                try
                {
                    currentUser = DBManager.GetUserByLogin(_login);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to get user with login +" + _login + ".\n" + e.Message);
                    Logger.Log($"SignIn Failed to get user ", e);
                    return false;
                }

                if (currentUser == null)
                {
                    MessageBox.Show("User with login " + _login + " doesn't exist!");
                    Logger.Log("SignIn User doesn't exist");
                    return false;
                }

                try
                {
                    if (!currentUser.CheckPassword(_password))
                    {
                        MessageBox.Show("Wrong password!");
                        Logger.Log("Wrong password");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to validate password!\n" + e.Message);
                    Logger.Log($"SignIn Failed to validate password", e);
                    return false;
                }

                StationManager.CurrentUser = currentUser;
                SerializationManager.Serialize(StationManager.CurrentUser, FileFolderHelper.LastUserFilePath);
                return true;
            });
            _login = "";
            _password = "";
            OnPropertyChanged("Login");
            OnPropertyChanged("Password");
            LoaderManager.Instance.HideLoader();
            if (result) 
                NavigationManager.Instance.Navigate(ModesEnum.Main);
        }
        private void SignUpExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SignUp);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged(string propertyName)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
