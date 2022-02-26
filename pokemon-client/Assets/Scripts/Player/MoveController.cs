using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//人物摇杆，挂在人物上
public class MoveController : MonoBehaviour
{
    //
    public float speed = 6.0F;
    public float jumpSpeed = 10.0F;
    public float gravity = 10.0F;
    public Vector3 moveDirection = Vector3.zero;
    //
    private Transform player, cameraMain;
    public bool okForJump=true;
    //
    private Animator anim;
    public float moveMent=0f;
    //

    void Start()
    {
        player = transform;
        cameraMain = GameObject.FindWithTag("MainCamera").transform;
        anim = this.GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetFloat("Speed",moveMent);
        CharacterController controller = GetComponent<CharacterController>();
        //是否触碰地面
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        if (GameObject.Find("CM FreeLook1").GetComponent<CinemaLook>().playerInput.PlayerMain.Jump.triggered&&okForJump)
        {
            okForJump = false;
            moveDirection.y = jumpSpeed;
        }
        if (Time.frameCount % 150 == 0)
        {
            okForJump = true;
        }
        moveDirection.y -= gravity * Time.deltaTime;//模拟重力
        controller.Move(moveDirection * Time.deltaTime);//移动
    }
    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMove += EasyJoystick_On_JoystickMove;
    }

    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
    }

    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= On_JoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
        EasyJoystick.On_JoystickMove -= EasyJoystick_On_JoystickMove;
    }


    void On_JoystickMoveEnd(MovingJoystick move)
    {
        /*
        if (move.joystickName == "Move")
        {
            GetComponent<Animation>().CrossFade("Idle");
        }
        */
        if (move.joystickName == "Move")
        {
            //anim.ResetTrigger("Idle");
            //anim.SetTrigger("Idle");
            moveMent = 0f;
        }
    }
    void On_JoystickMove(MovingJoystick move)
    {


        if (move.joystickName == "Move")
        {
            /*
            //
            if (Mathf.Abs(move.joystickAxis.y) > 0 && Mathf.Abs(move.joystickAxis.y) < 0.5)
            {
                GetComponent<Animation>().CrossFade("Fly Forward In Place");

            }
            else if (Mathf.Abs(move.joystickAxis.y) >= 0.5)
            {
                GetComponent<Animation>().CrossFade("Fly Forward In Place");
            }
            */
            //anim.SetTrigger("Run");
            moveMent = move.joystickAxis.x * move.joystickAxis.x + move.joystickAxis.y * move.joystickAxis.y;
        }
    }
    void EasyJoystick_On_JoystickMove(MovingJoystick move)//移动摇杆时触发
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 direction = new Vector3(move.joystickAxis.x, 0, move.joystickAxis.y);
        if (cameraMain == null)
        {
            cameraMain = GameObject.FindWithTag("MainCamera").transform;
        }
        if (direction != Vector3.zero)
        {
            player.rotation = Quaternion.LookRotation(Quaternion.Euler(new Vector3(0, cameraMain.rotation.eulerAngles.y, 0)) * direction);
            controller.Move(transform.forward * Time.deltaTime * 6f* (direction.x * direction.x + direction.z * direction.z));
        }
    }
}
