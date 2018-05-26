using Xamarin.Forms;

namespace XamarinFaceBot
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new XamarinFaceBot.Paginas.PaginaBot();
		}
	}
}
