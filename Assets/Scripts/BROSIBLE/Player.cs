using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerController controller;
    public float runSpeed;
    float horizontalMove = 0f;
    public bool jump = false;
    public bool run = false;
    public Animator animator;

    public GameObject panel;
    
    
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript.movSpeed = 20;
    }
    public void TakeDamage()
    {
        StartCoroutine(takedamage(1f));
        Debug.Log(takedamage(1f)); // � ������ �������� �� 2
    }
    private IEnumerator takedamage(float damage)
    {
        playerScript.playerHP -= damage;
        yield return new WaitForSeconds(2f);
    }

    void Update()
    {
        runSpeed = playerScript.movSpeed;
        if (playerScript.playerHP <= 0)
        {
            Destroy(gameObject);
            panel.SetActive(true);
            Time.timeScale = 0f;

        }
        if (!controller.wasGrounded)
        {
            animator.Play("princess_jump");
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetKey("a") || Input.GetKey("d"))
        {
            run = true;
        }

        else
        {
            run = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.Play("princess_jump");
        }
    }

    void FixedUpdate()
    {
       controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
       jump = false;

       if(run)
       {
            if (controller.wasGrounded)
            {
                animator.Play("Princess run");
            }
       }

       else
       {
            if(Input.GetButton("Fire1"))
            {
                animator.Play("Princess ability");
            }

            else if(jump)
            {
                animator.Play("Princess jump");
            }

            else if(!jump && !run && controller.wasGrounded)
            {
                animator.Play("Princess Idle");
            }

            controller.Move(0, false);
       }
    }
}


