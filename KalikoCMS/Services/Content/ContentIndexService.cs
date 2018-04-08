namespace KalikoCMS.Services.Content {
    using Infrastructure;
    using Interfaces;

    public class ContentIndexService : IContentIndexService {
        private static ContentIndex _index;

        static ContentIndexService() {
            _index = new ContentIndex();
        }

        public void Initialize() {
            
        }
    }
}