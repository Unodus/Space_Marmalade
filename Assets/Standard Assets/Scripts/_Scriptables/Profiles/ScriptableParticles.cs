using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleProfile", menuName = "ScriptableObjects/ParticleProfile", order = 3)]

public class ScriptableParticles : ScriptableBase
{

    public enum Particle
    {
        Point
    }
    [Serializable]
    public class ParticleSettings // Each palette
    {
        public Particle Name;
        public GameObject ParticlePrefab;
    }

    public ParticleSettings[] Particles;    // array of all palettes



}
