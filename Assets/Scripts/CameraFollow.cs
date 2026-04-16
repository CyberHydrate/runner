using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    public float dashOffset = 2f; // 闪避时相机右移距离

    private PlayerMove player;

    void Start()
    {
        player = target.GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        float offset = 0f;

        // 如果在闪避 → 相机往右偏
        if (player != null && player.isDashing)
        {
            offset = dashOffset;
        }

        Vector3 targetPos = new Vector3(
            target.position.x + offset,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}