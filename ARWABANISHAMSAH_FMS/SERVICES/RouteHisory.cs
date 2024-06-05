using FPro;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ARWABANISHAMSAH_FMS.SERVICES
{
    public class RouteHisory
    {

        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";
  

        public async Task AddRouteHistory(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleIDValue);
            tags.TryGetValue("VehicleDirection", out var vehicleDirectionValue);
            tags.TryGetValue("Status", out var statusValue);
            tags.TryGetValue("VehicleSpeed", out var vehicleSpeedValue);
            tags.TryGetValue("Epoch", out var epochValue);
            tags.TryGetValue("Address", out var addressValue);
            tags.TryGetValue("Longitude", out var longitudeValue);
            tags.TryGetValue("Latitude", out var latitudeValue);

            double longitude = double.Parse(longitudeValue);
            double latitude = double.Parse(latitudeValue);
            char status = statusValue[0];
            long vehicleId = long.Parse(vehicleIDValue);
            int vehicleDirection = int.Parse(vehicleDirectionValue);
            DateTimeOffset.TryParse(epochValue, out var epoch);
            long epochVal = epoch.ToUnixTimeMilliseconds();


            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmdInsertRouteHistory = new NpgsqlCommand("INSERT INTO RouteHistory (VehicleID, VehicleDirection, Status, VehicleSpeed, Epoch, Address, Latitude, Longitude) VALUES (@VehicleID, @VehicleDirection, @Status, @VehicleSpeed, @Epoch, @Address, @Latitude, @Longitude)", conn);
            {
                cmdInsertRouteHistory.Parameters.AddWithValue("VehicleID", vehicleId);
                cmdInsertRouteHistory.Parameters.AddWithValue("VehicleDirection", vehicleDirection);
                cmdInsertRouteHistory.Parameters.AddWithValue("Status", status);
                cmdInsertRouteHistory.Parameters.AddWithValue("VehicleSpeed", vehicleSpeedValue);
                cmdInsertRouteHistory.Parameters.AddWithValue("Epoch", epochVal);
                cmdInsertRouteHistory.Parameters.AddWithValue("Address", addressValue);
                cmdInsertRouteHistory.Parameters.AddWithValue("Latitude", latitude);
                cmdInsertRouteHistory.Parameters.AddWithValue("Longitude", longitude);
                await cmdInsertRouteHistory.ExecuteNonQueryAsync();
            }
        }

        public async Task<GVAR> GetRouteHistoryByVehicleId(int id)
        {

            GVAR Gvar = new GVAR();
            DataTable routeHistoryTable = new DataTable();
            routeHistoryTable.Columns.Add("RouteHistoryID", typeof(long));
            routeHistoryTable.Columns.Add("VehicleID", typeof(long));
            routeHistoryTable.Columns.Add("VehicleDirection", typeof(int));
            routeHistoryTable.Columns.Add("Status", typeof(string));  
            routeHistoryTable.Columns.Add("VehicleSpeed", typeof(string));
            routeHistoryTable.Columns.Add("Epoch", typeof(string));
            routeHistoryTable.Columns.Add("Address", typeof(string));
            routeHistoryTable.Columns.Add("Longitude", typeof(float));
            routeHistoryTable.Columns.Add("Latitude", typeof(float));


            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new NpgsqlCommand("SELECT RouteHistoryID, VehicleID, VehicleDirection, Status, VehicleSpeed, Epoch, Address, Latitude, Longitude FROM RouteHistory WHERE VehicleID = @VehicleID ORDER BY Epoch", conn);
            {
                cmd.Parameters.AddWithValue("VehicleID", id);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    DataRow row = routeHistoryTable.NewRow();
                    long epochTime = reader.GetInt64(5);
                    DateTime epochtime = DateTimeOffset.FromUnixTimeSeconds(epochTime).DateTime;
                    row["RouteHistoryID"] = reader.GetInt64(0);
                    row["VehicleID"] = reader.GetInt64(1);
                    row["VehicleDirection"] = reader.GetInt32(2);
                    row["Status"] = reader.GetString(3); 
                    row["VehicleSpeed"] = reader.GetString(4);
                    row["Epoch"] = epochtime;
                    row["Address"] = reader.GetString(6);
                    row["Longitude"] = reader.GetFloat(7);
                    row["Latitude"] = reader.GetFloat(8);
                    routeHistoryTable.Rows.Add(row);
                }
            }

            Gvar.DicOfDT["RouteHistory"] = routeHistoryTable;

            return Gvar;
        }
    }

}
