using FPro;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ARWABANISHAMSAH_FMS.SERVICES
{
    public class DriverService
    {
        private readonly string connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";

        /// <summary>
        /// Function for retrive all drivers query
        /// </summary>
        /// <returns>A list of drivers returned from the DB</returns>
        public async Task<GVAR> getDrivers()
        {
            GVAR Gvar= new GVAR();
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("Select * from driver ", conn);
                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            

            DataTable driverTable = new DataTable();
            driverTable.Columns.Add("DriverID", typeof(long));
            driverTable.Columns.Add("DriverName", typeof(string));
            driverTable.Columns.Add("PhoneNumber", typeof(long));
            while (await reader.ReadAsync())
            {
                DataRow row = driverTable.NewRow();
                row["DriverID"] = reader.GetInt64(0);
                row["DriverName"] = reader.GetString(1);
                row["PhoneNumber"] = reader.GetInt64(2);
                driverTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Drivers"] = driverTable;

            return Gvar;
        }
        /// <summary>
        /// this function add a driver to the DB
        /// </summary>
        /// <param name="driver"> The driver information to be added</param>
        /// <returns></returns>

        public async Task addDriver(GVAR Gvar)
        {

            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("DriverName", out var driverName);
            tags.TryGetValue("PhoneNumber", out var phoneNumber);
            long.TryParse(phoneNumber, out long phoneNumberValue);

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await  conn.OpenAsync();
                using var cmd = new NpgsqlCommand("INSERT INTO Driver (DriverName,PhoneNumber)VALUES (@driverName,@phoneNumber) ", conn);
                {
                    cmd.Parameters.AddWithValue("driverName",driverName);
                    cmd.Parameters.AddWithValue("phoneNumber", phoneNumberValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete certain driver from DB and deleting the corrosponding vehichale info.
        /// </summary>
        /// <param name="driverID"> The id of the driver to be deleted. </param>
        /// <returns></returns>

        public async Task deleteDriver(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("DriverID", out var driverID);
            long.TryParse(driverID, out long driverIDValue);

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction()) 
                {
                    try
                    {

                        using var realtedcmd = new NpgsqlCommand("DELETE FROM VehiclesInformations WHERE DriverID =@driverID ", conn);
                        {
                            realtedcmd.Parameters.AddWithValue("driverID", driverIDValue);
                            realtedcmd.ExecuteNonQuery();
                        }

                        using var cmd = new NpgsqlCommand("DELETE FROM Driver WHERE DriverID =@driverID ", conn);
                        {
                            cmd.Parameters.AddWithValue("driverID", driverIDValue);
                            cmd.ExecuteNonQuery();
                        }
                        await transaction.CommitAsync();

                    }
                    catch (Exception e)
                    {
                        transaction.RollbackAsync();
                        throw new Exception("An error occurred while deleting the driver.", e);
                    }
                }
            }
        }

        /// <summary>
        /// This function update a driver information in DB accordingto his ID.
        /// </summary>
        /// <param name="driverID">The ID of the driver to update his info.</param>
        /// <param name="phoneNumber"> The driver new phone number .</param>
        /// <returns></returns>
        public async Task UpdateDriver(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("DriverName", out var driverName);
            tags.TryGetValue("PhoneNumber", out var phoneNumber);
            long.TryParse(phoneNumber, out long phoneNumberValue);
            tags.TryGetValue("DriverID", out var driverID);
            long.TryParse(driverID, out long driverIDValue);


            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("UPDATE Driver SET  PhoneNumber = @newPhoneNumber WHERE DriverID = @driverID", conn))
                {
                    cmd.Parameters.AddWithValue("newPhoneNumber", phoneNumberValue);
                    cmd.Parameters.AddWithValue("driverID", driverIDValue);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
