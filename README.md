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
- **Batch Processing:** Optimized data transfer with configurable batch sizes
- **Transaction Support:** Ensures data integrity with commit/rollback capabilities
- **Progress Tracking:** Real-time migration progress indicators

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
   - Click the "Execute Migration" button
   - Configure migration options (transaction support, batch size)
   - Monitor progress in real-time
   - View detailed logs of the migration process

### Migration Options

The migration executor provides several options to customize the migration process:

- **Use Transaction:** Wraps the entire migration in a transaction for data integrity
- **Batch Size:** Number of rows to process in a single batch (default: 1000)
- **Continue On Error:** Whether to continue migration if an error occurs


## Saving and Loading Configurations

You can save your current migration configuration by clicking the "Save" button. This creates a file that can be loaded later to quickly set up the same migration again, which is especially useful for:

- Regular scheduled migrations
- Development/testing scenarios
- Disaster recovery procedures

## Architecture

The Database Migration Tool consists of several key components:

1. **DataMigrationEngine:** Core component handling the actual data transfer between databases
2. **MigrationManager:** Coordinates the migration of multiple tables with progress reporting
3. **MigrationExecutor:** UI for configuring and monitoring migrations
4. **ExecuteMigrationButton:** Integration with the main application UI
5. **MigrationHelper:** Utility functions for migration tasks

## Technical Details

- Built with .NET Framework and WinForms
- Uses Krypton Toolkit for enhanced UI elements
- Implements parameterized queries for SQL injection protection
- Optimized for large database migrations with batch processing
- Provides comprehensive logging and error handling

## Security Features

- Parameterized queries to prevent SQL injection
- Safe handling of database credentials
- Transaction support to maintain data integrity
- Comprehensive error handling and logging

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
