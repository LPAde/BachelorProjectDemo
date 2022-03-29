using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private string surveyLink;
    
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OpenSurvey()
        {
            Application.OpenURL(surveyLink);
        }

        public void OpenExtras()
        {
            SceneManager.LoadScene(4);
        }
    }
}
