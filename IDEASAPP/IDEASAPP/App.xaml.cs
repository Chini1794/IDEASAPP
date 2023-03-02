using IDEASAPP.Services;
using IDEASAPP.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

			DependencyService.Register<NegocioMiembroDataStore>();
			DependencyService.Register<AporteDataStore>();
			DependencyService.Register<PersonaMiembroDataStore>();
			DependencyService.Register<AnonimoMiembroDataStore>();
			DependencyService.Register<AportesPersonaDataStore>();
			DependencyService.Register<AportesAnonimoDataStore>();
			DependencyService.Register<PromocionDataStore>();
			DependencyService.Register<CalificacionDataStore>();
			DependencyService.Register<CategoriaAporteDataStore>();
			DependencyService.Register<EstadoDataStore>();
			DependencyService.Register<TipoAporteDataStore>();
			DependencyService.Register<LoginDataStore>();
			MainPage = new AppShell();

		}

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
