using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class TitleDirector : MonoBehaviour
{
    [SerializeField] GameObject snowFlake;
    [SerializeField] Text titleText;
    private void Start()
    {
        Vector3 vct = new Vector3(150, 200, 0);
        titleText.rectTransform.DOMove(vct, 1.5f);
    }
    public void Update()
    {
        if (/* タッチしたら */true)
        {
            // UserModelのLoadUserData呼び出し
            if (/* userIDを読み込めたら */true)
            {
                // 次のシーンへ
            }
            else
            {
                // UserModelのRegistUserでユーザー登録
                // 次のシーンへ
            }
        }
    }

    private void FixedUpdate()
    {
        StartCoroutine(Snow());
    }
    IEnumerator Snow()
    {
        float scd = Random.Range(25, 76);
        int x = Random.Range(-10, 11);
        Vector3 vct = new Vector3(x, 7, -0.1f);
        Instantiate(snowFlake, vct, Quaternion.identity);
        yield return new WaitForSeconds(scd / 100);
    }
    public void StartGame()
    {
        Initiate.Fade("LobbyScene", new Color(0, 0, 0), 1.0f);
    }
    public void ExitGame()
    {
            
    }
}
