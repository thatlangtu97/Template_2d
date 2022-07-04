using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject attackPoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Attack();
        }
    }
    void Attack()
    {
     /*Collider2D[] hitEnemies  = Physics2D.OverlapCircleAll(attackPoint.transform.position,attackrange,enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);
        }*/
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint = null)
        {
            return;
        }
        //Gizmos.color= Color.red;
        //Gizmos.DrawWireSphere(attackPoint.transform.position, attackrange);
    }
}
