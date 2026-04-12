using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;
    public float fixedY;  // 地面高度，比如 -2

    void Update()
    {
        // 只跟着玩家X走，Y永远固定
        transform.position = new Vector3(player.position.x + 12, fixedY, 0);
    }
}