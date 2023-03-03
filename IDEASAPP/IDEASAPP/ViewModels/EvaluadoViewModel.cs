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
		public Command LoadEnviadosCommand { get; }
		public Command LoadEnEsperaCommand { get; }
		public Command LoadRespondidosCommand { get; }

		public EvaluadoViewModel()
		{
			Aportes = new ObservableCollection<Aporte>();
			LoadEnviadosCommand = new Command(async () => await LoadEnviado());
			LoadEnEsperaCommand = new Command(async () => await LoadEnEspera());
			LoadRespondidosCommand = new Command(async () => await LoadRespondidos());
		}


		 async Task LoadEnviado()
		{
			Aportes.Clear();

			var aportesPersona = await GetAportesPersona(Convert.ToInt32(Application.Current.Properties["idUsuario"]));

			foreach (Aporte aporte in aportesPersona.OrderByDescending(x => x.FechaAporte))
			{
				if (aporte.CEstado == 1)
				{
					Estado estado = await EstadoDataStore.GetItemAsync(aporte.CEstado);
					Calificacion calificacion = await CalificacionDataStore.GetItemAsync(aporte.CCalificacion);
					CategoriaAporte categoriaAporte = await CategoriaAporteDataStore.GetItemAsync(aporte.CCategoriaAporte);

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

		}
		 async Task LoadEnEspera()
		{
			Aportes.Clear();

			var aportesPersona = await GetAportesPersona(Convert.ToInt32(Application.Current.Properties["idUsuario"]));

			foreach (Aporte aporte in aportesPersona.OrderByDescending(x => x.FechaAporte))
			{
				if (aporte.CEstado >= 2 && aporte.CEstado <= 3)
				{
					Estado estado = await EstadoDataStore.GetItemAsync(aporte.CEstado);
					Calificacion calificacion = await CalificacionDataStore.GetItemAsync(aporte.CCalificacion);
					CategoriaAporte categoriaAporte = await CategoriaAporteDataStore.GetItemAsync(aporte.CCategoriaAporte);

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

		}

		 async Task LoadRespondidos()
		{
			Aportes.Clear();

			var aportesPersona = await GetAportesPersona(Convert.ToInt32(Application.Current.Properties["idUsuario"]));

			foreach (Aporte aporte in aportesPersona.OrderByDescending(x => x.FechaAporte))
			{
				if (aporte.CEstado != 5 && aporte.CEstado >= 4 && aporte.CEstado <= 9)
				{
					Estado estado = await EstadoDataStore.GetItemAsync(aporte.CEstado);
					Calificacion calificacion = await CalificacionDataStore.GetItemAsync(aporte.CCalificacion);
					CategoriaAporte categoriaAporte = await CategoriaAporteDataStore.GetItemAsync(aporte.CCategoriaAporte);
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

		}
		async Task<ObservableCollection<Aporte>> GetAportesPersona(int idPersona)
		{
			ObservableCollection<Aporte> aportes = new ObservableCollection<Aporte>();
			var resultado = await AportesPersonaDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CPersona == idPersona)
				{
					Aporte aporte = await AporteDataStore.GetItemAsync(item.CAporte);
					aportes.Add(aporte);
				}
			}
			return aportes;
		}

	}
}
