﻿namespace Tracklisted.Integration.Lastfm.Models.User.GetTopTracks
{
    public class GetUserTopTracksRequest : IPageable
    {
        public GetUserTopTracksRequest()
        {
            Limit = 50;
            Page = 1;
            Period = UserTopTracksPeriod.Overall;
        }
        public string User { get; set; }
        public UserTopTracksPeriod Period { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
    }
}
