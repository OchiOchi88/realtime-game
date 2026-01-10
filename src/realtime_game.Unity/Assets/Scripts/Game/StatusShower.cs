using UnityEngine;
using UnityEngine.UI;

public class StatusShower : MonoBehaviour
{
    [SerializeField] RawImage hp;
    [SerializeField] RawImage snowBall;
    [SerializeField] Slider power;
    [SerializeField] PlayerManager player;
    private RawImage[] displayHp = new RawImage[3];
    private RawImage[] displaySB = new RawImage[5];

    public void StartGame()
    {
        for (int i = 0; i < player.hp; i++)
        {
            RawImage instantiateHp = Instantiate(hp, transform.transform);
            instantiateHp.rectTransform.anchorMin = new Vector2(0, 1);
            instantiateHp.rectTransform.anchorMax = new Vector2(0, 1);
            //instantiateHp.rectTransform.anchoredPosition3D = new Vector3(1, 1, 1);
            instantiateHp.rectTransform.position = new Vector3(75 * i, 450, 0);
            displayHp[i] = instantiateHp;
            instantiateHp.enabled = true;
        }
        for (int i = 0; i < player.haveSnowBall; i++)
        {
            RawImage instantiateSB = Instantiate(snowBall, transform.transform);
            instantiateSB.rectTransform.anchorMin = new Vector2(0, 0);
            instantiateSB.rectTransform.anchorMax = new Vector2(0, 0);
            //instantiateSB.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            instantiateSB.rectTransform.position = new Vector3(30 * i, 0, 0);
            displaySB[i] = instantiateSB;
            instantiateSB.enabled = true;
        }
    }
    public void Damage()
    {
        for (int i = 2; i >= 0; i--)
        {
            if (displayHp[i] == null)
            {
                continue;
            }
            Destroy(displayHp[i]);
            break;
        }
    }   
    public void Throw()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (displaySB[i] == null)
            {
                continue;
            }
            Destroy(displaySB[i]);
            break;
        }
        power.value = 0.0f;
    }
    public void Make()
    {
        for(int i = 0; i < displaySB.Length; i++)
        {
            if(displaySB[i] != null)
            {
                continue;
            }
            RawImage instantiateSB = Instantiate(snowBall, transform.transform);
            instantiateSB.rectTransform.anchorMin = new Vector2(0, 0);
            instantiateSB.rectTransform.anchorMax = new Vector2(0, 0);
            //instantiateSB.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            instantiateSB.rectTransform.position = new Vector3(30 * i, 0, 0);
            displaySB[i] = instantiateSB;
            instantiateSB.enabled = true;
            break;
        }
    }
    public void Charge(float getPower)
    {
        power.value = getPower;
    }
}
