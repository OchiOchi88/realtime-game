using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    RoomModel roomModel;
    Vector3 pos;
    Quaternion rot;
    private void Awake()
    {
        GameObject go = GameObject.Find("GameDirector");
        roomModel = go.GetComponent<RoomModel>();
        InvokeRepeating("MoveSend", 0.1f, 0.1f);
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        pos = transform.transform.position;
        rot = transform.transform.rotation;
        Vector3 dir = new Vector3(x, 0, z);
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void Join()
    {
        transform.position = new Vector3(0, 1, 0);
    }
    
    private void MoveSend()
    {
        //Debug.Log(roomModel);
        roomModel.MoveAsync(pos, rot);
    }
}
