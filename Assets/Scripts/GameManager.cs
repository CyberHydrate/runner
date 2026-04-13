using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 全局唯一入口
    public GameObject gameStartPanel;
    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject player;
    public TextMeshProUGUI endtext;
    public int gameState;//0未开始，1进行中，2游戏结束

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        switch (gameState)
        {
            case 0:
                gameStartPanel.SetActive(true);
                gamePlayPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case 1:
                gameStartPanel.SetActive(false);
                gamePlayPanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case 2:
                endtext.text = "Distance : " + player.transform.position.x.ToString()+"m";
                gameStartPanel.SetActive(false);
                gamePlayPanel.SetActive(false);
                gameOverPanel.SetActive(true);
                
                break;
        }
    }
    public void SetGameState(int i)
    {
        gameState = i;
    }
    public void GameStart()
    {
        SetGameState(1);
        if (FindObjectOfType<PlayerMove>() != null)
        {
            FindObjectOfType<PlayerMove>().enabled = true;
        }


        if (FindObjectOfType<EnemySpawner>() != null)
        {
            FindObjectOfType<EnemySpawner>().enabled = true;
        }


        if (FindObjectOfType<PlayerShoot>() != null)
        {
            FindObjectOfType<PlayerShoot>().enabled = true;
        }
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
        SetGameState(2);
        
    }
    public void Restart()
    {
        SetGameState(0);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}