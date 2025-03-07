using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Practica_NavegacionEntrePage.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OlvidoPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        //private void Ingresar_Clicked(object sender, EventArgs e)
        //{
        //    string email = EmailEntry.Text;
        //    string password = PasswordEntry.Text;

        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        //    {
        //        DisplayAlert("Error", "Por favor ingresa el correo y la contraseña.", "OK");
        //        return;
        //    }

        //    DisplayAlert("Bienvenido", "Inicio de sesión exitoso.", "OK");
        //}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
