using System.Collections.Generic;

namespace Tracklisted.ViewModel.UserTopTracksList
{
    public class UserTopTracksListViewModel
    {
        public UserTopTracksListViewModel()
        {
            TopTracksList = new List<UserTopTrackViewModel>();
        }

        public long Timestamp { get; set; }
        public string User { get; set; }
        public string Period { get; set; }
        public List<UserTopTrackViewModel> TopTracksList { get; set; }
    }
}
