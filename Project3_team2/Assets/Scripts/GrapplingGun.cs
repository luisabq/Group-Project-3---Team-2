using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    
    
    public bool IsGrappling {  get; private set; }

    public Vector3 GrapplePoint { get; private set; }

    [SerializeField] private float _grappleDistance;
    

    [SerializeField] private float _reelInAcceleration;

    private PlayerMovement _playerMovement;
    private Rigidbody _playerRigidbody;

    private bool _isApplyingGrappleForces;

    private float _ropeLength;
    private bool _isRopeInTension;

    private float _reelInSpeed;
    private bool _isReelingIn;

    private float _reatactionTimer;



    private void Start()
    {
        _playerMovement = transform.parent.GetComponentInParent<PlayerMovement>();
        _playerRigidbody = transform.parent.GetComponentInParent<Rigidbody>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }

            if (Input.GetMouseButton(0))
            {
                _isApplyingGrappleForces = true;

                _isRopeInTension = _ropeLength * _ropeLength < (GrapplePoint - _playerRigidbody.position).sqrMagnitude;
            }
            else
            {
                IsGrappling = false;
                _isApplyingGrappleForces = false;
                _playerMovement.enabled = true;


            }
        }



    private void FixedUpdate()
    {
        if (_isApplyingGrappleForces)
        {
            ApplyGrappleForces();

            if (Vector3.Dot(_playerRigidbody.linearVelocity, GrapplePoint - _playerRigidbody.position) <= 0 && _isRopeInTension)
            {
                TugPlayer();
            }

            if (_ropeLength > 1.5f && _isReelingIn)
            {
                _reelInSpeed += _reelInAcceleration * Time.fixedDeltaTime;
                _ropeLength -= _reelInSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _isReelingIn = false;
                _ropeLength = 1.5f;
            }
        }
    }

    private void StartGrapple()
    {
        if (IsGrappling)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, _grappleDistance))
        {
            GrapplePoint = hit.point;
            IsGrappling = true;          
            _playerMovement.enabled = false;
            _ropeLength = (GrapplePoint - _playerRigidbody.position).magnitude;
            _isReelingIn = true;
            _reelInSpeed = 0;
        }
    }

    private void ApplyGrappleForces()
    {
        Vector3 direction = (GrapplePoint - _playerRigidbody.position).normalized;
        float theta = Vector3.Angle(direction, Vector3.up) * Mathf.Deg2Rad;

        float centripetalAcceleration = _playerRigidbody.linearVelocity.sqrMagnitude / _ropeLength;
        Vector3 tension = _playerRigidbody.mass * (centripetalAcceleration + Physics.gravity.magnitude * Mathf.Cos(theta)) * direction;

        if (_isRopeInTension)
        {
            if (_isReelingIn)
            {
                _playerRigidbody.AddForce(_playerRigidbody.mass * _reelInAcceleration * direction);
            }

            _playerRigidbody.AddForce(tension, ForceMode.Force);
        }
    }

    private void TugPlayer()
    {
        Vector3 direction = (GrapplePoint - _playerRigidbody.position).normalized;

        Vector3 tangentialVelocity = Vector3.ProjectOnPlane(_playerRigidbody.linearVelocity, direction);
        _playerRigidbody.linearVelocity = tangentialVelocity;

        _isRopeInTension = true;

        _playerRigidbody.position = GrapplePoint - direction * _ropeLength;
    }

}
