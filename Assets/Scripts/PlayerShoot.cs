using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [Header("子弹")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    [Header("弹药")]
    public int maxBullet = 6;
    [SerializeField] 
    public int currentBullet;

    public float shootDelay = 0.2f;
    private Animator _anim;
    private bool _isShooting = false;
    private bool _isDead = false;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.gameState == 1 && !_isShooting && ! _isDead)
        {
            if (currentBullet > 0)
            {
                StartCoroutine(ShootRoutine());
            }
            else
            {
                Debug.Log("没子弹了，不播动画");
            }
        }
    }

    //delay shoot anim
    IEnumerator ShootRoutine()
    {
        _isShooting = true;

        _anim.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootDelay);

        if (currentBullet > 0)
        {
            Shoot();
        }

        _isShooting = false;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // 生成子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //  关键修复：根据角色朝向（localScale.x）自动判断方向
        float dir = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(dir * bulletSpeed, 0);

        currentBullet--;
        Destroy(bullet, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰到的物体标签是不是 "AmmoBox"
        if (collision.CompareTag("AmmoBox"))
        {
            Debug.Log("114");
            // 补满子弹
            if (currentBullet <= 3) currentBullet = currentBullet + 3;
            else currentBullet = maxBullet;

            // 销毁补给箱
            Destroy(collision.gameObject);

            Debug.Log("子弹补满啦！");
        }
        if(collision.CompareTag("Enemy"))
        {
            //Die();
            Debug.Log("Die");
            GameManager.instance.GameOver();
        }
    }
}