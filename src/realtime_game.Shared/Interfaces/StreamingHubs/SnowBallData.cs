using Shared.Interfaces.StreamingHubs;
using System;
using UnityEngine;
namespace Shared.Interfaces.StreamingHubs
{
    public class SnowBallData
    {
        public SnowBall SnowBall; 

        public Guid ConnectionId { get; set; }
        public Vector3 pos { get; set; }
        public Quaternion rot { get; set; }
    }
}