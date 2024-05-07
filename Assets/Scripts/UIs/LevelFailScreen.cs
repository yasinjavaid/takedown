using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnRetry() 
    {
        GameManager.Instance.Restart();
    }
}
