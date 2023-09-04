using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{
	private const string SHOOT_TRIGGER = "Shoot";
	private readonly int shootHash = Animator.StringToHash(SHOOT_TRIGGER);
	[SerializeField] private ParticleSystem _muzzleFlashEmitter;
	[SerializeField] private Animator _artilleryAnimator;
	[SerializeField] private Transform _gunPivot;
	[SerializeField] private Transform _gunBasePivot;
	[SerializeField] private Transform _shellSpawnPoint;
	[SerializeField] private Rigidbody _shellPrefab;
	[SerializeField] private ArtilleryRangeDetector _rangeDetector;
	[SerializeField] private float _gunAngle;
	[SerializeField] private float _shootingRate = 2;
	[SerializeField] private float _rotationSpeed;
	private CursorFollower _shootingTarget;
	private bool _isRotating;
	private float _velocity;
	private float _timeOfPreviousShot = 0;

	private void Start()
	{
		_gunPivot.eulerAngles = new Vector3(-_gunAngle, _gunPivot.eulerAngles.y, _gunPivot.eulerAngles.z);
		_rangeDetector.TargetDetected += OnTargetDetected;
		_rangeDetector.TargetLost += OnTargetLost;
	}

	void Update()
	{
		HandleRotation();
		HandleTargetShooting();
	}

	private void OnTargetDetected(CursorFollower target)
	{
		_shootingTarget = target;
		_isRotating = true;
	}

	private void OnTargetLost(CursorFollower target)
	{
		if (target == _shootingTarget)
		{
			_shootingTarget = null;
		}
	}
	private void HandleRotation()
	{
		if (_shootingTarget == null)
		{
			return;
		}
		Vector3 directionVector = (_shootingTarget.transform.position - transform.position).normalized;
		Vector3 direction = new Vector3(directionVector.x, 0, directionVector.z);
		Vector3 newRotation = Vector3.MoveTowards(_gunBasePivot.forward, direction, Time.deltaTime * _rotationSpeed).normalized;
		_gunBasePivot.rotation = Quaternion.LookRotation(newRotation);
		_isRotating = _gunBasePivot.forward != direction;
	}

	private void HandleTargetShooting()
	{
		if (_shootingTarget == null)
		{
			return;
		}
		if (_isRotating)
		{
			return;
		}
		Vector3 fromTo = _shootingTarget.transform.position - _shellSpawnPoint.position;
		Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);
		float x = fromToXZ.magnitude;
		float y = fromTo.y;
		_velocity = BallisticCalculations.CalculateVelocity(x, y, _gunAngle);
		HandleShooting();
	}

	private void HandleShooting()
	{
		if (Time.time < _timeOfPreviousShot)
		{
			return;
		}
		Shoot();
	}

	private void Shoot()
	{
		_muzzleFlashEmitter.gameObject.SetActive(true);
		_muzzleFlashEmitter.Play();
		_artilleryAnimator.SetTrigger(shootHash);
		_timeOfPreviousShot = Time.time + _shootingRate;
		Rigidbody shell = Instantiate(_shellPrefab, _shellSpawnPoint.position, Quaternion.identity);
		shell.transform.rotation = _gunPivot.rotation;
		shell.AddForce(_shellSpawnPoint.forward * _velocity, ForceMode.VelocityChange);
	}
}
