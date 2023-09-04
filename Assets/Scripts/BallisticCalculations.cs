using System;
using UnityEngine;

public class BallisticCalculations : MonoBehaviour
{
	private static float gravity = Mathf.Abs(Physics.gravity.y);

	public static float CalculateVelocity(float x, float y, float angle)
	{
		float radians = angle * Mathf.Deg2Rad;
		float v2 = (gravity * x * x) / (2 * (y - Mathf.Tan(radians) * x) * (float)Math.Pow(Mathf.Cos(radians), 2));
		float v = Mathf.Sqrt(Mathf.Abs(v2));
		return v;
	}
}
