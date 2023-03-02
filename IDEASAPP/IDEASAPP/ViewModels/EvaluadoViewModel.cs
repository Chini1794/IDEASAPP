using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
	public class EvaluadoViewModel : BaseViewModel
	{
		public ObservableCollection<Aporte> Aportes { get; }
		public Command LoadAportesCommand { get; }

		public EvaluadoViewModel()
		{
			Aportes = new ObservableCollection<Aporte>();
			LoadAportesCommand = new Command(async () => await LoadAportes());
		}

		async Task LoadAportes()
		{
			Aportes.Clear();
			ObservableCollection<Aporte> aux = new ObservableCollection<Aporte>();

			var aportesPersona = await GetAportesPersona(Convert.ToInt32(Application.Current.Properties["idUsuario"]));

			foreach (AportesPersona aportePersona in aportesPersona)
			{
				Aporte aporte = await AporteDataStore.GetItemAsync(aportePersona.CAporte);
				Calificacion calificacion = await CalificacionDataStore.GetItemAsync(aporte.CCalificacion);
				CategoriaAporte categoriaAporte = await CategoriaAporteDataStore.GetItemAsync(aporte.CCategoriaAporte);
				Estado estado = await EstadoDataStore.GetItemAsync(aporte.CEstado);
				TipoAporte tipoAporte = await TipoAporteDataStore.GetItemAsync(aporte.CTipoAporte);
				NegocioMiembro negocio = await NegocioMiembroDataStore.GetItemAsync(aporte.CNegocio);
				aporte.SourceFoto = ImageSource.FromStream(() => new MemoryStream(negocio.DFoto));
				aporte.NombreEmpresa = negocio.DNombreComercial;
				aporte.VistaCalificacion = calificacion.DDescripcion;
				aporte.VistaCategoriaAporte = categoriaAporte.DCategoriaAporte;
				aporte.VistaEstado = estado.DEstado;
				aporte.VistaTipoAporte = tipoAporte.DTipoAporte;

				Aportes.Add(aporte);
			}
		}


		async Task<ObservableCollection<AportesPersona>> GetAportesPersona(int idPersona)
		{
			ObservableCollection<AportesPersona> aportesPersona = new ObservableCollection<AportesPersona>();
			var resultado = await AportesPersonaDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CPersona == idPersona)
				{
					aportesPersona.Add(item);
				}
			}

			return aportesPersona;

		}

	}
}
