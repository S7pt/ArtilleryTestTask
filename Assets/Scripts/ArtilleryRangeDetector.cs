using System;
using UnityEngine;

public class ArtilleryRangeDetector : MonoBehaviour
{
	public event Action<CursorFollower> TargetDetected;
	public event Action<CursorFollower> TargetLost;
	private CursorFollower _targetFollower;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out _targetFollower))
		{
			TargetDetected?.Invoke(_targetFollower);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(!other.TryGetComponent(out CursorFollower cursorFollower))
		{
			return;
		}

		if(cursorFollower == _targetFollower)
		{
			TargetLost?.Invoke(_targetFollower);
		}
	}
}
