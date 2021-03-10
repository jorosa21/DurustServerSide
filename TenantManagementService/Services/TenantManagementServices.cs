using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenantManagementService.Helper;
using TenantManagementService.Model;
using TenantManagementService.Model.CompanyModel;

namespace TenantManagementService.Services
{

    public interface ITenantManagementServices
    {



        CompanyIUResponse CompanyIU(CompanyIURequest model);


        BranchIUResponse BranchIU(BranchIURequest model);
        BranchIUResponse MultipleBranchIU(BranchIURequest[] model);

    }



    public class TenantManagementServices : ITenantManagementServices
    {
        //public DropdownSetting resp = new DropdownSetting();

        private connectionString connection { get; set; }
        private readonly AppSetting _appSettings;
        private string instance_name { get; set; }

        public TenantManagementServices(IOptions<AppSetting> appSettings,
            IOptions<connectionString> settings)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
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
                oCmd.Parameters.AddWithValue("@city", model.SelectedCity);
                oCmd.Parameters.AddWithValue("@region", model.SelectedRegion);
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
            string instance_name = model.instance_name;
            string username = model.username;
            string password = model.password;
            int branch = 0;


            BranchIUResponse resp = new BranchIUResponse();
            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + username + ";Password=" + password + ";MultipleActiveResultSets=True;";

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
                oCmd.Parameters.AddWithValue("@bankAccount", model.bankAccount);
                oCmd.Parameters.AddWithValue("@barangay", model.barangay);
                oCmd.Parameters.AddWithValue("@branchName", model.branchName);
                oCmd.Parameters.AddWithValue("@building", model.building);
                oCmd.Parameters.AddWithValue("@municipality", model.municipality);
                oCmd.Parameters.AddWithValue("@pagibig", model.pagibig);
                oCmd.Parameters.AddWithValue("@philhealth", model.philhealth);
                oCmd.Parameters.AddWithValue("@SelectedBank", model.SelectedBank);
                oCmd.Parameters.AddWithValue("@SelectedBranchCountry", model.SelectedBranchCountry);
                oCmd.Parameters.AddWithValue("@SelectedCity", model.SelectedCity);
                oCmd.Parameters.AddWithValue("@SelectedIndustry", model.SelectedIndustry);
                oCmd.Parameters.AddWithValue("@SelectedPCity", model.SelectedPCity);
                oCmd.Parameters.AddWithValue("@SelectedPCode", model.SelectedPCode);
                oCmd.Parameters.AddWithValue("@SelectedPRegion", model.SelectedPRegion);
                oCmd.Parameters.AddWithValue("@SelectedRdoBranch", model.SelectedRdoBranch);
                oCmd.Parameters.AddWithValue("@SelectedRdoOffice", model.SelectedRdoOffice);
                oCmd.Parameters.AddWithValue("@SelectedRegion", model.SelectedRegion);
                oCmd.Parameters.AddWithValue("@sss", model.sss);
                oCmd.Parameters.AddWithValue("@street", model.street);
                oCmd.Parameters.AddWithValue("@tin", model.tin);
                oCmd.Parameters.AddWithValue("@unit", model.unit);
                oCmd.Parameters.AddWithValue("@zipCode", model.zipCode);
                oCmd.Parameters.AddWithValue("@company_id", model.company_id);
                oCmd.Parameters.AddWithValue("@created_by", model.CreatedBy);
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.branch_id = Convert.ToInt32(sdr["branch_id"].ToString());
                    resp.guid = sdr["guid"].ToString();
                    resp.created_by = Convert.ToInt32(sdr["created_by"].ToString());

                }
                sdr.Close();


                if (model.iP_IU != null)
                {
                    foreach (var ip in model.iP_IU)
                    {
                        oCmd.CommandText = "branch_ip_in";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Clear();
                        oCmd.Parameters.AddWithValue("@branch_id", resp.branch_id);
                        oCmd.Parameters.AddWithValue("@ip_address", ip.description);
                        oCmd.Parameters.AddWithValue("@created_by", ip.createdBy);
                        oCmd.ExecuteNonQuery();
                    }
                }


                if (model.Contact_IU != null)
                {
                    foreach (var contact in model.Contact_IU)
                    {
                        oCmd.CommandText = "branch_contact_in";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Clear();
                        oCmd.Parameters.AddWithValue("@branch_id", resp.branch_id);
                        oCmd.Parameters.AddWithValue("@contact_type_id", contact.id);
                        oCmd.Parameters.AddWithValue("@contact_number", contact.number);
                        oCmd.Parameters.AddWithValue("@created_by", contact.createdBy);
                        oCmd.ExecuteNonQuery();
                    }
                }


                if (model.Email_IU != null)
                {
                    foreach (var email in model.Email_IU)
                    {
                        oCmd.CommandText = "branch_email_in";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Clear();
                        oCmd.Parameters.AddWithValue("@branch_id", resp.branch_id);
                        oCmd.Parameters.AddWithValue("@email_type_id", email.id);
                        oCmd.Parameters.AddWithValue("@email_address", email.email_address);
                        oCmd.Parameters.AddWithValue("@created_by", email.createdBy);
                        oCmd.ExecuteNonQuery();
                    }
                }


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
            int branch = 0;


