using System;

namespace KalikoCMS.Security {
    [Flags]
    public enum AccessRights {
        Read = 1,
        Create = 2,
        Edit = 4,
        Delete = 8,
        Publish = 16
    }
}