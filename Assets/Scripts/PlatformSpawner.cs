using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float platformWidth = 10f; // 一块平台的长度
    public float platformY = 2f;      // 平台固定高度

    private float nextSpawnX;

    void Start()
    {
        // 初始第一块位置
        nextSpawnX = 0;
        SpawnPlatform();
    }

    void Update()
    {
        // 玩家快走到头时生成下一块
        if (transform.position.x > nextSpawnX - 5)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // 严格一块接一块
        Vector3 pos = new Vector3(nextSpawnX, platformY, 0);
        Instantiate(platformPrefab, pos, Quaternion.identity);

        // 下一块就在右边紧挨着
        nextSpawnX += platformWidth;
    }
}