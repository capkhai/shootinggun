using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject bullet;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator animator;
    private float speed = 1.2f;
    private float maxHealthPoint = 20f;
    public float currentHealthPoint;
    public float deathTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        currentHealthPoint = maxHealthPoint;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
        target = GameObject.Find("Player").transform;
        if (target != null)
        {
            Vector3 direction  = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
            if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
        if (currentHealthPoint < 0)
        {
            animator.SetTrigger("death");
            deathTime += Time.deltaTime;
            if (deathTime > 0.5) Destroy(this.gameObject);
        }



    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ReduceHP();
        } 
            
    }
    void ReduceHP()
    {
        animator.SetTrigger("hit");

        currentHealthPoint--;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ReduceHP();
        }

    }
}
