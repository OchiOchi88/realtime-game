using UnityEngine;
using UnityEngine.Rendering;

public class SnowDirector : MonoBehaviour
{
    [SerializeField] private Vector3 localGravity;  //  重力の変更
    private Rigidbody rBody;
    private void FixedUpdate()
    {
        SetLocalGravity();
        if (transform.position.y <= -7.5f)
        {
            Destroy(transform.gameObject);
        }
    }
    private void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
        rBody.useGravity = false; //最初にrigidBodyの重力を使わなくする
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
    private void SetLocalGravity()
    {
        rBody.AddForce(localGravity, ForceMode.Acceleration);
    }
}
