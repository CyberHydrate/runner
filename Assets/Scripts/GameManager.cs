using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 全局唯一入口
    public GameObject gameStartPanel;
    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject player;
    public TextMeshProUGUI endtext;
    public GameObject platformPrefab;
    public int gameState;//0未开始，1进行中，2游戏结束

    bool mark=false;
    private void Awake()
    {
        instance = this;
        mark=false;
    }
    private void Update()
    {
        switch (gameState)
        {
            case 0:
                if (!mark)
                {
                    BeforeStart();

                }
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
                endtext.text = "Distance : " + player.transform.position.x.ToString() + "m";
                gameStartPanel.SetActive(false);
                gamePlayPanel.SetActive(false);
                gameOverPanel.SetActive(true);

                mark = false;
                break;
        }
    }
    public void SetGameState(int i)
    {
        gameState = i;
    }
    public void BeforeStart()
    {
        player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
        player.GetComponent<PlayerMove>()._anim.SetTrigger("start");
        Instantiate(platformPrefab, new Vector3(player.transform.position.x, player.transform.position.y-2.346f, player.transform.position.z), Quaternion.identity);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            Destroy(obj);
        }
        GameObject[] ammoboxs = GameObject.FindGameObjectsWithTag("AmmoBox");
        foreach (GameObject obj in ammoboxs)
        {
            Destroy(obj);
        }
        mark = true;
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

    public void GameOver()
    {
        if (gameState == 2) return;

        SetGameState(2);

        if (player != null)
        {
            Animator _Anim = player.GetComponent<Animator>();
            if (_Anim != null)
            {
                _Anim.SetTrigger("Die");
            }
        }

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

        Debug.Log("lock death anim");
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