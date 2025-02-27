<<<<<<< HEAD
# Database Migration Tool

A powerful cross-database migration utility that enables seamless data transfer between different database systems including Oracle, PostgreSQL, and Microsoft SQL Server.

## Features

- **Cross-Database Support:** Migrate between multiple database types
  - Oracle Database
  - PostgreSQL
  - Microsoft SQL Server
  - MySQL
- **Visual Schema Mapping:** Intuitive interface for mapping tables and columns between source and destination databases
- **Data Type Transformation:** Automatically handle data type compatibility between different database systems
- **Connection Management:** Secure storage of database connection settings
- **Configuration Files:** Save and load migration settings for reuse
- **Selective Migration:** Choose specific tables to migrate rather than entire databases

## Screenshots

*[Add actual screenshots of the application here]*

## Getting Started

### Prerequisites

- .NET Framework 4.7.2 or higher
- Database client drivers:
  - Oracle: Oracle.ManagedDataAccess client
  - PostgreSQL: Npgsql client
  - MS SQL Server: SQL Client
  - MySQL: MySQL.Data client

### Installation

1. Download the latest release from the [Releases](https://github.com/yourusername/database-migration-tool/releases) page
2. Extract the zip file to your desired location
3. Run `Convertor.exe` to start the application

## Usage

1. **Select Source Database:**
   - Choose your source database type (Oracle, PostgreSQL, MS SQL, MySQL)
   - Enter connection details (server, port, credentials)
   - Test the connection

2. **Select Destination Database:**
   - Choose your destination database type
   - Enter connection details
   - Test the connection

3. **Select Tables:**
   - Click "Next" to load tables from both databases
   - Check the tables you want to migrate
   - Map source tables to destination tables

4. **Map Fields:**
   - For each table, define field mappings between source and destination
   - The system will show data types to help ensure compatibility

5. **Execute Migration:**
   - Run the migration process
   - The tool will display progress and results

### Example Configuration Files

The tool comes with example configuration files in the `Examples_Inputs` folder that demonstrate common migration scenarios:

- MssqlToMssql.txt
- MssqlToPostges.txt
- OracleToOracle.txt
- OracleToPostgre.txt
- OracleToPostgresServer.txt
- PostgreToOracleServer.txt
- Postgres.txt
- PostgresToPostgres.txt

## Saving and Loading Configurations

You can save your current migration configuration by clicking the "Save" button. This creates a file that can be loaded later to quickly set up the same migration again, which is especially useful for:

- Regular scheduled migrations
- Development/testing scenarios
- Disaster recovery procedures

## Technical Details

- Built with .NET Framework and WinForms
- Uses Krypton Toolkit for enhanced UI elements
- Implements secure credential management
- Optimized for large database migrations

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Krypton Toolkit for UI components
- Database client libraries:
  - Oracle.ManagedDataAccess
  - Npgsql
  - MySql.Data

## Contributing

We welcome contributions! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Support

If you encounter any issues or need assistance, please create an issue on our [GitHub issue tracker](https://github.com/yourusername/database-migration-tool/issues).
=======
# Database Migration Tool

A powerful cross-database migration utility that enables seamless data transfer between different database systems including Oracle, PostgreSQL, and Microsoft SQL Server.

## Features

- **Cross-Database Support:** Migrate between multiple database types
  - Oracle Database
  - PostgreSQL
  - Microsoft SQL Server
  - MySQL
- **Visual Schema Mapping:** Intuitive interface for mapping tables and columns between source and destination databases
- **Data Type Transformation:** Automatically handle data type compatibility between different database systems
- **Connection Management:** Secure storage of database connection settings
- **Configuration Files:** Save and load migration settings for reuse
- **Selective Migration:** Choose specific tables to migrate rather than entire databases

## Screenshots


## Getting Started

### Prerequisites

- .NET Framework 4.7.2 or higher
- Database client drivers:
  - Oracle: Oracle.ManagedDataAccess client
  - PostgreSQL: Npgsql client
  - MS SQL Server: SQL Client
  - MySQL: MySQL.Data client

### Installation

1. Download the latest release from the [Releases](https://github.com/yourusername/database-migration-tool/releases) page
2. Extract the zip file to your desired location
3. Run `Convertor.exe` to start the application

## Usage

1. **Select Source Database:**
   - Choose your source database type (Oracle, PostgreSQL, MS SQL, MySQL)
   - Enter connection details (server, port, credentials)
   - Test the connection

2. **Select Destination Database:**
   - Choose your destination database type
   - Enter connection details
   - Test the connection

3. **Select Tables:**
   - Click "Next" to load tables from both databases
   - Check the tables you want to migrate
   - Map source tables to destination tables

4. **Map Fields:**
   - For each table, define field mappings between source and destination
   - The system will show data types to help ensure compatibility

5. **Execute Migration:**
   - Run the migration process
   - The tool will display progress and results

### Example Configuration Files

The tool comes with example configuration files in the `Examples_Inputs` folder that demonstrate common migration scenarios:

- MssqlToMssql.txt
- MssqlToPostges.txt
- OracleToOracle.txt
- OracleToPostgre.txt
- OracleToPostgresServer.txt
- PostgreToOracleServer.txt
- Postgres.txt
- PostgresToPostgres.txt

## Saving and Loading Configurations

You can save your current migration configuration by clicking the "Save" button. This creates a file that can be loaded later to quickly set up the same migration again, which is especially useful for:

- Regular scheduled migrations
- Development/testing scenarios
- Disaster recovery procedures

## Technical Details

- Built with .NET Framework and WinForms
- Uses Krypton Toolkit for enhanced UI elements
- Implements secure credential management
- Optimized for large database migrations

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Krypton Toolkit for UI components
- Database client libraries:
  - Oracle.ManagedDataAccess
  - Npgsql
  - MySql.Data

## Contributing

We welcome contributions! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Support

If you encounter any issues or need assistance, please create an issue on our [GitHub issue tracker](https://github.com/yourusername/database-migration-tool/issues).
>>>>>>> 58b845daa36c6e604890bb49e81fa974c04a703f
