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
        float distance = player.transform.position.x;
        counter.text = ((int)distance).ToString() + " m";
    }
}
