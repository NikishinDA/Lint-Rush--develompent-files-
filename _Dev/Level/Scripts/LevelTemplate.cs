using System;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
    
    public class LevelTemplate : ScriptableObject
    {
        public bool clothless;
        public ChunkTemplate[] template;
    }
    [Serializable]
    public class ChunkTemplate
    {
        public ChunkType type;
        public AdditionalObstacle additional;
    }

    public enum ChunkType
    {
        Simple,
        Poop,
        FoodWaste,
        Roaches,
        Sink
    }

    public enum AdditionalObstacle
    {
        None,
        Comb,
        FlyTape
    }
}
