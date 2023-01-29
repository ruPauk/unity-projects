using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidbody;
    Transform transform;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }


    void Update()
    {
        rigidbody.velocity = new Vector2(moveSpeed, 0f);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        moveSpeed *= -1;
        FlipMonster();
    }

    void FlipMonster()
    {
        transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), 1f);
    }
}
