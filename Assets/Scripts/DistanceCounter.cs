using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI counter;
    void FixedUpdate()
    {
        counter.text = player.transform.position.x.ToString() + " m";
    }
}
