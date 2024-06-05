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
    public class DetailedGeofenceService
    {
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";

        public async Task<GVAR> getAllCircularGeofences()
        {
            var Gvar = new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM CircleGeofence", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable GeofencesTable = new DataTable();
            GeofencesTable.Columns.Add("GeofenceID", typeof(long));
            GeofencesTable.Columns.Add("Radius", typeof(long));
            GeofencesTable.Columns.Add("Latitude", typeof(long));
            GeofencesTable.Columns.Add("Logitude", typeof(long));
          

            while (await reader.ReadAsync())
            {
                DataRow row = GeofencesTable.NewRow();
                row["GeofenceID"]= reader.GetInt64(1);
                row["Radius"]= reader.GetInt64(2);
                row["Latitude"] = reader.GetInt64(3);
                row["Logitude"] = reader.GetInt64(4);
               
                GeofencesTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Circle Geofence"] = GeofencesTable;

            return Gvar;
        }

        public async Task<GVAR> getAllPolygonGeofences()
        {
            var Gvar = new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM PolygonGeofence", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable GeofencesTable = new DataTable();
            GeofencesTable.Columns.Add("GeofenceID", typeof(long));
            GeofencesTable.Columns.Add("Latitude", typeof(long));
            GeofencesTable.Columns.Add("Logitude", typeof(long));


            while (await reader.ReadAsync())
            {
                DataRow row = GeofencesTable.NewRow();
                row["GeofenceID"] = reader.GetInt64(1);
                row["Latitude"] = reader.GetInt64(2);
                row["Logitude"] = reader.GetInt64(3);

                GeofencesTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Polygon Geofence"] = GeofencesTable;

            return Gvar;
        }

        public async Task<GVAR> getAllRectangleGeofences()
        {
            var Gvar = new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM RectangleGeofence", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable GeofencesTable = new DataTable();
            GeofencesTable.Columns.Add("GeofenceID", typeof(long));
            GeofencesTable.Columns.Add("North", typeof(long));
            GeofencesTable.Columns.Add("East", typeof(long));
            GeofencesTable.Columns.Add("West", typeof(long));
            GeofencesTable.Columns.Add("South", typeof(long));



            while (await reader.ReadAsync())
            {
                DataRow row = GeofencesTable.NewRow();
                row["GeofenceID"] = reader.GetInt64(1);
                row["North"] = reader.GetInt64(2);
                row["East"] = reader.GetInt64(3);
                row["West"] = reader.GetInt64(4);
                row["South"] = reader.GetInt64(5);

                GeofencesTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Rectangle Geofence"] = GeofencesTable;

            return Gvar;
        }
    }
}
