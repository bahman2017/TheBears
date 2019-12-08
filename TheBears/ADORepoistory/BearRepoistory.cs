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

        public IEnumerable<BearModel> List() => throw new NotImplementedException();

        public bool Add(BearModel entity)
        {
            SqlCommand com = new SqlCommand("AddBear", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", entity.Name);
            com.Parameters.AddWithValue("@typeId", entity.TypeId);
            com.Parameters.AddWithValue("@habitat", entity.Habitat);
            com.Parameters.AddWithValue("@sex", entity.Sex);
            com.Parameters.AddWithValue("@age", entity.Age);
            com.Parameters.AddWithValue("@height", entity.Height);
            com.Parameters.AddWithValue("@weight", entity.Weight);

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

        public bool Delete(BearModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(BearModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
