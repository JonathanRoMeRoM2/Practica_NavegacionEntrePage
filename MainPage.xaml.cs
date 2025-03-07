using Microsoft.Maui.Controls;
using Practica_NavegacionEntrePage.Views;
namespace Practica_NavegacionEntrePage
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Registrate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void TieneUnaCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }

}
