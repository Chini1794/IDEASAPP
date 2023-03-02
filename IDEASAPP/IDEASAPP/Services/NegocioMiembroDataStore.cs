using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class NegocioMiembroDataStore : IDataStore<NegocioMiembro>
	{
		Task<bool> IDataStore<NegocioMiembro>.AddItemAsync(NegocioMiembro item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<NegocioMiembro>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<NegocioMiembro> IDataStore<NegocioMiembro>.GetItemAsync(int id)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<NegocioMiembro>(content);
			return await Task.FromResult(resultado);
		}

		 async Task<IEnumerable<NegocioMiembro>> IDataStore<NegocioMiembro>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro");
			request.Method = HttpMethod.Get;
			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<IEnumerable<NegocioMiembro>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<NegocioMiembro>.UpdateItemAsync(NegocioMiembro item)
		{
			throw new NotImplementedException();
		}
	}
}
