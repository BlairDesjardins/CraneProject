using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButton : Interactable
{
    public GameObject magnet;

    private float upState = 0.003f;
    private float downState = -0.012f;
    private bool pressed;

    private void Start()
    {
        pressed = false;
    }

    private void Update()
    {
        Material mat = transform.Find("Button").GetComponent<Renderer>().material;

        if (pressed == true)
        {
            Vector3 buttonPos = transform.Find("Button").localPosition;
            buttonPos.y = Mathf.Lerp(buttonPos.y, downState, Time.deltaTime * 10f);
            transform.Find("Button").localPosition = buttonPos;
            mat.color = Color.green;
            magnet.GetComponent<Magnet>().SetMagnetPower(true);
        }
        else if (pressed == false)
        {
            Vector3 buttonPos = transform.Find("Button").localPosition;
            buttonPos.y = Mathf.Lerp(buttonPos.y, upState, Time.deltaTime * 10f);
            transform.Find("Button").localPosition = buttonPos;
            mat.color = Color.red;
            magnet.GetComponent<Magnet>().SetMagnetPower(false);
        }
    }

    public override void OnHoverEnter(WandController ctrl)
    {
        ctrl.input.TriggerHapticPulse((ushort)(500));
    }

    protected override void OnBeginInteraction()
    {
        pressed = !pressed;
    }

}