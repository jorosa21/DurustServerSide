
using AuthService.Helper;
using AuthService.Model;
using Microsoft.AspNetCore.DataProtection;
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

using AuthService;
using System.Security.Cryptography;

namespace AuthService.Services
{

    public interface IAuthService
    {

        AuthenticateResponse AuthenticateLogin(AuthenticateRequest model);
        AuthenticateResponse GetById( int userId);
    }

    public class AuthServices :  IAuthService
    {

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;

        public AuthServices(IOptions<AppSettings> appSettings, IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }



        public AuthenticateResponse AuthenticateLogin(AuthenticateRequest model)
        {
            AuthenticateResponse resp = new AuthenticateResponse();
           

            string _con = connection._DB_Master;
            DataTable dt = new DataTable();
            string UserHash = Crypto.password_encrypt(model.password);
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
                oCmd.Parameters.AddWithValue("@user_name", model.username);
                oCmd.Parameters.AddWithValue("@user_hash", UserHash);
                oCmd.Parameters.AddWithValue("@company_code", model.company_code);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {

                    resp.id = sdr["user_id"].ToString() == "0"? "0" : Crypto.url_encrypt(sdr["user_id"].ToString());
                    resp.email_address = sdr["email_address"].ToString();
                    resp.routing = sdr["routing"].ToString();
                    resp.type = sdr["type"].ToString();
                    resp.active = Convert.ToBoolean(sdr["active"].ToString());
                    resp.lock_account = Convert.ToBoolean(sdr["lock_account"].ToString());
                    resp.email_verified = Convert.ToBoolean(sdr["email_verified"].ToString());
                    resp.company_id = Crypto.url_encrypt(sdr["company_id"].ToString());
                    resp.company_code = sdr["company_code"].ToString();
                    resp.instance_name = Crypto.url_encrypt(sdr["instance_name"].ToString());
                    resp.company_user_name = Crypto.url_encrypt(sdr["company_user_name"].ToString());
                    resp.company_user_hash = Crypto.url_encrypt(sdr["company_user_hash"].ToString());
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

            if (resp.id != "")
            {

                resp.Token = generateJwtToken(resp);
            }


            return resp;
        }


        private string generateJwtToken(AuthenticateResponse user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthenticateResponse GetById(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
