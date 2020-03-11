using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandle : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void Play()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }
}
