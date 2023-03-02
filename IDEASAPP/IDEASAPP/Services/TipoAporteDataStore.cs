using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class TipoAporteDataStore : IDataStore<TipoAporte>
	{
		Task<bool> IDataStore<TipoAporte>.AddItemAsync(TipoAporte item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<TipoAporte>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<TipoAporte> IDataStore<TipoAporte>.GetItemAsync(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/TipoAporte/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<TipoAporte>(content);

			return resultado;
		}

		Task<IEnumerable<TipoAporte>> IDataStore<TipoAporte>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<TipoAporte>.UpdateItemAsync(TipoAporte item)
		{
			throw new NotImplementedException();
		}
	}
}
