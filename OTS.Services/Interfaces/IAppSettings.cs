namespace OTS.Services.Interfaces
{
    public interface IAppSettings
    {
        string SiteName { get; }
        int MaxPages { get; }
        string SearchPage { get; }
    }
}
