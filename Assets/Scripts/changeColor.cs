using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class changeColor : MonoBehaviour
{
    Light2D _light;

    [SerializeField]
    Color colorOn = Color.red;
    [SerializeField]
    Color colorOff = Color.green;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    public void ChangeToColorOn()
    {
        _light.color = colorOn;
    }

    public void ChangeToColorOff()
    {
        _light.color = colorOff;
    }
}
