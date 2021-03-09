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

        List<DropdownResponse> Dropdown_List(string dropdowntype_id, string dropdown_type);

        List<DropdownResponse> Dropdown_entitlement(string dropdowntype_id, string dropdown_type, string dropdowntype_id_to);

        List<DropdownTypeResponse> Dropdowntype_view();

        CompanyIUResponse CompanyIU(CompanyIURequest model);


        BranchIUResponse BranchIU(BranchIURequest model);
        BranchIUResponse MultipleBranchIU(BranchIURequest[] model);

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

        public List<DropdownResponse> Dropdown_List(string dropdown_type_id, string dropdown_type)
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
                oCmd.Parameters.AddWithValue("@dropdown_type", dropdown_type);
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

        public List<DropdownResponse> Dropdown_entitlement(string dropdown_type_id, string dropdown_type, string dropdowntype_id)
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
                oCmd.Parameters.AddWithValue("@dropdown_type", dropdown_type);
                oCmd.Parameters.AddWithValue("@dropdown_type_id_to", dropdowntype_id);
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

                            id_to = Convert.ToInt32(dr["id"].ToString()),
                            description_to = dr["description"].ToString(),
                            type_id_to = Convert.ToInt32(dr["type_id"].ToString()),

                            to_id_to = Convert.ToInt32(dr["to_id"].ToString()),
                            to_description_to = dr["to_description"].ToString(),
                            to_type_id_to = Convert.ToInt32(dr["to_type_id"].ToString()),

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
                oCmd.Parameters.AddWithValue("@company_id", model.companyID);
                oCmd.Parameters.AddWithValue("@company_code", model.companyCode);
                oCmd.Parameters.AddWithValue("@company_name", model.companyName);
                oCmd.Parameters.AddWithValue("@unit_floor", model.unit);
                oCmd.Parameters.AddWithValue("@building", model.building);
                oCmd.Parameters.AddWithValue("@street", model.street);
                oCmd.Parameters.AddWithValue("@barangay", model.barangay);
                oCmd.Parameters.AddWithValue("@municipality", model.municipality);
                oCmd.Parameters.AddWithValue("@city", model.city);
                oCmd.Parameters.AddWithValue("@region", model.region);
                oCmd.Parameters.AddWithValue("@country", model.selectedCompanyCountry);
                oCmd.Parameters.AddWithValue("@zip_code", model.zipCode);
                oCmd.Parameters.AddWithValue("@company_logo", model.img);
                oCmd.Parameters.AddWithValue("@created_by", model.createdBy);
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


        public BranchIUResponse BranchIU(BranchIURequest model)
        {
            BranchIUResponse resp = new BranchIUResponse();
            string _con;
                if (model.instance_name is null)
            {
                _con = "Data Source=" + connection.server + ";Initial Catalog=" + model.instance_name + ";User ID=" + model.username + ";Password=" + model.password + ";MultipleActiveResultSets=True;";
            }
            else
            {
                _con = connection._DB_Master;
            }
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
                oCmd.CommandText = "branch_in_up";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@branch_id", model.branch_id);
                oCmd.Parameters.AddWithValue("@branch_code", model.branch_code);
                oCmd.Parameters.AddWithValue("@branch_name", model.branch_name);
                oCmd.Parameters.AddWithValue("@unit_floor", model.unit_floor);
                oCmd.Parameters.AddWithValue("@building", model.building);
                oCmd.Parameters.AddWithValue("@street", model.street);
                oCmd.Parameters.AddWithValue("@barangay", model.barangay);
                oCmd.Parameters.AddWithValue("@municipality", model.municipality);
                oCmd.Parameters.AddWithValue("@city", model.city);
                oCmd.Parameters.AddWithValue("@region", model.region);
                oCmd.Parameters.AddWithValue("@country", model.country);
                oCmd.Parameters.AddWithValue("@zip_code", model.zip_code);
                oCmd.Parameters.AddWithValue("@email_address", model.email_address);
                oCmd.Parameters.AddWithValue("@sss", model.sss);
                oCmd.Parameters.AddWithValue("@philhealth", model.philhealth);
                oCmd.Parameters.AddWithValue("@tin", model.tin);
                oCmd.Parameters.AddWithValue("@rdo", model.rdo);
                oCmd.Parameters.AddWithValue("@pagibig", model.pagibig);
                oCmd.Parameters.AddWithValue("@pagibig_branch", model.pagibig_branch);
                oCmd.Parameters.AddWithValue("@ip_address", model.ip_address);
                oCmd.Parameters.AddWithValue("@industry", model.industry);
                oCmd.Parameters.AddWithValue("@bank_id", model.bank_id);
                oCmd.Parameters.AddWithValue("@bank_account", model.bank_account);
                oCmd.Parameters.AddWithValue("@company_id", model.company_id);
                oCmd.Parameters.AddWithValue("@created_by", model.created_by);
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.branch_id = Convert.ToInt32(sdr["branch_id"].ToString());
                    resp.guid = sdr["guid"].ToString();
                    resp.created_by = Convert.ToInt32(sdr["created_by"].ToString());
                  
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



        public BranchIUResponse MultipleBranchIU(BranchIURequest[] model)
        {
            string instance_name = model[0].instance_name;
            string username = model[0].username;
            string password = model[0].password;

            BranchIUResponse resp = new BranchIUResponse();
            string _con;
            if (instance_name is null)
            {
                _con = "Data Source=" + connection.server + ";Initial Catalog=" + instance_name + ";User ID=" + username + ";Password=" + password + ";MultipleActiveResultSets=True;";
            }
            else
            {
                _con = connection._DB_Master;
            }
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
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        oCmd.CommandText = "branch_in_up";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Clear();
                        oCmd.Parameters.AddWithValue("@branch_id", item.branch_id);
                        oCmd.Parameters.AddWithValue("@branch_code", item.branch_code);
                        oCmd.Parameters.AddWithValue("@branch_name", item.branch_name);
                        oCmd.Parameters.AddWithValue("@unit_floor", item.unit_floor);
                        oCmd.Parameters.AddWithValue("@building", item.building);
                        oCmd.Parameters.AddWithValue("@street", item.street);
                        oCmd.Parameters.AddWithValue("@barangay", item.barangay);
                        oCmd.Parameters.AddWithValue("@municipality", item.municipality);
                        oCmd.Parameters.AddWithValue("@city", item.city);
                        oCmd.Parameters.AddWithValue("@region", item.region);
                        oCmd.Parameters.AddWithValue("@country", item.country);
                        oCmd.Parameters.AddWithValue("@zip_code", item.zip_code);
                        oCmd.Parameters.AddWithValue("@email_address", item.email_address);
                        oCmd.Parameters.AddWithValue("@sss", item.sss);
                        oCmd.Parameters.AddWithValue("@philhealth", item.philhealth);
                        oCmd.Parameters.AddWithValue("@tin", item.tin);
                        oCmd.Parameters.AddWithValue("@rdo", item.rdo);
                        oCmd.Parameters.AddWithValue("@pagibig", item.pagibig);
                        oCmd.Parameters.AddWithValue("@pagibig_branch", item.pagibig_branch);
                        oCmd.Parameters.AddWithValue("@ip_address", item.ip_address);
                        oCmd.Parameters.AddWithValue("@industry", item.industry);
                        oCmd.Parameters.AddWithValue("@bank_id", item.bank_id);
                        oCmd.Parameters.AddWithValue("@bank_account", item.bank_account);
                        oCmd.Parameters.AddWithValue("@company_id", item.company_id);
                        oCmd.Parameters.AddWithValue("@created_by", item.created_by);
                        oCmd.ExecuteNonQuery();
                    }
                }

               
                ////oCmd.Parameters.AddWithValue("@active", model.active);
                //SqlDataReader sdr = oCmd.ExecuteReader();
                //while (sdr.Read())
                //{
                //    resp.branch_id = Convert.ToInt32(sdr["branch_id"].ToString());
                //    resp.guid = sdr["guid"].ToString();
                //    resp.created_by = Convert.ToInt32(sdr["created_by"].ToString());

                //}
                //sdr.Close();
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
