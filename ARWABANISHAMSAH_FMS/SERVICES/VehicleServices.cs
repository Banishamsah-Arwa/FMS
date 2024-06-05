using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Npgsql;
using FPro;
using System.Collections.Concurrent;
using System.Data;
using System.Collections.Generic;

namespace ARWABANISHAMSAH_FMS.SERVICES
{
   
    public class VehicleService
    {

        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";

        public async Task<GVAR> getAllVehicles()
        {
var Gvar=new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM Vehicles", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable vehicleTable = new DataTable();
            vehicleTable.Columns.Add("VehicleID", typeof(long));
            vehicleTable.Columns.Add("VehicleNumber", typeof(long));
            vehicleTable.Columns.Add("VehicleType", typeof(string));

            while (await reader.ReadAsync())
            {
                DataRow row = vehicleTable.NewRow();
                row["VehicleID"] = reader.GetInt64(0);
                row["VehicleNumber"] = reader.GetInt64(1);
                row["VehicleType"] = reader.GetString(2);
                vehicleTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Vehicles"] = vehicleTable;

            return Gvar;
        }


        public async Task AddVehicle(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleNumber", out var vehicleNumber);
            tags.TryGetValue("VehicleType", out var vehicleType);

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("INSERT INTO Vehicles (VehicleNumber, VehicleType) VALUES (@VehicleNumber, @VehicleType)", conn);
            {cmd.Parameters.AddWithValue("VehicleNumber",long.Parse(vehicleNumber));
            cmd.Parameters.AddWithValue("VehicleType", vehicleType);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteVehicle(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            long.TryParse(vehicleID, out long vehicleIDValue);

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var transaction = await conn.BeginTransactionAsync();

            try
            {
                using (var cmd = new NpgsqlCommand("DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID", conn))
                {
                    cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                    cmd.Transaction = transaction;
                    await cmd.ExecuteNonQueryAsync();
                }

                using (var cmd = new NpgsqlCommand("DELETE FROM RouteHistory WHERE VehicleID = @VehicleID", conn))
                {
                    cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                    cmd.Transaction = transaction;
                    await cmd.ExecuteNonQueryAsync();
                }

                using (var cmd = new NpgsqlCommand("DELETE FROM Vehicles WHERE VehicleID = @VehicleID", conn))
                {
                    cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                    cmd.Transaction = transaction;
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task UpdateVehicle(GVAR Gvar)
        {

           
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            tags.TryGetValue("VehicleNumber", out var vehicleNumber);
            tags.TryGetValue("VehicleType", out var vehicleType);
            long.TryParse(vehicleNumber, out long vehicleNumberValue);
            long.TryParse(vehicleID, out long vehicleIDValue);

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("UPDATE Vehicles SET VehicleNumber = @VehicleNumber, VehicleType = @VehicleType WHERE VehicleID = @VehicleID", conn);
            {cmd.Parameters.AddWithValue("VehicleNumber", vehicleNumberValue);
            cmd.Parameters.AddWithValue("VehicleType", vehicleType);
            cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
