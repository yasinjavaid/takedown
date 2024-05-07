using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;

public class GameplayScreen : MonoBehaviour
{
    public GameObject onBoarding;
    public GameObject hand;
    private void OnEnable()
    {
        LeanTouch.OnFingerDown += LeanTouch_OnFingerDown;
        hand.transform.DOScale(0.8f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

   

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= LeanTouch_OnFingerDown;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LeanTouch_OnFingerDown(LeanFinger obj)
    {
        onBoarding.SetActive(false);
    }
}
