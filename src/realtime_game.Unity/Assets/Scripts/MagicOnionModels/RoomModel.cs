using Cysharp.Threading.Tasks;
using MagicOnion.Client;
using MagicOnion;
using realtime_game.Shared.Interfaces.StreamingHubs;
using Shared.Interfaces.StreamingHubs;
using System;
using UnityEngine;
using realtime_game.Shared.Models.Contexts;
using System.Threading.Tasks;

public class RoomModel : BaseModel, IRoomHubReceiver
{
    private GrpcChannelx channel;
    private IRoomHub roomHub;

    //　接続ID
    public Guid ConnectionId { get; set; }

    //　ユーザー接続通知
    public Action<JoinedUser> OnJoinedUser { get; set; }

    // ユーザー切断通知
    public Action<Guid> OnLeftUser { get; set; }

    // ユーザー切断通知
    public Action OnLeftUserAll { get; set; }

    public Action<Guid, Vector3, Quaternion> OnMoveCharacter { get; set; }
    public bool IsJoined { get; private set; } = false;


    //　MagicOnion接続処理
    public async UniTask ConnectAsync()
    {
        channel = GrpcChannelx.ForAddress(ServerURL);
        roomHub = await StreamingHubClient.
             ConnectAsync<IRoomHub, IRoomHubReceiver>(channel, this);
        this.ConnectionId = await roomHub.GetConnectionId();
    }

    //　MagicOnion切断処理
    public async UniTask DisconnectAsync()
    {
        if (roomHub != null) await roomHub.DisposeAsync();
        if (channel != null) await channel.ShutdownAsync();
        roomHub = null; channel = null;
    }

    //　破棄処理 
    async void OnDestroy()
    {
        DisconnectAsync();
    }

    //　入室
    public async UniTask JoinAsync(string roomName, int userId)
    {
        JoinedUser[] users = await roomHub.JoinAsync(roomName, userId);
        foreach (var user in users)
        {
            if (OnJoinedUser != null)
            {
                Debug.Log(user.UserData.Id);
                Debug.Log(user.UserData.Name);
                Debug.Log(user.UserData.Token);
                OnJoinedUser(user);
            }
        }
        IsJoined = true;
    }

    // 退室
    public async UniTask LeaveAsync()
    {
        await roomHub.LeaveAsync();
        Debug.Log("退室完了");

        // 自分以外のオブジェクトを削除する
        if(OnJoinedUser != null)
        {
            OnLeftUserAll?.Invoke();
        }
    }


    //　入室通知 (IRoomHubReceiverインタフェースの実装)
    public void OnJoin(JoinedUser user)
    {
        if (OnJoinedUser != null)
        {
            OnJoinedUser(user);
        }
    }
    public void OnLeave(Guid connectionId)
    {
        if (OnLeftUser != null)
        {
            OnLeftUser(connectionId);
        }
    }

    //位置・回転を送信する
    public Task MoveAsync(Vector3 pos, Quaternion rot)
    {
        //Debug.Log("roomHub:" + roomHub);
        //Debug.Log("roomHubPos:" + pos);
        //Debug.Log("roomHubRot:" + rot);
        roomHub.MoveAsync(pos,rot);
        return Task.CompletedTask;
    }
    public void OnMove(Guid connectionId, Vector3 pos, Quaternion rot)
    {
        if (!IsJoined) return;
        Debug.Log(OnMoveCharacter);
        Debug.Log("connectionId:" + connectionId);
        Debug.Log("pos:" + pos);
        Debug.Log("rot:" + rot);
        OnMoveCharacter(connectionId, pos, rot);
    }
}
