namespace KalikoCMS.Data.SqlServer {
    using Microsoft.EntityFrameworkCore;

    internal class CmsContext : CmsContextBase {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("CONNECTIONGOESHERE");
        }
    }
}
