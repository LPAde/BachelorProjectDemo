using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;
    
    [SerializeField] private float health;
    
    [SerializeField] private List<AttackBehaviour> attacks;
    [SerializeField] private Animator anim;

    public Action OnPlayerDeath;

    public List<AttackBehaviour> Attacks => attacks;
    
    private void Awake()
    {
        if(Player == null)
            Player = this;
    }

    /// <summary>
    /// Attacks all enemies with the chosen attack.
    /// </summary>
    /// <param name="attackIndex"> The chosen attack. </param>
    public float Attack(int attackIndex)
    {
        var attack = attacks[attackIndex];
        anim.SetTrigger("Attack");

        for (var index = 0; index < CombatManager.Instance.Enemies.Count; index++)
        {
            CombatManager.Instance.Enemies[index].TakeDamage(attack.strength);
        }

        return attack.strength;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        anim.SetTrigger("Hurt");
        if(health <= 0)
            OnPlayerDeath.Invoke();
    }
}