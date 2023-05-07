using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(light, .1f);
        Destroy(gameObject, 2f);
    }

}
