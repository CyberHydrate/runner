using System;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
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
    public PlatformSpawner ps;

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
        //1. 重置玩家
        player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
        player.GetComponent<PlayerMove>()._anim.SetTrigger("start");
        player.GetComponent<PlayerShoot>().currentBullet = 3;

        //2. 清空敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            Destroy(obj);
        }

        //3. 清空弹药箱
        GameObject[] ammoboxs = GameObject.FindGameObjectsWithTag("AmmoBox");
        foreach (GameObject obj in ammoboxs)
        {
            Destroy(obj);
        }

        //4. 清空平台
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in platforms)
        {
            Destroy(obj);
        }

        //5. 重置生成器位置（核心！）
        ps.platformSpawner.transform.position = new Vector3(player.transform.position.x, ps.platformY, 0);

        //6. 生成第一块平台
        for (int i = 0; i < 2; i++)
        {
            ps.SpawnPlatform();
        }

        //7.重置计时器
        ps.counter = 0;

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

        // 获取玩家脚本
        PlayerMove pm = player.GetComponent<PlayerMove>();

        // 如果正在无敌 → 直接不死亡
        if (pm != null && pm.isInvincible)
        {
            Debug.Log("无敌帧中，忽略死亡");
            return;
        }

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