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
	public class PersonaMiembroDataStore : IDataStore<PersonaMiembro>
	{
		Task<bool> IDataStore<PersonaMiembro>.AddItemAsync(PersonaMiembro item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<PersonaMiembro>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<PersonaMiembro> IDataStore<PersonaMiembro>.GetItemAsync(int id)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/PersonaMiembro/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<PersonaMiembro>(content);
			return await Task.FromResult(resultado);
		}

		async Task<IEnumerable<PersonaMiembro>> IDataStore<PersonaMiembro>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/PersonaMiembro");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<PersonaMiembro>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<PersonaMiembro>.UpdateItemAsync(PersonaMiembro item)
		{
			throw new NotImplementedException();
		}


	}
}
