using System.Linq;
using System.Threading.Tasks;
using Tracklisted.DAL.UserTopTracksStore;
using Tracklisted.Model.UserTopTracks;
using Tracklisted.ViewModel.UserTopTracksList;

namespace Tracklisted.Queries.GetUserTopTracksList
{
    public interface IGetUserTopTracksListQuery
    {
        Task<UserTopTracksListViewModel> Get(string commandId);
    }

    public class GetUserTopTracksListQuery : IGetUserTopTracksListQuery
    {
        private readonly IUserTopTracksRepository _userTopTracksRepository;

        public GetUserTopTracksListQuery(IUserTopTracksRepository userTopTracksRepository)
        {
            _userTopTracksRepository = userTopTracksRepository;
        }

        public async Task<UserTopTracksListViewModel> Get(string commandId)
        {
            var result = await _userTopTracksRepository.GetTopTracksByCommandId(commandId);

            return MapResult(result);
        }

        private UserTopTracksListViewModel MapResult(UserTopTracks result)
        {
            var viewModel = new UserTopTracksListViewModel
            {
                Timestamp = result.Timestamp,
                Period = result.Period,
                User = result.User,
                TopTracksList = result.Tracks.Select(MapTrack).ToList()
            };

            return viewModel;
        }

        private UserTopTrackViewModel MapTrack(TopTrackData track)
        {
            var viewModel = new UserTopTrackViewModel
            {
                TrackName = track.TrackName,
                ArtistName = track.ArtistName,
                AlbumName = track.AlbumName,
                Rank = track.Rank,
                MediumImageUrl = track.Images.LastfmImageUrlMedium,
                PreviewUrl = track.SpotifyPreview,
                SpotifyUrl = track.SpotifyUrl,
                SpotifyUri = track.SpotifyUri,
                LastfmUrl = track.LastfmUrl,
                SpotifyAlbumUrl = track.SpotifyAlbumUrl
            };

            return viewModel;
        }
    }
}
