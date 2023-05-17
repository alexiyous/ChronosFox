using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireFlicker : MonoBehaviour
{
    Light2D _light;

    public float minimumIntensity;
    public float maximumIntensity;

    public float minRadius;
    public float maxRadius;

    public float duration;
    public float durationRandom;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light2D>();
        _light.intensity = minimumIntensity;
        _light.pointLightOuterRadius = minRadius;
        StartCoroutine(ChangeIntensity());

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private IEnumerator ChangeIntensity()
    {
        while(true)
        {
            elapsedTime = 0f;
            durationRandom = Random.Range(duration * 0.5f, duration);
            float currentIntensity = minimumIntensity;
            while (elapsedTime < durationRandom)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / durationRandom;
                _light.intensity = Mathf.Lerp(minimumIntensity, maximumIntensity, percentageComplete);
                _light.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, percentageComplete);

                yield return null;
            }

            float temp = minimumIntensity;
            minimumIntensity = maximumIntensity;
            maximumIntensity = temp;

            float temp2 = minRadius;
            minRadius = maxRadius;
            maxRadius = temp2;
        }
        
    }
}
