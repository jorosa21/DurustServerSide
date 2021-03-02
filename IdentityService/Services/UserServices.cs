using IdentityService.Entities;
using IdentityService.Helper;
using IdentityService.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Services
{


    public interface IRegistrationService
    {

        AuthenticateResponse Registration(Registration model);

        User GetById(int id);
    }


    public interface IUserService
    {

        AuthenticateResponse AuthenticateLogin(AuthenticateRequest model);

        User GetById(int id);
        Task Register(User user);
    }

    public class RegistrationService : IRegistrationService
    {

        public User resp = new User();

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;

        public RegistrationService(IOptions<AppSettings> appSettings, IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }



        public RegistrationResponse Register(Registration model)
        {
            //User resp = new User();
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
                oCmd.CommandText = "users_in";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@user_name", model.Username);
                oCmd.Parameters.AddWithValue("@user_hash", UserHash);
                oCmd.Parameters.AddWithValue("@email_address", model.email_address);
                oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.Id = Convert.ToInt32(sdr["user_id"].ToString());
                    //resp.email_address = sdr["email_address"].ToString();
                    //resp.type = sdr["type"].ToString();
                    //resp.active = Convert.ToBoolean(sdr["active"].ToString());
                    //resp.Username = sdr["user_name"].ToString();
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

            //// authentication successful so generate jwt token
            //var token = generateJwtToken(resp);

            return new RegistrationResponse(resp);
        }

        private string generateJwtToken(User user)
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


        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AuthenticateResponse Registration(Registration model)
        {
            throw new NotImplementedException();
        }
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        public User resp = new User();

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }


        public AuthenticateResponse AuthenticateLogin(AuthenticateRequest model)
        {
            //User resp = new User();
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
                oCmd.Parameters.AddWithValue("@user_name", model.Username);
                oCmd.Parameters.AddWithValue("@user_hash", UserHash);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.Id = Convert.ToInt32(sdr["user_id"].ToString());
                    resp.email_address = sdr["email_address"].ToString();
                    resp.type = sdr["type"].ToString();
                    resp.active = Convert.ToBoolean(sdr["active"].ToString());
                    resp.Username = sdr["user_name"].ToString();
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
            var token = "";
            //// return null if user not found
            if (resp.Id != 0)
            {

                token = generateJwtToken(resp);
            }
            // authentication successful so generate jwt token

            return new AuthenticateResponse(resp, token);
        }



        public User GetById(int id)
        {
            return resp;
        }

        // helper methods

        private string generateJwtToken(User user)
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

        public Task Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
