using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("移动设置")]
    public float speed = 2f;

    [Header("攻击检测")]
    public float attackRange = 2f; // 玩家进入这个范围，敌人播攻击动画
    private Transform _player;

    private Animator _anim;
    private bool _isDead = false;
    private bool _isAttacking = false;

    void Start()
    {
        _anim = GetComponent<Animator>();

        // 自动寻找玩家
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) _player = p.transform;

        // 初始化移动动画
        _anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (_isDead) return;

        // 1. 持续向左移动
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 2. 检测与玩家的距离以触发攻击动画
        if (_player != null)
        {
            float distance = Vector2.Distance(transform.position, _player.position);

            // 玩家进入范围，并且当前不在攻击 → 触发攻击
            if (distance <= attackRange && !_isAttacking)
            {
                _isAttacking = true;
                _anim.SetTrigger("Attack");
            }
        }
    }

    public void OnAttackEnd()
    {
        _isAttacking = false;
    }

    // 处理死亡（被子弹打中）
    public void Die()
    {
        if (_isDead) return;

        _isDead = true;
        _anim.SetTrigger("Dead"); // 触发死亡动画

        // 禁用碰撞体，防止尸体还能撞死玩家
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到的是子弹
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // 销毁子弹
        }
    }

    public void OnDeathEnd()
    {
        Destroy(gameObject);
    }
}