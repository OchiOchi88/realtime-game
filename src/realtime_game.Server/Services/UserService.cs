using Grpc.Core;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.AspNetCore.Diagnostics;
using realtime_game.Server.Models.Contexts;
using realtime_game.Shared.Interfaces.Services;
using realtime_game.Shared.Models.Contexts;
using System.ComponentModel;
using System.Net.Http.Headers;


public class UserService : ServiceBase<IUserService>, IUserService
{
    string[] name = { "Jobi", "Yamagami", "Chiba", "Sakai" };
    string[] token = { "Cyber", "Usausa", "Mountain", "SunGlass" };
    
    public async UnaryResult<int> RegistUserAsync(string name)
    {
        //  テーブルにレコード追加
        using var context = new GameDbContext();

        //  バリデーションチェック(名前登録済みかどうか)
        if(context.Users.Count() > 0 &&
            context.Users.Where(user => user.Name == name).Count() > 0)
        {
            Console.WriteLine(name + "がログインしました。");
            return 0;
            //throw new ReturnStatusException(Grpc.Core.StatusCode.OK, "存在するユーザーのためログインしました！");
        }

        //  tokenをランダムで生成(仮)
        Random tokenGen = new Random();
        int[] tokenNums = new int[16];
        string tokenHolder = "";
        for (int i = 0; i < tokenNums.Length; i++)
        {
            tokenNums[i] = tokenGen.Next(0, 9);
            tokenHolder += tokenNums[i].ToString();
        }

        //  テーブルにレコードを追加
        User user = new User();
        user.Name = name;
        user.Token = tokenHolder;
        user.Created_at = DateTime.Now;
        user.Updated_at = DateTime.Now;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user.Id;
    }
    public async UnaryResult<string[]> GetUserAsync(int id)
    {
        //  id指定でユーザー情報を取得するAPI


        string[] returnInfo = new string[2];
        returnInfo[0] = name[id];
        returnInfo[1] = token[id];
        return returnInfo;
    }
    public async UnaryResult<string[]> GetAllUserAsync()
    {
        //  テーブルにレコード追加
        return name;
    }
    //public async UnaryResult<string> UpdateUserNameAsync(User user)
    //{
        
    //    //  テーブルにレコード追加
    //    return ;
    //}
}
