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
    public class BearTypeRepoistory : IRepository<BearTypeModel>
    {
        private SqlConnection con;
        private readonly IConfiguration configuration;

        public BearTypeRepoistory(IConfiguration config)
        {
            configuration = config;
            string constr = configuration.GetConnectionString("DefaultConnection"); ;
            con = new SqlConnection(constr);
        }

        public IEnumerable<BearTypeModel> List()
        {
            var res = new List<BearTypeModel>();

            SqlCommand com = new SqlCommand("GetBearType", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                res.Add(
                    new BearTypeModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                    }
                );
            }
            return res;
        }

        public bool Add(BearTypeModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(BearTypeModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(BearTypeModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
