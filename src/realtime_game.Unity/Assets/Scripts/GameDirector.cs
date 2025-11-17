using Cysharp.Threading.Tasks.Triggers;
using Shared.Interfaces.StreamingHubs;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] RoomModel roomModel;
    PlayerManager pm;
    Dictionary<Guid, GameObject> characterList = new Dictionary<Guid, GameObject>();
    public TMP_InputField roomNameInput;
    public int userId = 1;
    async void Start()
    {
        Vector3 vct = new Vector3(0, 1, 0);
        GameObject characterObject = Instantiate(characterPrefab, vct,Quaternion.identity);  //最初に自キャラのインスタンス生成
        pm = characterObject.GetComponent<PlayerManager>();
        //ユーザーが入室した時にOnJoinedUserメソッドを実行するよう、モデルに登録しておく
        roomModel.OnJoinedUser += this.OnJoinedUser;
        //接続
        await roomModel.ConnectAsync();
    }
    public async void JoinRoom()
    {
        string room = roomNameInput.text;
        if (string.IsNullOrEmpty(room))
        {
            Debug.Log("参加できません、ルーム名を入力してください。");
            return;
        }
        //入室
        pm.Join();
        await roomModel.JoinAsync(room, userId);
    }
    //ユーザーが入室した時の処理
    private void OnJoinedUser(JoinedUser user)
    {
        //GameObject characterObject = Instantiate(characterPrefab);  //インスタンス生成
        GameObject characterObject = characterPrefab;
        characterObject.transform.position = new Vector3(0, 1, 0);
        characterList[user.ConnectionId] = characterObject;  //フィールドで保持
    }

}
