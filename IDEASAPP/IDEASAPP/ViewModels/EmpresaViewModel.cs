using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace IDEASAPP.ViewModels
{
	[QueryProperty(nameof(EmpresaId), nameof(EmpresaId))]
	class EmpresaViewModel : BaseViewModel
	{
		public ObservableCollection<Aporte> AportesEmpresa { get; }
		private int empresaId;
		private string nombre;
		private string direccion;
		private string descripcion;
		private int numeroAportes;
		private ImageSource empresaFoto;
		public Command LoadAportesCommand { get; }
		public Command VerMasCommand { get; }
		public Command NuevoCommand { get; }
		public EmpresaViewModel()
		{
			VerMasCommand = new Command(OnVerMasTapped);
			NuevoCommand = new Command(OnNuevoTapped);
			AportesEmpresa = new ObservableCollection<Aporte>();
			LoadAportesCommand = new Command(async () => await LoadAportes());

		}

		public string Nombre
		{
			get => nombre;
			set => SetProperty(ref nombre, value);
		}
		public int NumeroAportes
		{
			get => numeroAportes;
			set => SetProperty(ref numeroAportes, value);
		}

		public string Direccion
		{
			get => direccion;
			set => SetProperty(ref direccion, value);
		}
		public string Descripcion
		{
			get => descripcion;
			set => SetProperty(ref descripcion, value);
		}
		public ImageSource EmpresaFoto
		{
			get => empresaFoto;
			set => SetProperty(ref empresaFoto, value);
		}


		public int EmpresaId
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

		async Task LoadAportes()
		{
			AportesEmpresa.Clear();

			ObservableCollection<Aporte> aux = new ObservableCollection<Aporte>();

			var resultado = await AporteDataStore.GetItemsAsync(); 

			foreach (var item in resultado.OrderByDescending(x => x.FechaAporte))
			{
				if (item.CNegocio == Convert.ToInt32(EmpresaId))
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
					aux.Add(item);
				}
			}
		
			foreach (var item in aux.Take(2)) {
				AportesEmpresa.Add(item);
			}
	
		}

		async Task<AportesPersona> GetAportesPersona(int idAporte)
		{

			var resultado =  await AportesPersonaDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CAporte == idAporte)
				{
					return item;
				}
			}

			return null;

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

		public async void LoadEmpresaId(int empresaId)
		{
			var negocio = await NegocioMiembroDataStore.GetItemAsync(empresaId);

			Nombre = negocio.DNombreComercial;
			Direccion = negocio.DDireccion;
			Descripcion = negocio.DDescripcion;
			EmpresaFoto = ImageSource.FromStream(() => new MemoryStream(negocio.DFoto));
			NumeroAportes = await GetNumeroAportes(negocio.Id);

		}


		private async void OnVerMasTapped(object obj)
		{

			await Shell.Current.GoToAsync($"///about/empresa/comentarios?{nameof(ComentariosViewModel.EmpresaId)}={EmpresaId}");

		}		
		private async void OnNuevoTapped(object obj)
		{

			await Shell.Current.GoToAsync($"///about/empresa/nuevo?{nameof(NuevoComentarioViewModel.EmpresaId)}={EmpresaId}");

		}
	}
}
