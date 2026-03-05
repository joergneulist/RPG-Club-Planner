using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Security.Claims;

using api.Models;


namespace api.Functions
{
    public class ProfileFunctions
    {
        [Function("GetMyProfile")]
        public async Task<HttpResponseData> GetMyProfile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            // 1. Get Google ID from SWA Header
            var principal = PrincipalParser.Parse(req);
            var googleId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(googleId))
                return req.CreateResponse(HttpStatusCode.Unauthorized);

            using var conn = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString"));

        // 2. Check if user exists, else create as 'Pending'
        var user = await conn.QueryFirstOrDefaultAsync<User>(
                @"IF NOT EXISTS (SELECT 1 FROM Users WHERE ExternalId = @googleId)
                  BEGIN
                      INSERT INTO Users (ExternalId, Provider, Email, RoleName)
                      VALUES (@googleId, 'google', @email, 'Pending')
                  END
                  SELECT * FROM Users WHERE ExternalId = @googleId", 
                new { googleId, email });

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(user);
            return response;
        }
    }
}

