using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator anim;
    //private SpriteRenderer spriterend;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //spriterend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel() //Being triggered by animation event
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
