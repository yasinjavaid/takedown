using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public struct WayPoints 
{
    public bool isfade;
    public float time;
    public float delay;
    public Vector3 playerPosPoint;
    public Vector3 playerRotPoint;
}
public class Level : MonoBehaviour
{   [SerializeField]
    private List<GameObject> enemies;
    public int pathStartPoint = 0;
    public WayPoints[] waypoint;
    public float playermovementTime = 0;
    public bool isHeadingToTarget = false;
    private int currentPlayermovePoint = 0;
    private void OnEnable()
    {
        if (isHeadingToTarget)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].gameObject.GetComponent<IHeadingForTarget>().HeadingToTarget(); 
            }
        }
    }
    public void MovePlayerToLevel()
    {

        currentPlayermovePoint = pathStartPoint;
        MovePlayerWayPoint();
       
    }
    public void MovePlayerWayPoint()
    {
        if (currentPlayermovePoint < waypoint.Length)
        {
            if (waypoint[currentPlayermovePoint].isfade)
            {
                UIManager.Instance.FadeIn();
            }
            GameManager.Instance.mianCamera.
              transform.parent.gameObject.
              transform.parent = GameManager.Instance.player.transform;
            GameManager.Instance.player.transform.DOMove(
                waypoint[currentPlayermovePoint].playerPosPoint, waypoint[currentPlayermovePoint].time).SetDelay(waypoint[currentPlayermovePoint].delay).SetAutoKill(true);
            GameManager.Instance.player.transform.DORotate(
                 waypoint[currentPlayermovePoint].playerRotPoint, waypoint[currentPlayermovePoint].time).OnComplete(() =>
                {
                    currentPlayermovePoint++;
                    if (currentPlayermovePoint >= waypoint.Length) 
                    {
                        GameManager.Instance.mianCamera.transform.parent.
                        gameObject.transform.parent = null;
                       
                    }
                    MovePlayerWayPoint();
                }).SetDelay(waypoint[currentPlayermovePoint].delay).SetAutoKill(true);
        }
        

       
    }
    public void AttackAllEnemies()
    {
        StartCoroutine(EnemyTowardsPlayer());
    }
    public IEnumerator EnemyTowardsPlayer() 
    {
        yield return null;
        for (int i = 0; i < enemies.Count; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f,0.7f));
            enemies[i].gameObject.GetComponent<IHeadingForTarget>().HeadingToTarget();
        }
    }
    public void RemoveEnemyFromList(GameObject enemy) 
    {
        enemies.Remove(enemy);
        CheckAllEnemiesRemaining();
    }
    public void CheckAllEnemiesRemaining() 
    {
        if (enemies.Count <= 0)
        {
            StartCoroutine(Utilities.Instance.WaitforSeconds(1,
                ()=> 
                { 
                    GameManager.Instance.levelManager.LoadNextLevel(); 
                }));
        }
    }
}
