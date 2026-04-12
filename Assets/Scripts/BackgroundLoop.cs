using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Header("背景设置")]
    public float scrollSpeed = 2f; // 背景滚动速度，跟玩家速度大致一致
    private float spriteWidth; // 单个背景图的宽度
    public GameObject Player;

    void Start()
    {
        // 获取背景图的宽度（Sprite的x缩放 * 像素宽）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // 持续向左移动
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 如果当前背景图完全移出了屏幕左边
        if (transform.position.x < Player.transform.position.x-25)
        {
            // 把它瞬移到另一张图的右边
            Reposition();
        }
    }

    void Reposition()
    {
        // 计算另一张图的位置 + 自身宽度
        Vector2 newPos = new Vector2(transform.position.x + 2 * spriteWidth, transform.position.y);
        transform.position = newPos;
    }
}