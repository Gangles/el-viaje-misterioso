using UnityEngine;
using System.Collections;

public class Platformer : MonoBehaviour
{
	public bool climbUnlocked = true;
	public bool overrideJump = false;
	public bool facingRight = false;
	public bool requestJump = false;
	public bool hasJumped = false;
	public bool grounded = false;
	public bool onWall = false;
	public float groundedTime = -100.0f;
	public float jumpDownTime = -100.0f;
	public float onWallTime = -100.0f;
	public float yFloor = -3.0f;

	public PhysicsMaterial2D playerGround, playerAir;
	public LayerMask groundMask, wallMask;

	private const float MOVEFORCE = 60.0f;
	private const float CLIMBFORCE = 80.0f;
	private const float MAXSPEED = 0.9f;
	private const float MAXCLIMB = 0.5f;
	private const float JUMPFORCE = 150.0f;
	private const float WALLFORCE = -200.0f;
	public Transform groundCheck, wallCheck;

	private Vector3 initPos;
	private Rigidbody2D rb2d;
	private Animator animator;
	private Collider2D coll2d;

	void Awake ()
	{
		initPos = transform.position;
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		coll2d = GetComponent<Collider2D>();
	}

	void Update ()
	{
		// reset if you fall off a cliff
		if (transform.position.y < yFloor)
			transform.position = initPos;

		if (hasJumped || overrideJump)
		{
			jumpDownTime = -1.0f;
			requestJump = false;
		}
		
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundMask);
		if (grounded) groundedTime = Time.time;

		if (climbUnlocked)
		{
			onWall = Physics2D.Linecast(transform.position, wallCheck.position, wallMask);
			if (onWall) onWallTime = Time.time;
		}

		if ((grounded && rb2d.velocity.y == 0.0f) || onWall)
			hasJumped = false;

		if (!overrideJump && Input.GetButtonDown("Jump") && rb2d.velocity.y <= 0.0f)
			jumpDownTime = Time.time;

		if (!requestJump && jumpDownTime > 0.0f)
		{
			bool jumpButton = (Time.time - jumpDownTime) < 0.1f;
			bool canJumpGround = (Time.time - groundedTime) < 0.1f;
			bool canJumpWall = (Time.time - onWallTime) < 0.25f;
			requestJump = (canJumpGround || canJumpWall) && jumpButton;
		}

		animator.SetBool("grounded", grounded);
		animator.SetBool("onWall", onWall);
		animator.SetFloat("xSpeed", Mathf.Abs(rb2d.velocity.x));
		animator.SetFloat("ySpeed", Mathf.Abs(rb2d.velocity.y));
		animator.SetFloat("ascent", rb2d.velocity.y);
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		float scale = transform.localScale.y;

		if (h == 0f && Input.GetKey(KeyCode.RightArrow))
		{
			h = 1.0f; // hack for scene change
		}

		coll2d.sharedMaterial = grounded? playerGround : playerAir;

		// if jump was released early, limit velocity upwards
		if(!Input.GetButton("Jump") && !requestJump && rb2d.velocity.y > 1.0f)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, 1.0f);
		}
		
		if (grounded || requestJump)
			rb2d.gravityScale = 1.0f * scale; // on ground or pending jump
		else if (rb2d.velocity.y < 0.0f && v == 0.0f && onWall)
			rb2d.gravityScale = 0.02f * scale; // on wall, fall slowly
		else if (rb2d.velocity.y < 0.0f && hasJumped)
			rb2d.gravityScale = 2.0f * scale; // falling, use higher gravity
		else
			rb2d.gravityScale = 1.0f * scale; // default

		if (Mathf.Abs(rb2d.velocity.x) > MAXSPEED * scale)
		{
			float xMax = Mathf.Sign(rb2d.velocity.x) * MAXSPEED * scale;
			rb2d.velocity = new Vector2(xMax, rb2d.velocity.y);
		}

		if (requestJump && !hasJumped)
		{
			// apply a vertical force
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0.0f);
			rb2d.AddForce(new Vector2(0.0f, JUMPFORCE * scale));

			// peel a little off the wall if need be
			if (onWall && !grounded && h == 0.0f)
			{
				float wf = Mathf.Sign(rb2d.velocity.x) * WALLFORCE * scale;
				rb2d.AddForce(new Vector2(wf, 0.0f));
			}
			
			hasJumped = true;
		}

		if (h > 0.0f && facingRight || h < 0.0f && !facingRight)
			Flip();

		// apply force to move horizontally
		if (Mathf.Abs(h) > 0.0f && h * rb2d.velocity.x < MAXSPEED * scale)
		{
			rb2d.AddForce(Vector2.right * h * MOVEFORCE * scale);
		}
		// if releasing horizontal move, stop quickly
		else if (!onWall && h == 0.0f && Mathf.Abs(rb2d.velocity.x) > 0.0f)
		{
			rb2d.velocity = new Vector2(0.0f, rb2d.velocity.y);
		}

		// apply force to move vertically
		if (v > 0.0f && onWall && v * rb2d.velocity.y < MAXCLIMB * scale)
		{
			rb2d.AddForce(Vector2.up * v * CLIMBFORCE * scale);
		}

		// climb over the top lip of a wall
		if (v > 0.0f && !onWall && !grounded && (Time.time - onWallTime) < 0.2f)
		{
			rb2d.AddForce(Vector2.right * Mathf.Sign(transform.localScale.x) * CLIMBFORCE * scale);
		}
	}

	private void Flip ()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1.0f;
		transform.localScale = scale;
	}

	public void ForceSettle ()
	{
		rb2d.velocity = new Vector2(0.0f, 0.0f);
		animator.SetBool("grounded", true);
		animator.SetFloat("xSpeed", 0.0f);
		animator.SetFloat("ySpeed", 0.0f);
	}
}
