using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Internal._Dev.Level.Scripts
{
    
    public class Chunk : MonoBehaviour
    {
        public Transform begin;
        public Transform end;
        public ChunkType type;

        public virtual void SpawnAdditional(AdditionalObstacle addType)
        {
            //Do nothing
        }
    }
}
