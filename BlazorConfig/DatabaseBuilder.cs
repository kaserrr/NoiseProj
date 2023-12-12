using Microsoft.Extensions.Logging;
using System;
using System.Data.SQLite;
using System.IO;

namespace Database
{
    public static class DatabaseBuilder
    {
        private const string DbFileName = "Files/config.db"; // Update with your actual database file name

        public static void EnsureDatabase(ILogger logger)
        {
            if (!File.Exists(DbFileName))
            {
                logger.LogInformation("Database file does not exist. Creating...");
                CreateDatabase(logger);
            }
            else
            {
                logger.LogInformation("Database file already exists.");
            }
        }

        private static void CreateDatabase(ILogger logger)
        {
            logger.LogInformation("Creating database...");

            SQLiteConnection.CreateFile(DbFileName);

            using (var connection = new SQLiteConnection($"Data Source={DbFileName};Version=3;"))
            {
                connection.Open();

                // Create your tables or perform any other initialization logic here
                var createTableCommand = @"
            CREATE TABLE IF NOT EXISTS ConfigurationData (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserName TEXT
            );

            CREATE TABLE IF NOT EXISTS Sensors (
                SensorId INTEGER PRIMARY KEY AUTOINCREMENT,
                SensorName TEXT
            );
        ";

                using (var command = new SQLiteCommand(createTableCommand, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            logger.LogInformation("Database creation complete.");
        }

    }
}