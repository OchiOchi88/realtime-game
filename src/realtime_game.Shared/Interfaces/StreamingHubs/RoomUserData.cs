using Shared.Interfaces.StreamingHubs;
using UnityEngine;
namespace Shared.Interfaces.StreamingHubs
{
    public class RoomUserData
    {
        public JoinedUser JoinedUser;

        public Vector3 pos { get; set; }
        public Quaternion rot { get; set; }
        public bool ready { get; set; }
    }
}