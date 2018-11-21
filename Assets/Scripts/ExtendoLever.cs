using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendoLever : Interactable {

    public float BreakDistance = .2f;
    public GameObject arm;

    Transform lever;
    Transform leverHandle;
    Transform start;
    Transform curr;
    Transform basePoint;

    Vector3 leverAngle = new Vector3(-135f, 0f, 0f);

    void Start () {
        lever = transform.Find("Lever");
        leverHandle = lever.Find("LeverHandle");
        start = transform.Find("StartPos");
        curr = lever.Find("CurrentPos");
        basePoint = transform.Find("BasePoint");
    }
	
	void Update ()
    {
        if (attachedController != null)
        {
            Vector3 controllerPos = attachedController.transform.position;
            Vector3 invContPos = basePoint.InverseTransformPoint(controllerPos);

            float distanceToHandleBall = Vector3.Distance(controllerPos, leverHandle.position);
            if (distanceToHandleBall > BreakDistance || invContPos.y < 0)
            {
                attachedController.input.TriggerHapticPulse(2999);
                DetachController();
                return;
            }

            float xAngle = Mathf.Atan(invContPos.z/invContPos.y) * Mathf.Rad2Deg;
            xAngle = Mathf.Clamp(xAngle, -60f, 60f);
            leverAngle.x = -135f + xAngle;
            lever.rotation = Quaternion.Slerp(lever.rotation, Quaternion.Euler(leverAngle), Time.deltaTime * 4f);
            attachedController.input.TriggerHapticPulse(500);
        }
        else
        {
            leverAngle = new Vector3(-135f, 0, 0);
            lever.rotation = Quaternion.Slerp(lever.rotation, Quaternion.Euler(leverAngle), Time.deltaTime * 4f);
        }

        Vector3 pos = start.InverseTransformPoint(curr.position);
        arm.GetComponent<ExtendoArm>().SetExtendSpeed(pos.z * 0.2f);

    }

    public override void OnHoverEnter(WandController ctrl)
    {
        ctrl.input.TriggerHapticPulse((ushort)(500));
    }
}
