using System.Net;
using System.Text;
using System.Text.Json;

namespace Backend.Tests.MockHelpers
{
	public class MockHttpMessageHandler : HttpMessageHandler
	{
		private readonly Queue<(HttpStatusCode statusCode, byte[]? content)> _responses;

		public MockHttpMessageHandler(HttpStatusCode statusCode, byte[]? content)
		{
			_responses = new();
			_responses.Enqueue((statusCode, content));
		}

		public MockHttpMessageHandler(IEnumerable<(HttpStatusCode statusCode, byte[]? content)> responses)
		{
			_responses = new Queue<(HttpStatusCode, byte[]?)>(responses);
		}

		public static HttpClient CreateMockHttpClient(HttpStatusCode statusCode, byte[]? content)
		{
			var handler = new MockHttpMessageHandler(statusCode, content);
			return new HttpClient(handler);
		}

		public static HttpClient CreateMockHttpClient(IEnumerable<(HttpStatusCode statusCode, byte[]? content)> responses)
		{
			var handler = new MockHttpMessageHandler(responses);
			return new HttpClient(handler);
		}

		public static HttpClient CreateMockHttpClientWithJson<T>(HttpStatusCode statusCode, T? jsonObject)
		{
			byte[]? content = null;
			if (jsonObject != null)
			{
				var json = JsonSerializer.Serialize(jsonObject);
				content = Encoding.UTF8.GetBytes(json);
			}

			var handler = new MockHttpMessageHandler(statusCode, content);
			return new HttpClient(handler);
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken _)
		{
			if (_responses.Count == 0)
			{
				throw new InvalidOperationException("No responses available in the mock HTTP handler.");
			}

			var (statusCode, content) = _responses.Dequeue();
			var httpResponse = new HttpResponseMessage(statusCode);
			
			if (content != null)
			{
				httpResponse.Content = new ByteArrayContent(content);
			}

			return Task.FromResult(httpResponse);
		}
	}
}
