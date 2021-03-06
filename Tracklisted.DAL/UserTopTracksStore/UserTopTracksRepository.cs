﻿using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Tracklisted.Model.UserTopTracks;

namespace Tracklisted.DAL.UserTopTracksStore
{
    public interface IUserTopTracksRepository
    {
        Task<UserTopTracks> Add(UserTopTracks model);
        Task<UserTopTracks> GetTopTracksByCommandId(string commandId);
    }
    public class UserTopTracksRepository : IUserTopTracksRepository
    {
        private readonly IUserTopTracksContext context;

        public UserTopTracksRepository(IUserTopTracksContext context)
        {
            this.context = context;
        }

        public async Task<UserTopTracks> Add(UserTopTracks model)
        {
            await context.TopTracksCollection
                .InsertOneAsync(model);

            return model;
        }

        public async Task<UserTopTracks> GetTopTracksByCommandId(string commandId)
        {
            var commandGuid = new Guid(commandId);
            return await context.TopTracksCollection
                .Find(x => x.CommandId == commandGuid)
                .FirstOrDefaultAsync();
        }
    }
}
