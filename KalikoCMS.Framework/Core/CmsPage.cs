
namespace KalikoCMS.Core {
    using Interfaces;
    using System;

    public class CmsPage : MarshalByRefObject, IContent {
        public string Name { get; set; }
    }
}