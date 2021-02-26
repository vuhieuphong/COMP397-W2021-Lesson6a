using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum OrtizState
{
    IDLE,
    RUN,
    JUMP
}

public class OrtizBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS;

    public GameObject player;
    
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }


        if (HasLOS && Vector3.Distance(transform.position, player.transform.position) < 2.5)
        {
            // could be an attack
            animator.SetInteger("AnimState", (int)OrtizState.IDLE);
            transform.LookAt(transform.position - player.transform.forward);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)OrtizState.JUMP);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)OrtizState.RUN);
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        HasLOS = false;
    //    }
    //}
}
