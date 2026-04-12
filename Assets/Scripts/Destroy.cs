using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    // 找到玩家
    private Transform player;

    void Start()
    {
        // 自动找场景里标签为 Player 的物体
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // 敌人X - 玩家X ≤ -20 时销毁
        if (transform.position.x - player.position.x <= -20f)
        {
            Destroy(gameObject);
        }
    }
}