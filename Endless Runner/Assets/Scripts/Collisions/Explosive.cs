using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : CollisionHandler
{
	public ParticleHandle explosiveParticleHandler;
	public float TimeToDespawn = 0.2f;

	public override void OnCollide()
	{
		base.OnCollide();
		explosiveParticleHandler.gameObject.transform.parent = GameObject.FindGameObjectWithTag("ParticleDebris").transform;
		explosiveParticleHandler.Play();
		Destroy(gameObject, TimeToDespawn);
	}
}
