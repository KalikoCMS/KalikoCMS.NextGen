namespace KalikoCMS.Data.SqlServer {
    using Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class InitializeDatabase : IStartupSequence {
        public void Startup() {
            EnsureDatabaseIsLatestVersion();
        }

        private void EnsureDatabaseIsLatestVersion() {
            var context = new SqlServerCmsContext();
            context.Database.Migrate();
        }

        public int StartupOrder => -1000;
    }
}