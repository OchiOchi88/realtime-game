using Cysharp.Runtime.Multicast;
//using realtime_game.Server.StreamingHubs;
using realtime_game.Shared.Interfaces.StreamingHubs;
using Shared.Interfaces.StreamingHubs;
using UnityEngine;

namespace Server.StreamingHubs
{
    // ルーム内のユーザー単体の情報
    public class RoomContext : IDisposable
    {
        public Guid Id { get; } // ルームID
        public string Name { get; } // ルーム名
        public bool IsStart { get; set; } = false;// 部屋の試合が既に始まっているかどうか(試合中に入室されるのを防ぐため)
        public IMulticastSyncGroup<Guid, IRoomHubReceiver> Group { get; } // グループ
        public Dictionary<Guid, RoomUserData> RoomUserDataList { get; } =
            new Dictionary<Guid, RoomUserData>(); // ユーザデータ一覧
        public Dictionary<Guid, SnowBallData> SnowBallList { get; } =
            new Dictionary<Guid, SnowBallData>();   //  フィールド内の雪玉一覧

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