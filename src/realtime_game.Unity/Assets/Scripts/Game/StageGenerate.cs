using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] GameObject ground;
    [SerializeField] GameObject wall;
    [Header("ステージの幅")]
    public int fieldX = 5;
    public int fieldZ = 5;
    void Start()
    {
        float x = (float)fieldX / 2 * -1 + 0.5f;
        float z = (float)fieldZ / 2 * -1 + 0.5f;
        
        GameObject[,] tile = new GameObject[fieldX, fieldZ];
        for(int i = 0; i < fieldZ; i++)
        {
            for(int j = 0; j < fieldX; j++)
            {
                Vector3 vct3 = new Vector3(x, 0, z);
                tile[j,i] = Instantiate(ground, vct3, transform.rotation);
                x += 1.0f;
            }
            x = (float)fieldX / 2 * -1 + 0.5f;
            z += 1.0f;
        }
        GameObject border;
        for(int i = -1; i <= 1; i++)
        {
            if(i == 0)
            {
                continue;
            }
            Vector3 vct3 = new Vector3((x - 1) * i, 0, 0);
            border = Instantiate(wall, vct3, transform.rotation);
            border.transform.localScale = new Vector3(1, 100.0f, 100.0f);
        }
        for(int j = -1; j <= 1; j++)
        {
            if (j == 0)
            {
                continue;
            }
            Vector3 vct3 = new Vector3(0, 0, (z) * j);
            border = Instantiate(wall, vct3, transform.rotation);
            border.transform.localScale = new Vector3(100.0f, 100.0f, 1);
        }
    }
}
