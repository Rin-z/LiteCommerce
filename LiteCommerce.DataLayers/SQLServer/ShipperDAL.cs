﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class ShipperDAL : _BaseDAL, IShipperDAL
    {

        private SqlConnection connection;

        public ShipperDAL(string connectionstring) : base(connectionstring)
        {

        }

        public int Add(Shipper data)
        {
            int ShipperId;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Shippers (ShipperName,Phone) values (@ShipperName,@Phone)Select @@IDENTITY";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Connection = connection;
                ShipperId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return ShipperId;
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ShipperId)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete from Shippers Where ShipperId = @ShipperId";
                cmd.Parameters.AddWithValue("@ShipperId", ShipperId);
                int rowAffect = cmd.ExecuteNonQuery();
                resuft = rowAffect > 0;
                connection.Close();
            }
            return resuft;
        }

        public Shipper Get(int ShipperId)
        {
            Shipper data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Shippers Where ShipperID = @ShipperID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ShipperID", ShipperId);
                cmd.Connection = connection;

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    if (dataReader.Read())
                    {
                        if (ShipperId >= 0)
                        {
                            ShipperId = Convert.ToInt32(dataReader["ShipperId"]);
                            string ShipperName = Convert.ToString(dataReader["ShipperName"]);
                            string Phone = Convert.ToString(dataReader["Phone"]);
                            data = new Shipper(ShipperId, ShipperName, Phone);
                        }

                    }
                }
                connection.Close();
            }


            return data;
        }

        public List<Shipper> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Update(Shipper data)
        {
            bool resuft = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update Shippers set ShipperName=@ShipperName,Phone=@Phone Where ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperId);
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                resuft = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return resuft;
        }

    }
    
}
