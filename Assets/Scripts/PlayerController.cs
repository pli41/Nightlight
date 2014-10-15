using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	public float acceleration = 365f;
	public float jumpForce = 1000f;
	private bool jump = false;

	private float playerDirection = 1;
	private Transform groundCheck1;
	private Transform groundCheck2;
	private bool grounded = false;
	private Animator animator;

	[HideInInspector]
	public string color;

	void Awake()
	{
		groundCheck1 = transform.Find("groundCheck1");
		groundCheck2 = transform.Find ("groundCheck2");
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() {
		grounded = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));
		grounded |= Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float inputDir = Input.GetAxis("Horizontal");

		if(grounded){
			if(inputDir != 0){
				animator.SetInteger("state", 1);
				Debug.Log("run");
			}
			else{
				animator.SetInteger("state", 0);
				Debug.Log("idle");
			}
		}
		else{
			animator.SetInteger("state", 2);
			Debug.Log("jump");
		}


		if(inputDir == 0 && rigidbody2D.velocity.x != 0 && grounded) {
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

		}

		if(inputDir * rigidbody2D.velocity.x < maxSpeed) {
			if(!grounded) {
				rigidbody2D.AddForce(Vector2.right * inputDir * acceleration / 50);
			} else {
				rigidbody2D.AddForce(Vector2.right * inputDir * acceleration);

			}
		}

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		if(inputDir > 0 && playerDirection < 0) {
			flip();
		}
		if (inputDir < 0 && playerDirection > 0) {
			flip();
		}

		if(jump)
		{
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");
			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

	void flip ()
	{
		// Switch the way the player is labelled as facing.
		playerDirection *= -1;
		
		// Multiply the player's x local scale by -1.
		Vector3 playerScale = transform.localScale;
		playerScale.x *= -1;
		transform.localScale = playerScale;
	}
}
