using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;

namespace api.Models
{
    public static class PrincipalParser
    {
        private class ClientPrincipal
        {
            public string IdentityProvider { get; set; } = "";
            public string UserId { get; set; } = "";
            public string UserDetails { get; set; } = "";
            // Initialisiere mit einer leeren Sammlung, damit die Eigenschaft niemals null ist.
            public IEnumerable<string> UserRoles { get; set; } = Array.Empty<string>();
        }

        public static ClaimsPrincipal? Parse(HttpRequestData req)
        {
            var principal = new ClientPrincipal();

            if (req.Headers.TryGetValues("x-ms-client-principal", out var headers))
            {
                var data = headers.First();
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.UTF8.GetString(decoded);

                // Deserialisiere in eine temporäre Variable und ersetze nur, wenn non-null.
                var deserialized = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (deserialized != null)
                {
                    principal = deserialized;
                }
            }

            if (principal == null || string.IsNullOrEmpty(principal.UserId))
            {
                return null;
            }

            var identity = new ClaimsIdentity(principal.IdentityProvider);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));

            return new ClaimsPrincipal(identity);
        }
    }
}
