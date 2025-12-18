using UnityEngine;

public class ratingManager : MonoBehaviour
{
    void Start()
    {
        // 弱者が勝利した場合の増減レート
        Debug.Log($"弱者の勝利{CalcRating(1000, 1900)}");

        // 強者が勝利した場合の増減レート
        Debug.Log($"強者の勝利{CalcRating(1900, 1000)}");
    }

    // 勝者と敗者のレートから、増減レートを計算
    private float CalcRating(int winnerRate, int loserRate)
    {
        const int K = 32; // レート計算用の定数。これが大きくなれば増減レートも大きくなる
        return K / Mathf.Pow(10, ((winnerRate - loserRate) / 400f) + 1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
