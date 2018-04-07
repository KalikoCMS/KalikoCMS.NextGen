namespace KalikoCMS.Infrastructure {
    using Security;

    public class AccessRightsNode {
        public string Name { get; }
        public bool IsUser { get; }
        public AccessRights AccessRights { get; }

        public AccessRightsNode(string name, bool isUser, AccessRights accessRights) {
            Name = name;
            IsUser = isUser;
            AccessRights = accessRights;
        }
    }
}