using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public float breakForce;
    public float breakTorque;

    float connectRange = 0.3f;

    void Start () {
        ConnectBlock(transform.TransformDirection(Vector3.up));
        ConnectBlock(transform.TransformDirection(Vector3.down));
        ConnectBlock(transform.TransformDirection(Vector3.right));
        ConnectBlock(transform.TransformDirection(Vector3.left));
        ConnectBlock(transform.TransformDirection(Vector3.forward));
        ConnectBlock(transform.TransformDirection(Vector3.back));
    }

    void ConnectBlock(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, connectRange))
        {
            if (hit.transform.tag != "Barrel") {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = hit.rigidbody;
                joint.breakForce = breakForce;
                joint.breakTorque = breakTorque;
            }
        }
    }
    
}
