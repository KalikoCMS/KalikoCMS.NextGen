namespace KalikoCMS.Core.Collections {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    // TODO: Store ContentReferences instead of Ids
    public sealed class ContentCollection : IEnumerable<Content> {
        private static readonly object PadLock = new object();

        private readonly int _languageId;

        public ContentCollection() {
            ContentIds = new Collection<Guid>();
        }

        public ContentCollection(Collection<Guid> contentIds) {
            ContentIds = contentIds;
        }

        public ContentCollection(IList<Guid> contentIds) {
            ContentIds = new Collection<Guid>(contentIds);
        }

        public int Count => ContentIds.Count;

        public Collection<Guid> ContentIds { get; }

        public SortDirection SortDirection { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool Sorted { get; set; }

        public void Add(Guid contentId) {
            ContentIds.Add(contentId);
        }

        public void Remove(Guid contentId) {
            ContentIds.Remove(contentId);
        }

        public bool Contains(Guid contentId) {
            return ContentIds.Contains(contentId);
        }

        public void Sort(SortOrder sortOrder, SortDirection sortDirection) {
            lock(PadLock) {
                if (IsAlreadySorted(sortOrder, sortDirection)) {
                    return;
                }

                var values = new ArrayList(ContentIds).ToArray(typeof(Guid));
                var keys = GetKeysFromParameters(sortOrder, values);

                Array.Sort(keys, values);
                ContentIds.Clear();

                if (sortDirection == SortDirection.Ascending) {
                    AddRange(values);
                }
                else {
                    AddRangeReversed(values);
                }

                Sorted = true;
                SortOrder = sortOrder;
                SortDirection = sortDirection;
            }
        }

        private void AddRange(ICollection items) {
            foreach (var item in items) {
                ContentIds.Add((Guid)item);
            }
        }

        private void AddRangeReversed(Array items) {
            for (var i = items.Length - 1; i >= 0; i--) {
                var item = items.GetValue(i);
                ContentIds.Add((Guid)item);
            }
        }

        private Array GetKeysFromParameters(SortOrder sortOrder, Array values) {
            Array keys = new ArrayList(values.Length).ToArray();

            switch (sortOrder) {
                case SortOrder.PageName:
                    return GetPageNameKeysForSort();
                case SortOrder.StartPublishDate:
                    return GetStartPublishDateKeysForSort();
                case SortOrder.SortIndex:
                    return GetSortIndexForSort();
                case SortOrder.CreatedDate:
                    return GetCreatedDateForSort();
                case SortOrder.UpdateDate:
                    return GetUpdateDateForSort();
            }

            return keys;
        }

        private Array GetUpdateDateForSort() {
            throw new NotImplementedException(); // TODO
            //return _contentIds.Select(contentId => PageFactory.GetPage(contentId).UpdateDate).ToArray();
        }

        private Array GetCreatedDateForSort() {
            throw new NotImplementedException(); // TODO
            //return _contentIds.Select(contentId => PageFactory.GetPage(contentId).CreatedDate).ToArray();
        }

        private Array GetSortIndexForSort() {
            throw new NotImplementedException(); // TODO
            //return _contentIds.Select(contentId => PageFactory.GetPage(contentId).SortIndex).ToArray();
        }

        private Array GetPageNameKeysForSort() {
            throw new NotImplementedException(); // TODO
            //return _contentIds.Select(contentId => PageFactory.GetPage(contentId).PageName).ToArray();
        }

        private Array GetStartPublishDateKeysForSort() {
            throw new NotImplementedException(); // TODO
            //return _contentIds.Select(contentId => PageFactory.GetPage(contentId).StartPublish ?? DateTime.MinValue).ToArray();
        }

        private bool IsAlreadySorted(SortOrder sortOrder, SortDirection sortDirection) {
            return Sorted && SortOrder == sortOrder && SortDirection == sortDirection;
        }

        private static bool IsPageSourcesEqual(ContentCollection contentA, ContentCollection contentB) {
            if (ReferenceEquals(contentA, contentB)) {
                return true;
            }

            if (ReferenceEquals(contentA, null) || ReferenceEquals(contentB, null)) {
                return false;
            }

            if (contentA.ContentIds.Count != contentB.ContentIds.Count) {
                return false;
            }

            return contentA.ContentIds.All(contentB.ContentIds.Contains);
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            return this == obj as ContentCollection;
        }

        public override int GetHashCode() {
            var hash = ContentIds.GetHashCode();
            return hash;
        }

        #region IEnumerable<Content> Members

        IEnumerator<Content> IEnumerable<Content>.GetEnumerator() {
            return (IEnumerator<Content>)GetEnumerator();
        }

        public IEnumerator GetEnumerator() {
            return new ContentCollectionEnumerator(ContentIds);
        }

        #endregion

        public static ContentCollection operator +(ContentCollection contentSource1, ContentCollection contentSource2) {
            contentSource1.AddRange(contentSource2.ContentIds);
            return contentSource1;
        }

        public static bool operator ==(ContentCollection contentSource1, ContentCollection contentSource2) {
            return IsPageSourcesEqual(contentSource1, contentSource2);
        }

        public static bool operator !=(ContentCollection contentSource1, ContentCollection contentSource2) {
            return !IsPageSourcesEqual(contentSource1, contentSource2);
        }
    }
}