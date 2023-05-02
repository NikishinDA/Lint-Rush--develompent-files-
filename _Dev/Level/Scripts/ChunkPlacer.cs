using System.Collections.Generic;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class ChunkPlacer : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Chunk chunkPrefab;
        [SerializeField] private Chunk[] firstPrefab;
        [SerializeField] private Chunk finalPrefab;
        [SerializeField] private float spawnDistance;
        [SerializeField] private int concurrentChunkNumber;
        [SerializeField] private int levelLength;
        [SerializeField] private Chunk[] chunkPool;
        [SerializeField] private Transform chunkParent;
        private List<Chunk> spawnedChunks;
        private bool finishSpawned = false;
        private int currentLength = 4;

        [SerializeField] private LevelTemplate[] templates;
        private LevelTemplate currentTemplate;
        
        [Header("Debug")]
        [SerializeField] private bool infinite = false;
        private void Awake()
        {
            spawnedChunks = new List<Chunk>();
            EventManager.AddListener<GameOverEvent>(OnGameOver);
            EventManager.AddListener<GameStartEvent>(OnGameStart);

            int level = PlayerPrefs.GetInt("Level", 1);
            currentTemplate = level < 8 ? templates[level-1] : templates[Random.Range(2,7)];
            VarSaver.Clothless = currentTemplate.clothless;
            VarSaver.LevelLength = currentTemplate.template.Length;
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<GameOverEvent>(OnGameOver);
            EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        }

        private void OnGameStart(GameStartEvent obj)
        {
            var evt = GameEventsHandler.GameInitializeEvent;
            EventManager.Broadcast(evt);
        }

        private void Start()
        {
            currentLength = firstPrefab.Length;
            levelLength = currentTemplate.template.Length;
            foreach (Chunk ch in firstPrefab)
            {
                spawnedChunks.Add(ch);
            }
        }

        private void Update()
        {
            if ((!finishSpawned) &&
                (playerTransform.position.z >
                 spawnedChunks[spawnedChunks.Count - 1].end.position.z - spawnDistance))
            {
                SpawnChunk();
            }
        }

        private void SpawnChunk()
        {
            Chunk newChunk;
            if (currentLength < levelLength)
            {
                ChunkTemplate nextChunk = currentTemplate.template[currentLength - 1];
                newChunk = Instantiate(chunkPool[(int) nextChunk.type], chunkParent);
                newChunk.type = nextChunk.type;
                newChunk.SpawnAdditional(nextChunk.additional);
            }
            else
            {
                newChunk = Instantiate(finalPrefab,chunkParent);
                finishSpawned = true;
            }

            newChunk.transform.position =
                spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
            spawnedChunks.Add(newChunk);

            currentLength++;
            if (spawnedChunks.Count > concurrentChunkNumber)
            {
                Destroy(spawnedChunks[0].gameObject);
                spawnedChunks.RemoveAt(0);
            }
        }

        private void OnGameOver(GameOverEvent obj)
        {
            finishSpawned = true;
        }
    }
}