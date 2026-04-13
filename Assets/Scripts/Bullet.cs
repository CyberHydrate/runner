using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 子弹碰到敌人
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().Die();
            Destroy(gameObject);            // 子弹也消失
        }

        // 碰到地面/箱子也销毁（防止子弹乱飞）
        if (collision.CompareTag("Ground") || collision.CompareTag("AmmoBox"))
        {
            Destroy(gameObject);
        }
    }
}