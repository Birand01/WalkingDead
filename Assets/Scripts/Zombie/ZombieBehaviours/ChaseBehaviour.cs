using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    Zombie zombie;
    private Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie = animator.GetComponent<Zombie>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        zombie.agent.SetDestination(player.position);
        if (zombie.currentState == Zombie.States.Attack )
        {
            AudioManager.instance.Play("ZombieAttack");
            animator.SetBool("isAttacking", true);
        }
        if(zombie.currentState==Zombie.States.Patrol)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie.agent.SetDestination(zombie.agent.transform.position);
    }
}
