namespace KalikoCMS.Core.Collections {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ContentCollectionEnumerator : IEnumerator<Content> {
        private readonly Collection<Guid> _contentIds;
        private int _index = -1;

        public ContentCollectionEnumerator(Collection<Guid> contentIds) {
            _contentIds = contentIds;
        }

        public bool MoveNext() {
            _index++;

            return _index < _contentIds.Count;
        }

        public void Reset() {
            _index = -1;
        }

        Content IEnumerator<Content>.Current => GetContentFromCollection();

        public object Current => GetContentFromCollection();

        private CmsPage GetContentFromCollection() {
            var contentId = _contentIds[_index];
            //TODO: CmsPage page = PageFactory.GetPage(pageId);
            throw new NotImplementedException();
            //return page;
        }

        public void Dispose() { }
    }
}