            BranchIUResponse resp = new BranchIUResponse();
            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + username + ";Password=" + password + ";MultipleActiveResultSets=True;";

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
                        oCmd.Parameters.Clear(); oCmd.Parameters.AddWithValue("@branch_id", item.branch_id);
                        oCmd.Parameters.AddWithValue("@bankAccount", item.bankAccount);
                        oCmd.Parameters.AddWithValue("@barangay", item.barangay);
                        oCmd.Parameters.AddWithValue("@branchName", item.branchName);
                        oCmd.Parameters.AddWithValue("@building", item.building);
                        oCmd.Parameters.AddWithValue("@municipality", item.municipality);
                        oCmd.Parameters.AddWithValue("@pagibig", item.pagibig);
                        oCmd.Parameters.AddWithValue("@philhealth", item.philhealth);
                        oCmd.Parameters.AddWithValue("@SelectedBank", item.SelectedBank);
                        oCmd.Parameters.AddWithValue("@SelectedBranchCountry", item.SelectedBranchCountry);
                        oCmd.Parameters.AddWithValue("@SelectedCity", item.SelectedCity);
                        oCmd.Parameters.AddWithValue("@SelectedIndustry", item.SelectedIndustry);
                        oCmd.Parameters.AddWithValue("@SelectedPCity", item.SelectedPCity);
                        oCmd.Parameters.AddWithValue("@SelectedPCode", item.SelectedPCode);
                        oCmd.Parameters.AddWithValue("@SelectedPRegion", item.SelectedPRegion);
                        oCmd.Parameters.AddWithValue("@SelectedRdoBranch", item.SelectedRdoBranch);
                        oCmd.Parameters.AddWithValue("@SelectedRdoOffice", item.SelectedRdoOffice);
                        oCmd.Parameters.AddWithValue("@SelectedRegion", item.SelectedRegion);
                        oCmd.Parameters.AddWithValue("@sss", item.sss);
                        oCmd.Parameters.AddWithValue("@street", item.street);
                        oCmd.Parameters.AddWithValue("@tin", item.tin);
                        oCmd.Parameters.AddWithValue("@unit", item.unit);
                        oCmd.Parameters.AddWithValue("@zipCode", item.zipCode);
                        oCmd.Parameters.AddWithValue("@company_id", item.company_id);
                        oCmd.Parameters.AddWithValue("@created_by", item.CreatedBy);
                        //oCmd.ExecuteNonQuery();
                        SqlDataReader sdr = oCmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            resp.branch_id = Convert.ToInt32(sdr["branch_id"].ToString());
                            resp.guid = sdr["guid"].ToString();
                            resp.created_by = Convert.ToInt32(sdr["created_by"].ToString());

                            branch = Convert.ToInt32(sdr["branch_id"].ToString());

                        }
                        sdr.Close();

                        if (item.iP_IU != null)
                        {
                            foreach (var ip in item.iP_IU)
                            {
                                oCmd.CommandText = "branch_ip_in";
                                oCmd.CommandType = CommandType.StoredProcedure;
                                oCmd.Parameters.Clear();
                                oCmd.Parameters.AddWithValue("@branch_id", branch);
                                oCmd.Parameters.AddWithValue("@ip_address", ip.description);
                                oCmd.Parameters.AddWithValue("@created_by", ip.createdBy);
                                oCmd.ExecuteNonQuery();
                            }
                        }


                        if (item.Contact_IU != null)
                        {
                            foreach (var contact in item.Contact_IU)
                            {
                                oCmd.CommandText = "branch_contact_in";
                                oCmd.CommandType = CommandType.StoredProcedure;
                                oCmd.Parameters.Clear();
                                oCmd.Parameters.AddWithValue("@branch_id", branch);
                                oCmd.Parameters.AddWithValue("@contact_type_id", contact.id);
                                oCmd.Parameters.AddWithValue("@contact_number", contact.number);
                                oCmd.Parameters.AddWithValue("@created_by", contact.createdBy);
                                oCmd.ExecuteNonQuery();
                            }
                        }


                        if (item.Email_IU != null)
                        {
                            foreach (var email in item.Email_IU)
                            {
                                oCmd.CommandText = "branch_email_in";
                                oCmd.CommandType = CommandType.StoredProcedure;
                                oCmd.Parameters.Clear();
                                oCmd.Parameters.AddWithValue("@branch_id", branch);
                                oCmd.Parameters.AddWithValue("@email_type_id", email.id);
                                oCmd.Parameters.AddWithValue("@email_address", email.email_address);
                                oCmd.Parameters.AddWithValue("@created_by", email.createdBy);
                                oCmd.ExecuteNonQuery();
                            }
                        }

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


    }

}
