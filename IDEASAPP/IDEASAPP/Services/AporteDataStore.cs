using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class AporteDataStore : IDataStore<Aporte>
	{
		async Task<bool> IDataStore<Aporte>.AddItemAsync(Aporte item)
		{
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(item);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response.StatusCode == HttpStatusCode.OK;
		}

		Task<bool> IDataStore<Aporte>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<Aporte> IDataStore<Aporte>.GetItemAsync(int id)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<Aporte>(content);

			return await Task.FromResult(resultado);
		}

		 async Task<IEnumerable<Aporte>> IDataStore<Aporte>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Aporte>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<Aporte>.UpdateItemAsync(Aporte item)
		{
			throw new NotImplementedException();
		}


	}
}
