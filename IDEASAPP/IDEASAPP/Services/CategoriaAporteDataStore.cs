using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class CategoriaAporteDataStore : IDataStore<CategoriaAporte>
	{
		Task<bool> IDataStore<CategoriaAporte>.AddItemAsync(CategoriaAporte item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<CategoriaAporte>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<CategoriaAporte> IDataStore<CategoriaAporte>.GetItemAsync(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/CategoriaAporte/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<CategoriaAporte>(content);

			return resultado;
		}

		Task<IEnumerable<CategoriaAporte>> IDataStore<CategoriaAporte>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<CategoriaAporte>.UpdateItemAsync(CategoriaAporte item)
		{
			throw new NotImplementedException();
		}
	}
}
