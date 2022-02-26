using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂在camera上，相机跟随
public class Follow : MonoBehaviour
{
    public float distanceAway = 12f;

    public float distanceUp = 8f;

    public float smooth = 2f; // how smooth the camera movement is



    private Vector3 m_TargetPosition; // the position the camera is trying to be in)



    Transform follower; //the position of Player
    void Start()

    {

        //找到坦克

        follower = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        // setting the target position to be the correct offset from the

        m_TargetPosition = follower.position + Vector3.up * distanceUp - follower.forward * distanceAway;



        // making a smooth transition between it's current position and the position it wants to be in

        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);



        // make sure the camera is looking the right way!

        transform.LookAt(follower);
        }
}
