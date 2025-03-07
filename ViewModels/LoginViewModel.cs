using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Practica_NavegacionEntrePage.Services;
using System.Threading.Tasks;

namespace Practica_NavegacionEntrePage.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DatabaseService _databaseService;

        public LoginViewModel()
        {
            _databaseService = new DatabaseService();
            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand = new Command(async () => await GoToRegister());
            ForgotPasswordCommand = new Command(async () => await GoToForgotPassword());
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingresa el correo y la contraseña.", "OK");
                return;
            }

            var user = await _databaseService.GetUserByCredentials(Email, Password);
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Correo o contraseña incorrectos.", "OK");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Bienvenido", $"Hola, {user.Name}", "OK");
        }

        private async Task GoToRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.RegisterPage());
        }

        private async Task GoToForgotPassword()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.ForgotPasswordPage());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
