namespace KalikoCMS.Services {
    internal static class ServiceLocatorConstants {
        // Message shown if an exception occurs during a GetAllInstances call
        internal const string ActivateAllExceptionMessage = "Activation error occurred while trying to get all instances of type {0}";

        //Message shown on exception in GetInstance method
        internal const string ActivationExceptionMessage = "Activation error occurred while trying to get instance of type {0}, key \"{1}\"";

        //Message shown if ServiceLocator.Current called before ServiceLocationProvider is set.
        internal const string ServiceLocationProviderNotSetMessage = "ServiceLocationProvider must be set.";
    }
}