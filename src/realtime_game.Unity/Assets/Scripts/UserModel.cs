using Cysharp.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using Newtonsoft.Json;
using realtime_game.Shared.Interfaces.Services;
using realtime_game.Shared.Models.Contexts;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Overlays;
using UnityEngine;

public class UserModel : BaseModel
{
    User user; //  登録ユーザーID
    private int userId;
    //private int userID; //  自分のユーザーID
    private string userName;    //  入力される想定の自分のユーザー名
    static public string nameData;

    //  プロパティ
    public string Name
    {
        get
        {
            return this.userName;
        }
    }

    private static UserModel instance;
    public static UserModel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserModel();
            }
            return instance;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public async UniTask<bool> RegistUserAsync(string name)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<IUserService>(channel);
        try
        {   //  登録成功
            userId = await client.RegistUserAsync(name);
            userName = name;
            return true;
        }catch (RpcException e)
        {   //  登録失敗
            Debug.Log(e);
            return false;
        }
    }
    public async UniTask<User> GetUserAsync(int id)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<IUserService>(channel);
        try
        {
            //  取得成功
            user = await client.GetUserAsync(id);
            return user;
        }catch (RpcException e)
        {
            Debug.Log(e);
            return null;
        }
    }
    // ユーザーIDをローカルファイルに保存する
    public void SaveUserData()
    {
        UserData saveData = new UserData();
        saveData.Name = this.userName;
        nameData = userName;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
                new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }
    // ユーザーIDをローカルファイルから読み込む
    public bool LoadUserData()
    {
        return true; if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return false;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        Debug.Log(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        UserData saveData = JsonConvert.DeserializeObject<UserData>(json);
        this.userName = saveData.Name;
        nameData = userName;
        return true;
    }
}
