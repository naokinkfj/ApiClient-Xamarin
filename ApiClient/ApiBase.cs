using System;
using System.Net.Http;

namespace ApiClient
{
	public abstract class ApiBase<TResult, TError>
	{
		public ApiBase ()
		{
		}

		protected abstract HttpMethod GetHttpMethod ();
		protected abstract String GetRelativePath();

		protected HttpContent GetContent() {
			return null;
		}
			

		public HttpRequestMessage GetRequest() {
			var relativeUri = new Uri (GetRelativePath (), UriKind.Relative);
			var request = new HttpRequestMessage (GetHttpMethod(), relativeUri);
			request.Content = GetContent ();
			return request;
		}

		public abstract Result<TResult, TError> HandleResponse (HttpResponseMessage respone);

		public static class QiitaApi {
			public const string BaseUrl = "http://qiita.com";
			private const string BasePath = "/api/v3";

			public static class User
			{
				private const string Path = BasePath + "/user";

				public class Get: ApiBase<string, string> {
					protected override string GetRelativePath ()
					{
						return Path;
					}
				}
			}
		}
	}
}

