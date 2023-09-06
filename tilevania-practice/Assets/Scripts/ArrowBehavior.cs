using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField] float xArrowSpeed = 1.0f;

    Rigidbody2D arrowRigidbody;
    PlayerMovement player;

    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        arrowRigidbody.velocity = new Vector2(xArrowSpeed * player.transform.localScale.x, arrowRigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
