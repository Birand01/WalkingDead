using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    float timer;
    Zombie zombie;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie = animator.GetComponent<Zombie>();
        timer = 0;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            timer += Time.deltaTime;
            if (timer > 10)
                animator.SetBool("isPatrolling", false);


            zombie.SetRandomDestinationPoint();

            if (zombie.currentState == Zombie.States.Chase)
            {
                animator.SetBool("isChasing", true);
            }
   
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie.agent.SetDestination(zombie.agent.transform.position);
    }

}
