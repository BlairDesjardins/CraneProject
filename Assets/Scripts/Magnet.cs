using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

    private Collider magnetRange;
    private Collider grabRange;
    private GameObject[] barrels;
    private bool magnetPower;

	void Start () {
        magnetRange = transform.Find("Magnet Range").GetComponent<Collider>();
        barrels = GameObject.FindGameObjectsWithTag("Barrel");
        magnetPower = false;
    }

    void Update()
    {
        if (magnetPower)
        {
            foreach (GameObject barrel in barrels)
            {
                Collider barrelColl = barrel.GetComponent<Collider>();

                if (magnetRange.bounds.Intersects(barrelColl.bounds))
                {
                    Vector3 heading = ((transform.position - new Vector3(0, .2f, 0)) - barrel.transform.position).normalized;
                    //float distance = Vector3.Distance(transform.position, barrel.transform.position);
                    barrel.GetComponent<Rigidbody>().AddForce(heading * 1.5f);
                }
            }
        }
    }

    public void SetMagnetPower(bool value)
    {
        magnetPower = value;
    }
}
