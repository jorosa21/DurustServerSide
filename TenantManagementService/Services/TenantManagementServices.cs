using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantManagementService.Helper;
using TenantManagementService.Model;
using TenantManagementService.Model.CompanyModel;
using TenantManagementService.Model.DropdownModel;

namespace TenantManagementService.Services
{

    public interface ITenantManagementServices
    {

        CompanyResponse CompanyIU(CompanyIURequest model);

        BranchResponse BranchIU(BranchIURequest model);

        BranchResponse MultipleBranchIU(BranchIURequest[] model);



        List<DropdownResponse> Dropdown_List(string instance_name, string user_name, string user_hash, string dropdown_type_id);

        DropdownIUResponse DropdownIU(DropdownIURequest model);

        List<CompanyResponse> company_view_sel(string company_id,  string created_by);

        List<BranchResponse> branch_view(string instance_name, string user_name, string user_hash, string company_id, string branch_id, string created_by);

        List<IPResponse> branch_ip_view(string instance_name, string user_name, string user_hash,  string branch_id);

        List<ContactResponse> branch_contact_view(string instance_name, string user_name, string user_hash,  string branch_id);

        List<EmailResponse> branch_email_view(string instance_name, string user_name, string user_hash,  string branch_id);

    }



    public class TenantManagementServices : ITenantManagementServices
    {

        private connectionString connection { get; set; }
        private readonly AppSetting _appSettings;
        private string instance_name { get; set; }
        private readonly IDataProtector _protector;

        public TenantManagementServices(IOptions<AppSetting> appSettings,
            IOptions<connectionString> settings, IDataProtectionProvider provider)
        {
            _appSettings = appSettings.Value;
            connection = settings.Value;
            _protector = provider.CreateProtector("mysecretkey");
        }



        public CompanyResponse CompanyIU(CompanyIURequest model)
        {

            CompanyResponse resp = new CompanyResponse();
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
                oCmd.Parameters.AddWithValue("@company_id", model.companyID == "0" ? 0 : Crypto.url_decrypt(model.companyID));
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
                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(model.createdBy));
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.companyID = Crypto.url_encrypt(sdr["company_id"].ToString());
                    resp.createdBy = Crypto.url_encrypt(sdr["created_by"].ToString());
                    resp.companyCode = sdr["company_code"].ToString();
                    resp.instance_name = Crypto.url_encrypt(sdr["instance_name"].ToString());
                    resp.password = Crypto.url_encrypt(sdr["user_hash"].ToString());
                    resp.username = Crypto.url_encrypt(sdr["user_name"].ToString());
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


