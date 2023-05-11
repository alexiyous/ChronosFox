using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwaveController : MonoBehaviour
{
    public static shockwaveController instance;

    [SerializeField] private float _shockWaveTime = 0.75f;

    private Coroutine _shockWaveCoroutine;

    private Material _material;

    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _material = GetComponent<SpriteRenderer>().material;
    }


    public void CallShockwave()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }
    public void CallShockwaveInverse()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(1f, -0.1f));
    }

    private IEnumerator ShockWaveAction(float startPosition, float endPosition)
    {
        _material.SetFloat(_waveDistanceFromCenter, startPosition);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;

        while(elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPosition, endPosition, (elapsedTime / _shockWaveTime));
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }
    }
}
