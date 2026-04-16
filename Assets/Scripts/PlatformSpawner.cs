using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float platformWidth = 10f; // 一块平台的长度
    public float platformY = 2f;      // 平台固定高度
    public GameObject platformSpawner;
    public float counter;

    void Start()
    {
        SpawnPlatform();
    }

    void Update()
    {
        if (GameManager.instance.gameState==1)
        {
            counter += Time.deltaTime;
            if (counter > 1)
            {
                SpawnPlatform();
                counter = 0;
            }
        }
    }

    public void SpawnPlatform()
    {
        Vector3 pos = new Vector3(platformSpawner.transform.position.x, platformY, 0);
        Instantiate(platformPrefab, pos, Quaternion.identity);

        // 关键：往前移动生成点
        platformSpawner.transform.position += new Vector3(platformWidth, 0, 0);
    }
}