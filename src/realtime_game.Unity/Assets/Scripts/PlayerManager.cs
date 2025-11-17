using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void Join()
    {
        transform.position = new Vector3(0, 1, 0);
    }
}
