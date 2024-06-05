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
    public class GeofenceService
    {
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=arwabanishamsa_fms;Username=postgres;Password=123456;";

        public async Task<GVAR> getAllGeofences()
        {
            var Gvar = new GVAR();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM Geofences", conn);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            DataTable GeofencesTable = new DataTable();
            GeofencesTable.Columns.Add("GeofenceID", typeof(long));
            GeofencesTable.Columns.Add("GeofenceType", typeof(string));
            GeofencesTable.Columns.Add("AddedDate", typeof(long));
            GeofencesTable.Columns.Add("StrokeColor", typeof(string));
            GeofencesTable.Columns.Add("StrokeOpacity", typeof(long));
            GeofencesTable.Columns.Add("StrokeWeight", typeof(long));
            GeofencesTable.Columns.Add("FillColor", typeof(string));
            GeofencesTable.Columns.Add("FillOpacity", typeof(long));

            while (await reader.ReadAsync())
            {
                DataRow row = GeofencesTable.NewRow();
                row["GeofenceID"]= reader.GetInt64(0);
                row["GeofenceType"]= reader.GetString(1);
                row["AddedDate"]= reader.GetInt64(2);
                row["StrokeColor"]= reader.GetString(3);
                row["StrokeOpacity"]= reader.GetInt64(4);
                row["StrokeWeight"]= reader.GetInt64(5);
                row["FillColor"]= reader.GetString(6);
                row["FillOpacity"] = reader.GetInt64(7);
                GeofencesTable.Rows.Add(row);
            }

            Gvar.DicOfDT["Geofences"] = GeofencesTable;

            return Gvar;
        }
    }
}
