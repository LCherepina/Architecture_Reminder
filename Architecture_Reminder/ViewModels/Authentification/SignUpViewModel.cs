using Architecture_Reminder.Managers;
using Architecture_Reminder.Tools;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Architecture_Reminder.DBModels;

namespace Architecture_Reminder.ViewModels.Authentification
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        private string _firstName;
        private string _lastName;
        private string _email;
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
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
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
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute));
            }
        }
        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute, SignUpCanExecute));
            }
        }
        #endregion

        #region ConstructorAndInit
        internal SignUpViewModel()
        {
        }
        #endregion

        private async void SignUpExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(_email);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to validate data. " + e.Message);
                    Logger.Log("SignUp Email is not valid");
                    return 1;
                }

                try
                {
                    User user = new User(_login, _password, _firstName, _lastName, _email);
                    if (DBManager.UserExists(user.Login))
                        throw new Exception("User with login " + user.Login + " exists!");
                    DBManager.AddUser(user);
                    StationManager.CurrentUser = user;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to create user." + e.Message);
                    Logger.Log($"SignUp User exist", e);
                    return 2;
                }
                MessageBox.Show("User with login " + _login + " is successfuly created!");
                Logger.Log("SignUp New user created");
                SerializationManager.Serialize(StationManager.CurrentUser, FileFolderHelper.LastUserFilePath);
                return 0;
            });
            LoaderManager.Instance.HideLoader();
            if (result == 1)
            {
                _email = "";
                OnPropertyChanged("Email");
            }else if (result == 2)
                CleanAllFields();
            if (result == 0) {
                CleanAllFields();
                NavigationManager.Instance.Navigate(ModesEnum.Main);
            }
        }

        private void CleanAllFields()
        {
            _login = "";
            _password = "";
            _firstName = "";
            _lastName = "";
            _email = "";
            OnPropertyChanged("Login");
            OnPropertyChanged("Password");
            OnPropertyChanged("FirstName");
            OnPropertyChanged("LastName");
            OnPropertyChanged("Email");
        }

        private void CloseExecute(object obj)
        {
            StationManager.CloseApp();
        }

        private bool SignUpCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_login) &&
                !String.IsNullOrEmpty(_password) &&
                !String.IsNullOrEmpty(_firstName) &&
                !String.IsNullOrEmpty(_lastName) &&
                !String.IsNullOrEmpty(_email);
        }

        private void SignInExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
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