using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryShell : MonoBehaviour
{
	private const int TIME_TO_DEATH = 3;
	private const string SHOOTABLE_TAG = "Shootable";
	[SerializeField] private ParticleSystem _deathParticle;
	[SerializeField] private GameObject _capsuleMesh;

	private void Start()
	{
		_deathParticle.gameObject.SetActive(false);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag(SHOOTABLE_TAG))
		{
			return;
		}
		Destroy(_capsuleMesh);
		_deathParticle.gameObject.SetActive(true);
		_deathParticle?.Play();
		Destroy(gameObject, TIME_TO_DEATH);
	}
}
