using System;
using UnityEngine;

public class BallisticCalculations : MonoBehaviour
{
	private static float gravity = Mathf.Abs(Physics.gravity.y);
	public static float CalculateAngle(float velocity, float x, float heightDelta)
	{
		float phaseAngle = Mathf.Atan2(x, heightDelta) * Mathf.Rad2Deg;
		float e1 = ((gravity * x * x) / (velocity * velocity)) - heightDelta;
		e1 /= Mathf.Sqrt((heightDelta * heightDelta) + (x * x));
		float e2 = Mathf.Acos(e1) * Mathf.Rad2Deg;
		e2 += phaseAngle;
		e2 /= 2;
		return e2;
	}

	public static float CalculateVelocity(float x, float y, float angle)
	{
		float radians = angle * Mathf.Deg2Rad;
		float v2 = (gravity * x * x) / (2 * (y - Mathf.Tan(radians) * x) * (float)Math.Pow(Mathf.Cos(radians), 2));
		float v = Mathf.Sqrt(Mathf.Abs(v2));
		return v;
	}
}
