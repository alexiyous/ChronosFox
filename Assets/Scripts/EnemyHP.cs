using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP = 1;
    private int currentHP;
    public enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = enemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            enemy.Die();
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
