using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave1 : MonoBehaviour
{
    public GameObject enemy;
    public static Wave1 Instance;
    private Transform enemiesParent;
    private float timeBetweenSpawn = 2f;
    float currentTimeBetweenSpawn;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }
    // Update is called once per frame
    void Update()
    {
        currentTimeBetweenSpawn -= Time.deltaTime;
        if (currentTimeBetweenSpawn < 0)
        {
            CreateEnemy();
            currentTimeBetweenSpawn = timeBetweenSpawn;
        }
    }
    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-20,20), Random.Range(-20,20));
    }
    void CreateEnemy()
    {
        float x = Random.Range(-3, 3);  
        float y = Random.Range(-3, 3);
        enemiesParent.position = new Vector3(0, 0, 0);
        var e = Instantiate(enemy, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);
    }    
}
