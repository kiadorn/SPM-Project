using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Controller{
    [Header("Movement")]
	public float MaxWallAngleDelta;
	public float MaxSpeed;
	public LayerMask CollisionLayers;
	public float Gravity;
	public BoxCollider2D Collider;
	public Vector2 Velocity;
	public float GroundCheckDistance;
	public float InputMagnitudeToMove;
	public MinMaxFloat SlopeAngles;
	//public SpriteRenderer spriteRenderer;
	public GameObject pauseScreen;

    [Header("Dash Indicator")]
    public GameObject centerPoint;
    public float dashIndicatorSpeed;
    public float dashIndicatorFade;

    private float lastXDir;
    private float inputX;
    private float inputY;

	//Audio
	[HideInInspector]
	public AudioSource[] sources;
	[Header("Audio Clips")]
	public AudioClip Footsteps;
	public AudioClip[] Jump;
	[ReadOnlyAttribute] public AudioClip JumpJustPlayed;
	public AudioClip[] Dash;
	[ReadOnlyAttribute] public AudioClip DashJustPlayed;
	public AudioClip[] DeathSound;
	[ReadOnlyAttribute] public AudioClip DeathSoundJustPlayed;
	public AudioClip[] Hurt;
	[ReadOnlyAttribute] public AudioClip HurtJustPlayed;

	private void Update()
	{
		CurrentState.Update();
        MenuInput();
        SetLastXDir();
        MoveDashIndicator();
	}

	private void Start(){
		sources = GetComponents<AudioSource> ();
        if (gameObject.transform.GetChild(3) != null) centerPoint = GameObject.Find("CenterPoint");
    }

	public float GetLastXDirection(){
		return lastXDir;
	}

    private void SetLastXDir()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            lastXDir = 1f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            lastXDir = -1f;
        }
    }

    private void MenuInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            pauseScreen.GetComponent<PauseScript>().PauseUnpauseGame();
            //player transition till pause state?
        }
    }

    private void MoveDashIndicator()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX == 0 && inputY == 0)
        {
            if (lastXDir == 1f)
            {
                centerPoint.transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                centerPoint.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        else
        {
            float angle = Mathf.Atan2(inputY, inputX);
            Quaternion currentAngle = centerPoint.transform.rotation;
            Quaternion targetAngle = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            centerPoint.transform.rotation = Quaternion.Lerp(currentAngle, targetAngle, dashIndicatorSpeed);
        }
        if (CurrentState is AirState || CurrentState is WallState)
        {
            if (centerPoint.GetComponentInChildren<SpriteRenderer>().color.a <= 1)
            {
                centerPoint.GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, 0, dashIndicatorFade);
            }
        } else
        {
            if (centerPoint.GetComponentInChildren<SpriteRenderer>().color.a >= 0)
            {
                centerPoint.GetComponentInChildren<SpriteRenderer>().color -= new Color(0, 0, 0, dashIndicatorFade);
            }
        }
    }

	public RaycastHit2D[] DetectHits(bool addGroundCheck = false)
	{
		Vector2 direction = Velocity.normalized;
		float distance = Velocity.magnitude * Time.deltaTime;
		Vector2 position = transform.position + (Vector3) Collider.offset;
		List<RaycastHit2D> hits = Physics2D.BoxCastAll(position, Collider.size, 0.0f, direction, distance, CollisionLayers).ToList();
		RaycastHit2D[] groundHits = Physics2D.BoxCastAll(position, Collider.size, 0.0f, Vector2.down, GroundCheckDistance, CollisionLayers);
		hits.AddRange(groundHits);
		for (int i = 0; i < hits.Count; i++)
		{
			RaycastHit2D safetyHit = Physics2D.Linecast(position, hits[i].point, CollisionLayers);
			if (safetyHit.collider != null) hits[i] = safetyHit;
		}
		return hits.ToArray();
	}

    public void SnapToHit(RaycastHit2D hit)
    {
        Vector2 vectorToPoint = hit.point - (Vector2)transform.position;
        vectorToPoint -= MathHelper.PointOnRectangle(vectorToPoint.normalized, Collider.size);
        Vector3 movement = (Vector3)hit.normal * Vector2.Dot(hit.normal, vectorToPoint.normalized) * vectorToPoint.magnitude;
        if (Vector2.Dot(Velocity.normalized, vectorToPoint.normalized) > 0.0f)
            transform.position += movement;
    }

    public void Trans(String name)
    {
        if (name == "PauseNoVelocityState")
        {
            TransitionTo<PauseNoVelocityState>();
        }
         else if (name == "AirState") {
            TransitionTo<AirState>();
        }
    }
}
