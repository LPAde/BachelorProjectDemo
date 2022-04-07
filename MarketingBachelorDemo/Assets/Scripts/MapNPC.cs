using System.Collections.Generic;
using UI;
using UnityEngine;

public class MapNPC : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private List<string> noItemDialog;
    [SerializeField] private List<string> itemDialog;
    [SerializeField] private bool canTalk;

    private void Update()
    {
        if (!canTalk) 
            return;
        
        if (Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.CurrentStatus == GameStatus.Idle)
        {
            DialogManager.Instance.StartDialog(npcName,
                GameManager.Instance.PlayerHasItem ? itemDialog : noItemDialog);
            
            if(GameManager.Instance.PlayerHasItem)
                GameManager.Instance.OpenDoor.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canTalk = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canTalk = false;
    }
}