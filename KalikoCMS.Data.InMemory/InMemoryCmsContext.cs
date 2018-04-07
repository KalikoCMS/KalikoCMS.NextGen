namespace KalikoCMS.Data.InMemory {
    using Microsoft.EntityFrameworkCore;

    public class InMemoryCmsContext : CmsContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase("KalikoCMS");
        }
    }
}