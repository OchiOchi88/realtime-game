using MagicOnion;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realtime_game.Shared.Interfaces.Services
{
    /// <summary>
    /// 初めてのRPCサービス
    /// </summary>
    public interface ICalculateService : IService<ICalculateService>
    {
        //  [ここにどのようなAPIを作るのか、関数形式で定義を作成する]
        /// <summary>
        /// 乗算処理を行う
        /// </summary>
        /// <param name="x">かける数字一つ目</param>
        /// <param name="y">かける数字二つ目</param>
        /// <returns></returns>
        //  「乗算API」二つの整数を引数で受け取り乗算値を返す
        UnaryResult<int> MulAsync(int x, int y);

        //  受け取った配列の値の合計を返す
        UnaryResult<int> SumAllAsync(int[] numList);

        //  x + yを[0]に、x - yを[1]に、x * yを[2]に、x / yを[3]に入れて配列で返す
        UnaryResult<int[]> CalcForOperateionAsync(int x, int y);

        //  少数の値3つをフィールドに持つNumberクラスに渡して、3つの値の合計値を返す
        UnaryResult<float> SumAllNumberAsync(Number numData);
    }
}
