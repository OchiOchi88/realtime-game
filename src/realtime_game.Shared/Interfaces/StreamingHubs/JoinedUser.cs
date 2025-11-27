using MessagePack;
using realtime_game.Shared.Models.Contexts;
using System;
using UnityEngine;

namespace Shared.Interfaces.StreamingHubs
{
    /// <summary>
    /// 入室済みユーザー
    /// </summary>
    [MessagePackObject]
    public class JoinedUser
    {
        [Key(0)]
        public Guid ConnectionId { get; set; }// 接続ID
        [Key(1)]
        public User UserData { get; set; }// ユーザー情報
        [Key(2)]
        public int JoinOrder { get; set; } // 参加順番
        [Key(3)]
        public Vector3 pos { get; set; }// 位置
        [Key(4)]
        public Quaternion rot { get; set; } // 回転
    }
}

