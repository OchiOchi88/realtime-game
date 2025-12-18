using MagicOnion;
using realtime_game.Shared.Interfaces.StreamingHubs;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Globalization;
namespace Shared.Interfaces.StreamingHubs
{
    /// <summary>
    /// クライアントから呼び出す処理を実装するクラス用インターフェース
    /// </summary>
    public interface IRoomHub : IStreamingHub<IRoomHub, IRoomHubReceiver>
    {
        //Task<Guid> GetConnectionId();

        // [サーバーに実装]
        // [クライアントから呼び出す]
        //  ユーザーが入室できるかチェック
        Task<bool> JoinCheck(string roomName);
        // ユーザー入室
        Task<JoinedUser[]> JoinAsync(string roomName, int userId);

        //  ユーザー退室
        Task LeaveAsync();

        //  接続ID取得
        public Task<Guid> GetConnectionId();

        //位置・回転をサーバーに送信する
        Task MoveAsync(Vector3 pos, Quaternion rot);

        Task ReadyAsync();
    }
}
