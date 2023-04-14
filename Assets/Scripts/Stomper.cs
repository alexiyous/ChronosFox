using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Stomper : MonoBehaviour
{
    public int dealDamage = 1;
    private Timeline time;
    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        time = transform.parent.GetComponent<Timeline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weak Point"))
        {
            time.rigidbody2D.velocity = new Vector2(0f, 0f);
            time.rigidbody2D.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(dealDamage);
            
        }
    }
}
