using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 4f;
    Rigidbody2D BulletRigidbody;
    PlayerMovement ThePlayer;
    float XSpeed;

    void Start()
    {
        BulletRigidbody = GetComponent<Rigidbody2D>();
        ThePlayer = FindObjectOfType<PlayerMovement>();
        XSpeed = ThePlayer.transform.localScale.x * BulletSpeed;
        transform.localScale = new Vector2(Mathf.Sign(XSpeed) * transform.localScale.x, transform.localScale.y);
    }

    void Update()
    {
        BulletRigidbody.velocity = new Vector2(XSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

}
