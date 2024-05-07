using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public Vector3 vect = new Vector3(0, 0.2f, 0);
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void ShakeCamera() 
    {
        transform.DOShakePosition(0.7f, vect, 10);
    }
}
