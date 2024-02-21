using Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Project.DataLayer
{
    public interface IUserReg
    {
        public abstract void Reg(UserClass uc);
        public abstract UserClass userLogin(UserClass uc);
        public abstract List<UserClass> userdetails();
        public abstract List<ProductClass> AllProduct();
        public abstract void insertProduct(ProductClass product, string ui);
        public abstract List<ProductClass> AllProductlist();
        public abstract List<ProductClass> deProductlist1();
        public abstract List<ProductClass> deProductlist();
        public abstract List<ProductClass> apprProductlist(string ui);
        public abstract List<ProductClass> rejProductlist();
        public abstract ProductClass upIsdelete(string pname, string ui);
        public abstract ProductClass upIsapprove(string pname, string ui);
        public abstract ProductClass upIsreject(string pname, string ui);

        public abstract void doActive(string pname, string ui);
        public abstract void doApprove(string email, string ui);
        public abstract void doInActive(string email, string ui);
        public abstract void douserActive(string email, string ui);
    }
    public class UserReg:IUserReg
    {
        public string connectionString = "Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True";
        public void Reg(UserClass uc)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                
                SqlCommand cmd = new SqlCommand("Registration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", uc.username);
                cmd.Parameters.AddWithValue("@mob", uc.mob);
                cmd.Parameters.AddWithValue("@email", uc.email);
                cmd.Parameters.AddWithValue("@usertype", uc.usertype);
                cmd.Parameters.AddWithValue("@password", uc.password);
                conn.Open();
                //int n = cmd.ExecuteNonQuery();
                SqlDataReader sdr = cmd.ExecuteReader();
                //if (n > 0)
                //{
                //    conn.Close();
                //}
                while(sdr.Read())
                {
                    uc.code = (int)sdr["code"];
                    uc.msg = sdr["msg"].ToString();
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong Is There.....Please Check Once !");
            }
            finally
            {
                conn.Close();
            }
        }
        public  UserClass userLogin(UserClass uc)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                UserClass uc2 = new UserClass();
                SqlCommand cmd = new SqlCommand("UserData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", uc.email);
                cmd.Parameters.AddWithValue("@password", uc.password);
                //cmd.Parameters.AddWithValue("@userid", uc.UserId);
                conn.Open();
                SqlDataReader srd = cmd.ExecuteReader();
                while (srd.Read())
                {
                    uc2.email = srd["Email"].ToString(); //Convert.ToString(srd["username"]);

                    uc2.password = Convert.ToString(srd["Password"]);
                    uc2.usertype = srd["RoleName"].ToString();
                    uc2.UserId = srd["UserId"].ToString();
                    uc2.isActive = ((bool)srd["IsActive"]) ? 1 : 0;
                }
                srd.Close();

                return uc2;
            }catch(Exception ex)
            {
                throw;
            }
            finally { conn.Close(); }
        }
        public  List<UserClass> userdetails()
        {
            List<UserClass> lst = new List<UserClass>();
            UserClass user = new UserClass();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("AllUser", conn);
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    lst.Add(new UserClass {
                    username = Convert.ToString(sdr["Name"]),
                    mob = sdr["contactno"].ToString(),
                    email = sdr["email"].ToString(),
                    isActive = ((bool)sdr["IsActive"]) ? 1 : 0,
                        isApprove = ((bool)sdr["IsApprove"]) ? 1 : 0
                    });
                }
                sdr.Close();
                return lst;
        }

        public  List<ProductClass> AllProduct()
        {
            List<ProductClass> lst = new List<ProductClass>();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("AllProduct", conn);
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lst.Add(new ProductClass
                {
                    pname = Convert.ToString(sdr["Prod_Name"]),
                    ptype = sdr["Type_Name"].ToString(),
                    pprice = (decimal)sdr["Prod_Price"],
                    username = sdr["Name"].ToString(),
                    insertedby = sdr["InsertedBy"].ToString(),
                    date = sdr["InsertedOn"].ToString()
                });
            }
            sdr.Close();

            return lst;
        }

        public  void insertProduct(ProductClass product, string ui)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("insertProd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prod_name", product.pname);
                cmd.Parameters.AddWithValue("@prod_price", product.pprice);
                cmd.Parameters.AddWithValue("@prod_type", product.ptype);
                cmd.Parameters.AddWithValue("@insertedby", ui);
                conn.Open();
                //int n = cmd.ExecuteNonQuery();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    product.code = (int)sdr["code"];
                    product.msg = sdr["msg"].ToString();
                }
                sdr.Close();

            }
            catch(SqlException ex)
            {
              throw new Exception("Something Wrong ... Try Again");
            }
            finally { conn.Close(); }
        }

        public  List<ProductClass> AllProductlist()
        {
            List<ProductClass> lst = new List<ProductClass>();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("AllProduct", conn);
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lst.Add(new ProductClass
                {
                    pname = Convert.ToString(sdr["Prod_Name"]),
                    ptype = sdr["Type_Name"].ToString(),
                    pprice = (decimal)sdr["Prod_Price"]
                    
                });
            }
            sdr.Close();

            return lst;
        }
		public  List<ProductClass> deProductlist1()
		{
			List<ProductClass> lst = new List<ProductClass>();

			SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
			SqlCommand cmd = new SqlCommand("delProduct", conn);
			conn.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader sdr = cmd.ExecuteReader();
			while (sdr.Read())
			{
				lst.Add(new ProductClass
				{
					pname = Convert.ToString(sdr["Prod_Name"]),
					ptype = sdr["ptype"].ToString(),
					pprice = (decimal)sdr["Prod_Price"],
					iswithdraw = (bool)sdr["IsWithdrawal"],
					slno = (int)sdr["sn"],
					//isreject = sdr["IsReject"] == DBNull.Value ? false : (bool)sdr["IsReject"],
					//isreject = sdr["IsReject"] == DBNull.Value ? false : (sdr["IsReject"] != DBNull.Value && (bool)sdr["IsReject"]),
					//isreject = sdr["IsReject"] == DBNull.Value ? (bool?)null : (bool)sdr["IsReject"],
					//isreject = (sdr["IsReject"] == DBNull.Value) ? false : (bool)sdr["IsReject"],
					//isreject = sdr["IsReject"] != DBNull.Value && (bool)sdr["IsReject"],
					//isreject = sdr["IsReject"] != DBNull.Value && Convert.ToInt32(sdr["IsReject"]) == 1,
					//isreject = (sdr["IsReject"] != DBNull.Value) && ((int)sdr["IsReject"] == 1),
					isreject = ((bool)sdr["isReject"]) ? 1 : 0,

					// userid = sdr["userid"].ToString(),
					userid = sdr["Name"].ToString(),
					udate = sdr["udate"].ToString()
				});
			}
			sdr.Close();

			return lst;
		}
		public  List<ProductClass> deProductlist()
        {
			List<ProductClass> lst = new List<ProductClass>();

			SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
			SqlCommand cmd = new SqlCommand("delProduct1", conn);
			conn.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			SqlDataReader sdr = cmd.ExecuteReader();
			while (sdr.Read())
			{
				lst.Add(new ProductClass
                {
                    pname = Convert.ToString(sdr["Prod_Name"]),
                    ptype = sdr["ptype"].ToString(),
                    pprice = (decimal)sdr["Prod_Price"],
                    iswithdraw = (bool)sdr["IsWithdrawal"],
                    slno = (int)sdr["sn"],
                    //isreject = sdr["IsReject"] == DBNull.Value ? false : (bool)sdr["IsReject"],
                    //isreject = sdr["IsReject"] == DBNull.Value ? false : (sdr["IsReject"] != DBNull.Value && (bool)sdr["IsReject"]),
                    //isreject = sdr["IsReject"] == DBNull.Value ? (bool?)null : (bool)sdr["IsReject"],
                    //isreject = (sdr["IsReject"] == DBNull.Value) ? false : (bool)sdr["IsReject"],
                    //isreject = sdr["IsReject"] != DBNull.Value && (bool)sdr["IsReject"],
                    //isreject = sdr["IsReject"] != DBNull.Value && Convert.ToInt32(sdr["IsReject"]) == 1,
                    //isreject = (sdr["IsReject"] != DBNull.Value) && ((int)sdr["IsReject"] == 1),
                    isreject = ((bool)sdr["isReject"])?1:0,

               // userid = sdr["userid"].ToString(),
                userid = sdr["Name"].ToString(),
                    udate = sdr["udate"].ToString()
			    });
			}
			sdr.Close();

			return lst;
		}
        public  List<ProductClass> apprProductlist(string ui)
        {
            List<ProductClass> lst = new List<ProductClass>();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("apprProduct", conn);
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", ui);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                lst.Add(new ProductClass
                { pname = srd["Prod_Name"].ToString(),
                ptype = srd["ptype"].ToString(),
                pprice = (decimal)srd["Prod_Price"],
                slno = (int)srd["sn"],
                approveby = srd["ApproveBy"].ToString(),
                apdate = srd["ApproveOn"].ToString(),
                insertedby = srd["InsertedBy"].ToString(),
                indate = srd["InsertedOn"].ToString(),
                    //pc.isreject = (bool)srd["IsReject"];
                    // isreject = srd["IsReject"] == DBNull.Value ? false : (bool)srd["IsReject"]
                    //isreject = (bool)srd["isReject"]
                    isreject = ((bool)srd["isReject"]) ? 1 : 0
                });
            }
            srd.Close();

            return lst;
        }
        public  List<ProductClass> rejProductlist()
        {
            List<ProductClass> lst = new List<ProductClass>();

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("rejProduct", conn);
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                lst.Add(new ProductClass
                {
                    pname = srd["Prod_Name"].ToString(),
                    ptype = srd["ptype"].ToString(),
                    pprice = (decimal)srd["Prod_Price"],
                    slno = (int)srd["sn"],
                    insertedby = srd["Name"].ToString(),
                    //username = srd["Name"].ToString(),
                    rejectby = srd["username"].ToString(),
                rejdate = srd["RejectedOn"].ToString(),
                indate = srd["InsertedOn"].ToString(),
                    //pc.isreject = (bool)srd["IsReject"];
                    // isreject = srd["IsReject"] == DBNull.Value ? false : (bool)srd["IsReject"]
                    //isreject = (bool)srd["isReject"]
                    isreject = ((bool)srd["isReject"]) ? 1 : 0
                });
            }
            srd.Close();

            return lst;
        }
        
        public  ProductClass upIsdelete(string pname,string ui)
        {
            try
            {
                ProductClass pc = new ProductClass();
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UpIsdelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prod_name", pname);
                cmd.Parameters.AddWithValue("@userid", ui);
                conn.Open();
                SqlDataReader srd = cmd.ExecuteReader();
                while (srd.Read())
                {
                    pc.pname = srd["Prod_Name"].ToString();
                    pc.ptype = srd["ptype"].ToString();
                    pc.pprice = (decimal)srd["Prod_Price"];
                    pc.iswithdraw = (bool)srd["IsWithdrawal"];
                    pc.slno = (int)srd["sn"];
                    //pc.isreject = (bool)srd["IsReject"];

                    //pc.isreject = (bool)srd["IsReject"];
                    pc.isreject = ((bool)srd["isReject"]) ? 1 : 0;

                }
                srd.Close();
                return pc;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public  ProductClass upIsapprove(string pname, string ui)
        {
            ProductClass pc = new ProductClass();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UpIsapprove", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prod_name", pname);
            cmd.Parameters.AddWithValue("@userid", ui);
            conn.Open();
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                pc.pname = srd["Prod_Name"].ToString();
                pc.ptype = srd["ptype"].ToString();
                pc.pprice = (decimal)srd["Prod_Price"];
                pc.slno = (int)srd["sn"];
                pc.approveby = srd["ApproveBy"].ToString();
                pc.apdate = srd["ApproveOn"].ToString();
                pc.insertedby = srd["InsertedBy"].ToString();
                pc.indate = srd["InsertedOn"].ToString();
                //pc.isreject = (bool)srd["IsReject"];

                //pc.isreject = (bool)srd["IsReject"];
                pc.isreject = ((bool)srd["isReject"]) ? 1 : 0;
            }
            srd.Close();
            return pc;
        }
        public  ProductClass upIsreject(string pname, string ui)
        {
            ProductClass pc = new ProductClass();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UpIsreject", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prod_name", pname);
            cmd.Parameters.AddWithValue("@userid", ui);
            conn.Open();
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                pc.pname = srd["Prod_Name"].ToString();
                pc.ptype = srd["ptype"].ToString();
                pc.pprice = (decimal)srd["Prod_Price"];
                pc.slno = (int)srd["sn"];
                pc.insertedby = srd["InsertedBy"].ToString();
                pc.indate = srd["InsertedOn"].ToString();
                pc.rejectby = srd["RejectedBy"].ToString();
                pc.rejdate = srd["RejectedOn"].ToString();
                //pc.isreject = (bool)srd["IsReject"];

                //pc.isreject = (bool)srd["IsReject"];
                pc.isreject = ((bool)srd["isReject"]) ? 1 : 0;
            }
            srd.Close();
            return pc;
        }
        
        public  void doActive(string pname, string ui)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("doActive", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prod_name", pname);
                cmd.Parameters.AddWithValue("@userid", ui);
                conn.Open();
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    conn.Close();
                }
            }catch (Exception ex) {
                throw;
            }
        }

        public  void doApprove(string email,string ui)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("doApprove", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@userid", ui);
            conn.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                conn.Close();
            }
        }
        
        public  void doInActive(string email, string ui)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("doInActive", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@userid", ui);
            conn.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                conn.Close();
            }
        }
        public  void douserActive(string email, string ui)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("douserActive", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@userid", ui);
            conn.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                conn.Close();
            }
        }
    }
}
