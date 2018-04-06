namespace KalikoCMS.Data {
    using Entities;
    using Microsoft.EntityFrameworkCore;

    internal abstract class CmsContextBase : DbContext {
        internal DbSet<ContentAccessRightsEntity> ContentAccessRights { get; set; }
        internal DbSet<ContentEntity> Content { get; set; }
        internal DbSet<ContentLanguageEntity> ContentLanguages { get; set; }
        internal DbSet<ContentPropertyEntity> ContentProperties { get; set; }
        internal DbSet<ContentProviderEntity> ContentProviders { get; set; }
        internal DbSet<ContentTypeEntity> ContentTypes { get; set; }
        internal DbSet<LanguageEntity> Languages { get; set; }
        internal DbSet<PropertyEntity> Properties { get; set; }
        internal DbSet<PropertyTypeEntity> PropertyTypes { get; set; }
        internal DbSet<RedirectEntity> Redirects { get; set; }
        internal DbSet<SystemInformationEntity> SystemInformation { get; set; }
        internal DbSet<TagContextEntity> TagContexts { get; set; }
        internal DbSet<TagEntity> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContentTagEntity>()
                .ToTable("ContentTags")
                .HasKey(x => new {x.ContentId, x.TagId});
        }
    }
}