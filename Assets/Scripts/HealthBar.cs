using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject barObject;

    public void UpdateHealth(float percentage)
    {
        var original = barObject.transform.localScale;
        barObject.transform.localScale = new Vector3(percentage, original.y, original.z);
    }
    
}
