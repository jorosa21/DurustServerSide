using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterSettingService.Entities;
using MasterSettingService.Services;
using MasterSettingService.Helper;
using Microsoft.Extensions.Options;
using MasterSettingService.Model.CompanyModel;
using MasterSettingService.Model.DropdownModel;
using System.Data;
using System.Data.SqlClient;

namespace MasterSettingService.Services

{


    public interface IMasterSettingServices
    {

        List<DropdownResponse> Dropdown_List(DropdownRequest model);


        List<DropdownTypeResponse> Dropdowntype_view();

        CompanyIUResponse CompanyIU(CompanyIURequest model);

        DropdownIUResponse DropdownIU(DropdownIURequest model);
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

        public List<DropdownTypeResponse> Dropdowntype_view()
        {
            //DropdownResponse resp = new DropdownResponse();

            List<DropdownTypeResponse> resp = new List<DropdownTypeResponse>();
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
                oCmd.CommandText = "dropdown_type_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                        select new DropdownTypeResponse()
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


        public CompanyIUResponse CompanyIU(CompanyIURequest model)
        {

            CompanyIUResponse resp = new CompanyIUResponse();
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
                oCmd.CommandText = "company_in_up";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@company_id", model.company_id);
                oCmd.Parameters.AddWithValue("@company_code", model.company_code);
                oCmd.Parameters.AddWithValue("@company_name", model.company_name);
                oCmd.Parameters.AddWithValue("@unit_floor", model.unit_floor);
                oCmd.Parameters.AddWithValue("@building", model.building);
                oCmd.Parameters.AddWithValue("@street", model.street);
                oCmd.Parameters.AddWithValue("@barangay", model.barangay);
                oCmd.Parameters.AddWithValue("@municipality", model.municipality);
                oCmd.Parameters.AddWithValue("@city", model.city);
                oCmd.Parameters.AddWithValue("@region", model.region);
                oCmd.Parameters.AddWithValue("@country", model.country);
                oCmd.Parameters.AddWithValue("@zip_code", model.zip_code);
                oCmd.Parameters.AddWithValue("@company_logo", model.company_logo);
                oCmd.Parameters.AddWithValue("@created_by", model.created_by);
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.company_id = Convert.ToInt32(sdr["company_id"].ToString());
                    resp.guid = sdr["guid"].ToString();
                    resp.created_by = Convert.ToInt32(sdr["created_by"].ToString());
                    resp.company_code = sdr["company_code"].ToString();
                    resp.instance_name = sdr["instance_name"].ToString();
                    resp.user_hash = sdr["user_hash"].ToString();
                    resp.user_name = sdr["user_name"].ToString();
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

            return resp;
        }



        public DropdownIUResponse DropdownIU(DropdownIURequest model)
        {

            DropdownIUResponse resp = new DropdownIUResponse();
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
                oCmd.CommandText = "dropdown_in_up";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@dropdown_id", model.dropdown_id);
                oCmd.Parameters.AddWithValue("@dropdown_type_id", model.dropdown_type_id);
                oCmd.Parameters.AddWithValue("@dropdown_description", model.dropdown_description);
                oCmd.Parameters.AddWithValue("@created_by", model.created_by);
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.dropdown_id = Convert.ToInt32(sdr["dropdown_id"].ToString());
                    resp.dropdown_type_id = Convert.ToInt32(sdr["dropdown_type_id"].ToString());
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

            return resp;
        }

    }


}
