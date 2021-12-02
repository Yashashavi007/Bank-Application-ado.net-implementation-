using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Technovert.Banking.Service
{
    internal class DBService
    {
        
        
        internal static void CreateCustomerTable()
        {
            string conString = "server=localhost; port=3306; Uid=root; Pwd=J@gu@r007; database=BankApp";
            MySqlConnection conn = new MySqlConnection(conString);
            conn.Open();
            string query1 = "create table BankApp.Customer(ID varchar(100) Primary Key," +
                            "Name varchar(100)," +
                            "Gender varchar(5)," +
                            "AccountNumber varchar(100)," +
                            "Balance float," +
                            "Status varchar(20));";

            MySqlCommand cmd = new MySqlCommand(query1, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Customer Table created successfull !!");

        }
        public static void InsertIntoTable(string id, string name, string gender, string accNo, float balance, string status="Open")
        {
            string conString = "server=localhost; port=3306; Uid=root; Pwd=J@gu@r007; database=BankApp";
            MySqlConnection conn = new MySqlConnection(conString);
            conn.Open();
            string query2 = "insert into BankApp.Customer values" +
                            "(@id, @name, @gender, @accNo, @balance, @status);";
            using (MySqlCommand cmd = new MySqlCommand(query2, conn))
            {
                cmd.Parameters.Add("@id", MySqlDbType.VarChar, 100).Value = id;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = name;
                cmd.Parameters.Add("@gender", MySqlDbType.VarChar, 5).Value = gender;
                cmd.Parameters.Add("@accNo", MySqlDbType.VarChar, 100).Value = accNo;
                cmd.Parameters.Add("@balance", MySqlDbType.Float).Value = balance;
                cmd.Parameters.Add("@status", MySqlDbType.VarChar, 20).Value = status;

                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }
        public static void UpdateTable()
        {

        }
        public static bool CheckTable()
        {
            string conString = "server=localhost; port=3306; Uid=root; Pwd=J@gu@r007; database=BankApp";
            MySqlConnection conn = new MySqlConnection(conString);
            conn.Open();
            string query3 = "SELECT count(*) from information_schema.tables WHERE table_name = 'BankApp.Customer';";
            using (MySqlCommand cmd = new MySqlCommand(query3, conn))
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine(reader[0]);
                    if (Convert.ToInt32(reader[0]) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static void RetrieveData()
        {
            string conString = "server=localhost; port=3306; Uid=root; Pwd=J@gu@r007; database=BankApp";
            MySqlConnection conn = new MySqlConnection(conString);
            conn.Open();
            string query4 = "select * from BankApp.Customer;";
            using (MySqlCommand cmd = new MySqlCommand(query4, conn))
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Console.WriteLine($"{reader[0]} | {reader[1]} | {reader[2]} | {reader[3]} | {reader[4]} | {reader[5]}");
                }
                reader.Close();
                conn.Close();
            }
        }
        public static void DeleteTable()
        {

        }


    }
}
