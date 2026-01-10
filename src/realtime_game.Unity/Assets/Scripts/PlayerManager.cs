using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameDirector gd;
    [SerializeField] GameObject snowBall;
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject result;
    [SerializeField] GameObject snowBallInstant;
    RoomModel roomModel;
    Vector3 pos;
    Quaternion rot;
    bool isJoined = false;
    public int haveSnowBall;   //  持っている雪玉
    public int hp;         //  体力
    public float power;    //  投げる力（攻撃力）
    Vector3 dir;
    bool isStart = false;
    private GameObject player;
    private Animator animator; // キャラクターオブジェクトのAnimator
    bool shot = false;
    [SerializeField] StatusShower ss;
    private int makingSB = 0;
    //  後でアニメーションを追加
    //public RuntimeAnimatorController walking;
    //public RuntimeAnimatorController running;
    //public RuntimeAnimatorController standing;

    public float moveSpeed = 5.0f; // キャラクターの移動速度
    public bool damaged;
    private void Start()
    {
        animator = GetComponent<Animator>();
        damaged = false;
    }
    private void Awake()
    {
        GameObject go = GameObject.Find("GameDirector");
        roomModel = go.GetComponent<RoomModel>();
        InvokeRepeating("MoveSend", 0.1f, 0.075f);
    }
    public void GetMe()
    {
        player = this.gameObject;
        Debug.Log("playerObject:" + player);
    }
    void FixedUpdate()
    {
        if (snowBallInstant == null)
        {
            Debug.LogError("snowBallInstant is NULL");
            return;
        }
        pos = transform.transform.position;
        rot = transform.transform.rotation;
        snowBallInstant.transform.position = transform.position + transform.forward * 1.125f;

        float mx = Input.GetAxis("Mouse X");
        //ScreenMovement(mx);
        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.0000001f)
        {
            mx = mx * 5;

            // 回転軸はワールド座標のY軸
            player.transform.RotateAround(player.transform.position, Vector3.up, mx);
        }
        //  前進した
        if (Input.GetKey(KeyCode.W))
        {
            //  後でアニメーションを追加
            //animator.runtimeAnimatorController = walking;

            //プレイヤーの正面に向かって移動する
            transform.position += transform.forward * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            //  後でアニメーションを追加
            //animator.runtimeAnimatorController = walking;

            //プレイヤーの正面に向かって移動する
            transform.position += (transform.right * -1 ) * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            //  後でアニメーションを追加
            //animator.runtimeAnimatorController = walking;

            //プレイヤーの正面に向かって移動する
            transform.position += (transform.forward * -1 ) * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            //  後でアニメーションを追加
            //animator.runtimeAnimatorController = walking;

            //プレイヤーの正面に向かって移動する
            transform.position += transform.right * speed * Time.deltaTime;

        }
        //  とまった
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            if (isStart)
            {

                //  後でアニメーションを追加
                //animator.runtimeAnimatorController = standing;
            }
        }
        if (isStart)
        {
            if (!Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) &&
                !Input.GetKey(KeyCode.D) &&
                haveSnowBall <= 5)
            {
                makingSB++;
                if (makingSB >= 100)
                {
                    haveSnowBall++;
                    ss.Make();
                    makingSB = 0;
                }
                //Debug.Log("雪玉" + makingSB);
            }
            if (!Input.GetKey(KeyCode.Space) && haveSnowBall >= 1 && power >= 0.01f)
            {
                shot = true;
                haveSnowBall -= 1;
                ss.Throw();
                GameObject thrownSnowBall = Instantiate(snowBall, snowBallInstant.transform.position, Quaternion.identity);
                ThrownSnowBallManager tsbm = thrownSnowBall.GetComponent<ThrownSnowBallManager>();
                tsbm.Move(power,rot);
                gd.ThrowSnowBall(
                    snowBallInstant.transform.position,
                    rot
                );
                power = 0.0f;
            }
            else if (Input.GetKey(KeyCode.Space) && haveSnowBall >= 1)
            {
                shot = false;
                power += 0.02f;
                ss.Charge(power);
                //Debug.Log("power" + power);
            }
        }
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

        //if (dir.x == 0.0f && dir.z == 0.0f && isStart == true)
        //{
        //    haveSnowBall += 0.05f;
        //}
        isJoined = roomModel.IsJoined;
        if (!isJoined)
        {
            return;
        }
        roomModel.MoveAsync(pos, rot);
        //roomModel.SnowBallMoveAsync( transform.position, transform.rotation);
    }
    public void GameStart()
    {
        ss.StartGame();
        isStart = true;
    }
    //void ScreenMovement(float mx)
    //{
    //    // X方向に一定量移動していれば横回転
    //    if (Mathf.Abs(mx) > 0.0000001f)
    //    {
    //        mx = mx * 5;

    //        // 回転軸はワールド座標のY軸
    //        player.transform.RotateAround(player.transform.position, Vector3.up, mx);
    //    }
    //}
    public void Damage()
    {
        hp--;
        ss.Damage();
        if(hp <= 0)
        {
            result.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "snowBall")
        {
            Damage();
        }
    }
}
