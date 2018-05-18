namespace TestSiteCore.Services {
    using System;

    public class LocalServices : ILocalService {
        public string TestMethod() {
            return "Response from LocalServices.TestMethod " + DateTime.Now.ToString();
        }
    }
}