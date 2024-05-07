using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartCollider : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow") )
        {
            if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("EnemyHead"))
            {
                enemy.GetComponent<Idamage>().GetDamage(100);
            }
          
        }
    }
}
