using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(BasicEntity))]
public class B_Attack : MonoBehaviour
{
    private float attackspd;
    private float range;
    private Entity entitySelf;
    private bool alive;
    private B_Death deathManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alive = true;
        entitySelf = GetComponent<BasicEntity>();
        range = entitySelf.GetRange();
        attackspd = entitySelf.GetAttackSpeed();
        deathManager = GetComponent<B_Death>();

        StartCoroutine(AttackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        alive = deathManager.isAlive(); 
        range = entitySelf.GetRange();
        attackspd = entitySelf.GetAttackSpeed();
    }

    private IEnumerator AttackCoroutine()
    {    
        while (alive)
        {
            yield return new WaitForSeconds(attackspd);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, range, LayerMask.GetMask("Character"));
            if (entitySelf.GetIsActive())
            {
                foreach (RaycastHit hit in hits)
                {
                    var entityOther = hit.collider.gameObject.GetComponent<BasicEntity>();
                    var otherHealth = hit.collider.gameObject.GetComponent<B_Health>();
                    var otherMana = hit.collider.gameObject.GetComponent<B_Mana>();
                    var selfMana = GetComponent<B_Mana>();
                    if (entityOther.GetIsEnemy() != entitySelf.GetIsEnemy())
                    {
                        int atk = entitySelf.GetAttack();
                        int def = entityOther.GetDef();
                        int damage;
                        if (atk > def)
                        {
                            damage = atk - def;
                        }
                        else
                        {
                            damage = 0;
                        }
                        otherHealth.damage(damage);
                        otherMana.IncreaseMana();
                        selfMana.IncreaseMana();
                        break;
                    }
                }
            }
        }
    }
}
