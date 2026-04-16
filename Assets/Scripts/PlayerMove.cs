using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Rigidbody2D rb;
    public bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public Animator _anim;

    // ====== …¡±‹≤Œ ˝ ======
    public float dashDistance = 3f;   // …¡±‹æ‡¿Î
    public float dashDuration = 0.2f; // …¡±‹ ±º‰
    public float dashCooldown = 1.5f; // ¿‰»¥ ±º‰

    public bool isDashing = false;
    private bool canDash = true;
    public bool isInvincible = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.gameState == 1 && !isDashing)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (!isDashing)
        {
            rb.velocity = new Vector2(0, 0);
        }

        // ====== …¡±‹¥•∑¢ ======
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            _anim.SetTrigger("Dash");
            StartCoroutine(Dash());
        }

        _anim.SetBool("isRun", true);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // ====== …¡±‹¬ﬂº≠ ======
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        isInvincible = true;

        float elapsed = 0f;
        float startX = transform.position.x;
        float targetX = startX + dashDistance;

        while (elapsed < dashDuration)
        {
            float newX = Mathf.Lerp(startX, targetX, elapsed / dashDuration);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // »∑±£µΩ¥Ô÷’µ„
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        isInvincible = false;
        isDashing = false;

        // ¿‰»¥
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}