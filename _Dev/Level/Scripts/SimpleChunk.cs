using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Internal._Dev.Level.Scripts
{
    enum SpawnAdditions
    {
        Yes,
        No,
        Random
    }
    public class SimpleChunk : Chunk
    {
        [SerializeField] private GameObject[] clothesPool;
        [SerializeField] private bool spawnClothes = true;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private SpawnAdditions spawnAdditions;
        [SerializeField] private Transform additionalSpawnpoint;
        [SerializeField] private GameObject[] additions;

        [SerializeField] private float additionalSpawnChance;

        [SerializeField] private float width;
        [SerializeField] private float height;

        [SerializeField] private int columns;
        [SerializeField] private int rows;
        [SerializeField] private GameObject[] hairs;
        [SerializeField] private Transform hairParent;
        [SerializeField] private float positionOffset;
        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            if (spawnClothes)
            {
               /* foreach (var point in spawnPoints)
                {
                    GameObject go = Instantiate(
                        clothesPool[Random.Range(0, clothesPool.Length)],
                        point);
                    if (Random.value < 0.5f)
                    {
                        go.transform.Rotate(0, 180, 0);
                        Vector3 newPos = go.transform.position;
                        newPos.x *= -1;
                        go.transform.position = newPos;
                    }
                }*/
               SpawnHair();
            }

        }

        private void SpawnHair()
        {
            float oneHeight = height / (columns+1);
            float oneWidth = width / (rows+1);
            for (int i = 1; i < rows+1; i++)
            {
                for (int j = 1; j < columns+1; j++)
                {
                    GameObject go = Instantiate(hairs[Random.Range(0, hairs.Length)], hairParent);
                    go.transform.localPosition = new Vector3(j * oneHeight - height/2, 0, i * oneWidth - width/2);
                    Vector2 randPos = Random.insideUnitCircle * positionOffset;
                    go.transform.localPosition += new Vector3(randPos.x, 0, randPos.y);
                    go.transform.Rotate(0,0,Random.Range(0, 360));
                }
            }
        }

        public override void SpawnAdditional(AdditionalObstacle addType)
        {
            if (addType == AdditionalObstacle.None)
            {
                return;
            }
            else
            {
                Instantiate(additions[(int) addType - 1], additionalSpawnpoint);
            }
        }
    }
}
