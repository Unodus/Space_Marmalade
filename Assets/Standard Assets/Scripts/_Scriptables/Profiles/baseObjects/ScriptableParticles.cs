using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleProfile", menuName = "ScriptableObjects/ParticleProfile", order = 3)]

public class ScriptableParticles : ScriptableBase
{

    [Serializable]
    public class ParticleSettings // Each palette
    {
        public string Name;
        public baseObject particle;
    }

    public ParticleSettings[] Particles;    // array of all palettes



}
