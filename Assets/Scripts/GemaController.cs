using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemaController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;
    public float velocity = 0.9f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.down * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Nave"))
        {
            Destroy(gameObject);
        }

    }
}
