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
using MasterSettingService.Model.MenuViewModel;

namespace MasterSettingService.Services

{


    public interface IMasterSettingServices
    {

        List<DropdownResponse> Dropdown_List(string dropdowntype_id);

        List<DropdownResponse> Dropdown_entitlement(string dropdowntype_id);

        List<DropdownTypeResponse> Dropdowntype_view();


        DropdownIUResponse DropdownIU(DropdownIURequest model);

        MenuViewResponse Menu_view();
    }



    public class MasterSettingServices : IMasterSettingServices
    {
        //public DropdownSetting resp = new DropdownSetting();

        private connectionString connection { get; set; }
        private readonly AppSettings _appSettings;
        private string instance_name { get; set; }

        public MasterSettingServices(IOptions<AppSettings> appSettings,
            IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
        }

        public List<DropdownResponse> Dropdown_List(string dropdown_type_id)
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
                oCmd.Parameters.AddWithValue("@dropdown_type_id", dropdown_type_id);
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                       select new DropdownResponse()
                       {
                           id = Convert.ToInt32(dr["id"].ToString()),
                           description = dr["description"].ToString(),
                           type_id = Convert.ToInt32(dr["type_id"].ToString()),

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

        public List<DropdownResponse> Dropdown_entitlement(string dropdown_type_id)
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
                oCmd.CommandText = "dropdown_view_entitlement";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@dropdown_type_id", dropdown_type_id);
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                        select new DropdownResponse()
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            description = dr["description"].ToString(),
                            type_id = Convert.ToInt32(dr["type_id"].ToString()),

                            to_id = Convert.ToInt32(dr["to_id"].ToString()),
                            to_description = dr["to_description"].ToString(),
                            to_type_id = Convert.ToInt32(dr["to_type_id"].ToString()),

                            id_to = Convert.ToInt32(dr["id_to"].ToString()),
                            description_to = dr["description_to"].ToString(),
                            type_id_to = Convert.ToInt32(dr["type_id_to"].ToString()),

                            to_id_to = Convert.ToInt32(dr["to_id_to"].ToString()),
                            to_description_to = dr["to_description_to"].ToString(),
                            to_type_id_to = Convert.ToInt32(dr["to_type_id_to"].ToString()),

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


        public MenuViewResponse Menu_view()
        {
            //DropdownResponse resp = new DropdownResponse();

            MenuViewResponse resp = new MenuViewResponse();
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
                oCmd.CommandText = "dynamic_menu_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.menu_view = sdr["menu_view"].ToString();

                }
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
