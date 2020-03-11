using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public bool DestroyOnAwake = false;

	void Awake()
	{
		if (DestroyOnAwake)
		{
			Remove();
		}
	}

	public void Remove()
	{
		Destroy(gameObject);
	}
}
