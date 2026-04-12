using UnityEngine;

// 敌人生成器：控制随机时间在指定位置生成敌人
public class EnemySpawner : MonoBehaviour
{
    [Header("=== 核心配置 ===")]
    [Tooltip("要生成的敌人预制体（拖进来）")]
    public GameObject enemyPrefab;

    [Tooltip("敌人生成的位置（拖SpawnPoint进来）")]
    public Transform spawnPoint;

    [Header("=== 生成间隔设置 ===")]
    [Tooltip("最小生成间隔（秒），比如1秒")]
    public float minSpawnInterval = 1.5f;

    [Tooltip("最大生成间隔（秒），比如3秒")]
    public float maxSpawnInterval = 3.5f;

    // 内部计时器，不用改
    private float _currentTimer;
    private float _nextSpawnTime;

    // 游戏开始时，初始化第一次生成时间
    void Start()
    {
        // 随机设置第一次生成的时间
        SetRandomNextSpawnTime();
    }

    // 每帧更新，计时生成
    void Update()
    {
        // 计时器累加每帧的时间
        _currentTimer += Time.deltaTime;

        // 当计时器超过下一次生成时间，就生成敌人
        if (_currentTimer >= _nextSpawnTime)
        {
            SpawnEnemy(); // 生成敌人
            _currentTimer = 0; // 重置计时器
            SetRandomNextSpawnTime(); // 重新随机下一次生成时间
        }
    }

    // 随机生成下一次的时间（在min和max之间）
    void SetRandomNextSpawnTime()
    {
        _nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // 生成敌人的核心方法
    void SpawnEnemy()
    {
        // 安全检查：防止预制体/生成点没赋值报错
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("EnemySpawner：敌人预制体或生成点没赋值！请检查Inspector面板");
            return;
        }

        // 实例化（生成）敌人：位置=生成点位置，旋转=默认
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    // 【可选】游戏结束时调用，停止生成敌人
    public void StopSpawning()
    {
        enabled = false; // 禁用脚本，停止Update
    }
}