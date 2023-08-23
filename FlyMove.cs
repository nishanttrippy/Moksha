using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : StateMachineBehaviour
{

    private Animator anim;
    private Transform objTransform;
    private Angel angel;
    private AudioSource audioClip;
    float timeTrack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeTrack = Time.time;
        objTransform = animator.GetComponent<Transform>();
        angel = animator.GetComponent<Angel>();
        audioClip = animator.GetComponent<AudioSource>();

        audioClip.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time - timeTrack > 5.0f)
        {
            animator.SetTrigger("Teleport");
        }
        else
        {
            objTransform.Translate(angel.Move().x * Time.deltaTime, angel.Move().y, 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
