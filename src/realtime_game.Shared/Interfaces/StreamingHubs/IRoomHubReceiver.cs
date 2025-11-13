using Shared.Interfaces.StreamingHubs;

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

    }
}