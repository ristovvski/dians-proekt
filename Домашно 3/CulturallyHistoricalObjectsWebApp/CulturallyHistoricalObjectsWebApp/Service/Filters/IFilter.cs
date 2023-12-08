namespace CulturallyHistoricalObjectsWebApp.Service.Filters
{
    public interface IFilter <T, U>
    {
        U execute (T input, U objects);
    }
}