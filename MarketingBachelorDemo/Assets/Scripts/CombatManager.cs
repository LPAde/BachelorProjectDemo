using System.Collections.Generic;
using UI;
using UnityEngine;

public enum BattleStatus
{
    Start,
    PlayerTurn,
    PlayerDialog,
    EnemyTurn,
    EnemyDialog,
    End
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    
    [SerializeField] private BattleStatus currentBattleStatus;
    
    [SerializeField] private List<EnemyBehaviour> enemies;
    [SerializeField] private List<GameObject> spawnPoints;

    private float currentDamageNumber;
    private byte playerAttackIndex;
    private bool playerWon;
    
    public BattleStatus CurrentBattleStatus => currentBattleStatus;
    public List<EnemyBehaviour> Enemies => enemies;
    public List<GameObject> SpawnPoints => spawnPoints;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleCombat();
    }

    private void HandleCombat()
    {
        switch (currentBattleStatus)
        {
            case BattleStatus.Start:
                
                if(CombatUIHandler.Instance.HandleCombatDialog())
                    ChangeBattleStatus(BattleStatus.PlayerTurn);
                
                break;
            
            case BattleStatus.PlayerTurn:

                if (CombatUIHandler.Instance.HandleCombatDialog())
                {
                    currentDamageNumber = 0;
                    currentDamageNumber = PlayerBehaviour.Player.Attack(playerAttackIndex);
                    ChangeBattleStatus(BattleStatus.PlayerDialog);
                }
                    
                break;
            
            case BattleStatus.PlayerDialog:

                if (CombatUIHandler.Instance.HandleCombatDialog(currentDamageNumber))
                {
                    if (playerWon)
                    {
                        playerWon = false;
                        ChangeBattleStatus(BattleStatus.End);
                    }
                    else 
                        ChangeBattleStatus(BattleStatus.EnemyTurn);
                }

                break;
            
            case BattleStatus.EnemyTurn:

                currentDamageNumber = 0;
                foreach (var enemy in enemies)
                {
                   currentDamageNumber += enemy.Attack();
                }
                
                ChangeBattleStatus(BattleStatus.EnemyDialog);
                break;
            
            case BattleStatus.EnemyDialog:
                
                if(CombatUIHandler.Instance.HandleCombatDialog(currentDamageNumber))
                    ChangeBattleStatus(BattleStatus.PlayerTurn);
                
                break;
            
            case BattleStatus.End:

                if (CombatUIHandler.Instance.HandleCombatDialog())
                {
                    PlayerBehaviour.Player.transform.position = GameManager.Instance.PlayerOriginalPosition;
                    GameManager.Instance.ChangeStatus(GameStatus.Idle);
                    gameObject.SetActive(false);
                    CombatUIHandler.Instance.gameObject.SetActive(false);
                }
                break;
        }
    }
    
    /// <summary>
    /// Adds an enemy to the list of enemies.
    /// </summary>
    /// <param name="newEnemy"> The enemy to add. </param>
    public void AddEnemy(EnemyBehaviour newEnemy)
    {
        enemies.Add(newEnemy);
    }
    
    /// <summary>
    /// Removes an enemy from the list of enemies.
    /// </summary>
    /// <param name="newEnemy"> The enemy to remove. </param>
    public void RemoveEnemy(EnemyBehaviour newEnemy)
    {
        enemies.Remove(newEnemy);

        if (enemies.Count <= 0)
        {
            playerWon = true;
        }
    }

    public void ChangeBattleStatus(BattleStatus newStatus)
    {
        currentBattleStatus = newStatus;
    }

    public void DoPlayerAttack(byte attackIndex)
    {
        playerAttackIndex = attackIndex;
    }
}