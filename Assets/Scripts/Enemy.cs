using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Idamage, IHeadingForTarget
{ 
    public NavMeshAgent agent;
    public Animation anim;
    public string attackAnimName, hitAnimName, dieAnimName, runAnimName;
    public bool igonreHit;
    public int health = 100;
    public bool ignoreXrotation;
    private bool isHeadingTowardTarget = false;
    private bool isDead = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
       
    }
    private void Update()
    {
        if (isHeadingTowardTarget && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Attack();
                }
            }
        }
        if (isHeadingTowardTarget)
        {
            //var dir = GameManager.Instance.player.targetForEnemy.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            if (!ignoreXrotation)
            {
                transform.LookAt(GameManager.Instance.player.targetForEnemy.position);
                transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
            }
            else
            { 
                transform.LookAt(GameManager.Instance.player.targetForEnemy.position);
            }
        }
    }
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        if (!igonreHit)
        {
            anim[hitAnimName].speed = 0.8f;
            anim.CrossFade(hitAnimName, 0.3f, PlayMode.StopSameLayer);
        }
        anim[dieAnimName].speed = 0.7f;
        anim.CrossFadeQueued(dieAnimName, 1f,QueueMode.CompleteOthers);
        GameManager.Instance.levelManager
            .levels[GameManager.Instance.levelManager.currentLevel]
            .RemoveEnemyFromList(gameObject);

        StartCoroutine(Utilities.Instance.WaitforFrames(2, () =>
        {
            GameManager.Instance.levelManager
             .levels[GameManager.Instance.levelManager.currentLevel].AttackAllEnemies();
        }));
    }
   
    public void Attack()
    {
        if (isDead || GameManager.Instance.levelFail) return;
        anim.Play(attackAnimName);
        agent.isStopped = true;
        isHeadingTowardTarget = false;
    
        StartCoroutine(Reattack());
    }
    IEnumerator Reattack() 
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.Instance.ShowBloodEffect();
        yield return new WaitForSeconds(0.7f);
        GameManager.Instance.player.GetDamage(40);
        Attack();
    }
    public void GetDamage(int damage)
    {
        if (!isDead)
        {
            Die();
            /* hea -= damage;
             slider.value = bearHealth;
             if (bearHealth <= 0)
             {

                 Die();
             }*/
        }
    }
    public void HeadingToTarget()
    {
        anim.Play(runAnimName);
        isHeadingTowardTarget = true;
        agent.SetDestination(GameManager.Instance.player.targetForEnemy.position);
    }
}
