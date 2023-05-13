using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToPlayer : MonoBehaviour
{
    public Transform player;
    public GameObject pivot;
    public bool isInRange;
    public BoxCollider2D range;
    public int dir = 180;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            Aim();
        }
    }

    public void Aim()
    {
        Vector3 direction = player.position - transform.position;
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.Euler(0, 0, rotation + dir);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
