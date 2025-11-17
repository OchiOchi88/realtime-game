using System;
using MagicOnion;
using MagicOnion.Client;
using realtime_game.Shared;
using realtime_game.Shared.Interfaces.Services;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    public int numA = 100;
    public int numB = 200;
    [Header("ユーザー登録ネーム")]
    public string registName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        try
        {
            var channel = GrpcChannelx.ForAddress("http://localhost:5244");
            var client = MagicOnionClient.Create<IUserService>(channel);

            var result = await client.RegistUserAsync(registName);
            Debug.Log("ログインしました!");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        //try
        //{
        //    var channel = GrpcChannelx.ForAddress("http://localhost:5244");
        //    var client = MagicOnionClient.Create<IMyFirstService>(channel);

        //    var result = await client.SumAsync(numA, numB);
        //    Debug.Log($"numA({numA}) + numB({numB}) = {result}");
        //}
        //catch (Exception e)
        //{
        //    Debug.LogException(e);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}