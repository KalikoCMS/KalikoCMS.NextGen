namespace TestLegacyCore.Models {
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;

    [SiteSettings]
    public class MySite : CmsSite
    {
        public override void SetDefaults() {
            ChildSortOrder = KalikoCMS.Core.Collections.SortOrder.CreatedDate;
        }
    }
}
