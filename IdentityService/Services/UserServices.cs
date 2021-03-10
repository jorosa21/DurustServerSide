using IdentityService.Entities;
using IdentityService.Helper;
using IdentityService.Model;
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

namespace IdentityService.Services
{


    public interface IUserService
    {

        RegistrationResponse Create(Registration model);
        VerificationResponse Verification(VerificationRequest model);
    }


    public class UserService : IUserService
    {
        public User resp = new User();

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;
        private readonly IDataProtector _protector;


        public UserService(IOptions<AppSettings> appSettings, IOptions<connectionString> settings, IDataProtectionProvider provider)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
            _protector = provider.CreateProtector("mysecretkey");
        }

        public RegistrationResponse Create(Registration model)
        {
            // validation
            //if (string.IsNullOrWhiteSpace(model.Password))
            //    throw new AppException("Password is required");

            User resp = new User();
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
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.id = _protector.Protect(sdr["user_id"].ToString());
                    resp.email_address = sdr["email_address"].ToString();
                }
                sdr.Close();
                oTrans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                oConn.Close();
            }

            return new RegistrationResponse(resp);
        }


        public VerificationResponse Verification(VerificationRequest model)
        {
          

            User resp = new User();
            string _con = connection._DB_Master;
            DataTable dt = new DataTable();
            SqlConnection oConn = new SqlConnection(_con);
            SqlTransaction oTrans;
            oConn.Open();
            oTrans = oConn.BeginTransaction();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.Transaction = oTrans;
            try
            {
                oCmd.CommandText = "users_verification";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@id", _protector.Unprotect(model.id));

                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.id = _protector.Protect(sdr["id"].ToString());
                }
                sdr.Close();
                oTrans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                oConn.Close();
            }

            return new VerificationResponse(resp);
        }


    }
}
