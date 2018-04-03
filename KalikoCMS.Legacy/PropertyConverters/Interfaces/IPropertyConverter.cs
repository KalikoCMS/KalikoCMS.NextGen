namespace KalikoCMS.Legacy.PropertyConverters.Interfaces
{
    public interface IPropertyConverter<in TIn, out TOut> {
        TOut Convert(TIn property);
    }
}
