using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Hero hero;
    public MainHUDScreen HUD;

    NavMeshAgent agent;
    bool attack;
    Enemy target;
    Animator anim;
    float nextDamageEvent;
    float attackspeed;
    Rigidbody rb;
    bool spellCasting;
    SpellController spellController;
    float maxDistance = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetComponentInChildren<Animator>();
        attackspeed = (1 / hero.attackSpeed);
        rb = GetComponent<Rigidbody>();
        spellController = GetComponent<SpellController>();
        GameObject go = GameObject.Find("MainCanvas");
        HUD = go.GetComponent<MainHUDScreen>();
    }

    void Update()
    {
        //Movement
        bool keydown = false;
        if (Input.GetKey(KeyCode.W))
        { 
            rb.velocity = transform.forward * hero.movementSpeed;
            keydown = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * hero.movementSpeed;
            keydown = true;
        }
        if(!keydown)
        {
            rb.velocity = Vector3.zero;
        }

        keydown = false;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 200f, Space.World);
            keydown = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * 200f, Space.World);
            keydown = true;
        }
        if(!keydown)
        {
            rb.angularVelocity = Vector3.zero;
        }


        if (Input.GetMouseButtonDown(1))
        {
            HUD.DisableTarget();
            target = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                attack = false;
                if (hit.collider.CompareTag("Enemy"))
                {
                    target = hit.collider.gameObject.GetComponent<Enemy>();
                    Debug.Log(target);
                    HUD.DisplayTarget(target);
                    attack = true;
                }
            }
        }

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
        //anim.SetTrigger("Attack01");
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
