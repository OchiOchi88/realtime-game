using MagicOnion;
using MagicOnion.Server;
using realtime_game.Shared.Interfaces.Services;

public class CalclateService : ServiceBase<ICalculateService>, ICalculateService
{
    // 『乗算API』二つの整数を引数で受け取り乗算値を返す
    public async UnaryResult<int> MulAsync(int x, int y)
    {
        Console.WriteLine("Received:" + x + "," + y);
        return x * y;
    }
    public async UnaryResult<int> SumAllAsync(int[] numList)
    {
        int sum = 0;
        foreach(int num in numList)
        {
            sum += num;
        }
        return sum;
    }
    public async UnaryResult<int[]> CalcForOperateionAsync(int x, int y)
    {
        int[] num = new int[4];
        num[0] = x + y;
        num[1] = x - y;
        num[2] = x * y;
        num[3] = x / y;
        return num;
    }
    public async UnaryResult<float> SumAllNumberAsync(Number numData)
    {
        float sum = 0.0f;
        float[] numList = new float[3];
        numList[0] = numData.x;
        numList[1] = numData.y;
        numList[2] = numData.z;
        foreach(float num in numList)
        {
            sum += num;
        }
        return sum;
    }
}
