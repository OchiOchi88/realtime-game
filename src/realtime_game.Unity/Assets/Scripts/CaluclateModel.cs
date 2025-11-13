using Grpc.Net.Client;
using UnityEngine;
using MagicOnion;
using MagicOnion.Client;
using Cysharp.Threading.Tasks;
using realtime_game.Shared.Interfaces.Services;

public class CalculateModel : MonoBehaviour
{
    [Header("演算メソッドの変数")]
    [Tooltip("足し算メソッド１つ目の変数")]public int mulNumberX;
    [Tooltip("足し算メソッド２つ目の変数")] public int mulNumberY;
    [Tooltip("全部合算メソッドの変数")] public int[] sumAllNumber;
    [Tooltip("四則演算メソッドの１つ目の変数")] public int opNumberX;
    [Tooltip("四則演算メソッドの２つ目の変数")] public int opNumberY;
    [Tooltip("少数足し算メソッドの変数")] public float[] floatSumAllNumber = new float[3];
    const string ServerURL = "http://localhost:5244";
    async void Start()
    {
        Number fSum = new Number();
        fSum.x = floatSumAllNumber[0];
        fSum.y = floatSumAllNumber[1];
        fSum.z = floatSumAllNumber[2];
        int result = await Mul(mulNumberX,mulNumberY);
        int sumAllResult = await SumAll(sumAllNumber);
        int[] opResults = await CalcForOperateion(opNumberX, opNumberY);
        float floatSumAllResult = await SumAllNumber(fSum);
        Debug.Log("Mulメソッド:"+result);
        Debug.Log("SumAllメソッド:" + sumAllResult);
        foreach(int opResult in opResults)
        {
            Debug.Log("Opメソッド:" + opResult);
        }
        Debug.Log("FloatSumメソッド:" + floatSumAllResult);
    }
    public async UniTask<int> Mul(int x,int y)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<ICalculateService>(channel);
        var result = await client.MulAsync(x, y);
        return result; 
    }
    public async UniTask<int> SumAll(int[] numList)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<ICalculateService>(channel);
        var result = await client.SumAllAsync(numList);
        return result;
    }
    public async UniTask<int[]> CalcForOperateion(int x, int y)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<ICalculateService>(channel);
        var result = await client.CalcForOperateionAsync(x, y);
        return result;
    }
    public async UniTask<float> SumAllNumber(Number numData)
    {
        var channel = GrpcChannelx.ForAddress(ServerURL);
        var client = MagicOnionClient.Create<ICalculateService>(channel);
        var result = await client.SumAllNumberAsync(numData);
        return result;
    }
}
