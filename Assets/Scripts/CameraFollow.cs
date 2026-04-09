using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // 拖你的主角进去
    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
        // 只跟随 X 轴，Y 和 Z 保持相机原来位置
        Vector3 targetPos = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );

        // 平滑跟过去
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}