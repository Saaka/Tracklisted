using Newtonsoft.Json;
using System;
using Tracklisted.Commands.GetArtistTopTracks;

namespace Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure
{
    public interface ICommandBodyMapper
    {
        BaseCommand MapToBaseCommand(string body);
        BaseCommand MapToCommand(CommandType commandType, string body);
    }

    internal class CommandBodyMapper : ICommandBodyMapper
    {
        public BaseCommand MapToBaseCommand(string body)
        {
            return JsonConvert.DeserializeObject<BaseCommand>(body);
        }

        public BaseCommand MapToCommand(CommandType commandType, string commandBody)
        {
            switch (commandType)
            {
                case CommandType.GetArtistTopTracks:
                    return JsonConvert.DeserializeObject<GetArtistTopTracksCommand>(commandBody);
                case CommandType.GetUserTopTracks:
                    return JsonConvert.DeserializeObject<GetUserTopTracksCommand>(commandBody);
                default:
                    throw new ArgumentException("Invalid command type");
            }
        }
    }
}
