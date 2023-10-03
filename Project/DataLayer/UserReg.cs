using Project.Models;
using System.Data.SqlClient;
using System.Data;

namespace Project.DataLayer
{
    public class UserReg
    {
        public void Reg(UserClass uc)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Registration", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", uc.username);
            cmd.Parameters.AddWithValue("@mob", uc.mob);
            cmd.Parameters.AddWithValue("@email", uc.email);
            cmd.Parameters.AddWithValue("@usertype", uc.usertype);
            cmd.Parameters.AddWithValue("@password", uc.password);
            conn.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                conn.Close();
            }
        }
        public static UserClass userLogin(UserClass uc)
        {
            UserClass uc2 = new UserClass();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("UserData", conn);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", uc.email);
            cmd.Parameters.AddWithValue("@password", uc.password);
            conn.Open();
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                uc2.email = srd["Email"].ToString(); //Convert.ToString(srd["username"]);

                uc2.password = Convert.ToString(srd["Password"]);
                uc2.usertype = srd["RoleName"].ToString();
            }
            srd.Close();
            
            return uc2;
        }
        public static List<UserClass> userdetails()
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
                    email = sdr["email"].ToString()
                    });
                }
                sdr.Close();
                return lst;
        }

        public static List<ProductClass> AllProduct()
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
                    pprice = (int)sdr["Prod_Price"],
                    username = sdr["InsertedBy"].ToString(),
                    date = sdr["InsertedOn"].ToString()
                });
            }
            sdr.Close();

            return lst;
        }

        public static void insertProduct(ProductClass product)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0F29O5M\\SQLEXPRESS;Initial Catalog=Machine_Test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Registration", conn);
            cmd.CommandType = CommandType.StoredProcedure;
        }
    }
}