        public BranchResponse BranchIU(BranchIURequest model)
        {
            string instance_name = Crypto.url_decrypt(model.instance_name);
            string username = Crypto.url_decrypt(model.user_name);
            string password = Crypto.url_decrypt(model.user_hash);
            string branch_id = "";
            string created_by = Crypto.url_decrypt(model.CreatedBy);

            BranchResponse resp = new BranchResponse();
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
                oCmd.Parameters.AddWithValue("@branch_id", model.branchID == "0"? 0: Crypto.url_decrypt(model.branchID));
                oCmd.Parameters.AddWithValue("@bank_account", model.bankAccount);
                oCmd.Parameters.AddWithValue("@barangay", model.barangay);
                oCmd.Parameters.AddWithValue("@branch_name", model.branchName);
                oCmd.Parameters.AddWithValue("@building", model.building);
                oCmd.Parameters.AddWithValue("@municipality", model.municipality);
                oCmd.Parameters.AddWithValue("@pagibig", model.pagibig);
                oCmd.Parameters.AddWithValue("@philhealth", model.philhealth);
                oCmd.Parameters.AddWithValue("@bank_id", model.SelectedBank);
                oCmd.Parameters.AddWithValue("@country", model.SelectedBranchCountry);
                oCmd.Parameters.AddWithValue("@city", model.SelectedCity);
                oCmd.Parameters.AddWithValue("@industry", model.SelectedIndustry);
                oCmd.Parameters.AddWithValue("@pagibig_branch", model.SelectedPCity);
                oCmd.Parameters.AddWithValue("@pagibig_code", model.SelectedPCode);
                oCmd.Parameters.AddWithValue("@pagibig_region", model.SelectedPRegion);
                oCmd.Parameters.AddWithValue("@rdo", model.SelectedRdoOffice);
                oCmd.Parameters.AddWithValue("@rdo_branch", model.SelectedRdoBranch);
                oCmd.Parameters.AddWithValue("@region", model.SelectedRegion);
                oCmd.Parameters.AddWithValue("@sss", model.sss);
                oCmd.Parameters.AddWithValue("@street", model.street);
                oCmd.Parameters.AddWithValue("@tin", model.tin);
                oCmd.Parameters.AddWithValue("@unit_floor", model.unit);
                oCmd.Parameters.AddWithValue("@zip_code", model.zipCode);
                oCmd.Parameters.AddWithValue("@company_id", Crypto.url_decrypt(model.company_id));
                oCmd.Parameters.AddWithValue("@created_by", created_by);
                //oCmd.Parameters.AddWithValue("@active", model.active);
                SqlDataReader sdr = oCmd.ExecuteReader();
                while (sdr.Read())
                {
                    resp.branch_id = Crypto.url_encrypt(sdr["branch_id"].ToString());
                    resp.CreatedBy = Crypto.url_encrypt(sdr["created_by"].ToString());
                    branch_id = sdr["branch_id"].ToString();
                }
                sdr.Close();


                if (model.iP_IU != null)
                {
                    foreach (var ip in model.iP_IU)
                    {
                        oCmd.CommandText = "branch_ip_in";
                        oCmd.CommandType = CommandType.StoredProcedure;
                        oCmd.Parameters.Clear();
                        oCmd.Parameters.AddWithValue("@branch_id", branch_id);
                        oCmd.Parameters.AddWithValue("@ip_address", ip.description);
                        oCmd.Parameters.AddWithValue("@created_by", created_by);
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
                        oCmd.Parameters.AddWithValue("@branch_id", branch_id);
                        oCmd.Parameters.AddWithValue("@contact_type_id", contact.id);
                        oCmd.Parameters.AddWithValue("@contact_number", contact.number);
                        oCmd.Parameters.AddWithValue("@created_by", created_by);
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
                        oCmd.Parameters.AddWithValue("@branch_id", branch_id);
                        oCmd.Parameters.AddWithValue("@email_type_id", email.id);
                        oCmd.Parameters.AddWithValue("@email_address", email.email_address);
                        oCmd.Parameters.AddWithValue("@created_by", created_by);
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


        public BranchResponse MultipleBranchIU(BranchIURequest[] model)
        {
            string instance_name = Crypto.url_decrypt(model[0].instance_name);
            string username = Crypto.url_decrypt(model[0].user_name);
            string password = Crypto.url_decrypt(model[0].user_hash);
            int branch = 0;
            string company_id = Crypto.url_decrypt(model[0].company_id);


            BranchResponse resp = new BranchResponse();
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
                        oCmd.Parameters.Clear(); oCmd.Parameters.AddWithValue("@branch_id", item.branchID);
                        oCmd.Parameters.AddWithValue("@bank_account", item.bankAccount);
                        oCmd.Parameters.AddWithValue("@barangay", item.barangay);
                        oCmd.Parameters.AddWithValue("@branch_name", item.branchName);
                        oCmd.Parameters.AddWithValue("@building", item.building);
                        oCmd.Parameters.AddWithValue("@municipality", item.municipality);
                        oCmd.Parameters.AddWithValue("@pagibig", item.pagibig);
                        oCmd.Parameters.AddWithValue("@philhealth", item.philhealth);
                        oCmd.Parameters.AddWithValue("@bank_id", item.SelectedBank);
                        oCmd.Parameters.AddWithValue("@country", item.SelectedBranchCountry);
                        oCmd.Parameters.AddWithValue("@city", item.SelectedCity);
                        oCmd.Parameters.AddWithValue("@industry", item.SelectedIndustry);
                        oCmd.Parameters.AddWithValue("@pagibig_branch", item.SelectedPCity);
                        oCmd.Parameters.AddWithValue("@pagibig_code", item.SelectedPCode);
                        oCmd.Parameters.AddWithValue("@pagibig_region", item.SelectedPRegion);
                        oCmd.Parameters.AddWithValue("@rdo", item.SelectedRdoOffice);
                        oCmd.Parameters.AddWithValue("@rdo_branch", item.SelectedRdoBranch);
                        oCmd.Parameters.AddWithValue("@region", item.SelectedRegion);
                        oCmd.Parameters.AddWithValue("@sss", item.sss);
                        oCmd.Parameters.AddWithValue("@street", item.street);
                        oCmd.Parameters.AddWithValue("@tin", item.tin);
                        oCmd.Parameters.AddWithValue("@unit_floor", item.unit);
                        oCmd.Parameters.AddWithValue("@zip_code", item.zipCode);
                        oCmd.Parameters.AddWithValue("@company_id", company_id);
                        oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(item.CreatedBy));
                        //oCmd.ExecuteNonQuery();
                        SqlDataReader sdr = oCmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            resp.branch_id = Crypto.url_encrypt(sdr["branch_id"].ToString());

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
                                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(item.CreatedBy));
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
                                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(item.CreatedBy));
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
                                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(item.CreatedBy));
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
                oTrans.Rollback();
            }
            finally
            {
                oConn.Close();
            }

