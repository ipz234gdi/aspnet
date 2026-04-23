using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace CinemaBooking.Client.Services
{
    public class AuthDelegatingHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthDelegatingHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string?>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
