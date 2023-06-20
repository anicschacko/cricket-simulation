using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMMenuBehaviour : MonoBehaviour
{
    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Button playButton;
    [SerializeField] private Toggle bowlingAutomate;

    internal void ShowMenu()
    {
        RestartAnimations(mainCameraAnimator.gameObject);
        RestartAnimations(canvasAnimator.gameObject);
        this.gameObject.SetActive(true);
    }

    void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
    }

    //Triggered by the Animation event on mainCameraAnimation
    public void OnAnimationComplete()
    {
        Debug.Log("Animation Complete");
    }

    private void RestartAnimations(GameObject go)
    {
        go.SetActive(false);
        go.SetActive(true);
    }

    private void OnClickPlay()
    {
        var info = mainCameraAnimator.GetCurrentAnimatorStateInfo(0);
        var delay = info.length + info.normalizedTime;
        mainCameraAnimator.SetTrigger("GameScene");
        this.gameObject.SetActive(false);
        Utils.WaitForSeconds(delay, ()=>
        {
            GameEvents.OnClickPlay?.Invoke(bowlingAutomate.isOn);
        });

    }
}
