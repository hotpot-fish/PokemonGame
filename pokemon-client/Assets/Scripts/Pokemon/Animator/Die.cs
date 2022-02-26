using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂在controller的status上，音效
public class Die : StateMachineBehaviour
{
    public AudioSource au;
    public AudioClip ac;
    public GameObject Current;
    public string path=null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Current = GameObject.FindWithTag("Current");
        string name = Current.name;
        path = "Bgm/" + name + "/Die";
        au =Current.GetComponent<AudioSource>();
        ac = (AudioClip)Resources.Load(path.Replace("(Clone)", ""));
        au.clip = ac; 
        au.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
