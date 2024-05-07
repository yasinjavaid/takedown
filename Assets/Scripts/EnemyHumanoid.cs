using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHumanoid : MonoBehaviour, Idamage, IHeadingForTarget
{
    public enum weapons 
    {
        sword,
        axe,
        hammer
    }
    public NavMeshAgent agent;
    public Animator anim;
    public float Health = 100;
    public string idle, idle2, idle3, animBoolAttack, animBoolRun, animBoolDie;
    private bool isHeadingTowardTarget = false;
    public bool ignoreXrotation;
    private bool isDead = false;
    public weapons weapon;
    // Start is called before the first frame update
    void Start()
    {
        switch (weapon)
        {
            case weapons.sword:
                anim.SetTrigger(idle2);
                break;
            case weapons.axe:
                anim.SetTrigger(idle);
                break;
            case weapons.hammer:
                anim.SetTrigger(idle3);
                break;
            default:
                break;
        }
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
        if (isHeadingTowardTarget)
        {
            //var dir = GameManager.Instance.player.targetForEnemy.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            if (!ignoreXrotation)
            {
                transform.LookAt(GameManager.Instance.player.targetForEnemy.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 40, transform.eulerAngles.z);
            }
            else
            {
                transform.LookAt(GameManager.Instance.player.targetForEnemy.position);
            }
        }
    }
    public void Attack()
    {
        if (isDead || GameManager.Instance.levelFail) return;
        anim.SetTrigger(animBoolAttack);
        agent.isStopped = true;
        isHeadingTowardTarget = false;

        StartCoroutine(Reattack());
    }
    IEnumerator Reattack()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.Instance.ShowBloodEffect();
        yield return new WaitForSeconds(1.2f);
        GameManager.Instance.player.GetDamage(40);
        Attack();
    }
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        anim.SetTrigger(animBoolDie);
        GameManager.Instance.levelManager
            .levels[GameManager.Instance.levelManager.currentLevel]
            .RemoveEnemyFromList(gameObject);

        StartCoroutine(Utilities.Instance.WaitforFrames(2, () =>
        {
            GameManager.Instance.levelManager
             .levels[GameManager.Instance.levelManager.currentLevel].AttackAllEnemies();
        }));
    }
    public void GetDamage(int demage)
    {
        if (!isDead)
        {
            Health -= demage;
            if (Health <= 0)
            {

                Die();
            }
        }
    }
    public void HeadingToTarget()
    {
        anim.SetTrigger(animBoolRun);
        isHeadingTowardTarget = true;
        agent.SetDestination(GameManager.Instance.player.targetForEnemy.position);
    }
}