using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNoY : MonoBehaviour
{
    public Camera cam;

    public Transform subject;

    Vector2 startPos;

    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPos;
    float distFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distFromSubject) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        Vector2 newPos = startPos + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, transform.position.y, startZ);
    }
}
