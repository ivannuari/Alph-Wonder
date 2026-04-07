using GaweDeweStudio;
using System;
using System.Collections;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public static Level2Controller Instance;

    [SerializeField] private TextItem textPrefab;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private float spawnRangeX = 4.35f;

    [Header("PARAMETER")]
    [SerializeField] private int currentLevel;
    [SerializeField] private Sprite[] allTitleImages;

    [Header("IN GAME SPRITES")]
    [SerializeField] private Sprite[] allImages;
    [SerializeField] private Sprite bombImage;

    [SerializeField] private float spawnDelay = 1.5f;

    [SerializeField] private int score;
    [SerializeField] private int health;

    private bool isPlaying = false;
    private Coroutine routine;

    public event Action<int> OnScoreUpdated;
    public event Action<int> OnHealthUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        health = 4;
        OnHealthUpdated?.Invoke(health);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(GameState newState)
    {
        if(newState == GameState.Game)
        {
            isPlaying = true;
            routine = StartCoroutine(SpawnLoop());
        }

        if(newState == GameState.Result)
        {
            StopCoroutine(routine);
        }
    }

    private IEnumerator SpawnLoop()
    {
        health = 4;
        score = 0;

        while (isPlaying)
        {
            SpawnItem();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnItem()
    {
        float randomX = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnPos.position.y, spawnPos.position.z);

        var clone = Instantiate(textPrefab, spawnPosition, Quaternion.identity);

        bool spawnBomb = UnityEngine.Random.value < 0.2f; // 20% bomb

        if (spawnBomb)
        {
            clone.Setup(bombImage, true, false);
        }
        else
        {
            bool spawnWrong = UnityEngine.Random.value < 0.35f; // 35% salah
            if (spawnWrong)
            {
                clone.Setup(allImages[UnityEngine.Random.Range(0, allImages.Length)], false, true);
            }
            else
            {
                clone.Setup(allImages[currentLevel], false, false);
            }
        }
    }

    public void AddScore()
    {
        score++;
        OnScoreUpdated?.Invoke(score);

        if(score >= 30)
        {
            GameManager.Instance.ChangeState(GameState.Result);
        }
    }

    public void TakeDamage()
    {
        health--;
        OnHealthUpdated?.Invoke(health);

        if(health <= 0)
        {
            GameManager.Instance.ChangeState(GameState.Result);
        }
    }

    public Sprite GetTitleImage()
    {
        return allTitleImages[currentLevel];
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}