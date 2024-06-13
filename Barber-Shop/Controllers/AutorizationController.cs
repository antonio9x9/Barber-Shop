using Barber_Shop.Autentication;
using Barber_Shop.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barber_Shop.Controllers
{
    [ApiController]
    [Route("api/Controller")]
    public class AutorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("newuser")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO UserDTO)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = UserDTO.Username,
                PasswordHash = UserDTO.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, UserDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _signInManager.SignInAsync(user, false);

            return Ok(GenerateToken(UserDTO));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var resul = await _signInManager.PasswordSignInAsync(
                                usuarioDTO.Username,
                                usuarioDTO.Password,
                                isPersistent: false,
                                lockoutOnFailure: false
                            );
            if (resul.Succeeded)
            {
                return Ok(GenerateToken(usuarioDTO));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "E-mail ou Senha inválidos");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(UserDTO UserDTO)
        {
            //Define declarações do usuário
            Claim[] claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.UniqueName, UserDTO.Username),
                    new Claim("BarberShop", "UserBarberShop"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            //gera uma chave com base em um algoritmo simetrico
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            SigningCredentials credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiracão do token.
            string? expiracao = _configuration["TokenConfiguration:ExpireHours"];
            DateTime expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            //retorna os dados com o token e informacoes
            return new UserToken()
            {
                IsAutenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }
    }
}
