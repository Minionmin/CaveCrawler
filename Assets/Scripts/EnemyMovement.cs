using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float EnemyMovespeed = 2f;
    Rigidbody2D EnemyRigidbody;
    BoxCollider2D EnemyTurnCollider;
    PlayerMovement ThePlayer;

    void Start()
    {
        EnemyRigidbody = GetComponent<Rigidbody2D>();
        EnemyTurnCollider = GetComponent<BoxCollider2D>();
        ThePlayer = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        EnemyRigidbody.velocity = new Vector2(EnemyMovespeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyTurnCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            EnemyMovespeed = -EnemyMovespeed;
            Turn();
        }
    }

    void Turn()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(EnemyRigidbody.velocity.x)), 1f);
    }
}
