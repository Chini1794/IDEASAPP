using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class AportesAnonimoDataStore : IDataStore<AportesAnonimo>
	{
		async Task<bool> IDataStore<AportesAnonimo>.AddItemAsync(AportesAnonimo item)
		{
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesAnonimo");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(item);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response.StatusCode == HttpStatusCode.OK;
		}

		Task<bool> IDataStore<AportesAnonimo>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<AportesAnonimo> IDataStore<AportesAnonimo>.GetItemAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<AportesAnonimo>> IDataStore<AportesAnonimo>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<AportesAnonimo>.UpdateItemAsync(AportesAnonimo item)
		{
			throw new NotImplementedException();
		}
	}
}
