namespace Tracklisted.Commands.GetUserTopTracksList
{
    public class CreateUserTopTracksListCommand : BaseCommand
    {
        public CreateUserTopTracksListCommand()
        {
            CommandType = CommandType.CreateUserTopTracksList;
        }
        public string LastfmUserName { get; set; }
    }
}
