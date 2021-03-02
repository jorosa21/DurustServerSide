using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IdentityServer.Entities;
using IdentityServer.Helpers;
using IdentityServer.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace IdentityServer.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        AuthenticateResponse AuthenticateLogin(AuthenticateRequest model);

        IEnumerable<Users> GetAll();
        Users GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Users> _users = new List<Users>
        {
            new Users { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var users = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (users == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(users);

            return new AuthenticateResponse(users, token);
        }

        public AuthenticateResponse AuthenticateLogin(AuthenticateRequest model)
        {
            Users resp = new Users();
            string _con = connection._DB_Master;
            DataTable dt = new DataTable();
            string UserHash = Crypto.password_encrypt(model.Password);
            SqlConnection oConn = new SqlConnection(_con);
            SqlTransaction oTrans;
            oConn.Open();
            oTrans = oConn.BeginTransaction();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.Transaction = oTrans;
            try
            {
                oCmd.CommandText = "login_authentication";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@username", model.Username);
                oCmd.Parameters.AddWithValue("@userhash", UserHash);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.Id = Convert.ToInt32(sdr["employee_id"].ToString());
                    resp.FirstName = sdr["first_name"].ToString();
                    resp.LastName = sdr["last_name"].ToString();
                    resp.Username = sdr["username"].ToString();
                }
                sdr.Close();
                oConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                oConn.Close();
            }

            // return null if user not found
            if (resp.Id == 0) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(resp);

            return new AuthenticateResponse(resp, token);
        }

        public IEnumerable<Users> GetAll()
        {
            return _users;
        }

        public Users GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        IEnumerable<Users> IUserService.GetAll()
        {
            throw new NotImplementedException();
        }

        Users IUserService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
