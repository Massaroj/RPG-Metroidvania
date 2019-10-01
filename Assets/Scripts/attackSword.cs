using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSword : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private float manaRegen;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        //Attacks when our cooldown is done
        if (timeBtwAttack <= 0)
        {
            //Any target in OverlapCircle takes damage
            if (Input.GetKeyDown(KeyCode.C))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                //Damages enemies hit by attack, and then regens a portion of the mana
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<enemy>().takeDamage(damage);
                    manaRegen = gameObject.GetComponentInParent<movementJump>().mana = gameObject.GetComponentInParent<movementJump>().mana += damage;
                    gameObject.GetComponentInParent<movementJump>().UI.GetComponent<UIManager>().updateMana((int)manaRegen);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        //Starts cooldown after we attack
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
       
    }

    //Draws circle for a visual
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
