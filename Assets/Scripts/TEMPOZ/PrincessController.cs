using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : MonoBehaviour
{
    private SpriteRenderer spRend;
    private Rigidbody2D rigBd;
    private Animator anim;

    [SerializeField] private float speed;

    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
        rigBd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigBd.velocity = new Vector2(speed * Time.deltaTime, 0);
            //transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            anim.Play("Princess run");
            spRend.flipX = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigBd.velocity = new Vector2(-speed * Time.deltaTime, 0);
            //transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            anim.Play("Princess run");
            spRend.flipX = true;
        }
        else 
        {
            if (Input.GetKey(KeyCode.E))
            {
                anim.Play("Princess ability"); // Анимация механики очарования 
            }
            else 
            {
                anim.Play("Princess Idle");
            }

            rigBd.velocity = new Vector2(0, 0);
        }
    }
}
