using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverBehaviour : MonoBehaviour
    {
        [SerializeField] private string surveyLink;
        
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Retry()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OpenSurvey()
        {
            Application.OpenURL(surveyLink);
        }
    }
}
