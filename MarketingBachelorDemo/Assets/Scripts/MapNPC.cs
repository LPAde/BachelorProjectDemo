using System;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class MapNPC : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private List<string> dialog;
    [SerializeField] private bool canTalk;

    private void Update()
    {
        if (canTalk)
        {
            if (Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.CurrentStatus == GameStatus.Idle)
            {
                DialogManager.Instance.StartDialog(npcName, dialog);
            }
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