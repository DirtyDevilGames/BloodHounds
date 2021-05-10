using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    //Movement variables
    private bool Control = true;
    public float speed;
    public float JumpHeight;
    public float Gravity = -9.81f;
    private bool isGrounded;
    private CharacterController MoxieController;
    private Vector3 Velocity;
    public Transform Cam;
    public float turnDampen = 0.1f;
    private float SmoothTurn;

    private bool DBLJump = true;
    private int NumJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        MoxieController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        isGrounded = MoxieController.isGrounded;
        if (CheckControl())
        {
            if (isGrounded && Velocity.y < 0)
            {
                DBLJump = true;
                NumJumps = 0;
                Velocity.y = 0.0f;
            }

            float horiz = Input.GetAxisRaw("Horizontal");
            //float vert = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(horiz, 0f, 0f).normalized;

            if (movement != Vector3.zero)
            {
                float TargetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref SmoothTurn, turnDampen);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 MoveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward;
                MoxieController.Move(MoveDir.normalized * speed * Time.deltaTime);

            }

            if (Input.GetButtonDown("Jump") && DBLJump)
            {
                NumJumps += 1;


                if (NumJumps == 2)
                {
                    DBLJump = false;
                }

                Velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
            }

            Velocity.y += Gravity * Time.deltaTime;
            MoxieController.Move(Velocity * Time.deltaTime);
        }
        else
        {
            Velocity = Vector3.zero;
        }
    }

    private bool CheckControl()
    {
        Combat combat = GetComponent<Combat>();

        //Add more checks to this to disable dashing
        if (combat.isDashing)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
