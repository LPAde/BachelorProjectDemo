using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ExtraBehaviour : MonoBehaviour
    {
        [SerializeField] private List<Sprite> conceptArts;
        [SerializeField] private Image image;
        [SerializeField] private int index;

        private void Start()
        {
            image.sprite = conceptArts[0];
        }

        void Update()
        {
            Inputs();
        }

        private void Inputs()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index--;
                
                if (index < 0)
                {
                    index = conceptArts.Count - 1;
                }

                image.sprite = conceptArts[index];
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                index++;
                
                if (index == conceptArts.Count)
                {
                    index = 0;
                }

                image.sprite = conceptArts[index];
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
                SceneManager.LoadScene(0);
        }
    }
}
