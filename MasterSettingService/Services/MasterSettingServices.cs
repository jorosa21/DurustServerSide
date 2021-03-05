using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterSettingService.Entities;
using MasterSettingService.Services;
using MasterSettingService.Helper;
using Microsoft.Extensions.Options;
using MasterSettingService.Model;
using MasterSettingService.Model.DropdownModel;
using System.Data;
using System.Data.SqlClient;
namespace MasterSettingService.Services
{


    public interface IMasterSettingServices
    {

        List<DropdownResponse> Dropdown_List(DropdownRequest model);

    }



    public class MasterSettingServices : IMasterSettingServices
    {
        //public DropdownSetting resp = new DropdownSetting();

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;

        public MasterSettingServices(IOptions<AppSettings> appSettings,
            IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }

        public List<DropdownResponse> Dropdown_List(DropdownRequest model)
        {
            //DropdownResponse resp = new DropdownResponse();

            List<DropdownResponse> resp = new List<DropdownResponse>();
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

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = oCmd;
                oCmd.CommandText = "dropdown_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@dropdown_type_id", model.dropdown_type_id);
                oCmd.Parameters.AddWithValue("@dropdown_type", model.dropdown_type);
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                       select new DropdownResponse()
                       {
                           id = Convert.ToInt32(dr["id"].ToString()),
                           description = dr["description"].ToString(),
                        
                       }).ToList();
                //while (sdr.Read())
                //{
                //    resp.id = Convert.ToInt32(sdr["id"].ToString());
                //    resp.description = sdr["description"].ToString();

                //}
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


            return resp;
        }

       
    }
}
