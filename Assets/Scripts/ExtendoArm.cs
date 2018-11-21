using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendoArm : MonoBehaviour {
    
    float extendSpeed;

    public void SetExtendSpeed(float speed)
    {
        extendSpeed = speed;
    }

    void Update ()
    {
        Vector3 position = transform.localPosition;
        position.z += extendSpeed;
        position.z = Mathf.Clamp(position.z, 2.5f, 9.0f);
        transform.localPosition = position;
    }
}
