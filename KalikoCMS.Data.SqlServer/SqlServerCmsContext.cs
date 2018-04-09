namespace KalikoCMS.Data.SqlServer {
    using Microsoft.EntityFrameworkCore;

    public class SqlServerCmsContext : CmsContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=NextGen;Data Source=(localdb)\\v11.0");
        }
    }
}
