using UnityEngine;
using UnityEngine.UI;

public class StatusShower : MonoBehaviour
{
    [SerializeField] Image hp;
    [SerializeField] Image snowBall;
    [SerializeField] Slider power;
    [SerializeField] PlayerManager player;
    private Image[] displayHp;
    private Image[] displaySB;

    private void Start()
    {
        for (int i = 0; i < player.hp; i++)
        {
            Image instantiateHp = Instantiate(hp, transform.transform);
            instantiateHp.rectTransform.anchoredPosition3D = new Vector3(1, 1, 1);
            instantiateHp.rectTransform.position = new Vector3(75 * i, 0, 0);
            displayHp[i] = instantiateHp;
        }
        for (int i = 0; i < player.snowBall; i++)
        {
            Image instantiateSB = Instantiate(snowBall, transform.transform);
            instantiateSB.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            instantiateSB.rectTransform.position = new Vector3(75 * i, 0, 0);
            displaySB[i] = instantiateSB;
        }
    }
    public void Damage()
    {
        for (int i = 0; i <= displayHp.Length; i++)
        {
            if (i < displayHp.Length)
            {
                continue;
            }
            Destroy(displayHp[i]);
        }
    }   
    public void Throw()
    {
        for (int i = 0; i <= displaySB.Length; i++)
        {
            if (i < displaySB.Length)
            {
                continue;
            }
            Destroy(displaySB[i]);
        }
    }
    public void Make()
    {
        for(int i = 0; i <= displaySB.Length; i++)
        {
            if(i < displaySB.Length)
            {
                continue;
            }
            Image instantiateSB = Instantiate(snowBall, transform.transform);
            instantiateSB.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            instantiateSB.rectTransform.position = new Vector3(75 * i, 0, 0);
            displaySB[i] = instantiateSB;
        }
    }
    public void Charge()
    {

    }
}
