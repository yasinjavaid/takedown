using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTapToStart() 
    {
        GameManager.Instance.StartGame();
        UIManager.Instance.HideAllScreen();
        UIManager.Instance.ShowScreen(UIManager.UIScreens.GamePlay);
    }
}
