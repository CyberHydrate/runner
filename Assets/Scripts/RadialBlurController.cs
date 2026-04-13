using UnityEngine;

public class RadialBlurController : MonoBehaviour
{
    public GameObject blurObject;
    public Transform player;

    public float blurStartDistance = 100f;

    bool hasActivated = false;

    void Start()
    {
        blurObject.SetActive(false);
    }

    void Update()
    {
        float distance = player.transform.position.x;

        if (!hasActivated && distance >= blurStartDistance)
        {
            hasActivated = true;
            blurObject.SetActive(true);
        }
    }
}