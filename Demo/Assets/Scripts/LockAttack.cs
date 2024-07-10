using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    public float stunDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " stunning");
                damageable.stunDuration = stunDuration;
                damageable.IsStun = true;
            }
        }
    }
}