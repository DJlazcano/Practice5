using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.IdentityModel.Tokens;
using Practice5_API.Authority;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice5_API.Controllers
{
	[ApiController]
	public class AuthorityController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public AuthorityController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpPost("/auth")]
		public IActionResult Aunthenticate([FromBody] AppCredential credential)
		{

			if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
			{
				var expiresAt = DateTime.UtcNow.AddMinutes(10);
				return Ok(new
				{
					access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, _configuration.GetValue<String>("SecretKey")),
					expires_at = expiresAt
				});
			}
			else
			{
				return BadRequest();
			}
		}


	}
}
