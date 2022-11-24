using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    ////////// PUBLIC FIELDS //////////
    public int enemyHealth = 3;
    public GameObject gibEffect;
    public float attackDelay = 10f;
    public float attackDistance = 2f;

    ////////// PRIVATE FIELDS //////////
    GameObject player;
    PlayerController playerController;
    float aiDelay = 0.2f;
    bool aiUpdate = true;
    LevelManager lvlMan;
    UnityEngine.AI.NavMeshAgent agent;
    private bool canAttack = true;
    Animator SpiderBotAnimator;


    void Start()
    {
        ////////// INITIALIZATIONS //////////
        GameObject LM = GameObject.FindGameObjectWithTag("Level Manager");
        lvlMan = LM.GetComponent<LevelManager>();
        lvlMan.numberOfEnemies.Add(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        SpiderBotAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Run AI at regular intervals to save on system resources.
        if (aiUpdate == true)
        {
            StartCoroutine(EnemyAIDelay());
        }

        //If Enemy health reaches 0, kill it!
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }

        //Check to see how far from the Player the enemy is. If in range, begin the attack co-routine. Otherwise, stop the co-routine.
        if (agent.velocity.x < 0.001 && agent.velocity.z < 0.001 && Vector3.Distance(this.gameObject.transform.position, player.transform.position) < attackDistance && canAttack == true)
        {
            StartCoroutine(EnemyAttack());
        }
        else if (agent.velocity.x > 0.001 || agent.velocity.z > 0.001 && Vector3.Distance(this.gameObject.transform.position, player.transform.position) > attackDistance)
        {
            StopCoroutine(EnemyAttack());
        }
    }

    //Take damage from Player bullets.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            enemyHealth--;
        }
    }

    //Stuff to do once dead.
    void EnemyDeath()
    {
        Instantiate(gibEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        lvlMan.numberOfEnemies.Remove(this.gameObject);
    }

    //This co-routine reduces the number of times that the NavMeshAgent checks to see where the player is.
    IEnumerator EnemyAIDelay()
    {
        aiUpdate = false;
        yield return new WaitForSeconds(aiDelay);
        agent.destination = player.transform.position;
        aiUpdate = true;
    }

    //Reduce Player health after a short delay. The delay time should be synced to the Enemy attack animation.
    IEnumerator EnemyAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        playerController.playerHealth--;
        canAttack = true;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            SpiderBotAnimator.SetInteger("EnemyIntState", 0);
        }
        else
        {
            SpiderBotAnimator.SetInteger("EnemyIntState", 1);
        }
    }
}


