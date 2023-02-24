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
            Routing.RegisterRoute("portal/login", typeof(LoginPage));
            Routing.RegisterRoute("portal/registro", typeof(RegistroPage));

            Routing.RegisterRoute("about/busqueda", typeof(BusquedaPage));
            Routing.RegisterRoute("about/empresa", typeof(EmpresaPage));
            Routing.RegisterRoute("about/empresa/comentarios", typeof(ComentariosPage));
            Routing.RegisterRoute("about/empresa/nuevo", typeof(NuevoComentarioPage));
        }

    }
}
