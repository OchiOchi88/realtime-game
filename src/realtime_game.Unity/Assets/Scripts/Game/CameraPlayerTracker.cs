using UnityEngine;

public class CameraPlayerTracker : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float distance; // カメラとプレイヤー間の距離
    public float height; // 高さ
    public float smoothSpeed; // 回転速度

    private void Update()
    {
        float my = Input.GetAxis("Mouse Y");
        float mx = Input.GetAxis("Mouse X");

        if (Mathf.Abs(mx) > 0.00001f)
        {
            mx = mx * 5;

            // 回転軸固定
            transform.RotateAround(player.transform.position, Vector3.up, mx);

        }

        if (Mathf.Abs(my) > 0.00001f)
        {
            if ((height - my) < -2 || (height - my) > 4)
            {
                my = 0;
            }
            height -= my / 10;
        }
    }
    private void FixedUpdate()
    {

        Vector3 playerCenter = player.transform.position + Vector3.up * height;

        Vector3 targetPosition = playerCenter - player.transform.forward * distance;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(player.transform);
    }
    public void SendReference(GameObject characterObject)
    {
        player = characterObject;
    }
}
