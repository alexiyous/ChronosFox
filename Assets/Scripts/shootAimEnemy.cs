using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using UnityEngine.Rendering.Universal;

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
    public bool isInRange;

    Movement mov;

    public Light2D _light;

    [SerializeField]
    Color colorOn = Color.red;
    [SerializeField]
    Color colorOff = Color.green;

    void Start()
    {
        shotCD = startShotCD;
        projectileClock = Timekeeper.instance.Clock("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        
        if(shotCD <= 0)
        {
            Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);

            shotCD = startShotCD ;
            AudioManager.instance.PlaySFXAdjusted(8);
        } else
        {
            shotCD -= Time.deltaTime * projectileClock.timeScale;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!isInRange)
            {
                ChangeToColorOn();
                PlayInRangeSound();
            }
            isInRange = true;
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isInRange)
            {
                ChangeToColorOff();
                PlayOutOfRangeSound();
            }
            isInRange = false;
        }
    }

    void PlayInRangeSound()
    {
        AudioManager.instance.PlaySFXAdjusted(9);
    }

    void PlayOutOfRangeSound()
    {
        AudioManager.instance.PlaySFXAdjusted(7);
    }

    public void ChangeToColorOn()
    {
        _light.color = colorOn;
        _light.intensity = 1f;
    }

    public void ChangeToColorOff()
    {
        _light.color = colorOff;
        _light.intensity = 0.85f;
    }
}

