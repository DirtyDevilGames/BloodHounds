using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private CharacterController MoxieController;
    private bool IsGrounded;

    //Attack Variables
    public float DashRange;
    public float Dashspeed;
    public bool isDashing;

    public Vector3 DashTarget;

    private void Start()
    {
        MoxieController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        IsGrounded = MoxieController.isGrounded;

        RaycastHit hit;
        if (Input.GetButtonDown("Dash") && isDashing == false)
        {
            isDashing = true;
            Vector3 tmp =  transform.forward * DashRange;
            DashTarget = transform.position + tmp;
        }

        if (isDashing)
        {
            Vector3 offset = DashTarget - transform.position;
            if (offset.magnitude > 6.0f)
            {
                print(offset);
                
                MoxieController.Move(offset * Dashspeed *Time.deltaTime);

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        print("Slash!");
                        isDashing = false;
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 5.0f);
                        
                    }if (hit.transform.tag == "Environment")
                    {
                        print("Bonk!");
                        isDashing = false;
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 5.0f);
                        
                    }


                }
            }
            else
            {
                isDashing = false;
            }



        }

    }

    void DashAttack(Vector3 Target)
    {

    }

}
