using FPro;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARWABANISHAMSAH_FMS.SERVICES
{
    public class VehicleInformationServices
    {
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";




        public async Task AddVehicleInformation(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            tags.TryGetValue("DriverID", out var driverID);
            tags.TryGetValue("VehicleMake", out var vehicleMake);
            tags.TryGetValue("VehicleModel", out var vehicleModel);
            tags.TryGetValue("PurchaseDate", out var purchaseDate);

            long.TryParse(vehicleID, out long vehicleIDValue);
            long.TryParse(driverID, out long driverIDValue);
            DateTimeOffset.TryParse(purchaseDate, out var purchaseDateValue);
            long purchaseDateEpoch = purchaseDateValue.ToUnixTimeMilliseconds();


            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("INSERT INTO VehiclesInformations (VehicleID, DriverID, VehicleMake, VehicleModel, PurchaseDate) " +
                                              "VALUES (@VehicleID, @DriverID, @VehicleMake, @VehicleModel, @PurchaseDate)", conn);
            {
                cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                cmd.Parameters.AddWithValue("DriverID", driverIDValue);
                cmd.Parameters.AddWithValue("VehicleMake", vehicleMake);
                cmd.Parameters.AddWithValue("VehicleModel", vehicleModel);
                cmd.Parameters.AddWithValue("PurchaseDate", purchaseDateEpoch);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteVehicleInformation(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            long.TryParse(vehicleID, out long vehicleIDValue);

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID", conn);
            {
                cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateVehicleInformation(GVAR Gvar)
        {


            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            tags.TryGetValue("DriverID", out var driverID);
            tags.TryGetValue("VehicleMake", out var vehicleMake);
            tags.TryGetValue("VehicleModel", out var vehicleModel);
            tags.TryGetValue("PurchaseDate", out var purchaseDate);

            long.TryParse(vehicleID, out long vehicleIDValue);
            long.TryParse(driverID, out long driverIDValue);
            DateTimeOffset.TryParse(purchaseDate, out var purchaseDateValue);
            long purchaseDateEpoch = purchaseDateValue.ToUnixTimeMilliseconds();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("UPDATE VehiclesInformations SET DriverID = @DriverID, VehicleMake = @VehicleMake, VehicleModel = @VehicleModel, PurchaseDate = @PurchaseDate WHERE VehicleID = @VehicleID", conn);
            {
                cmd.Parameters.AddWithValue("DriverID", driverIDValue);
                cmd.Parameters.AddWithValue("VehicleMake", vehicleMake);
                cmd.Parameters.AddWithValue("VehicleModel", vehicleModel);
                cmd.Parameters.AddWithValue("PurchaseDate", purchaseDateEpoch);
                cmd.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateDriverID(GVAR Gvar)
        {


            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            tags.TryGetValue("DriverName", out var driverName);
            long.TryParse(vehicleID, out long vehicleIDValue);

            long driverIDValue =0;

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new NpgsqlCommand("SELECT DriverID FROM Driver WHERE DriverName = @DriverName", connection);
            {
                cmd.Parameters.AddWithValue("DriverName", driverName);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    driverIDValue = reader.GetInt64(0);

                }
            }

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var command = new NpgsqlCommand("UPDATE VehiclesInformations SET DriverID = @DriverID WHERE VehicleID = @VehicleID", conn);
            {
                command.Parameters.AddWithValue("DriverID", driverIDValue);
                command.Parameters.AddWithValue("VehicleID", vehicleIDValue);
                await command.ExecuteNonQueryAsync();
            }
        }


    }
}
