using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringStreetLight : MonoBehaviour
{
    public Light2D lightSpot1;
    public Light2D lightSpot2;
    public GameObject sparkEffect;
    /*public float flickerIntensity;*/
    public float flickerSpeed;
    public float minIntensity;
    public float maxIntensity;
    public float dimmingDuration;
    public float dimmingDelay;

    private float originalIntensity;
    private float flickerTimer;
    private bool isDimming;
    private bool isRestoring;

    private void Start()
    {
        originalIntensity = lightSpot1.intensity;
        flickerTimer = flickerSpeed;

        // Start the flickering and dimming process
        InvokeRepeating("StartFlickering", 0f, flickerSpeed);
    }

    private void Update()
    {
        CheckState();
    }

    private void StartFlickering()
    {
        if (!isDimming && !isRestoring)
        {
            float randomValue = Random.value;

            if (randomValue < 0.1f)
            {
                StartDimming();
            }
        }
    }

    private void StartDimming()
    {
        isDimming = true;
    }

    private void StartRestoring()
    {
        isRestoring = true;
    }

    private void Flickering()
    {
        flickerTimer -= Time.deltaTime;

        if (flickerTimer <= 0)
        {
            float randomIntensity = Random.Range(minIntensity, maxIntensity);
            float flickerValue = originalIntensity * randomIntensity;

            lightSpot1.intensity = flickerValue;
            lightSpot2.intensity = flickerValue;

            // Random Value for spark effect
            float randomValue = Random.value;
            if (randomValue < 0.5f)
            {
                Instantiate(sparkEffect, lightSpot2.transform.position, Quaternion.identity);
            }

            flickerTimer = flickerSpeed;
        }
    }

    private void Dimming()
    {
        float dimmingAmount = originalIntensity * Time.deltaTime / dimmingDuration;
        lightSpot1.intensity -= dimmingAmount;
        lightSpot2.intensity -= dimmingAmount;
        

        if (lightSpot1.intensity <= 0.3)
        {
            lightSpot1.intensity = 0.3f;
            lightSpot2.intensity = 0.3f;
            
            isDimming = false;
            Invoke("StartRestoring", dimmingDelay);
        }
    }

    private void Restoring()
    {
        float restoringAmount = originalIntensity * Time.deltaTime / dimmingDuration;
        lightSpot1.intensity += restoringAmount;
        lightSpot2.intensity += restoringAmount;

        if (lightSpot1.intensity >= originalIntensity)
        {
            lightSpot1.intensity = originalIntensity;
            lightSpot2.intensity = originalIntensity;
            isRestoring = false;
        }
    }

    private void CheckState()
    {
        if (!isDimming && !isRestoring)
        {
            Flickering();
        }

        if (isDimming)
        {
            Dimming();
        }

        if (isDimming && isRestoring)
        {
            Flickering();
        }

        if (isRestoring)
        {
            Restoring();
        }
    }
}
