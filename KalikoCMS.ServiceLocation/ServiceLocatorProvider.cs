namespace KalikoCMS.ServiceLocation {
    using Services.Interfaces;

    /// <summary>
    /// This delegate type is used to provide a method that will
    /// return the current container. Used with the <see cref="IServiceLocator"/>
    /// static accessor class.
    /// </summary>
    /// <returns>An <see cref="ServiceLocator"/>.</returns>
    public delegate IServiceLocator ServiceLocatorProvider();
}