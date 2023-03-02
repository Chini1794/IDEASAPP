using IDEASAPP.Models;
using IDEASAPP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
		public IDataStore<NegocioMiembro> NegocioMiembroDataStore => DependencyService.Get<IDataStore<NegocioMiembro>>();
		public IDataStore<Aporte> AporteDataStore => DependencyService.Get<IDataStore<Aporte>>();
		public IDataStore<AportesPersona> AportesPersonaDataStore => DependencyService.Get<IDataStore<AportesPersona>>();
		public IDataStore<AportesAnonimo> AportesAnonimoDataStore => DependencyService.Get<IDataStore<AportesAnonimo>>();
		public IDataStore<PersonaMiembro> PersonaMiembroDataStore => DependencyService.Get<IDataStore<PersonaMiembro>>();
		public IDataStore<AnonimoMiembro> AnonimoMiembroDataStore => DependencyService.Get<IDataStore<AnonimoMiembro>>();
		public IDataStore<Calificacion> CalificacionDataStore => DependencyService.Get<IDataStore<Calificacion>>();
		public IDataStore<CategoriaAporte> CategoriaAporteDataStore => DependencyService.Get<IDataStore<CategoriaAporte>>();
		public IDataStore<Estado> EstadoDataStore => DependencyService.Get<IDataStore<Estado>>();
		public IDataStore<TipoAporte> TipoAporteDataStore => DependencyService.Get<IDataStore<TipoAporte>>();
		public IDataStore<Promocion> PromocionDataStore => DependencyService.Get<IDataStore<Promocion>>();
		public IDataStore<Login> LoginDataStore => DependencyService.Get<IDataStore<Login>>();


		bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
