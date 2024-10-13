using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float attackDistance;
    [SerializeField] private GameObject nimb;
    [SerializeField] private Transform hitbox;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float attackRange;
    private Animator anim;
    private bool m_FacingRight = true;
    private bool chill;
    private float distance;
    private bool isSummon = false;
    private Transform targetEnemy;
    private Transform targetSummon;
    Vector2 targetPosition;

    public  float EnemyMaxHp;
    public  float EnemyCurrentHp;


    [SerializeField] private AudioSource source;


    public static float dmg =0.5f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        EnemyCurrentHp = 10;
    }
    void FindTargetSummon()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Summon"); // ������� ������ ���������� ���� � ����� "Enemy"
        if (enemyObject != null)
        {
            targetSummon = enemyObject.transform;
        }
      
    }
    void FindTargetEnemy()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy"); // ������� ������ ���������� ���� � ����� "Enemy"
        if (enemyObject != null)
        {
            targetEnemy = enemyObject.transform; // ������������� ��� ��� ���� ��� �������������
        }
    }
    public void TakeDamage()
    {
        if (gameObject.tag == "Summon")
        {
            StartCoroutine(takedamage(0.5f));
        }
        else if(gameObject.tag == "Enemy")
        {
            StartCoroutine(takedamage(dmg));
            Debug.Log("summon daet pizdi" + dmg);
        }
      
    }
    private IEnumerator takedamage(float damage)
    {
        EnemyCurrentHp -= damage;
        Debug.Log(damage);
        yield return new WaitForSeconds(2f);
    }

    private void Update()
    {
        if(EnemyCurrentHp > EnemyMaxHp)
        {
            EnemyMaxHp = EnemyCurrentHp;
        }
        if(EnemyCurrentHp <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "Enemy")
            {
                WaveGeneratorScript.DecreaseEnemiesCount();
                playerScript.EnemyKilled++;
            }
        }
        if (targetEnemy != null)
        {
            if (targetEnemy.tag == "Summon")
            {
                targetEnemy = targetSummon;
            }
        }
        if (isSummon)
        {

            FindTargetEnemy();
            EnemyLogicSummon();
            if (targetEnemy != null)
            {
                if (targetEnemy.transform.position.x < transform.position.x)
                {
                    if (m_FacingRight)
                    {
                        Flip();

                    }
                }
                if (targetEnemy.transform.position.x > transform.position.x)
                {
                    if (!m_FacingRight)
                    {
                        Flip();

                    }
                }
            }
            else
            {
                anim.Play("idle_demon");
            }
        }
        else if (!isSummon)
        {
            FindTargetSummon();
            EnemyLogic();
            if (targetSummon != null)
            {
                if (targetSummon.transform.position.x < transform.position.x)
                {
                    if (m_FacingRight)
                    {
                        Flip();

                    }
                }
                if (targetSummon.transform.position.x > transform.position.x)
                {
                    if (!m_FacingRight)
                    {
                        Flip();

                    }
                }
            }
            else
            {
                if (target.transform.position.x < transform.position.x)
                {
                    if (m_FacingRight)
                    {
                        Flip();

                    }
                }
                if (target.transform.position.x > transform.position.x)
                {
                    if (!m_FacingRight)
                    {
                        Flip();

                    }
                }
            }


        }
    }
    private void MoveSummon()
    {
        if (targetEnemy != null)
        {

            Vector2 targetPosition = new Vector2(targetEnemy.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            anim.Play("run_demon");

        }
        else
        {
            anim.Play("idle_demon");
        }

    }
    private void EnemyLogicSummon()
    {
        if (targetEnemy != null)
        {
            distance = Vector2.Distance(transform.position, targetEnemy.transform.position);

            if (distance > attackDistance && !chill)
            {
                MoveSummon();
            }
            else if (attackDistance >= distance && !chill)
            {
                Attack();

            }
        }
    }
    void EnemyLogic()
    {
        if (targetSummon != null)
        {
            distance = Vector2.Distance(transform.position, targetSummon.transform.position);
            if (distance > attackDistance && !chill)
            {
                Move();
            }
            else if (attackDistance >= distance && !chill)
            {
                Attack();

            }
        }
        else
        {
            distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance > attackDistance && !chill)
            {
                Move();
            }
            else if (attackDistance >= distance && !chill)
            {
                Attack();

            }
        }
        
       
    }

    private void Move()
    {
        if (targetSummon != null) {


            targetPosition = new Vector2(targetSummon.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            anim.Play("run_demon");
        }
        else {
           
                targetPosition = new Vector2(target.transform.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.Play("run_demon");
            }
           
        }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitbox.transform.position, attackRange);
    }
    void Attack()
    {
        StartCoroutine(attacking());
    }
    IEnumerator attacking()
    {
        chill = true;
        anim.Play("attack_demon");
        source.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitbox.transform.position, attackRange, layerMask);
       foreach(Collider2D c in hitEnemies)
        {
            if (c.gameObject.GetComponent<enemy>() != null && this.gameObject.tag == "Summon" && c.gameObject.tag == "Enemy")
            {
                c.GetComponent<enemy>().TakeDamage();
                hitbox.gameObject.SetActive(false);
            }
            else if (c.gameObject.GetComponent<enemy>() != null && this.gameObject.tag == "Summon" && c.gameObject.tag == "Summon")
            {
                hitbox.gameObject.SetActive(false);
            }
            else if (c.gameObject.GetComponent<enemy>() != null &&  this.gameObject.tag == "Enemy" && c.gameObject.tag == "Enemy")
            {
                hitbox.gameObject.SetActive(false);
            }
            else if(c.gameObject.GetComponent<enemy>() != null && this.gameObject.tag == "Enemy" && c.gameObject.tag == "Summon")
            {
                c.GetComponent<enemy>().TakeDamage();
                hitbox.gameObject.SetActive(false);
            }
            else 
            {
                c.GetComponent<Player>().TakeDamage();
                hitbox.gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.4f);
        anim.Play("stun_demon");
        yield return new WaitForSeconds(1f);
        chill = false;
        hitbox.gameObject.SetActive(true);
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Enemy")
        {
            if (collision.CompareTag("spell"))
            {
                Destroy(collision.gameObject);
                isSummon = true;
                nimb.SetActive(true);
                this.tag = "Summon";
                WaveGeneratorScript.DecreaseEnemiesCount();

            }
        }
    }
}
