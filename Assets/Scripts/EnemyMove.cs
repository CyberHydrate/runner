using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x-speed,transform.position.y,transform.position.z);
    }
}
