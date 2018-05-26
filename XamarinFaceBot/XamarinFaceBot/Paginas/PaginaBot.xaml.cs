using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFaceBot.Helpers;
using XamarinFaceBot.Modelos;
using XamarinFaceBot.Servicios;

namespace XamarinFaceBot.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaBot : ContentPage
	{
        BotConnection bot;
        ObservableCollection<Mensaje> listaMensajes;

        public PaginaBot ()
		{
			InitializeComponent ();

            bot = new BotConnection(Constantes.BotUser);
            listaMensajes = new ObservableCollection<Mensaje>();

            lsvMensajes.ItemsSource = listaMensajes;
            bot.ObtenerMensajes(listaMensajes);
        }

        public void btnEnviar_Clicked(object sender, EventArgs e)
        {
            var texto = txtMensaje.Text;

            if (texto.Length > 0)
            {
                txtMensaje.Text = "";

                var mensaje = new Mensaje(texto, bot.Cuenta.Name);
                listaMensajes.Add(mensaje);
                bot.EnviarMensaje(texto);
            }
        }
    }
}