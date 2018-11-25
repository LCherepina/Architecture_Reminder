using Architecture_Reminder.ViewModels.Authentification;


namespace Architecture_Reminder.Views.Authentication
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView
    {
        public SignUpView()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            DataContext = signUpViewModel;
        }
    }
}