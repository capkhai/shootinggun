using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb; 
    private Animator animator;
    private Vector3 moveInput;
    private float rollBoost = 3f;
    private float rollTime = 0.25f;
    private float timeTrigger = 0f;
    private float TimeTrigger = 0.5f;
    public float stamina = 10f;
    public float healthPoint = 20f;
    private bool roll = true;

    public SpriteRenderer characterSR;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //move
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Speed",moveInput.sqrMagnitude);
        animator.SetBool("Roll", roll);

        if (moveInput.x > 0) transform.localScale = new Vector3(1, 1, 1);
        if (moveInput.x < 0) transform.localScale = new Vector3(-1 , 1, 1);
        Roll();
    }
   void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (rollTime <= 0) && (stamina >=5))
        {
            moveSpeed += rollBoost;
            roll = true;
            rollTime = 0.25f;
            stamina -= 5;
        }    
        if ((rollTime <= 0) && (roll == true))
        {
            roll = false;
            moveSpeed -= rollBoost;
        }
        else rollTime -= Time.deltaTime;
        if (stamina < 10) 
            stamina += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") ReduceHP();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        
        timeTrigger += Time.deltaTime;
        if ((collision.gameObject.tag == "Enemy") && (timeTrigger >= TimeTrigger))
        {
            ReduceHP();
            timeTrigger = 0f;
        }

    }
    void ReduceHP()
    {
        healthPoint--;
    }
}
