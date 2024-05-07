using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BearEnemy : MonoBehaviour ,Idamage,IHeadingForTarget
{
    public GameObject PlayerPosition;
    public NavMeshAgent agent;
    public Animator anim;
    public float bearHealth = 400;
    public Slider slider;
    public string animBoolAttack, animBoolRun, animBoolDie;
    private bool isHeadingTowardTarget = false;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
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
    }
    public void Attack()
    {
        if (isDead || GameManager.Instance.levelFail) return;
        anim.SetBool(animBoolAttack,true);
        agent.isStopped = true;
        isHeadingTowardTarget = false;

        StartCoroutine(Reattack());
    }
    IEnumerator Reattack()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.Instance.ShowBloodEffect();
        yield return new WaitForSeconds(1.2f);
        GameManager.Instance.player.GetDamage(30);
        Attack();
    }
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        anim.SetBool(animBoolRun,false);
        anim.SetBool(animBoolAttack, false);
        anim.SetBool(animBoolDie,true);
        GameManager.Instance.levelManager
            .levels[GameManager.Instance.levelManager.currentLevel]
            .RemoveEnemyFromList(gameObject);

        StartCoroutine(Utilities.Instance.WaitforFrames(2, () =>
        {
            GameManager.Instance.levelManager
             .levels[GameManager.Instance.levelManager.currentLevel].AttackAllEnemies();
        }));
    }
    public void GetDamage(int damage)
    {
        if (!isDead)
        {
            bearHealth -= damage;
            slider.value = bearHealth;
            if (bearHealth <= 0)
            {

                Die();
            }
        }
    }

    public void HeadingToTarget()
    {
        anim.SetBool(animBoolAttack,false);
        anim.SetBool(animBoolRun,true);
        isHeadingTowardTarget = true;
        agent.SetDestination(PlayerPosition.transform.position);
    }

  

   
}
