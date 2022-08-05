using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    Zombie zombie;
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie = animator.GetComponent<Zombie>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
        

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var zombieRotation = new Vector3(player.position.x, animator.transform.position.y, player.position.z) - animator.transform.position;
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(zombieRotation, Vector3.up),
            20f * Time.deltaTime);
       
        if (zombie.currentState == Zombie.States.Chase)
        {
            AudioManager.instance.Play("ZombieChase");
            animator.SetBool("isAttacking", false);
            animator.SetBool("isChasing", true);
        }

    }
   

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
