using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleProfile", menuName = "ScriptableObjects/ParticleProfile", order = 3)]

public class ScriptableParticles : ScriptableObject
{
    [Serializable]
    public class ParticleSettings // Each palette
    {
        public string Name;
        public GameObject ParticlePrefab;
    }

    public ParticleSettings[] Particles;    // array of all palettes

    public ParticleSettings GetParticleByName(string myName)
    {
        foreach (ParticleSettings i in Particles)
        {
            if (i.Name == myName)
            {
                return i;
            }
        }

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }

}
