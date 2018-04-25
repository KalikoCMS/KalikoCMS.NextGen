namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Entities;
    using Infrastructure;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Serialization;

    public class ContentRepository : RepositoryBase<ContentEntity, Guid>, IContentRepository {
        private readonly CmsContext _context;

        public ContentRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentEntity GetById(Guid id) {
            return _context.Set<ContentEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentId == id);
        }

        public IEnumerable<ContentNode> GetContentNodes() {
            return from content in _context.Content
                join contentLanguage in 
                    _context.ContentLanguages.Where(x => x.DeletedDate == null && (x.Status == ContentStatus.Published || (x.Status == ContentStatus.WorkingCopy && x.CurrentVersion == 0)))
                    on content.ContentId equals contentLanguage.ContentId into grouped
                orderby content.TreeLevel
                select new ContentNode() {
                    ContentId = content.ContentId,
                    SortOrder = content.SortOrder,
                    TreeLevel = content.TreeLevel,
                    ParentId = content.ParentId,
                    ContentTypeId = content.ContentTypeId,
                    Languages = (from contentLanguage in grouped
                        select new LanguageNode {
                            ContentLanguageId = contentLanguage.ContentLanguageId,
                            ContentName = contentLanguage.ContentName,
                            ChildSortOrder = contentLanguage.ChildSortOrder,
                            StopPublish = contentLanguage.StopPublish,
                            UpdateDate = contentLanguage.UpdateDate,
                            ChildSortDirection = contentLanguage.ChildSortDirection,
                            Status = contentLanguage.Status,
                            Author = contentLanguage.Author,
                            StartPublish = contentLanguage.StartPublish,
                            CreatedDate = contentLanguage.CreatedDate,
                            VisibleInSitemap = contentLanguage.VisibleInSitemap,
                            CurrentVersion = contentLanguage.CurrentVersion,
                            UrlSegment = contentLanguage.UrlSegment,
                            VisibleInMenu = contentLanguage.VisibleInMenu,
                            LanguageId = contentLanguage.LanguageId
                        }).ToList()
                };
        }

        public void SaveContent(Content content) {
            using (var transaction = _context.Database.BeginTransaction()) {
                try {
                    if (content.CurrentVersion == -1) {
                        // TODO: Map
                        var contentEntity = new ContentEntity {
                            //ContentId = content.ContentId,
                            ParentId = content.ParentId,
                            TreeLevel = content.TreeLevel,
                            ContentTypeId = content.ContentTypeId,
                            SortOrder = content.SortOrder
                        };
                        Create(contentEntity);

                        content.ContentId = contentEntity.ContentId;
                        content.CurrentVersion = 0;

                        var contentLanguageEntity = new ContentLanguageEntity {
                            Author = content.Author,
                            ChildSortDirection = content.ChildSortDirection,
                            ChildSortOrder = content.ChildSortOrder,
                            ContentId = contentEntity.ContentId,
                            ContentName = content.ContentName,
                            CreatedDate = content.CreatedDate,
                            CurrentVersion = content.CurrentVersion,
                            IsOriginal = true,
                            LanguageId = content.LanguageId,
                            StartPublish = content.StartPublish,
                            Status = content.Status,
                            StopPublish = content.StopPublish,
                            UpdateDate = content.UpdateDate,
                            UrlSegment = content.UrlSegment,
                            VisibleInMenu = content.VisibleInMenu,
                            VisibleInSitemap = content.VisibleInSitemap
                        };
                        _context.ContentLanguages.Add(contentLanguageEntity);
                        _context.SaveChanges();
                    }
                    else {
                        var contentLanguageEntity = _context.ContentLanguages.FirstOrDefault(x => x.ContentLanguageId == content.ContentLanguageId && x.Status == ContentStatus.WorkingCopy);
                        if (contentLanguageEntity == null) {
                            contentLanguageEntity = _context.ContentLanguages.FirstOrDefault(x => x.ContentId == content.ContentId && x.LanguageId == content.LanguageId && x.Status == ContentStatus.WorkingCopy);
                        }

                        if (contentLanguageEntity == null) {
                            contentLanguageEntity = _context.ContentLanguages.OrderByDescending(x => x.CurrentVersion).FirstOrDefault(x => x.ContentId == content.ContentId && x.LanguageId == content.LanguageId);

                            contentLanguageEntity = new ContentLanguageEntity {
                                LanguageId = content.LanguageId,
                                ContentId = content.ContentId,
                                CurrentVersion = contentLanguageEntity.CurrentVersion + 1,
                                Status = ContentStatus.WorkingCopy,
                                IsOriginal = contentLanguageEntity.IsOriginal,
                                DeletedDate = contentLanguageEntity.DeletedDate,
                            };
                            _context.ContentLanguages.Add(contentLanguageEntity);
                        }

                        contentLanguageEntity.Author = content.Author;
                        contentLanguageEntity.ChildSortDirection = content.ChildSortDirection;
                        contentLanguageEntity.ChildSortOrder = content.ChildSortOrder;
                        contentLanguageEntity.ContentName = content.ContentName;
                        contentLanguageEntity.CreatedDate = content.CreatedDate;
                        contentLanguageEntity.IsOriginal = contentLanguageEntity.IsOriginal;
                        contentLanguageEntity.DeletedDate = contentLanguageEntity.DeletedDate;
                        contentLanguageEntity.StartPublish = content.StartPublish;
                        contentLanguageEntity.StopPublish = content.StopPublish;
                        contentLanguageEntity.UpdateDate = content.UpdateDate;
                        contentLanguageEntity.UrlSegment = content.UrlSegment;
                        contentLanguageEntity.VisibleInMenu = content.VisibleInMenu;
                        contentLanguageEntity.VisibleInSitemap = content.VisibleInSitemap;
                        _context.Update(contentLanguageEntity);
                        _context.SaveChanges();
                    }

                    var properties = _context.ContentProperties.Where(x => x.ContentId == content.ContentId && x.LanguageId == content.LanguageId && x.Version == content.CurrentVersion);
                    foreach (var property in content.Property) {
                        var contentPropertyEntity = properties.FirstOrDefault(x => x.PropertyId == property.PropertyId);
                        if (contentPropertyEntity == null) {
                            contentPropertyEntity = new ContentPropertyEntity {
                                ContentId = content.ContentId,
                                LanguageId = content.LanguageId,
                                PropertyId = property.PropertyId,
                                ContentData = JsonSerialization.SerializeJson(property.Value),
                                Version = content.CurrentVersion
                            };
                            _context.ContentProperties.Add(contentPropertyEntity);
                        }
                        else {
                            contentPropertyEntity.ContentData = JsonSerialization.SerializeJson(property.Value);
                        }
                    }
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception exception) {
                    transaction.Rollback();
                    throw;
                }

            }
        }
    }
}