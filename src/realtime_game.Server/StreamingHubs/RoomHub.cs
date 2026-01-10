using MagicOnion.Server.Hubs;
using realtime_game.Server.Models.Contexts;
using realtime_game.Shared.Interfaces.StreamingHubs;
using Shared.Interfaces.StreamingHubs;
using realtime_game.Shared.Models.Contexts;
using System.Diagnostics;
using UnityEngine;
//using realtime_game.Server.StreamingHubs;


namespace Server.StreamingHubs
{
    public class RoomHub(RoomContextRepository roomContextRepository) : StreamingHubBase<IRoomHub, IRoomHubReceiver>, IRoomHub
    {
        private string roomNameHolder;
        private RoomContextRepository roomContextRepos;
        private RoomContext roomContext;
        public async Task<bool> JoinCheck(string roomName)
        {
            lock (roomContextRepos)
            {
                this.roomContext = roomContextRepos.GetContext(roomName);
                if(this.roomContext == null)
                {
                    return true;
                }
                if (this.roomContext.IsStart == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        // ルームに接続
        public async Task<JoinedUser[]> JoinAsync(string roomName, int userId)
        {
            // 同時に生成しないように排他制御
            lock (roomContextRepos)
            {
                // 指定の名前のルームがあるかどうかを確認
                this.roomContext = roomContextRepos.GetContext(roomName);
                if (this.roomContext == null)
                { // 無かったら生成
                    this.roomContext = roomContextRepos.CreateContext(roomName);
                    roomNameHolder = roomName;
                }
            }
            this.roomContext.IsStart = false;
            // ルームに参加 & ルームを保持
            this.roomContext.Group.Add(this.ConnectionId, Client);

            // DBからユーザー情報取得
            GameDbContext context = new GameDbContext();
            User user = context.Users.Where(user => user.Id == userId).First();

            // 入室済みユーザーのデータを作成
            var joinedUser = new JoinedUser();
            joinedUser.ConnectionId = this.ConnectionId;
            joinedUser.UserData = user;
            // ルームコンテキストにユーザー情報を登録
            var roomUserData = new RoomUserData() { JoinedUser = joinedUser };
            this.roomContext.RoomUserDataList[ConnectionId] = roomUserData;

            // 自分以外のルーム参加者全員にユーザーの入室通知を送信
            this.roomContext.Group.Except([this.ConnectionId]).OnJoin(joinedUser);

            //  コンソールにユーザー情報を表示する
            Console.WriteLine(user.Name + "がルーム「" + roomName + "」に参加しました");
            Console.WriteLine("接続ID:" + joinedUser.ConnectionId);
            Console.WriteLine($"ユーザーID:" + user.Id);
            Console.WriteLine($"ユーザー名:" + user.Name);

            // 入室リクエストをしたユーザーに参加者の情報をリストで返す
            return this.roomContext.RoomUserDataList.Select(
                f => f.Value.JoinedUser).ToArray();
        }

        // 接続時の処理
        protected override ValueTask OnConnected()
        {
            roomContextRepos = roomContextRepository;
            return default;
        }
        // 切断時の処理
        protected override ValueTask OnDisconnected()
        {
            return default;
        }

        // 接続ID取得
        public Task<Guid> GetConnectionId()
        {
            return Task.FromResult<Guid>(this.ConnectionId);
        }

        //  ルームから退室する
        public Task LeaveAsync()
        {
            //  退室したことを全メンバーに通知
            this.roomContext.Group.All.OnLeave(this.ConnectionId);

            //  ルーム内のメンバーから自分を削除
            this.roomContext.Group.Remove(this.ConnectionId);

            //  ルームデータから退室↓ユーザーを削除
            this.roomContext.RoomUserDataList.Remove(this.ConnectionId);
            if (roomContext.Group.Except([this.ConnectionId]) == null)   //  もし、自分が最後の一人なら
            {
                roomContextRepos.RemoveContext(roomNameHolder); //  ルームも削除
            }
            return Task.CompletedTask;
        }
        // 移動
        public Task MoveAsync(Vector3 pos, Quaternion rot)
        {

            // 位置情報を記録
            this.roomContext.RoomUserDataList[this.ConnectionId].pos = pos;
            // 回転情報を記録
            this.roomContext.RoomUserDataList[this.ConnectionId].rot = rot;
            // 移動情報を自分以外の全メンバーに通知
            this.roomContext.Group.Except([this.ConnectionId]).OnMove(this.ConnectionId, pos, rot);

            return Task.CompletedTask;
        }
        //  雪玉の位置同期
        public Task SnowBallMoveAsync(Guid snowBallId, Vector3 pos, Quaternion rot)
        {
            if (!this.roomContext.SnowBallList.ContainsKey(snowBallId))
                return Task.CompletedTask;

            var data = this.roomContext.SnowBallList[snowBallId];
            data.pos = pos;
            data.rot = rot;

            this.roomContext.Group
                .Except([this.ConnectionId])
                .OnSnowBallMove(snowBallId, pos, rot);

            return Task.CompletedTask;
        }
        public Task SnowBallThrowAsync(Vector3 pos, Quaternion rot)
        {
            var snowBall = new SnowBall
            {
                SnowBallId = Guid.NewGuid(),
                OwnerConnectionId = this.ConnectionId
            };

            var snowBallData = new SnowBallData
            {
                SnowBall = snowBall,
                pos = pos,
                rot = rot
            };

            this.roomContext.SnowBallList[snowBall.SnowBallId] = snowBallData;

            // 全員に「生成」を通知
            this.roomContext.Group.All.OnThrowSnowBall(snowBallData);

            return Task.CompletedTask;
        }
        //準備完了
        public async Task ReadyAsync()
        {
            //準備出来たことを自分のRoomUserDataに保存
            var roomUserData = this.roomContext.RoomUserDataList[this.ConnectionId];
            this.roomContext.RoomUserDataList[this.ConnectionId].ready = true;
            //全員準備できたか判定
            bool isReady = true;
            var roomUserDataList = this.roomContext.RoomUserDataList.Values.ToArray();
            foreach (var targetRoomUserData in roomUserDataList)
            {
                if(targetRoomUserData.ready == false)
                {
                    isReady = false;
                }
            }
            if(isReady == true)
            {
                this.roomContext.Group.All.OnStartGame();
                this.roomContext.IsStart = true;
            }
        }
        //public async Task StartAsync()
        //{

        //}
    }
}