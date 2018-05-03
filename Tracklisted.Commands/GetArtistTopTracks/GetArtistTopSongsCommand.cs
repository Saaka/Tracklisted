namespace Tracklisted.Commands.GetArtistTopTracks
{
    public class GetArtistTopTracksCommand : BaseCommand
    {
        public GetArtistTopTracksCommand()
        {
            CommandType = CommandType.GetArtistTopTracks;
        }
        public string ArtistName { get; set; }
    }
}
