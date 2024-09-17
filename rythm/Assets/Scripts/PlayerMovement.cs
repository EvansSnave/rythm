using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public float jumpSpeed;
	public Transform groundCheck;
	public LayerMask groundLayer;

	private bool _isGrounded;
	private Rigidbody2D _body;

	private void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_isGrounded = Physics2D.OverlapBox(groundCheck.position, new (GetComponent<SpriteRenderer>().bounds.size.x, 0.5f), 0, groundLayer);
		_body.velocity = new (Input.GetAxis("Horizontal") * speed, _body.velocity.y);

		FlipCharacterOnMovement();

		if (Input.GetKey(KeyCode.Space) && _isGrounded) _body.velocity = new (_body.velocity.x, jumpSpeed);
	}

	private void FlipCharacterOnMovement()
	{
		float _horizontalAxis = Input.GetAxis("Horizontal");

		if (_horizontalAxis > 0.01f) transform.localScale = Vector3.one;
		else if (_horizontalAxis < -0.01f) transform.localScale = new(-1, 1, 1);
	}

}
