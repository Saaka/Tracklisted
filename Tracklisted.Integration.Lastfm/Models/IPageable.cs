namespace Tracklisted.Integration.Lastfm.Models
{
    public interface IPageable
    {
        int Limit { get; set; }
        int Page { get; set; }
    }
}
