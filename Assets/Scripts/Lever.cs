using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {

    public float BreakDistance = .2f;

    public GameObject craneArm;

    Transform handle;
    Transform handleBall;
    Transform start;
    Transform curr;
    Transform basePoint;

    void Start () {
        handle = transform.Find("Handle");
        handleBall = handle.Find("HandleBall");
        start = transform.Find("StartPos");
        curr = handle.Find("CurrentPos");
        basePoint = transform.Find("BasePoint");
    }

    void Update()
    {
        Vector3 pos = start.InverseTransformPoint(curr.position);

        if (attachedController != null) {
            Vector3 controllerPos = attachedController.transform.position;
            Vector3 invContPos = basePoint.InverseTransformPoint(controllerPos);

            float distanceToHandleBall = Vector3.Distance(controllerPos, handleBall.position);
            if (distanceToHandleBall > BreakDistance || invContPos.y < 0) {
                attachedController.input.TriggerHapticPulse(2999);
                DetachController();
                return;
            }
            

            Vector3 relativePos = (controllerPos - handle.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(relativePos, handle.up);
            handle.localRotation = Quaternion.Slerp(handle.localRotation, rotation, Time.deltaTime * 4f);
            attachedController.input.TriggerHapticPulse(500);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(-135, 0, 0);
            handle.rotation = Quaternion.Slerp(handle.rotation, rotation, Time.deltaTime * 4f);
        }

        craneArm.transform.Rotate(Vector3.up, pos.x * 2f, Space.World);
        if (craneArm.transform.eulerAngles.y <= 310 && craneArm.transform.eulerAngles.y >= 309) craneArm.transform.rotation = Quaternion.Euler(0, -49.9f, 0);
        if (craneArm.transform.eulerAngles.y >= 50 && craneArm.transform.eulerAngles.y <= 51) craneArm.transform.rotation = Quaternion.Euler(0, 49.9f, 0);
        Transform pitch = craneArm.transform.Find("Pitch");
        pitch.Rotate(Vector3.right, pos.z * -2f);
        if (pitch.eulerAngles.x >= 355 && pitch.eulerAngles.x <= 356) pitch.localRotation = Quaternion.Euler(354.9f, 0, 0);
        if (pitch.eulerAngles.x <= 310 && pitch.eulerAngles.x >= 309) pitch.localRotation = Quaternion.Euler(310.1f, 0, 0);
    }

    public override void OnHoverEnter(WandController ctrl) {
        ctrl.input.TriggerHapticPulse((ushort)(500));
    }
}
