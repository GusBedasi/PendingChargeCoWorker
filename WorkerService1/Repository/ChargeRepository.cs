using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WorkerService1.Models;

namespace WorkerService1.Repository
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ChargeRepository> _logger;

        public ChargeRepository(string connectionString, ILogger<ChargeRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public void Update(Charge charge)
        {
            try
            {
                using(IDbConnection db = new SqlConnection(_connectionString))
                {
                    var chargeExist = db.Query<Charge>("SELECT * FROM Charges WHERE ExternalId ='" + charge.ExternalId + "'").FirstOrDefault();

                    if(chargeExist == null) 
                    {
                        _logger.LogWarning($"Charge with ExternalId: {charge.ExternalId} was not found");
                        return;
                    }

                    string sqlQuery = "UPDATE Charges SET PendingCancellation = " 
                        + Convert.ToByte(charge.PendingCancellation)+ " WHERE ExternalId = '" + charge.ExternalId + "';";

                    int rowsAffected = db.Execute(sqlQuery);

                    _logger.LogInformation($"Charge payload: {JsonConvert.SerializeObject(charge)}");
                    _logger.LogInformation($"Rows affected: {rowsAffected}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exceptiom: " + e);
                _logger.LogError(e.Message, e.StackTrace, e.InnerException);
            }
        }
    }
}
