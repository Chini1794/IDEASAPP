using IDEASAPP.ViewModels;
using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IDEASAPP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NuevoComentarioPage), typeof(NuevoComentarioPage));
            Routing.RegisterRoute("CuentaPage", typeof(CuentaPage));
            Routing.RegisterRoute("BusquedaPage", typeof(BusquedaPage));
            Routing.RegisterRoute("EmpresaPage", typeof(EmpresaPage));
            Routing.RegisterRoute("PortalPage", typeof(PortalPage));
            Routing.RegisterRoute("ComentariosPage", typeof(ComentariosPage));
            Routing.RegisterRoute("NuevoComentarioPage", typeof(NuevoComentarioPage));
        }

    }
}
