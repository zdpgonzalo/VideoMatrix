using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoMatrixSystem.Domain.Entities;

namespace VideoMatrixSystem.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public static readonly object ContextLock = new();

        /// <summary>
        /// List of Transmitters
        /// </summary>
        public DbSet<Transmitter> Transmitters { get; set; }

        /// <summary>
        /// List of Recievers
        /// </summary>
        public DbSet<Receiver> Receivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            lock (ContextLock)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    //var connectionString = "server=localhost;port=3306;database=videomatrixdb;user=root;password=videomatrixpass;";

                    //var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

                    //optionsBuilder.UseMySql(connectionString, serverVersion)
                    //        .LogTo(Console.WriteLine, LogLevel.Information)
                    //        .EnableSensitiveDataLogging()
                    //        .EnableDetailedErrors();

                    var DataBaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VideoMatrixDatabase");
                    Directory.CreateDirectory(DataBaseDirectory);

                    optionsBuilder.UseSqlite($"Data Source={Path.Combine(DataBaseDirectory, "LocalSQLiteDB.db")}");
                }

            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            lock (ContextLock)
            {
                try
                {
                    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public override int SaveChanges()
        {
            lock (ContextLock)
            {
                try
                {
                    return base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];

                            if (databaseValues != null)
                            {
                                var databaseValue = databaseValues[property];
                            }
                            else
                            {
                                proposedValues[property] = proposedValue;
                            }

                            // TODO: decide which value should be written to database
                            // proposedValues[property] = <value to be saved>;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(proposedValues);


                    }
                    return 0;
                }
                catch (DbUpdateException ex)
                {
                    var failedEntries = ex.Entries;
                    foreach (var entry in failedEntries)
                    {
                        var entityName = entry.Metadata.Name;
                        var properties = entry.Properties.Where(p => p.IsModified && !p.IsTemporary);
                        foreach (var property in properties)
                        {
                            var propertyName = property.Metadata.Name;
                            Console.WriteLine($"Failed to update field: {propertyName} in entity: {entityName}");
                        }
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        
        
    }
}
