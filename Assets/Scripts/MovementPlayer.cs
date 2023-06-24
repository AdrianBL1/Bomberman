using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    public Rigidbody2D rigidbody { get; private set; }
    //private Vector2 direction = Vector2.down;
    private Vector2 direction;

    bool isLeft = false;
    bool isRight = false;
    bool isUp = false;
    bool isDown = false;

    public Rigidbody2D rb;
    public float speedForce;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    public void clickLeft()
    {
        isLeft = true;
    }

    //public void releaseLeft()
    //{
    //    isLeft = false;
    //}

    public void clickRight()
    {
        isRight = true;
    }

    //public void releaseRight()
    //{
    //    isRight = false;
    //}

    public void clickUp()
    {
        isUp = true;
    }

    //public void releaseUP()
    //{
    //    isUp = false;
    //}

    public void clickDown()
    {
        isDown = true;
    }

    //public void releaseDown()
    //{
    //    isDown = false;
    //}

    public void stopMovement()
    {
        direction = Vector2.zero;
        isDown = false;
        isUp = false;
        isRight = false;
        isLeft = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speedForce * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);


        if (isLeft)
        {
            rb.AddForce(new Vector2(speedForce, 0));
            SetDirection(Vector2.left, spriteRendererLeft);
        } else
        {
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }

        if (isRight)
        {
            rb.AddForce(new Vector2(speedForce, 0));
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }

        if(isUp)
        {
            rb.AddForce(new Vector2(speedForce, 0));
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else
        {
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }

        if(isDown)
        {
            rb.AddForce(new Vector2(speedForce, 0));
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else
        {
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}