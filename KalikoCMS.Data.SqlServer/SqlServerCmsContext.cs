namespace KalikoCMS.Data.SqlServer {
    using Microsoft.EntityFrameworkCore;

    internal class SqlServerCmsContext : CmsContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("CONNECTIONGOESHERE");
        }
    }
}
