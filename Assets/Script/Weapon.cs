using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzle;
    public GameObject player;
    public float bulletForce = 9;
    public float TimeBtwBullet = 0.2f;
    public Transform firePos;


    private float timeBtwBullet = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0.2f, 0.2f ,0);
        RotateGun();
        timeBtwBullet -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timeBtwBullet < 0) 
        {
            FireBullet();
        }
    }
    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else transform.localScale = new Vector3(1, 1, 1);
    }
    void FireBullet()
    {
        timeBtwBullet = TimeBtwBullet;

        
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);

        Instantiate(muzzle, firePos.position, transform.rotation, transform);
        bullet.gameObject.tag = "Bullet";

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        
    }

}
