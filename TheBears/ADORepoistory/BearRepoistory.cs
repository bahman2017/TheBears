using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TheBears.Models;
using TheBears.Repository;

namespace TheBears.ADORepoistory
{
    public class BearRepoistory : IRepository<BearModel>
    {
        private SqlConnection con;
        private readonly IConfiguration configuration;

        public BearRepoistory(IConfiguration config)
        {
            configuration = config;
            string constr = configuration.GetConnectionString("DefaultConnection"); ;
            con = new SqlConnection(constr);
        }

        public IEnumerable<BearModel> List()
        {
            var res = new List<BearModel>();
            SqlCommand com = new SqlCommand("GetAllBears", con);
            com.CommandType = CommandType.StoredProcedure;
           
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                res.Add(
                    new BearModel
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        TypeName = Convert.ToString(dr["TypeName"])
                    });
            }
            return res;
        }


        public bool Add(BearModel entity)
        {
            SqlCommand com = new SqlCommand("AddBear", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", entity.Name);
            com.Parameters.AddWithValue("@typeName", entity.TypeName);
          

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        public bool Delete(int id)
        {
            SqlCommand com = new SqlCommand("DeleteBear", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        public bool Update(BearModel entity)
        {
            SqlCommand com = new SqlCommand("UpdateBear", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", entity.Id);
            com.Parameters.AddWithValue("@Name", entity.Name);
            com.Parameters.AddWithValue("@typeName", entity.TypeName);
       
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        public IEnumerable<BearModel> List(int page, int size, string sort, out int totalrow)
        {
            var res = new List<BearModel>();
            SqlCommand com = new SqlCommand("GetBears", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@page", page);
            com.Parameters.AddWithValue("@size", size);
            com.Parameters.AddWithValue("@sort", sort);
            com.Parameters.Add("@totalrow", SqlDbType.Int).Direction = ParameterDirection.Output;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            totalrow = Convert.ToInt32(com.Parameters["@totalrow"].Value);
            foreach (DataRow dr in dt.Rows)
            {
                res.Add(
                    new BearModel
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        TypeName = Convert.ToString(dr["TypeName"])
                        
                    });
            }
            return res;
        }
    }
}
