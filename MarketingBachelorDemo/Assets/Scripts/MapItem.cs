using UnityEngine;

public class MapItem : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    
    [SerializeField] private bool canObtain;

    private void Update()
    {
        if (!canObtain) 
            return;
        
        if (Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.CurrentStatus == GameStatus.Idle)
        {
           GameManager.Instance.ObtainItem();
           Destroy(indicator);
           Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canObtain = true;
        indicator.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        canObtain = false;
        indicator.SetActive(false);
    }
}
