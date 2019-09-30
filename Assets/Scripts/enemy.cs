using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);


        rb.velocity = new Vector2(Mathf.Sin(Time.time * speed), 0);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken");
    }
}
