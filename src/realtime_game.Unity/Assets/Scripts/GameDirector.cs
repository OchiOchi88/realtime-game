using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using MagicOnion.Client;
using MagicOnion;
using realtime_game.Shared.Interfaces.Services;
using realtime_game.Shared.Models.Contexts;
using Shared.Interfaces.StreamingHubs;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using UnityEngine.Timeline;
using Unity.VisualScripting;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject me;
    [SerializeField] GameObject otherCharacterPrefab;
    //[SerializeField] RoomModel roomModel;
    RoomModel roomModel;
    UserModel userModel;
    PlayerManager pm;
    Dictionary<Guid, GameObject> characterList = new Dictionary<Guid, GameObject>();
    public TMP_InputField roomNameInput;
    public TMP_InputField inputId;
    public Button leaveButton;
    private int myUserId = 4;
    User myself;
    [SerializeField] CameraPlayerTracker mt;
    async void Start()
    {
        Vector3 vct = new Vector3(0, 1, 0);
        GameObject characterObject = Instantiate(me, vct, Quaternion.identity);  //最初に自キャラのインスタンス生成
        mt.SendReference(characterObject);
        roomModel = GetComponent<RoomModel>();
        userModel = GetComponent<UserModel>();
        pm = characterObject.GetComponent<PlayerManager>();
        //ユーザーが入室した時にOnJoinedUserメソッドを実行するよう、モデルに登録しておく
        roomModel.OnJoinedUser += this.OnJoinedUser;
        Debug.Log("roomModel:" + roomModel);
        Debug.Log("userModel:" + userModel);
        Debug.Log("myUserId:" + myUserId);

        //接続
        await roomModel.ConnectAsync();
        try
        {
            // ユーザー情報を取得
            myself = await userModel.GetUserAsync(myUserId);
        }
        catch (Exception e)
        {
            Debug.Log("RegistUser failed");
            Debug.LogException(e);
        }
        // ユーザーが退室した時にOnLeftUserメソッドを実行できるよう、モデルに登録しておく
        roomModel.OnLeftUser += this.OnLeftUser;
        // ユーザーが退室した時にOnLeftUserAllメソッドを実行できるよう、モデルに登録しておく
        roomModel.OnLeftUserAll += this.OnLeftUserAll;
        roomModel.OnLeftUserAll += () =>
        {
            RemoveAllRemotePlayers(roomModel.ConnectionId);
        };
    }

    // 自分以外のユーザーの移動を反映
    private void OnMoveUser(Guid connectionId, Vector3 pos, Quaternion rot)
    {
        // いない人は移動できない
        if (!characterList.ContainsKey(connectionId))
        {
            return;
        }

        // DOTweenを使うことでなめらかに動く！
        characterList[connectionId].transform.DOMove(pos, 0.1f);
        characterList[connectionId].transform.position = pos;
    }

    // ユーザーが退室した時の処理
    private void OnLeftUser(Guid connectionId)
    {
        // いない人は退室できない
        if (!characterList.ContainsKey(connectionId))
        {
            return;
        }

        Destroy(characterList[connectionId]); // 対象のオブジェクトを削除
        characterList.Remove(connectionId); // リストから対象のデータを削除
    }
    // 自分が退室した時の処理
    private void OnLeftUserAll()
    {
        // 自分以外のオブジェクトを削除する
        List<Guid> connectionIdList = characterList.Keys.ToList();
        foreach (Guid connectionId in connectionIdList)
        {
            // 一人分の退室処理
            OnLeftUser(connectionId);
        }
    }

    public async void JoinRoom()
    {
        string room = roomNameInput.text;
        string idHolder = inputId.text;
        bool isSuccess = int.TryParse(idHolder, out myUserId);
        if (!isSuccess)
        {
            inputId.text = null;
            myUserId = -1;
        }
        if (string.IsNullOrEmpty(room))
        {
            Debug.Log("参加できません、ルーム名を入力してください。");
            return;
        }
        if (myUserId == -1)
        {
            Debug.Log("参加できません、正しいIDを入力してください。");
            return;
        }
        //入室
        pm.Join();
        Debug.Log(myUserId);
        Debug.Log(roomModel);
        leaveButton.transform.gameObject.SetActive(true);
        await roomModel.JoinAsync(room, myUserId);

    }
    //ユーザーが入室した時の処理
    private void OnJoinedUser(JoinedUser user)
    {
        //  既に追加済みのユーザーは生成しない
        if (characterList.ContainsKey(user.ConnectionId))
        {
            return;
        }
        
        //  自分は生成しない
        if(user.UserData.Id == myUserId)
        {
            return;
        }
        GameObject characterObject = Instantiate(otherCharacterPrefab);  //インスタンス生成
            //GameObject characterObject = characterPrefab;
        characterObject.transform.position = new Vector3(0, 1, 0);
        characterList[user.ConnectionId] = characterObject;  //フィールドで保持
    }
    public async void LeaveRoom()
    {
        //// ルーム名チェック
        //Text text = GameObject.Find("RoomName").gameObject.GetComponent<Text>();
        //string roomName = text.text;
        //if (roomName == "")
        //{
        //// ルーム名が入力されていない場合は何もしない
        //return;
        //}

        // 退室
        pm.Leave();
        await roomModel.LeaveAsync();
    }

    public void OnLeavedUser(Guid user)
    {
        if (characterList.TryGetValue(user, out var obj))
        {
            Destroy(obj);
        }
    }
    public void RemoveAllRemotePlayers(Guid selfId)
    {
        foreach (var p in characterList)
        {
            if (p.Key != selfId)
                Destroy(p.Value);
        }
        characterList.Clear();
    }
}
