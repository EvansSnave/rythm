using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public float jumpSpeed;
	public Transform groundCheck;
	public LayerMask groundLayer;

	private bool _isGrounded;
	private Rigidbody2D _body;
	private bool _canMove;

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_canMove = !GetComponent<PlayerRythm>().canPlayNotes;

		if (_canMove)
		{
			_body.velocity = new(Input.GetAxis("Horizontal") * speed, _body.velocity.y);

			FlipCharacterOnMovement();
		}
		else
		{
			_body.velocity = new(0, _body.velocity.y);
		}
	}

	private void FixedUpdate()
	{
		_isGrounded = Physics2D.OverlapBox(groundCheck.position, new(GetComponent<SpriteRenderer>().bounds.size.x, 0.3f), 0, groundLayer);
		if (Input.GetKey(KeyCode.Space) && _isGrounded && _canMove) _body.velocity = new(_body.velocity.x, jumpSpeed);
	}

	private void FlipCharacterOnMovement()
	{
		float _horizontalAxis = Input.GetAxis("Horizontal");

		if (_horizontalAxis > 0.01f) transform.localScale = Vector3.one;
		else if (_horizontalAxis < -0.01f) transform.localScale = new(-1, 1, 1);
	}

}
