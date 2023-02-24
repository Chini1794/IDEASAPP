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
		async Task LoadAportes()
		{

			ObservableCollection<Aporte> aux = new ObservableCollection<Aporte>();
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			AportesEmpresa.Clear();

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Aporte>>(content).OrderByDescending(x => x.FechaAporte);

			foreach (var item in resultado)
			{
				if (item.CNegocio == Convert.ToInt64(EmpresaId))
				{
					AportesPersona aportePersona = await GetAportePersona(item.Id);
					if (aportePersona == null)
					{

						item.NombrePersona = "Anonimo";
						item.SourceFoto = "../i_cuenta.png";
					}
					else
					{
						PersonaMiembro persona = await GetPersonaMiembro(aportePersona.CPersona);
						item.NombrePersona = persona.DNombre;
						item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(persona.DFoto));

					}
					aux.Add(item);
				}
			}

			foreach (var item in aux)
			{
				AportesEmpresa.Add(item);
			}

		}
		async Task<AportesPersona> GetAportePersona(int idAporte)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesPersona");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			AportesEmpresa.Clear();

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<AportesPersona>>(content);

			foreach (var item in resultado)
			{
				if (item.CAporte == idAporte)
				{
					return item;
				}
			}

			return null;

		}
		async Task<PersonaMiembro> GetPersonaMiembro(int idPersona)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/PersonaMiembro/" + idPersona);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			AportesEmpresa.Clear();

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<PersonaMiembro>(content);

			return resultado;

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

		public async void LoadEmpresaId(string id)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<NegocioMiembro>(content);
		}

	}
}