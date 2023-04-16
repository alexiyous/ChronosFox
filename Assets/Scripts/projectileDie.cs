using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDie : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
