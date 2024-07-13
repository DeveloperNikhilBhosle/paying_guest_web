using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SK_PG_WebApp.Helper;
using System;
using System.Collections;
using System.Data;

namespace SK_PG_WebApp.DAL
{
    public class ManualDbContext
    {
        
            IConfiguration _configuration = null;
            public ManualDbContext(IConfiguration configuration)
            {
                _configuration = configuration;

            }

            private MySql.Data.MySqlClient.MySqlConnection GetConnection()
            {
                return new MySql.Data.MySqlClient.MySqlConnection(ConfigurationExtensions.GetConnectionString(_configuration, "DBConn"));
            }
            public DataTable GetTable(string query)
            {
                using (MySql.Data.MySqlClient.MySqlConnection con = GetConnection())
                {
                    con.Open();
                    try
                    {
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        int timeOut = Convert.ToInt32("3600");
                        if (timeOut.IsNotNull() && timeOut != 0)
                        {
                            cmd.CommandTimeout = timeOut;
                        }

                        DataTable dt = new DataTable();
                        MySql.Data.MySqlClient.MySqlDataAdapter ad = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                        ad.Fill(dt);
                        con.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        throw ex;
                    }
                }



            }

            public DataTable GetTableV2(string query)
            {
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    try
                    {
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        int timeOut = Convert.ToInt32("3600");
                        if (timeOut.IsNotNull() && timeOut != 0)
                        {
                            cmd.CommandTimeout = timeOut;
                        }

                        DataTable dt = new DataTable();
                        MySql.Data.MySqlClient.MySqlDataAdapter ad = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                        ad.Fill(dt);
                        con.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        throw ex;
                    }
                }
            }


            public DataSet GetDataSet(string procedure, Hashtable hash = null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    using (MySqlConnection con = GetConnection())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(procedure, con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (hash.IsNotNull())
                                {
                                    foreach (string item in hash.Keys)
                                    {
                                        cmd.Parameters.AddWithValue(item, hash[item]);
                                    }
                                }

                                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                                {

                                    sda.Fill(ds);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            con.Close();
                        }

                    }
                    return ds;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return null;
                }


            }

            public DataTable GetDataTable(string procedure, Hashtable hash = null)
            {
                try
                {
                    DataTable ds = new DataTable();
                    using (MySqlConnection con = GetConnection())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(procedure, con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (hash.IsNotNull())
                                {
                                    foreach (string item in hash.Keys)
                                    {
                                        cmd.Parameters.AddWithValue(item, hash[item]);
                                    }
                                }

                                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                                {

                                    sda.Fill(ds);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            con.Close();
                        }

                    }
                    return ds;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();

                    return null;
                }


            }
        
    }
}
