using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Idle,
    Talking,
    Combat
}

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameStatus currentStatus;
    
    [SerializeField] private GameObject playerCombatPosition;
    [SerializeField] private Vector3 playerOriginalPosition;

    public Action<GameStatus> OnGameStatusChange;

    public GameStatus CurrentStatus
    {
        get => currentStatus;
        
        private set
        {
            currentStatus = value;
            OnGameStatusChange.Invoke(CurrentStatus);
        }
    }

    public Vector3 PlayerOriginalPosition => playerOriginalPosition;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        PlayerBehaviour.Player.OnPlayerDeath += GameOver;
    }

    /// <summary>
    /// Starts combat and spawns the enemies.
    /// </summary>
    /// <param name="enemies"> The enemies to be spawned. </param>
    public void StartCombat(List<GameObject> enemies)
    {
        ChangeStatus(GameStatus.Combat);
        var transform1 = PlayerBehaviour.Player.transform;
        playerOriginalPosition = transform1.position;
        transform1.position = playerCombatPosition.transform.position;
        CombatManager.Instance.gameObject.SetActive(true);
        CombatUIHandler.Instance.gameObject.SetActive(true);

        for (int i = 0; i < enemies.Count; i++)
        {
            Instantiate(enemies[i],CombatManager.Instance.SpawnPoints[i].transform.position,Quaternion.identity, CombatManager.Instance.gameObject.transform);
        }
        
    }

    public void ChangeStatus(GameStatus newStatus)
    {
        CurrentStatus = newStatus;
    }

    private void GameOver()
    {
        SceneManager.LoadScene(3);
    }
}
