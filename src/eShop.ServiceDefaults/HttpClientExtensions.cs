using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;

namespace eShop.ServiceDefaults;

public class TokenProvider
{
    public string? AccessToken { get; set; }
}

public static class HttpClientExtensions
{
    public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddScoped<TokenProvider>();
        builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();
        builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        return builder;
    }

    private class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenProvider _tokenProvider;

        public HttpClientAuthorizationDelegatingHandler(
            IHttpContextAccessor httpContextAccessor,
            TokenProvider tokenProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string? accessToken = null;

            if (_httpContextAccessor.HttpContext is HttpContext context)
            {
                accessToken = await context.GetTokenAsync("access_token");
            }

            accessToken ??= _tokenProvider?.AccessToken;

            if (accessToken is not null)
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
