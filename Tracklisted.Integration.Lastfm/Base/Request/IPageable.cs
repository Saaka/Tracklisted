namespace Tracklisted.Integration.Lastfm.Base.Request
{
    public interface IPageable
    {
        int Limit { get; set; }
        int Page { get; set; }
    }
}
