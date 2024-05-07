using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public CrossBow player;
    public Camera mianCamera;
    public LevelManager levelManager;
    public CameraManager cameraManager;
    public GameObject winConfetti;
    public bool levelFail = false;

    private int playerHitsCounts = 0;
    public void Awake()
    {
        InstantiateLevel();
    }

    private void InstantiateLevel()
    {
        mianCamera.transform.parent.gameObject.transform.parent = player.transform;
        player.transform.position = levelManager.levels[levelManager.currentLevel].waypoint[0].playerPosPoint;
        player.transform.eulerAngles = levelManager.levels[levelManager.currentLevel].waypoint[0].playerRotPoint;
        /*        mianCamera.transform.localPosition = levelManager.levels[levelManager.currentLevel].cameraPos;
                mianCamera.transform.localEulerAngles = levelManager.levels[levelManager.currentLevel].CameraRotation;*/
        StartCoroutine(Utilities.Instance.WaitforFrames(1,()=>
        {
            player.gameObject.SetActive(false);
            mianCamera.transform.parent.gameObject.transform.parent = null;
        }
        ));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartGame() 
    {
        player.gameObject.SetActive(true);
        player.CreateArrow();
    }
    
    public void GameComplete() 
    {
        UIManager.Instance.HideAllScreen();
        UIManager.Instance.ShowScreen(UIManager.UIScreens.LevelComplete);
        winConfetti.SetActive(true);
    }
    public void LevelFail() 
    {
        UIManager.Instance.ShowScreen(UIManager.UIScreens.LeveFail);
        levelManager.levels[levelManager.currentLevel].gameObject.SetActive(false);
        levelFail = true;
    }
    public void Restart() 
    {
        SceneManager.LoadScene("Day scene");
    }
}
