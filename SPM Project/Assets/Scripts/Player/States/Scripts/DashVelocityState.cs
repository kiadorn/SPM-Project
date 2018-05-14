using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Player/States/DashVelocity")]
public class DashVelocityState : State
{
    public float waitTime = 1f;
    public float speed;
    public float exitSpeed;
    private float timer;
    private Vector2 Velocity { get { return _controller.Velocity; }
        set { _controller.Velocity = value; } }
    private Transform transform { get { return _controller.transform; } }
    private PlayerController _controller;
    private float xDir;
    private float yDir;
    private List<Collider2D> _ignoredPlatforms = new List<Collider2D>();

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
		timer = 0;
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        Velocity = Vector2.zero;
		_controller.sources[2].clip = _controller.Dash;
		_controller.sources[2].Play ();
    }
    public override void Update()
    {
		if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0){
			xDir = _controller.GetLastXDirection();
		}
        Velocity = new Vector2(xDir, yDir).normalized * speed;
        RaycastHit2D[] hits = _controller.DetectHits();
        UpdateNormalForce(hits);
        Dash();
        transform.Translate(Velocity * Time.deltaTime);
    }
    
    public override void Exit()
    {
        Velocity = new Vector2(xDir, yDir).normalized * exitSpeed;
    }

    public void Dash()
    {
        timer += Time.deltaTime;
        if (timer < waitTime)
        {
            return;
        }
        _controller.TransitionTo<AirState>();
    }

    //Collision tests
    private void UpdateNormalForce(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return;
        RaycastHit2D snapHit = hits.FirstOrDefault(h => !h.collider.CompareTag("OneWay"));
        if (snapHit.collider != null) _controller.SnapToHit(snapHit);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("OneWay") && Velocity.y > 0.0f && !_ignoredPlatforms.Contains(hit.collider))
            {
                _ignoredPlatforms.Add(hit.collider);
            }
            if (_ignoredPlatforms.Contains(hit.collider))
                continue;

            Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                _controller.TransitionTo<GroundState>();

            if (MathHelper.GetWallAngleDelta(hit.normal) < _controller.MaxWallAngleDelta
                && Vector2.Dot((hit.point - (Vector2)transform.position).normalized,
                    Velocity.normalized) > 0.0f)
                if (!hit.collider.CompareTag("Unclimbable Wall"))
                {
                    _controller.TransitionTo<WallState>();
                }
        }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)_controller.Collider.offset, _controller.Collider.size, 0.0f, _controller.CollisionLayers);
        for (int i = _ignoredPlatforms.Count - 1; i >= 0; i--)
        {
            if (!colliders.Contains(_ignoredPlatforms[i]))
            {
                _ignoredPlatforms.Remove(_ignoredPlatforms[i]);
            }
        }

    }
}