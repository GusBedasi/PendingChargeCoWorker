using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using WorkerService1.Models;

namespace WorkerService1.Repository
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly string _connectionString;
        public ChargeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Update(Charge charge)
        {
            try
            {
                using(IDbConnection db = new SqlConnection(_connectionString))
                {
                    string sqlQuery = "UPDATE Charges SET PendingCancellation = " + Convert.ToByte(charge.PendingCancellation)+ " WHERE ExternalId = '" + charge.ExternalId + "';";

                    int rowsAffected = db.Execute(sqlQuery);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exceptiom: " + e);
                throw;
            }
        }
    }
}
