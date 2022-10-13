using APIEKS.Models;
using APIEKS.Models.Common;
using APIEKS.Models.Request;
using APIEKS.Models.Response;
using APIEKS.Tools;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APIEKS.Service
{
    public class UserService : IUserService
    {

        private readonly AppSetings _appSetings;

        public UserService(IOptions<AppSetings> appsettings)
        {
            _appSetings=appsettings.Value;  
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse response = new UserResponse();
            using (var db = new EKSContext())
            {
                string scontrasena = Encrypt.GetSHA256(model.contra);

                var usuario = db.Usuarios.Where(d => d.Correo == model.correo &&
                d.Contra == scontrasena).FirstOrDefault();
                if (usuario == null) return null;

                response.Correo = usuario.Correo;
                response.Token = GetToken(usuario);
               



            }
            return response;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHanldler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSetings.Secreto);
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Correo)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHanldler.CreateToken(tokenDescripcion);
            return tokenHanldler.WriteToken(token);
        }


    }
}
