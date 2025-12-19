using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    RoomModel roomModel;
    Vector3 pos;
    Quaternion rot;
    bool isJoined = false;
    public float snowBall;   //  éùÇ¡ÇƒÇ¢ÇÈê·ã 
    public int hp;         //  ëÃóÕ
    public float power;    //  ìäÇ∞ÇÈóÕÅiçUåÇóÕÅj
    Vector3 dir;
    bool isStart = false;

    private void Awake()
    {
        GameObject go = GameObject.Find("GameDirector");
        roomModel = go.GetComponent<RoomModel>();
        InvokeRepeating("MoveSend", 0.1f, 0.075f);
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        pos = transform.transform.position;
        rot = transform.transform.rotation;
        dir = new Vector3(x, 0, z);
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void Join()
    {
        dir = new Vector3(0, 0, 0);
        rot = new Quaternion(0, 0, 0, 0);
        transform.position = new Vector3(0, 1, 0);
        isJoined = true;
        
    }
    public void Leave()
    {
        isJoined = false;
    }
    
    private void MoveSend()
    {
        //Debug.Log(roomModel);

        if (dir.x == 0.0f && dir.z == 0.0f && isStart == true)
        {
            snowBall += 0.05f;
        }
        if (!isJoined)
        {
            return;
        }
        roomModel.MoveAsync(pos, rot);
    }
    public void GameStart()
    {
        isStart = true;    
    }
}
