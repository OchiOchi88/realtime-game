using MessagePack;
using realtime_game.Shared.Models.Contexts;
using System;
using UnityEngine;

namespace Shared.Interfaces.StreamingHubs
{
    public class SnowBall
    {
        public Guid SnowBallId;
        public Guid OwnerConnectionId;
    }
}
