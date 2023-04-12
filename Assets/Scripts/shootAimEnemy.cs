using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class shootAimEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private float shotCD;
    public float startShotCD = 1f;
    [HideInInspector]
    public Timeline time;
    public Clock projectileClock;

    [Space]
    public GameObject projectile;
    public Transform shootPoint;

    Movement mov;

    void Start()
    {
        shotCD = startShotCD;
        projectileClock = Timekeeper.instance.Clock("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(shotCD <= 0)
        {
            Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);

            shotCD = startShotCD ;
        } else
        {
            shotCD -= Time.deltaTime * projectileClock.timeScale;
        }
    }
}
