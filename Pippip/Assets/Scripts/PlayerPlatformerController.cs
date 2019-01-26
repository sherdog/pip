using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

	private GameObject pip;
    private Animator animator;
	private bool isFlipped;
	Vector2 lastMove = Vector2.zero;

    // Use this for initialization
    void Awake () 
    {
        animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

		float xDirection = lastMove.x - move.x;
		//Debug.Log("xDir = " + xDirection);

        bool flipSprite = (xDirection > 0 ) ? true : false;
		Debug.Log("flipSprite: " + flipSprite);
        if (flipSprite) 
        {
			this.transform.localScale = new Vector3(-1, 1, 1);
        }
		else
		{
			this.transform.localScale = new Vector3(1, 1, 1);
		}

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}