using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using SimpleParkingAdmin.Utils;
using Serilog;
using Serilog.Events;

namespace SimpleParkingAdmin
{
    public partial class DatabaseChecker : Form
    {
        private readonly IAppLogger _logger = CustomLogManager.GetLogger();

        static void Main()
        {
            Console.WriteLine("Database User Check Utility");
            Console.WriteLine("===========================");
            
            try
            {
                // Prompt for connection details
                Console.Write("Server (default: localhost): ");
                string server = Console.ReadLine();
                if (string.IsNullOrEmpty(server)) server = "localhost";
                
                Console.Write("Port (default: 3306): ");
                string portStr = Console.ReadLine();
                int port = string.IsNullOrEmpty(portStr) ? 3306 : int.Parse(portStr);
                
                Console.Write("Database (default: parking_system): ");
                string database = Console.ReadLine();
                if (string.IsNullOrEmpty(database)) database = "parking_system";
                
                Console.Write("Username (default: root): ");
                string username = Console.ReadLine();
                if (string.IsNullOrEmpty(username)) username = "root";
                
                Console.Write("Password: ");
                string password = Console.ReadLine();
                
                // Build connection string
                string connectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";
                
                // Test connection
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    Console.WriteLine("\nConnecting to database...");
                    connection.Open();
                    Console.WriteLine("Connected successfully!");
                    
                    // Check for users tables
                    Console.WriteLine("\nChecking for users tables...");
                    
                    // First check users table (new schema)
                    CheckUsersTable(connection, "users");
                    
                    // Then check t_user table (old schema)
                    CheckUsersTable(connection, "t_user");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void CheckUsersTable(MySqlConnection connection, string tableName)
        {
            try
            {
                // First check if table exists
                string checkTableQuery = $"SHOW TABLES LIKE '{tableName}'";
                using (MySqlCommand cmd = new MySqlCommand(checkTableQuery, connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        Console.WriteLine($"Table '{tableName}' does not exist.");
                        return;
                    }
                }
                
                Console.WriteLine($"\nFound table: '{tableName}'");
                
                // Get user records
                string userQuery = $"SELECT * FROM {tableName}";
                using (MySqlCommand cmd = new MySqlCommand(userQuery, connection))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    Console.WriteLine($"Found {dt.Rows.Count} users in '{tableName}':");
                    
                    if (dt.Rows.Count > 0)
                    {
                        // Determine column names based on table schema
                        string idColumn = tableName == "users" ? "id" : "id";
                        string usernameColumn = "username";  // Both tables use this
                        string roleColumn = tableName == "users" ? "level" : "role";
                        string nameColumn = "nama";
                        
                        // Show user details
                        foreach (DataRow row in dt.Rows)
                        {
                            // Handle potential missing columns
                            string id = dt.Columns.Contains(idColumn) ? row[idColumn].ToString() : "N/A";
                            string user = dt.Columns.Contains(usernameColumn) ? row[usernameColumn].ToString() : "N/A";
                            string role = dt.Columns.Contains(roleColumn) ? row[roleColumn].ToString() : "N/A";
                            string name = dt.Columns.Contains(nameColumn) ? row[nameColumn].ToString() : "N/A";
                            
                            Console.WriteLine($"  - ID: {id}, Username: {user}, Role: {role}, Name: {name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking '{tableName}': {ex.Message}");
            }
        }
    }
} 