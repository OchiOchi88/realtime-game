using Cysharp.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using realtime_game.Shared.Interfaces.Services;
using realtime_game.Shared.Models.Contexts;
using System;
using UnityEngine;

public class UserModel : BaseModel
{
    User user; //  ìoò^ÉÜÅ[ÉUÅ[ID
    private int userId;
    public async UniTask<bool> RegistUserAsync(string name)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<IUserService>(channel);
        try
        {   //  ìoò^ê¨å˜
            userId = await client.RegistUserAsync(name);
            return true;
        }catch (RpcException e)
        {   //  ìoò^é∏îs
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
            //  éÊìæê¨å˜
            user = await client.GetUserAsync(id);
            return user;
        }catch (RpcException e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
