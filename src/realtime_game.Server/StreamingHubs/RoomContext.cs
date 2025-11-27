using Cysharp.Runtime.Multicast;
//using realtime_game.Server.StreamingHubs;
using realtime_game.Shared.Interfaces.StreamingHubs;
using Shared.Interfaces.StreamingHubs;
using UnityEngine;

namespace Server.StreamingHubs
{
    // ルーム内のユーザー単体の情報
    public class RoomUserData
    {
        public JoinedUser JoinedUser;
        internal Vector3 pos;   //  デバッグ用の一時的な変数
        internal Quaternion rot;    //  デバッグ用の一時的な変数
    }
    public class RoomContext : IDisposable
    {
        public Guid Id { get; } // ルームID
        public string Name { get; } // ルーム名
        public IMulticastSyncGroup<Guid, IRoomHubReceiver> Group { get; } // グループ
        public Dictionary<Guid, RoomUserData> RoomUserDataList { get; } =
            new Dictionary<Guid, RoomUserData>(); // ユーザデータ一覧

        //  コンストラクタ
        public RoomContext(IMulticastGroupProvider groupProvider, string roomName)
        {
            Id = Guid.NewGuid();    //  ルームごとのテータにIDをつけておく
            Name = roomName;        //  ルーム名をフィールドに保存
            Group =
                groupProvider.GetOrAddSynchronousGroup<Guid, IRoomHubReceiver>(roomName);
        }

        public void Dispose()
        {
            Group.Dispose();
        }
    }
}