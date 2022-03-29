using System.Collections.Generic;
using UnityEngine;

public class MapEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        GameManager.Instance.StartCombat(enemies);
        Destroy(gameObject);
    }
}
