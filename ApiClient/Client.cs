using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApiClient
{	
	public class Result<TResult, TError> {
		
	}

	public class Client {
		public async Task<Result<TResult, TError>> SendRequestAsync<TResult, TError, TApi>(TApi api) where TApi : ApiBase<TResult, TError>
		{
			var httpClient = new HttpClient ();
			try {
				var response = await httpClient.SendAsync (api.GetRequest ());

				response.EnsureSuccessStatusCode();
				var result = api.HandleResponse(response);
				return result;
					
			} catch (HttpRequestException e) {
				Debug.WriteLine(e.Message);
			} catch (TaskCanceledException e) {
				Debug.WriteLine(e.Message);
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
			}
			return null;
		}
	}
}

