using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBox : MonoBehaviour {

    public ParticleSystem confetti;

    private GameObject[] barrels;
    private bool winState;

    void Start () {
        barrels = GameObject.FindGameObjectsWithTag("Barrel");
        winState = false;
    }

    void Update()
    {
        int bucketsInBox = 0;
        foreach (GameObject barrel in barrels)
        {
            Collider barrelColl = barrel.GetComponent<Collider>();

            if (GetComponent<Collider>().bounds.Intersects(barrelColl.bounds))
            {
                bucketsInBox++;
            }
        }
        if (bucketsInBox == barrels.Length && winState == false)
        {
            Debug.Log("Win!");
            winState = true;
            confetti.Play();
        }
    }
}
