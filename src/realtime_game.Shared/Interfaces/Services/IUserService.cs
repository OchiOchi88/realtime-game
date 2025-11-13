using MagicOnion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realtime_game.Shared.Interfaces.Services
{
    public interface IUserService : IService<IUserService>
    {
        //  ユーザーを登録するAPI
        UnaryResult<int> RegistUserAsync(string name);

        //  id指定でユーザー一覧を取得するAPI
        UnaryResult<string[]> GetUserAsync(int id);

        //  ユーザー一覧を取得するAPI
        UnaryResult<string[]> GetAllUserAsync();

        //  id指定でユーザー名を更新するAPI
        //UnaryResult<string> UpdateUserNameAsync(User user);
    }
}
