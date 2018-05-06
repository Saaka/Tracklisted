namespace Tracklisted.Commands.GetArtistTopTracks
{
    public class GetUserTopTracksCommand : BaseCommand
    {
        public GetUserTopTracksCommand()
        {
            CommandType = CommandType.GetUserTopTracks;
        }
        public string User { get; set; }
    }
}
