using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviourSingleton<UIManager>
{

    public Canvas MainCanvas;
    public StartScreen StartUI;
    public GameplayScreen GamePlay;
    public LevelCompleteScreen LevelComplete;
    public LevelFailScreen LevelFail;
    public CanvasGroup bloodEffectCanvasGroup;
    public Image FinalPanel;
    public enum UIScreens
    {
        StartUI,
        GamePlay,
        LevelComplete,
        LeveFail
    
    }
    void Start()
    {
        ShowScreen(UIScreens.StartUI);
    }
    public void ShowScreen(UIScreens screen) 
    {
        switch (screen)
        {
            case UIScreens.StartUI:
                StartUI.gameObject.SetActive(true);
                break;
            case UIScreens.GamePlay:
                GamePlay.gameObject.SetActive(true);
                break;
            case UIScreens.LevelComplete:
                LevelComplete.gameObject.SetActive(true);
                break;
            case UIScreens.LeveFail:
                LevelFail.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void HideAllScreen() 
    {
        StartUI.gameObject.SetActive(false);
        GamePlay.gameObject.SetActive(false);
        LevelComplete.gameObject.SetActive(false);
        LevelFail.gameObject.SetActive(false);
    }
    public void ShowBloodEffect() 
    {
        bloodEffectCanvasGroup.DOFade(1,0.5f).OnComplete(()=> 
        {
            bloodEffectCanvasGroup.DOFade(0, 0.3f).SetAutoKill(true);
        }).SetAutoKill(true);
    }
    public void FadeIn() 
    {
        FinalPanel.gameObject.SetActive(true);
        FinalPanel.DOFade(0.95f,1f).OnComplete( () => 
        {
         
            FadeOut();
        }
        ).SetAutoKill(true);
    }
    public void FadeOut() 
    {
        FinalPanel.DOFade(0f,1f).OnComplete(() =>
        { 
            FinalPanel.gameObject.SetActive(false);
        }
        ).SetAutoKill(true);

    }
}
