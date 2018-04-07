namespace KalikoCMS.Services.Localization.Interfaces {
    public interface ILocalizationService {
        string Translate(string key, bool errorIfMissing = true);
        string TryTranslate(string key);
    }
}
