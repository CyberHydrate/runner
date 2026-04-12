using UnityEngine;

// 弹药箱生成器：在指定时间间隔于指定位置生成弹药箱
public class AmmoBoxSpawner : MonoBehaviour
{
    [Header("=== 生成配置 ===")]
    [Tooltip("要生成的弹药箱预制体（拖拽赋值）")]
    public GameObject ammoBoxPrefab;

    [Tooltip("生成的位置（挂载SpawnPoint空物体）")]
    public Transform spawnPoint;

    [Header("=== 生成间隔设置 ===")]
    [Tooltip("最小生成间隔时间（建议≥2）")]
    public float minSpawnInterval = 5f;

    [Tooltip("最大生成间隔时间（建议≤10）")]
    public float maxSpawnInterval = 8f;

    // 内部计时器相关变量
    private float _currentTimer;
    private float _nextSpawnTime;

    // 游戏开始时初始化第一次生成时间
    void Start()
    {
        // 随机设置下一次生成时间
        SetRandomNextSpawnTime();
    }

    // 每帧更新计时器
    void Update()
    {
        // 累加每帧的时间
        _currentTimer += Time.deltaTime;

        // 当计时器达到下一次生成时间时，生成弹药箱
        if (_currentTimer >= _nextSpawnTime)
        {
            SpawnAmmoBox(); // 生成弹药箱
            _currentTimer = 0; // 重置计时器
            SetRandomNextSpawnTime(); // 重新随机设置下一次生成时间
        }
    }

    // 随机设置下一次生成时间（在min和max之间）
    void SetRandomNextSpawnTime()
    {
        _nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // 生成弹药箱的核心方法
    void SpawnAmmoBox()
    {
        // 安全校验：防止预制体/生成点未赋值
        if (ammoBoxPrefab == null || spawnPoint == null)
        {
            Debug.LogError("AmmoBoxSpawner的预制体或生成位置未赋值，请在Inspector面板中设置！");
            return;
        }

        // 实例化弹药箱：位置=生成点位置，旋转=默认无旋转
        Instantiate(ammoBoxPrefab, spawnPoint.position, Quaternion.identity);
    }

    // 外部调用：停止生成弹药箱（例如关卡结束/玩家拾取后）
    public void StopSpawning()
    {
        enabled = false; // 禁用脚本，停止Update执行
    }

    // 扩展方法：恢复生成弹药箱（可选）
    public void ResumeSpawning()
    {
        enabled = true;
        _currentTimer = 0;
        SetRandomNextSpawnTime();
    }
}