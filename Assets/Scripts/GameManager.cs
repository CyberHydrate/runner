using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 全局唯一入口

    private void Awake()
    {
        instance = this;
    }

    // 游戏结束方法（外部只调用这个）
    public void GameOver()
    {
        // 停止玩家移动
        if (FindObjectOfType<PlayerMove>() != null)
        {
            FindObjectOfType<PlayerMove>().enabled = false;
        }

        // 停止生成敌人
        if (FindObjectOfType<EnemySpawner>() != null)
        {
            FindObjectOfType<EnemySpawner>().enabled = false;
        }

        // 停止玩家射击
        if (FindObjectOfType<PlayerShoot>() != null)
        {
            FindObjectOfType<PlayerShoot>().enabled = false;
        }

        Debug.Log("【游戏结束】");
    }
}