            return resp;
        }

        public List<CompanyResponse> company_view_sel(string company_id,string created_by)
        {
            //DropdownResponse resp = new DropdownResponse();

            List<CompanyResponse> resp = new List<CompanyResponse>();
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
                oCmd.CommandText = "company_view_sel";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@company_id", Crypto.url_decrypt(company_id));
                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(created_by));
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                        select new CompanyResponse()
                        {
                            companyID = Crypto.url_encrypt(dr["company_id"].ToString()),
                            companyCode = dr["company_code"].ToString(),
                            createdBy = Crypto.url_encrypt(dr["created_by"].ToString()),
                            street = dr["street"].ToString(),
                            companyName = dr["company_name"].ToString(),
                            barangay = dr["barangay"].ToString(),
                            unit = dr["unit_floor"].ToString(),
                            building = dr["building"].ToString(),
                            municipality = dr["municipality"].ToString(),
                            SelectedCity = Convert.ToInt32(dr["city_id"].ToString()),
                            SelectedRegion = Convert.ToInt32(dr["region_id"].ToString()),
                            selectedCompanyCountry = Convert.ToInt32(dr["country_id"].ToString()),
                            zipCode = dr["zip_code"].ToString(),
                            img = dr["company_logo"].ToString(),

                            instance_name = Crypto.url_encrypt(dr["instance_name"].ToString()),
                            username = Crypto.url_encrypt(dr["user_name"].ToString()),
                            password = Crypto.url_encrypt(dr["user_hash"].ToString()),
                            active = Convert.ToBoolean(dr["active"].ToString())

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


        public List<BranchResponse> branch_view(string instance_name,string user_name, string user_hash,string company_id, string branch_id, string created_by)
        {
            //DropdownResponse resp = new DropdownResponse();

            instance_name = Crypto.url_decrypt(instance_name);
            user_hash = Crypto.url_decrypt(user_hash);
            user_name = Crypto.url_decrypt(user_name);
          


            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }

            List<BranchResponse> resp = new List<BranchResponse>();
            List<IPResponse> ipresp = new List<IPResponse>();
            List<EmailResponse> emailresp = new List<EmailResponse>();
            List<ContactResponse> contactresp = new List<ContactResponse>();
            //List<BranchResponse> resp = new List<BranchResponse>();
            DataTable dt = new DataTable();
            DataTable dt_ip = new DataTable();
            DataTable dt_contact = new DataTable();
            DataTable dt_email = new DataTable();
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


                oCmd.CommandText = "branch_view_sel";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@company_id", Crypto.url_decrypt(company_id));
                oCmd.Parameters.AddWithValue("@branch_id", branch_id == "0" ? 0 :Crypto.url_decrypt(branch_id));
                oCmd.Parameters.AddWithValue("@created_by", Crypto.url_decrypt(created_by));
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                        select new BranchResponse()
                        {
                            branch_id = Crypto.url_encrypt(dr["branch_id"].ToString()),
                            CreatedBy = Crypto.url_encrypt(dr["created_by"].ToString()),
                            branch_code = dr["branch_code"].ToString(),
                            branchName = dr["branch_name"].ToString(),
                            unit = dr["unit_floor"].ToString(),
                            building = dr["building"].ToString(),
                            street = dr["street"].ToString(),
                            barangay = dr["barangay"].ToString(),
                            municipality = dr["municipality"].ToString(),
                            SelectedCity = Convert.ToInt32(dr["city_id"].ToString()),
                            SelectedRegion = Convert.ToInt32(dr["region_id"].ToString()),
                            SelectedBranchCountry = Convert.ToInt32(dr["country_id"].ToString()),
                            zipCode = dr["zip_code"].ToString(),
                            sss = dr["sss"].ToString(),
                            philhealth = dr["philhealth"].ToString(),
                            pagibig = dr["pagibig"].ToString(),
                            tin = dr["tin"].ToString(),

                            SelectedRdoBranch = Convert.ToInt32(dr["rdo_branch_id"].ToString()),
                            SelectedRdoOffice = Convert.ToInt32(dr["rdo_id"].ToString()),
                            SelectedPCity = Convert.ToInt32(dr["pagibig_branch_id"].ToString()),
                            SelectedPCode = Convert.ToInt32(dr["pagibig_code"].ToString()),
                            SelectedPRegion = Convert.ToInt32(dr["pagibig_region_id"].ToString()),
                            SelectedIndustry = Convert.ToInt32(dr["industry_id"].ToString()),
                            SelectedBank = Convert.ToInt32(dr["bank_id"].ToString()),
                            bankAccount = dr["bank_account"].ToString(),
                            company_id = Crypto.url_encrypt(dr["company_id"].ToString()),

                            //instance_name = Crypto.url_encrypt(dr["instance_name"].ToString()),
                            //username = Crypto.url_encrypt(dr["user_name"].ToString()),
                            //password = Crypto.url_encrypt(dr["user_hash"].ToString()),
                            active = Convert.ToBoolean(dr["active"].ToString()),

                            //iP_IU = ipresp

                        }).ToList();







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


        public List<IPResponse> branch_ip_view(string instance_name, string user_name, string user_hash,  string branch_id)
        {
            //DropdownResponse resp = new DropdownResponse();

            instance_name = Crypto.url_decrypt(instance_name);
            user_hash = Crypto.url_decrypt(user_hash);
            user_name = Crypto.url_decrypt(user_name);

            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }

            List<IPResponse> resp = new List<IPResponse>();
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

                oCmd.CommandText = "branch_ip_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@branch_id", branch_id == "0" ? 0 : Crypto.url_decrypt(branch_id));
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                          select new IPResponse()
                          {
                              branch_id = Crypto.url_encrypt(dr["branch_id"].ToString()),
                              createdBy = Crypto.url_encrypt(dr["created_by"].ToString()),
                              description = dr["ip_address"].ToString(),

                          }).ToList();

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

        public List<ContactResponse> branch_contact_view(string instance_name, string user_name, string user_hash, string branch_id)
        {
            instance_name = Crypto.url_decrypt(instance_name);
            user_hash = Crypto.url_decrypt(user_hash);
            user_name = Crypto.url_decrypt(user_name);



            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }

            List<ContactResponse> resp = new List<ContactResponse>();
            //List<BranchResponse> resp = new List<BranchResponse>();
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

                oCmd.CommandText = "branch_contact_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@branch_id", branch_id == "0" ? 0 : Crypto.url_decrypt(branch_id));
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                               select new ContactResponse()
                               {
                                   branch_id = Crypto.url_encrypt(dr["branch_id"].ToString()),
                                   createdBy = Crypto.url_encrypt(dr["created_by"].ToString()),
                                   id = (dr["contact_type_id"].ToString()),
                                   description = (dr["contact_type"].ToString()),
                                   number = dr["contact_number"].ToString(),

                               }).ToList();




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


        public List<EmailResponse> branch_email_view(string instance_name, string user_name, string user_hash, string branch_id)
        {
            //DropdownResponse resp = new DropdownResponse();

            instance_name = Crypto.url_decrypt(instance_name);
            user_hash = Crypto.url_decrypt(user_hash);
            user_name = Crypto.url_decrypt(user_name);



            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }

            List<EmailResponse> resp = new List<EmailResponse>();
            //List<BranchResponse> resp = new List<BranchResponse>();
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
                oCmd.CommandText = "branch_email_view";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Clear();
                oCmd.Parameters.AddWithValue("@branch_id", branch_id == "0" ? 0 : Crypto.url_decrypt(branch_id));
                da.Fill(dt);
                resp = (from DataRow dr in dt.Rows
                             select new EmailResponse()
                             {
                                 branch_id = Crypto.url_encrypt(dr["branch_id"].ToString()),
                                 createdBy = Crypto.url_encrypt(dr["created_by"].ToString()),
                                 id = Convert.ToInt32(dr["email_type_id"].ToString()),
                                 description = (dr["email_type"].ToString()),
                                 email_address = dr["email_address"].ToString(),

                             }).ToList();




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

            string instance_name = Crypto.url_decrypt(model.instance_name);
            string user_hash = Crypto.url_decrypt(model.user_hash);
            string user_name = Crypto.url_decrypt(model.user_name);

            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }

            DropdownIUResponse resp = new DropdownIUResponse();
            //string _con = connection._DB_Master;
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
                oCmd.Parameters.AddWithValue("@active", model.active);
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

        public List<DropdownResponse> Dropdown_List(string instance_name, string user_name, string user_hash, string dropdown_type_id)
        {

            instance_name = Crypto.url_decrypt(instance_name);
            user_hash = Crypto.url_decrypt(user_hash);
            user_name = Crypto.url_decrypt(user_name);



            string _con;
            if (instance_name is null)
            {
                _con = connection._DB_Master;
            }
            else
            {
                _con = "Data Source=" + instance_name + ";Initial Catalog=mastersetupdb;User ID=" + user_name + ";Password=" + user_hash + ";MultipleActiveResultSets=True;";

            }


            List<DropdownResponse> resp = new List<DropdownResponse>();
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
                            type_description = (dr["type_description"].ToString()),
                            active = Convert.ToBoolean(dr["active"].ToString()),

                        }).ToList();
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
