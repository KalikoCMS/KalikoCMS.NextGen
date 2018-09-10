namespace KalikoCMS.Legacy.Data {
    using Configuration.Interfaces;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using ServiceLocation;

    public class LegacyDataContext : DbContext {
        public virtual DbSet<LegacyPageEntity> Pages { get; set; }
        public virtual DbSet<LegacyPageInstanceEntity> PageInstances { get; set; }
        public virtual DbSet<LegacyPagePropertyEntity> PageProperties { get; set; }
        public virtual DbSet<LegacyPageTagEntity> PageTags { get; set; }
        public virtual DbSet<LegacyPageTypeEntity> PageTypes { get; set; }
        public virtual DbSet<LegacyPropertyEntity> Properties { get; set; }
        public virtual DbSet<LegacyPropertyTypeEntity> PropertyTypes { get; set; }
        public virtual DbSet<LegacyRedirectEntity> Redirects { get; set; }
        public virtual DbSet<LegacySiteEntity> Sites { get; set; }
        public virtual DbSet<LegacySiteLanguageEntity> SiteLanguages { get; set; }
        public virtual DbSet<LegacySitePropertyEntity> SiteProperties { get; set; }
        public virtual DbSet<LegacySitePropertyDefinitionEntity> SitePropertyDefinitions { get; set; }
        public virtual DbSet<LegacySystemInfoEntity> SystemInfo { get; set; }
        public virtual DbSet<LegacyTagEntity> Tags { get; set; }
        public virtual DbSet<LegacyTagContextEntity> TagContexts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var configuration = ServiceLocator.Current.GetInstance<ICmsConfiguration>();

            optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<LegacyPageTypeEntity>().ToTable("DataStore");
            modelBuilder.Entity<LegacyPageEntity>().ToTable("Page");
            modelBuilder.Entity<LegacyPageInstanceEntity>().ToTable("PageInstance");
            modelBuilder.Entity<LegacyPagePropertyEntity>().ToTable("PageProperty");
            modelBuilder.Entity<LegacyPageTagEntity>().ToTable("PageTag");
            modelBuilder.Entity<LegacyPageTypeEntity>().ToTable("PageType");
            modelBuilder.Entity<LegacyPropertyEntity>().ToTable("Property");
            modelBuilder.Entity<LegacyPropertyTypeEntity>().ToTable("PropertyType");
            modelBuilder.Entity<LegacyRedirectEntity>().ToTable("Redirect");
            modelBuilder.Entity<LegacySiteEntity>().ToTable("Site");
            modelBuilder.Entity<LegacySiteLanguageEntity>().ToTable("SiteLanguage");
            modelBuilder.Entity<LegacySitePropertyEntity>().ToTable("SiteProperty");
            modelBuilder.Entity<LegacySitePropertyDefinitionEntity>().ToTable("SitePropertyDefinition");
            modelBuilder.Entity<LegacySiteLanguageEntity>().ToTable("SiteLanguage");
            modelBuilder.Entity<LegacySystemInfoEntity>().ToTable("SystemInfo");
            modelBuilder.Entity<LegacyTagEntity>().ToTable("Tag");
            modelBuilder.Entity<LegacyTagContextEntity>().ToTable("TagContext");
            modelBuilder.Entity<LegacyTagEntity>().ToTable("Tag");

            modelBuilder.Entity<LegacyPageEntity>(entity => {
                entity.HasIndex(e => e.PageTypeId)
                    .HasName("IX_Page_PageTypeId");

                entity.Property(e => e.PageId).ValueGeneratedNever();

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.PageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Page_PageType");
            });

            modelBuilder.Entity<LegacyPageInstanceEntity>(entity => {
                entity.HasIndex(e => e.LanguageId)
                    .HasName("idx_PageInstance_LanguageId");

                entity.HasIndex(e => e.PageId)
                    .HasName("idx_PageInstance_PageId");

                entity.HasIndex(e => new {e.PageId, e.LanguageId})
                    .HasName("IX_Page_PageIdLanguageId");

                entity.Property(e => e.Author).HasColumnType("varchar(256)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PageUrl)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.StartPublish).HasColumnType("datetime");

                entity.Property(e => e.StopPublish).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.VisibleInMenu).HasColumnType("tinyint");

                entity.Property(e => e.VisibleInSitemap).HasColumnType("tinyint");

                entity.HasOne(d => d.SiteLanguage)
                    .WithMany(p => p.PageInstances)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageInstance_SiteLanguage");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageInstances)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageInstance_Page");
            });

            modelBuilder.Entity<LegacyPagePropertyEntity>(entity => {
                entity.HasIndex(e => e.PageId)
                    .HasName("idx_PageProperty_PageId");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("idx_PageProperty_PropertyId");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageProperties)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageProperty_Page");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PageProperties)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageProperty_Property");
            });

            modelBuilder.Entity<LegacyPageTagEntity>(entity => {
                entity.HasKey(e => new {e.PageId, e.TagId})
                    .HasName("pk_PageTag");

                entity.HasOne(e => e.Page)
                    .WithMany(p => p.PageTags)
                    .HasForeignKey(p => p.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageTag_Page");

                entity.HasOne(e => e.Tag)
                    .WithMany(p => p.PageTags)
                    .HasForeignKey(p => p.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageTag_Tag");

                entity.HasIndex(e => e.PageId)
                    .HasName("idx_PageTag_PageId");

                entity.HasIndex(e => e.TagId)
                    .HasName("idx_PageTag_TagId");
            });

            modelBuilder.Entity<LegacyPageTypeEntity>(entity => {
                entity.Property(e => e.DisplayName).HasMaxLength(50);

                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.Property(e => e.PageTemplate).HasColumnType("varchar(100)");

                entity.Property(e => e.PageTypeDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<LegacyPropertyEntity>(entity => {
                entity.HasIndex(e => e.PageTypeId)
                    .HasName("idx_Property_PageTypeId");

                entity.HasIndex(e => e.PropertyTypeId)
                    .HasName("idx_Property_PropertyTypeId");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Property_PageType");
            });

            modelBuilder.Entity<LegacyPropertyTypeEntity>(entity => {
                entity.Property(e => e.PropertyTypeId).ValueGeneratedNever();

                entity.Property(e => e.Class).HasColumnType("varchar(100)");

                entity.Property(e => e.EditControl).HasColumnType("varchar(200)");

                entity.Property(e => e.Name).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<LegacyRedirectEntity>(entity => {
                entity.HasIndex(e => new {e.UrlHash, e.Url})
                    .HasName("IX_Redirect_UrlUrlHash");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<LegacySiteEntity>(entity => {
                entity.Property(e => e.SiteId).ValueGeneratedNever();

                entity.Property(e => e.Author).HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LegacySiteLanguageEntity>(entity => {
                entity.HasKey(e => e.LanguageId)
                    .HasName("pk_SiteLanguage");

                entity.Property(e => e.LongName).HasColumnType("varchar(255)");

                entity.Property(e => e.ShortName).HasColumnType("varchar(5)");
            });

            modelBuilder.Entity<LegacySitePropertyEntity>(entity => {
                entity.HasIndex(e => e.PropertyId)
                    .HasName("idx_SiteProperty_PropertyId");

                entity.HasIndex(e => e.SiteId)
                    .HasName("idx_SiteProperty_SiteId");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.SiteProperties)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteProperty_SitePropertyDefinition");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SiteProperties)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SiteProperty_Site");
            });

            modelBuilder.Entity<LegacySitePropertyDefinitionEntity>(entity => {
                entity.HasKey(e => e.PropertyId)
                    .HasName("pk_SitePropertyDefinition");

                entity.HasIndex(e => e.PropertyTypeId)
                    .HasName("idx_StPrprtyDfntn_PrprtyTypeId");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LegacyTagEntity>(entity => {
                entity.HasIndex(e => e.TagContextId)
                    .HasName("idx_Tag_TagContextId");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TagContext)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.TagContextId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Tag_TagContext");
            });

            modelBuilder.Entity<LegacyTagContextEntity>(entity => {
                entity.HasIndex(e => e.ContextName)
                    .HasName("IX_TagContext_ContextName");

                entity.Property(e => e.ContextName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}