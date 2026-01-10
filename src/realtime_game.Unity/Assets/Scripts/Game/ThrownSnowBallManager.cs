using UnityEngine;

public class ThrownSnowBallManager : MonoBehaviour
{
    float power = 0;
    float life = 100.0f;
    RoomModel roomModel;
    public void Move(float getPower , Quaternion rot)
    {
        transform.rotation = rot;
        power = getPower / 10 + 0.2f;
        life = 100 * getPower + 50;
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * power;
        life -= 1.0f;
        if(life <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
