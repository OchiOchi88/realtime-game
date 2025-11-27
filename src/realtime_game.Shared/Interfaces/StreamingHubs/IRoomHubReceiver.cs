using Shared.Interfaces.StreamingHubs;
using System;
using UnityEngine;

namespace realtime_game.Shared.Interfaces.StreamingHubs
{
    /// <summary>
    /// 初めてのRPCサービス
    /// </summary>
    public interface IRoomHubReceiver
    {
        // [クライアントに実装]
        // [サーバーから呼び出す]

        // ユーザーの入室通知
        void OnJoin(JoinedUser user);

        //  ユーザーの退室通知
        void OnLeave(Guid connectionId);

        void OnMove(Guid connectionId, Vector3 pos, Quaternion rot);
    }
}