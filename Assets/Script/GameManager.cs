using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    Ready,
    Starting,
    Playing,
    Over
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float delay = 2f;
    [SerializeField] TMP_Text scoreText;

    private GameState gameState;

    private Transform player;
    private EntitiesSpawner entitiesSpawner;

    private float score;
    private float timeElapsed;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        entitiesSpawner = FindObjectOfType<EntitiesSpawner>();

        gameState = GameState.Ready;
    }

    private void Start()
    {
        gameState = GameState.Starting;
        StartCoroutine(MainGameLoopRoutine());
    }

    private IEnumerator MainGameLoopRoutine()
    {
        yield return StartCoroutine(StartGameRoutine());
        yield return StartCoroutine(PlayGameRoutine());
        yield return StartCoroutine(EndGameRoutine());
    }


    private IEnumerator StartGameRoutine()
    {
        timeElapsed = 0f;

        yield return new WaitForSeconds(delay);
        gameState = GameState.Playing;
    }

    private void Update()
    {
        if (gameState == GameState.Starting || gameState == GameState.Playing)
        {
            //UpdateTime
        }
    }

    private IEnumerator PlayGameRoutine()
    {

        entitiesSpawner?.StartSpawn();

        while (gameState == GameState.Playing)
        {
            yield return null;
        }
    }



    private IEnumerator EndGameRoutine()
    {

        yield return new WaitForSeconds(delay);

        gameState = GameState.Ready;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static Vector3 GetPlayerPosition()
    {
        if (GameManager.Instance == null)
        {
            return Vector3.zero;
        }
        //Not working
        return (Instance.player != null) ? GameManager.Instance.player.position : Vector3.zero;
    }

    public static void EndGame()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        Instance.gameState = GameState.Over;
    }

    public static bool IsGameOver()
    {
        if (GameManager.Instance == null)
        {
            return false;
        }

        return (Instance.gameState == GameState.Over);
    }

    public static void AddScore(int scoreValue)
    {
        Instance.score += scoreValue;

        if (Instance.scoreText != null)
        {
            Instance.scoreText.text = Instance.score.ToString();
        }
    }
}