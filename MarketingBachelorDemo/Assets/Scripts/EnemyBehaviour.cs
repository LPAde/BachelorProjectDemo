using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private List<AttackBehaviour> attacks;
    [SerializeField] private Animator anim;

    private void Start()
    {
        CombatManager.Instance.AddEnemy(this);
    }

    /// <summary>
    /// Attacks with a random move.
    /// </summary>
    public float Attack()
    {
        var attack = attacks[Random.Range(0, attacks.Count)];
       // anim.SetBool(attack.animationBool, true);
        PlayerBehaviour.Player.TakeDamage(attack.strength);
        return attack.strength;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
            Death();
    }

    private void Death()
    {
        CombatManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
