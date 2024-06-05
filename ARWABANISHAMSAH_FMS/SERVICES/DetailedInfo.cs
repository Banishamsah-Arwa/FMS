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
    public class DetailedInfoService
    {
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";

        public async Task<GVAR> getAllVehiclesInformation()
        {
            GVAR Gvar = new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM Vehicles", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable VehiclesInformation = new DataTable();
            VehiclesInformation.Columns.Add("VehicleID", typeof(long));
            VehiclesInformation.Columns.Add("VehicleNumber", typeof(long));
            VehiclesInformation.Columns.Add("VehicleType", typeof(string));
            VehiclesInformation.Columns.Add("LastDirection", typeof(long));
            VehiclesInformation.Columns.Add("LastStatus", typeof(char));
            VehiclesInformation.Columns.Add("LastAddress", typeof(string));
            VehiclesInformation.Columns.Add("LastLatitude", typeof(long));
            VehiclesInformation.Columns.Add("LastLongitude", typeof(long));

            while (await reader.ReadAsync())
            {
                long vehicleID = reader.GetInt64(0);
                DataRow row = VehiclesInformation.NewRow();
                row["VehicleID"] = reader.GetInt64(0);
                row["VehicleNumber"] = reader.GetInt64(1);
                row["VehicleType"] = reader.GetString(2);

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT  VehicleDirection, Status, Address, Latitude, Longitude FROM RouteHistory WHERE VehicleID = @VehicleID ORDER BY Epoch DESC LIMIT 1", connection))
                    {
                        command.Parameters.AddWithValue("VehicleID", vehicleID);

                        using (var readerRouteHistory = await command.ExecuteReaderAsync())
                        {
                            if (await readerRouteHistory.ReadAsync())
                            {
                                row["LastDirection"] = readerRouteHistory.GetInt64(0);
                                row["LastStatus"] = readerRouteHistory.GetChar(1);
                                row["LastAddress"] = readerRouteHistory.GetString(2);
                                row["LastLatitude"] = readerRouteHistory.GetInt64(3);
                                row["LastLongitude"] = readerRouteHistory.GetInt64(4);
                            }
                        }
                    }
                }

                VehiclesInformation.Rows.Add(row);
            }

            Gvar.DicOfDT["Vehicles Information"] = VehiclesInformation;

            return Gvar;
        }







        public async Task<GVAR> getDetailedInfo(int id)
        {
            GVAR Gvar = new GVAR();
           
             long vehicleIDValue=id;

            long driverID =0;

            DataTable VehiclesInformation = new DataTable();
            VehiclesInformation.Columns.Add("VehicleNumber", typeof(long));
            VehiclesInformation.Columns.Add("VehicleType", typeof(string));
            VehiclesInformation.Columns.Add("DriverName", typeof(string));
            VehiclesInformation.Columns.Add("PhoneNumber", typeof(long));
            VehiclesInformation.Columns.Add("LastPosition", typeof(string));
            VehiclesInformation.Columns.Add("VehicleMake", typeof(string));
            VehiclesInformation.Columns.Add("VehicleModel", typeof(string));
            VehiclesInformation.Columns.Add("LastGPSTime", typeof(DateTime));
            VehiclesInformation.Columns.Add("LastGPSSpeed", typeof(string));
            VehiclesInformation.Columns.Add("LastAddress", typeof(string));

            DataRow row = VehiclesInformation.NewRow();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT VehicleNumber,VehicleType  from  Vehicles where VehicleID=@vehicleID ", conn);
            cmd.Parameters.AddWithValue("vehicleID", vehicleIDValue);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

       
            while (await reader.ReadAsync())
            {
                row["VehicleNumber"] = reader.GetInt64(0);
                row["VehicleType"] = reader.GetString(1);

            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(" SELECT DriverId,VehicleMake, VehicleModel FROM  VehiclesInformations WHERE VehicleID = @vehicleID", connection))
                {
                    command.Parameters.AddWithValue("VehicleID", vehicleIDValue);

                    using (var readerInfo = await command.ExecuteReaderAsync())
                    {
                        if (await readerInfo.ReadAsync())
                        { 
                            driverID=readerInfo.GetInt64(0);
                            row["VehicleMake"] = readerInfo.GetString(1);
                            row["VehicleModel"] = readerInfo.GetString(2);
                      

                        }
                    }
                }
            }

            using (var connectiondriver = new NpgsqlConnection(_connectionString))
            {
                await connectiondriver.OpenAsync();

                using (var command = new NpgsqlCommand(" SELECT DriverName,PhoneNumber FROM  Driver WHERE DriverID = @driverID", connectiondriver))
                {
                     command.Parameters.AddWithValue("driverID", driverID);

                    using (var readerDriver = await command.ExecuteReaderAsync())
                    {
                        if (await readerDriver.ReadAsync())
                        {
                            row["DriverName"] = readerDriver.GetString(0);
                            row["PhoneNumber"] = readerDriver.GetInt64(1);
                        }
                    }
                }
            }
            
             using (var connectionRoute = new NpgsqlConnection(_connectionString))
            {
                await connectionRoute.OpenAsync();

                using (var command = new NpgsqlCommand(" SELECT vehiclespeed, Epoch,Address, Latitude,Longitude FROM  RouteHistory WHERE VehicleID = @VehicleID ORDER BY Epoch DESC LIMIT 1", connectionRoute))
                {
                    command.Parameters.AddWithValue("VehicleID", vehicleIDValue);

                    using (var readertwo = await command.ExecuteReaderAsync())
                    {
                        if (await readertwo.ReadAsync())
                        {
                            long epochTime = readertwo.GetInt64(1);
                            DateTime lastGPSTime = DateTimeOffset.FromUnixTimeSeconds(epochTime).DateTime;
                            row["LastGPSTime"] = lastGPSTime;
                            row["LastGPSSpeed"] = readertwo.GetString(0);
                            row["LastAddress"] = readertwo.GetString(2);
                            string latitude = readertwo.GetInt64(3).ToString();
                            string longitude = readertwo.GetInt64(4).ToString();
                            row["LastPosition"] = $"{latitude}, {longitude}";

                        }
                    }
                }
            }
            


            VehiclesInformation.Rows.Add(row);

            Gvar.DicOfDT["Spicific Vehicle Information"] = VehiclesInformation;

            return Gvar;
        }



        public async Task<GVAR> getInfoRangeTime(GVAR Gvar)
        {
            Gvar.DicOfDic.TryGetValue("Tags", out var tags);
            tags.TryGetValue("VehicleID", out var vehicleID);
            tags.TryGetValue("StartEpoch", out var startEpoch);
            tags.TryGetValue("EndEpoch", out var endEpoch);
            long.TryParse(vehicleID, out long vehicleIDValue);
            long.TryParse(startEpoch, out long startEpochValue);
            long.TryParse(endEpoch, out long endEpochValue);



            DataTable VehicleRangeTtime = new DataTable();
            VehicleRangeTtime.Columns.Add("VehicleID", typeof(long));
            VehicleRangeTtime.Columns.Add("VehicleNumber", typeof(long));
            VehicleRangeTtime.Columns.Add("Address", typeof(string));
            VehicleRangeTtime.Columns.Add("Status", typeof(char));
            VehicleRangeTtime.Columns.Add("Latitude", typeof(long));
            VehicleRangeTtime.Columns.Add("Longitude", typeof(long));
            VehicleRangeTtime.Columns.Add("VehicleDirection", typeof(long));
            VehicleRangeTtime.Columns.Add("GPSSpeed", typeof(string));
            VehicleRangeTtime.Columns.Add("GPSTime", typeof(DateTime));



            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(
                    "SELECT vehicles.vehicleNumber ," +
                    "RouteHistory.address," +
                    "RouteHistory.status," +
                    "RouteHistory.latitude," +
                    "RouteHistory.Longitude ," +
                    "RouteHistory.vehicleDirection," +
                    "RouteHistory.vehiclespeed," +
                    "RouteHistory.epoch " +
                    " from Vehicles" +
                    " Join RouteHistory ON Vehicles.VehicleID =RouteHistory.VehicleID " +
                    "WHERE Vehicles.VehicleID=@vehicleID " +
                    "AND RouteHistory.Epoch BETWEEN @StartEpoch AND @EndEpoch " +
                    "ORDER BY RouteHistory.Epoch; ", connection))
                {
                    command.Parameters.AddWithValue("vehicleID", vehicleIDValue);
                    command.Parameters.AddWithValue("StartEpoch", startEpochValue);
                    command.Parameters.AddWithValue("EndEpoch", endEpochValue);

                    using (var readertwo = await command.ExecuteReaderAsync())
                    {
                        while (await readertwo.ReadAsync())
                        {
                            DataRow row = VehicleRangeTtime.NewRow(); 

                            long epochTime = readertwo.GetInt64(7);
                            DateTime GPSTime = DateTimeOffset.FromUnixTimeSeconds(epochTime).DateTime;
                            row["VehicleID"] = vehicleIDValue;
                            row["VehicleNumber"] = readertwo.GetInt64(0);
                            row["Address"] = readertwo.GetString(1);
                            row["Status"] = readertwo.GetChar(2) ;
                            row["Latitude"] = readertwo.GetInt64(3);
                            row["Longitude"] = readertwo.GetInt64(4);
                            row["VehicleDirection"] = readertwo.GetInt64(5);
                            row["GPSSpeed"] = readertwo.GetString(6);
                            row["GPSTime"] = GPSTime;
                            VehicleRangeTtime.Rows.Add(row);
                        }
                    }

                }
            }
            Gvar.DicOfDT["Vehicle in a specified time range"] = VehicleRangeTtime;
            return Gvar;
        }
    }
}
