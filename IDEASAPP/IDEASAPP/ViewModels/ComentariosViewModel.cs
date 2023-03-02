using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace IDEASAPP.ViewModels
{
	[QueryProperty(nameof(EmpresaId), nameof(EmpresaId))]
	public class ComentariosViewModel : BaseViewModel
    {

        private string empresaId;

		public ObservableCollection<Aporte> AportesEmpresa { get; }
		public Command LoadAportesCommand { get; }

        public ComentariosViewModel()
        {
			AportesEmpresa = new ObservableCollection<Aporte>();
			LoadAportesCommand = new Command(async () => await LoadAportes());
		}

		private string numeroAportes;
		public string NumeroAportes
		{
			get => numeroAportes;
			set => SetProperty(ref numeroAportes, value);
		}		
		private string calificacionEmpresa;
		public string CalificacionEmpresa
		{
			get => calificacionEmpresa;
			set => SetProperty(ref calificacionEmpresa, value);
		}

		public string EmpresaId
		{
			get
			{
				return empresaId;
			}
			set
			{
				empresaId = value;
				LoadEmpresaId(value);
			}
		}

		private async Task<int> GetNumeroAportes(int empresa)
		{
			int contador = 0;

			var resultado = await AporteDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CNegocio == empresa)
				{
					contador++;
				}
			}
			return contador;
		}

		async Task LoadAportes()
		{
			AportesEmpresa.Clear();
			ObservableCollection<Aporte> aux = new ObservableCollection<Aporte>();

			var resultado = await AporteDataStore.GetItemsAsync();

			foreach (var item in resultado.OrderByDescending(x => x.FechaAporte))
			{
				if (item.CNegocio == Convert.ToInt64(EmpresaId))
				{
					AportesPersona aportePersona = await GetAportesPersona(item.Id);
					if (aportePersona == null)
					{

						item.NombrePersona = "Anonimo";
						item.SourceFoto = "../i_cuenta.png";
					}
					else
					{
						PersonaMiembro persona = await PersonaMiembroDataStore.GetItemAsync(aportePersona.CPersona);
						item.NombrePersona = persona.DNombre;
						item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(persona.DFoto));

					}
					AportesEmpresa.Add(item);
				}
			}


		}
		async Task<AportesPersona> GetAportesPersona(int idAporte)
		{
			var resultado = await AportesPersonaDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CAporte == idAporte)
				{
					return item;
				}
			}

			return null;
		}

		public async void LoadEmpresaId(string id)
		{
			int numero = await GetNumeroAportes(Convert.ToInt32(id));
			NumeroAportes = "aportes(" + numero + ")";
			double cali = await GetCalificacion(Convert.ToInt32(id));
			CalificacionEmpresa = Convert.ToString(Math.Round(cali, 1)*2);
		}

		async Task<double> GetCalificacion(int id)
		{
			double calificacion = 0;
			int cantidad = 0;

			var resultado = await AporteDataStore.GetItemsAsync();

			foreach (var item in resultado.OrderByDescending(x => x.FechaAporte))
			{
				if (item.CNegocio == id)
				{
					calificacion += item.CCalificacion;
					cantidad++;
				}
			}

			return calificacion / cantidad;

		}
	}
}