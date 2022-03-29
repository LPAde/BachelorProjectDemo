using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance;
    
        [SerializeField] private string currentTalker;
        [SerializeField] private List<string> currentDialog;
        [SerializeField] private int currentDialogIndex;

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject textBox;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
        
            gameObject.SetActive(false);
        }

        private void Update()
        {
            InputCheck();
        }

        public void StartDialog(string newTalker, List<string> newDialog)
        {
            currentTalker = newTalker;
            currentDialog = newDialog;
        
            textBox.gameObject.SetActive(true); 
            gameObject.SetActive(true);
            currentDialogIndex = 0;
            text.text = currentTalker + ": " + currentDialog[currentDialogIndex];
            GameManager.Instance.ChangeStatus(GameStatus.Talking);
        }

        private void InputCheck()
        {
            if (!Input.GetKeyDown(KeyCode.Return))
                return;
        
            if (currentDialogIndex < currentDialog.Count - 1)
            {
                currentDialogIndex++;
                text.text = currentTalker + ": " + currentDialog[currentDialogIndex];
            }
            else
            {
                textBox.gameObject.SetActive(false); 
                gameObject.SetActive(false);
               
                GameManager.Instance.ChangeStatus(GameStatus.Idle);
            }
        }
    }
}