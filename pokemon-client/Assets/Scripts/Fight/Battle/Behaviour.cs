using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂在pokemon预制体上，战斗动画逻辑
public class Behaviour : MonoBehaviour
{
    Transform my_transform;

    Animator my_animator;

    int nextbehaviour=0;//1:skill1 2:skill2 3:skill3 4:skill4 5:

    void Start()
    {
        my_transform = this.transform;

        my_animator = this.GetComponent<Animator>();
    }

    public void setbehaviour(int a) {
        nextbehaviour = a;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextbehaviour==1) {
            my_animator.SetTrigger("bitetrigger");
            nextbehaviour = 0;
        }
        if (nextbehaviour == 2)
        {
            my_animator.SetTrigger("projectiletrigger");
            nextbehaviour = 0;
        }
        if (nextbehaviour == 3)
        {
            my_animator.SetTrigger("stingtrigger");
            nextbehaviour = 0;
        }
        if (nextbehaviour == 4)
        {
            my_animator.SetTrigger("casttrigger");
            nextbehaviour = 0;
        }
        if(nextbehaviour==5)
        {
            my_animator.SetBool("die", true);
            my_animator.SetBool("idle", false);
            my_animator.SetTrigger("damagetrigger");
        }
    }
}
