using Tracklisted.Commands.CreateUserTopTracksList;

namespace Tracklisted.Commands.GetUserTopTracksList
{
    public class CreateUserTopTracksListCommand : BaseCommand
    {
        public CreateUserTopTracksListCommand()
        {
            CommandType = CommandType.CreateUserTopTracksList;
        }
        public string LastfmUserName { get; set; }
        public UserTopTracksPeriod Period { get; set; }
    }
}
