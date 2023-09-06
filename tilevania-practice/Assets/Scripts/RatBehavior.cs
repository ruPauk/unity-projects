using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D ratRigidbody;

    void Start()
    {
        ratRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ratRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
            moveSpeed = -moveSpeed;
            FlipRatSprite();
    }

    void FlipRatSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(ratRigidbody.velocity.x)), 1f);
    }
}
