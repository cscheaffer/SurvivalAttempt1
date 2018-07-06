using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Hero hero;

    NavMeshAgent agent;
    bool attack;
    Enemy target;
    Animator anim;
    float nextDamageEvent;
    float attackspeed;
    Rigidbody rb;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetComponentInChildren<Animator>();
        attackspeed = (1 / hero.attackSpeed);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                attack = false;
                if (hit.collider.CompareTag("Enemy"))
                {
                    target = hit.collider.gameObject.GetComponent<Enemy>();
                    attack = true;
                }
                agent.destination = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {

                GameObject go = GameObject.Find("Enemy1");
                    Instantiate(go);
            }
        }

        if (agent.velocity.magnitude > 0.5)
            //anim.SetTrigger("Run");

        AttackSpeed();
    }

    public void AttackSpeed()
    {
        if (attack && WithinRange())
        {
            if (Time.time >= nextDamageEvent)
            {
                nextDamageEvent = Time.time + attackspeed;
                Attack();
            }
        }
        else
        {
            nextDamageEvent = Time.time + attackspeed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (target == null)
            return;
        if(target.gameObject.tag == "Enemy")
            rb.isKinematic = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.isKinematic = false;
    }
    public bool WithinRange()
    {
        if (target == null)
            return false;
        if ((transform.position - target.transform.position).sqrMagnitude < 4 * 4 && target.Health > 0)
        { 
            return true;
        }
        return false;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack01");
        target.Health = target.Health - hero.Damage;
        if (target.Health == 0)
        {
            Destroy(target.gameObject);

            attack = false;
        }
    }
    public void Spell(int numSpell)
    {
        if(numSpell == 1)
        {

        }
    }
}
