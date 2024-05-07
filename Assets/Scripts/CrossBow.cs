using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrossBow : MonoBehaviour , Idamage
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject throwPoint;
    [SerializeField] private GameObject arrowSpawnPoint;
    [SerializeField] private Animation animations;
    [SerializeField] private GameObject DumyArrow;
    [SerializeField] private Vector3 crossbowRotatoin;
    public GameObject aimPoint;
    public Transform targetForEnemy;
    public int Health = 100;
    public float force = 500;
    private bool isReadyToThow = false;
    
    private void Start()
    {
       // CreateArrow();
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            aimPoint.SetActive(true);
            // assign new position to where finger was pressed
            aimPoint.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y , aimPoint.transform.position.z);
            aimPoint.transform.DOMove(aimPoint.transform.position, 0.5f).OnComplete(() => { aimPoint.SetActive(false); });
        }



    }
    public void RotateBow(Vector3 touchToWordPos)
    {
        var vec = touchToWordPos - transform.position;
        vec = new Vector3(vec.x, vec.y + 10, vec.z);
        transform.rotation =
              Quaternion.LookRotation(-(vec).normalized,
                  Vector3.up);
        StartCoroutine(Utilities.Instance.WaitforFrames(10,
           () =>
               {
                   if (isReadyToThow)
                   {
                       ThrowArrow();
                   }
               }
           ));
    }
    public void CreateArrow() 
    {                  
        transform.DORotate(new Vector3(
            -30, 
            transform.eulerAngles.y,
            transform.eulerAngles.z),0.2f).SetDelay(0.2f)
            .OnComplete(ResetBow);
     
    }
    private void ResetBow() 
    {
        DumyArrow.SetActive(true);
        animations.Play("CrossbowFullPull");
        transform.DORotate(GameManager.Instance.levelManager.levels[GameManager.Instance.levelManager.currentLevel].
            waypoint[GameManager.Instance.levelManager.levels[GameManager.Instance.levelManager.currentLevel].waypoint.Length-1].
            playerRotPoint, 0.2f).SetDelay(0.2f).OnComplete(()=>
          {
              isReadyToThow = true;
          });
       // transform.DOMove(crossbowPos,0.5f);
    }
    public void ThrowArrow()
    {
        isReadyToThow = false;
        DumyArrow.SetActive(false);
        animations.Play("CrossbowFire");
        var clowArrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, Quaternion.identity);
        var clowArrowRigidbody = clowArrow.GetComponent<Rigidbody>();
        var dirFromPlayer = throwPoint.transform.position - clowArrow.transform.position ;
        clowArrow.transform.rotation = Quaternion.LookRotation((dirFromPlayer).normalized,
               Vector3.up);
        clowArrowRigidbody.useGravity = true;
        clowArrowRigidbody.AddForce(dirFromPlayer * force, ForceMode.Force);
        GameManager.Instance.cameraManager.ShakeCamera();
        StartCoroutine(Utilities.Instance.WaitforFrames(1,
            ()=> 
                {
                    clowArrowRigidbody.gameObject.GetComponent<Arrow>().isShot = true;
                    CreateArrow();
                }
          
                
                
                ));
    }

    public void GetDamage(int damage)
    {
        if (Health >= 0)
        {
            Health -= damage;
        }
        else
        {
            GameManager.Instance.LevelFail();
        }
    }
}
