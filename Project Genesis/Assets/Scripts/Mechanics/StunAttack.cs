using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAttack : MonoBehaviour
{
    public LayerMask enemyLayerMask;

    public void ApplyStun()
    {
        Collider2D enemyCol = Physics2D.OverlapCircle(transform.position, 2f, enemyLayerMask);
        if (enemyCol != null)
        {
            Health hp = enemyCol.gameObject.GetComponent<Health>();
            if (hp != null)
            {
                hp.OnStun.Invoke(transform.parent.position);
            }
        }
    }

}
