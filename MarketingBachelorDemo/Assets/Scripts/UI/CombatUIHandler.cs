using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CombatUIHandler : MonoBehaviour
    {
        public static CombatUIHandler Instance;

        [SerializeField] private TextMeshProUGUI playerAction;
        [SerializeField] private GameObject playerActionPanel;
        [SerializeField] private TextMeshProUGUI actionReport;
        [SerializeField] private GameObject actionReportPanel;

        [SerializeField] private bool isAttacking;
        [SerializeField] private byte currentActionIndex;

        [SerializeField] private List<string> possibleActions;
    
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
        
            gameObject.SetActive(false);
        }

        public bool HandleCombatDialog()
        {
            if (CombatManager.Instance.CurrentBattleStatus == BattleStatus.PlayerTurn)
            {
                playerActionPanel.gameObject.SetActive(true);
                actionReportPanel.gameObject.SetActive(false);
            }
            else
            {
                playerActionPanel.gameObject.SetActive(false);
                actionReportPanel.gameObject.SetActive(true);
            }
        
            switch (CombatManager.Instance.CurrentBattleStatus)
            {
                case BattleStatus.Start:
            
                    actionReport.text = "You are being attacked by " + CombatManager.Instance.Enemies.Count + " Enemies.";
                
                    if (Input.GetKeyDown(KeyCode.Return))
                        return true;
                
                    break;
            
                case BattleStatus.PlayerTurn:

                    isAttacking = true;
                
                    var currentActionReport = "";

                    if (!isAttacking)
                    {
                        for (int i = 0; i < possibleActions.Count; i++)
                        {
                            if (i == currentActionIndex)
                            {
                                currentActionReport += "> ";
                            }

                            currentActionReport += possibleActions[i] + "\n";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < PlayerBehaviour.Player.Attacks.Count; i++)
                        {
                            if (i == currentActionIndex)
                            {
                                currentActionReport += "> ";
                            }

                            currentActionReport += PlayerBehaviour.Player.Attacks[i].attackName + "\n";
                        }
                    }

                    playerAction.text = currentActionReport;

                    return InputCheck();

                case BattleStatus.End:

                    actionReport.text = "You beat the enemies.";
                
                    if (Input.GetKeyDown(KeyCode.Return))
                        return true;
                
                    break;
            }
        
            return false;
        }
    
        public bool HandleCombatDialog(float damage)
        {
            playerActionPanel.gameObject.SetActive(false);
            actionReportPanel.gameObject.SetActive(true);
        
            switch (CombatManager.Instance.CurrentBattleStatus)
            {
                case BattleStatus.PlayerDialog:
                
                    actionReport.text = "You dealt " + damage + " Damage.";
                
                    if (Input.GetKeyDown(KeyCode.Return))
                        return true;
                
                    break;
                case BattleStatus.EnemyDialog:
                
                    actionReport.text = "The Enemies dealt " + damage + " Damage.";
                
                    if (Input.GetKeyDown(KeyCode.Return))
                        return true;
                
                    break;
            }
        
            return false;
        }

        /// <summary>
        /// Acts according to the Input.
        /// </summary>
        /// <returns> If player did Action. </returns>
        private bool InputCheck()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(currentActionIndex != 0)
                    currentActionIndex--;
            }
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (isAttacking)
                {
                    if(PlayerBehaviour.Player.Attacks.Count - 1 != currentActionIndex)
                        currentActionIndex++;
                }
                else
                {
                    if (possibleActions.Count - 1 != currentActionIndex)
                        currentActionIndex++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && isAttacking)
            {
                isAttacking = false;
                currentActionIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isAttacking)
                {
                    CombatManager.Instance.DoPlayerAttack(currentActionIndex);
                    isAttacking = false;
                    return true;
                }
                else
                {
                    if (currentActionIndex == 0)
                        isAttacking = true;
                    else
                        return true;
                }
            }

            return false;
        }
    }
}