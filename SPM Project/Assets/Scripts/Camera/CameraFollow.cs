using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraFollow : MonoBehaviour
{
	[Header("Follow")]
	public float SmoothingTime;
	private Vector3 _currentVelocity;

	[Header("Look ahead")]
	public float MaxLookAhead;
	public float LookAheadAccelerationTime;
	private float _lookAheadSpeed;
	private float _lookAhead;

	[Header("Look around")]
    public float MaxLookAroundAmount;
	public float TimeBeforeLookAround;
	public float PlayerMaximumSpeedForLookAround;
	private float _playerStillTime;
	private float _lookAroundAmount;

    [Header("Camera Size")]
    [ReadOnly] public float height;
    [ReadOnly] public float width;

    [Header("Camera Bounds")]
    [ReadOnly] public bool Bound;
    [ReadOnly] public float minX;
    [ReadOnly] public float maxX;
    [ReadOnly] public float minY;
    [ReadOnly] public float maxY;


    public PlayerController Player;
	public Vector3 Offset;
	private Vector3 _targetPosition;

    private GameObject Target;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Target = Player.gameObject;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    private void Update() {
        UpdateTargetPosition();

    }

    private void LateUpdate()
    {
        UpdateLookAhead();
        UpdateLookAround();
        UpdateMovement();
    }

    private void UpdateLookAhead()
	{
		float targetLookAhead = MathHelper.Sign(Player.Velocity.x) * MaxLookAhead;
        _lookAhead = Mathf.SmoothDamp(_lookAhead, targetLookAhead, ref _lookAheadSpeed, LookAheadAccelerationTime);
	}

	private void UpdateTargetPosition()
	{
        _targetPosition = Target.transform.position;
		_targetPosition += Offset;
		_targetPosition += Vector3.right * _lookAhead;
		_targetPosition += Vector3.up * _lookAroundAmount;

    }

    private void UpdateMovement()
	{
        if (_lookAroundAmount == 0) {
            transform.position += new Vector3(0, Target.transform.position.y - transform.position.y, 0);
            //Vector3 moveToPos = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, moveToPos, 50 * Time.deltaTime);
        }
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, SmoothingTime);
        if (Bound)
        {
            Vector3 v3 = transform.position;
            v3.x = Mathf.Clamp(v3.x, minX, maxX);
            v3.y = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = v3;
            
        }
    }
	private void UpdateLookAround()
	{
		if (Player.Velocity.magnitude > PlayerMaximumSpeedForLookAround)
		{
			_lookAroundAmount = 0.0f;
			_playerStillTime = 0.0f;
			return;
		}
		_playerStillTime += Time.deltaTime;
        if (_playerStillTime < TimeBeforeLookAround) return;
        _lookAroundAmount = Input.GetAxisRaw("Vertical") * MaxLookAroundAmount;
	}

    public void UpdateBounds(float minX, float maxX, float minY, float maxY)
    {

        /*Vector2 bottomLeft = (Vector2)transform.position - new Vector2(width / 2, height / 2);
        Vector2 topRight = (Vector2)transform.position + new Vector2(width / 2, height / 2);
        //foreach (Collider2D c in Physics2D.OverlapAreaAll((bottomLeft, topRight, 0, -Mathf.Infinity
        Collider2D bounds = Physics2D.OverlapArea(bottomLeft, topRight, 9); */
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        Bound = true;

    }

    public void switchToCameraFocus(Vector3 focus, bool freeze)
    {
        if (freeze)
        {
            Player.TransitionTo<PauseWithVelocityState>();
        }
        this.GetComponent<CameraFocus>().endPos = focus;
        this.GetComponent<CameraFocus>().enabled = true;
        this.GetComponent<CameraFollow>().enabled = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        /*if (collision.gameObject.layer == 9)
        {
            minX = collision.transform.position.x - collision.gameObject.GetComponent<BoxCollider2D>().size.x/2;
            maxX = collision.transform.position.x + collision.gameObject.GetComponent<BoxCollider2D>().size.x/2;
            minY = collision.transform.position.y - collision.gameObject.GetComponent<BoxCollider2D>().size.y/2;
            maxY = collision.transform.position.y + collision.gameObject.GetComponent<BoxCollider2D>().size.y/2;
            Debug.Log("minX: " + minX + " maxX: " + maxX + " minY: " + minY + " maxY: " + maxY);
            //UpdateBounds(minX, maxX, minY, maxY);
        } */
    }
}
