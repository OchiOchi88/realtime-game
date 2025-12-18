using UnityEngine;
using UnityEngine.UI;

public class ButtonHider : MonoBehaviour
{

    void Start()
    {
        Button me = transform.GetComponent<Button>();
        me.interactable = false;
    }
}
