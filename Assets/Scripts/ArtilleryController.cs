using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{
    private const string ROTATION_PARAMETER = "RotationDirection";
    private const string SHOOT_TRIGGER = "Shoot";
	private const int MAXIMAL_VERTICAL_ROTATION = -30;
	private const int MINIMAL_VERTICAL_ROTATION = 9;
	private readonly int rotationHash = Animator.StringToHash(ROTATION_PARAMETER);
    private readonly int shootHash = Animator.StringToHash(SHOOT_TRIGGER);
	[SerializeField] private ParticleSystem _muzzleFlashEmitter;
    [SerializeField] private Animator _artilleryAnimator;
    [SerializeField] private Transform _gunPivot;
	[SerializeField] private Transform _gunBasePivot;
	[SerializeField] private float _horizontalSensitivity = 2;
	[SerializeField] private float _verticalSensitivity = 2;
	[SerializeField] private float _shootingRate = 2;
	private float _timeOfPreviousShot;

    void Update()
	{
		HandleMovementInput();
		HandleShootingInput();
	}

	private void HandleShootingInput()
	{
		if(Time.time < _timeOfPreviousShot)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			_muzzleFlashEmitter.gameObject.SetActive(true);
			_muzzleFlashEmitter.Play();
			_artilleryAnimator.SetTrigger(shootHash);
			_timeOfPreviousShot = Time.time + _shootingRate;
		}
	}

	private void HandleMovementInput()
	{
		float verticalInput = Input.GetAxisRaw("Vertical");
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		_artilleryAnimator.SetFloat(rotationHash, verticalInput);
		if (verticalInput != 0)
		{
			float convertedAngle = _gunPivot.eulerAngles.x;
			convertedAngle = (convertedAngle > 180) ? convertedAngle - 360 : convertedAngle;
			float newRotationX = Mathf.Clamp(convertedAngle + (Input.GetAxisRaw("Vertical") * -1 * Time.deltaTime * _verticalSensitivity),
				MAXIMAL_VERTICAL_ROTATION, MINIMAL_VERTICAL_ROTATION);
			Vector3 newRotation = new Vector3(newRotationX, _gunPivot.localEulerAngles.y, _gunPivot.localEulerAngles.z);
			_gunPivot.localEulerAngles = newRotation;
		}

		if (horizontalInput != 0)
		{
			float newRotationY = _gunBasePivot.localEulerAngles.y + (horizontalInput * _horizontalSensitivity * Time.deltaTime);
			Vector3 newRotation = new Vector3(_gunBasePivot.localEulerAngles.x, newRotationY, _gunPivot.localEulerAngles.z);
			_gunBasePivot.localEulerAngles = newRotation;
		}
	}
}
