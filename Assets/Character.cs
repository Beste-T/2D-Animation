using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    float currentSpeed = 0.0f;
    public float speed;
    float jumpAmount = 10.0f;
    bool canJump = true;
    bool isJumping = false;
    public Animator animator;
    

    private void Update()
    {
        movementControl();
    }
    
void movementControl()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3 (horizontalMovement, 0, 0);
        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime);

        if(canJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {  
            isJumping = true;
            rigidbody2D.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);   
            canJump = false;       
        }

        if (isJumping)
        {
            animator.Play("Tommy_Jumping");
        }


        

        if (movement.x < 0)
        {
            // If moving left, flip the character horizontally.
            transform.localScale = new Vector3(-1, 1, 1);
            currentSpeed = speed;
            animator.Play("Player_Walking");

        }
        else if (movement.x > 0)
        {
            // If moving right, keep the character's default scale.
            transform.localScale = new Vector3(1, 1, 1);
            currentSpeed = speed;
            animator.Play("Player_Walking");

        }

        else if (movement.x == 0)
        {
            animator.Play("Player_Idle2");
        }
       
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            canJump = true;
            isJumping = false;
        }
    }

    

}
