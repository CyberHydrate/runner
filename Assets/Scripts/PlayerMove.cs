   using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    public bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator _anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("123");
        }

        _anim.SetBool("isRun", true);
    }

    void FixedUpdate()
    {
        // 左右移动
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        // 地面检测（不掉下去的关键）
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // 角色翻转
        if (h != 0)
        {
            transform.localScale = new Vector3(h, 1, 1);
        }
    }
}