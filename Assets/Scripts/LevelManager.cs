using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    [HideInInspector]
    public int currentLevel = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(Utilities.Instance.WaitforSeconds(1,()=> 
        {
            levels[currentLevel].gameObject.SetActive(false);
            currentLevel++;
            if (currentLevel >= levels.Count)
            {
                GameManager.Instance.GameComplete();
            }
            else
            {
                levels[currentLevel].gameObject.SetActive(true);
                levels[currentLevel].MovePlayerToLevel();
            }
        }));
       
    }
}
