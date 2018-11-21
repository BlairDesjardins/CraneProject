using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : Interactable {

    private float fadeDuration = 1f;
    private float downState = -0.012f;
    private bool pressed;

    private void Start()
    {
        pressed = false;
    }

    private void Update()
    {
        if (pressed == true)
        {
            Vector3 buttonPos = transform.Find("Button").localPosition;
            buttonPos.y = Mathf.Lerp(buttonPos.y, downState, Time.deltaTime * 10f);
            transform.Find("Button").localPosition = buttonPos;
        }
    }

    public override void OnHoverEnter(WandController ctrl)
    {
        ctrl.input.TriggerHapticPulse((ushort)(500));
    }

    protected override void OnBeginInteraction()
    {
        pressed = true;
        fadeOut();
        Invoke("fadeIn", fadeDuration);
        StartCoroutine(ResetScene());
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(fadeDuration);
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    private void fadeOut()
    {
        SteamVR_Fade.Start(Color.clear, 0f);
        SteamVR_Fade.Start(Color.black, fadeDuration);
    }

    private void fadeIn()
    {
        SteamVR_Fade.Start(Color.black, 0f);
        SteamVR_Fade.Start(Color.clear, fadeDuration);
    }

}