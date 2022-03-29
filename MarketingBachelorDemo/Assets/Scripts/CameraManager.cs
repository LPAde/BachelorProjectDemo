using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject battleCam;
    [SerializeField] private GameObject overworldCam;
    
    private void Awake()
    {
        GameManager.Instance.OnGameStatusChange += HandleCamera;
    }

    private void HandleCamera(GameStatus status)
    {
        switch (status)
        {
            case GameStatus.Idle:
                overworldCam.SetActive(true);
                battleCam.SetActive(false);
                break;
            case GameStatus.Combat:
                overworldCam.SetActive(false);
                battleCam.SetActive(true);
                break;
        }
    }
}
