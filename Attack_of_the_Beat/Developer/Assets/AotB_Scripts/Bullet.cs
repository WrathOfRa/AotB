using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer rend;
    public float force;
    public Vector2 direction;

    public bool markForDestruction;

    public int lifeCount;
    public int lifeBuffer;
    public int timeToDie;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        markForDestruction = false;
        lifeCount = 0;
        lifeBuffer = 5;
        timeToDie = 1000;
    }

    // Start is called before the first frame update
    void Start()
    {
        //force = 1000.0f;
        //direction.x = 0.0f * force;
        //direction.y = -1.0f * force;
        //rb2d.AddForce(direction);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb2d.velocity = new Vector2(direction.x * force, direction.y * force);
        lifeCount++;
        if ((rb2d.velocity.magnitude < 0.1) && (!markForDestruction) && (lifeCount > lifeBuffer))
        {
            rend.color = Color.red;
            markForDestruction = true;
        }

        if (lifeCount > timeToDie)
            markForDestruction = true;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint2D contact = collision.contacts[0];

        //Vector2 reflectedVelocity = Vector2.Reflect(rb2d.velocity, contact.normal);

        //rb2d.velocity = reflectedVelocity;
        //markForDestruction = true;
    }
}
