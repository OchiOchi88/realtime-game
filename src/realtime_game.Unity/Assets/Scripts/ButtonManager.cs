using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button me;
    public void ButtonOn()
    {
        //Button me = transform.GetComponent<Button>();
        me.interactable = true;
    }
    public void ButtonOff()
    {
        //Button me = transform.GetComponent<Button>();
        me.interactable = false;
    }
}
