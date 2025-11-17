using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] GameObject ground;
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
    }
}
