using Tracklisted.Commands.CreateUserTopTracksList;

namespace Tracklisted.WebAPI.ViewModel.UserTopTracks
{
    public class CreateUserTopTracksList
    {
        public string UserName { get; set; }
        public UserTopTracksPeriod Period { get; set; }
    }
}
