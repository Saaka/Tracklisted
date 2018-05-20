using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Tracklisted.Model.UserTopTracks
{
    public class UserTopTracks
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public Guid Id { get; set; }
        public string CommandType { get; set; }
        public string User { get; set; }
        public string Period { get; set; }
        public List<TrackData> Tracks { get; set; }
    }
}
