using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public enum CombatState { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE };

    float PlayerHealth = 100f;
    float PlayerDamage = 10f;

    Enemy enemy;
    float EnemyHealth;
    float EnemyDamage;

    int turnCount = 0;

    CombatState combatState;

    public void StartCombat()
    { 
        combatState = CombatState.START;
        StartCoroutine(SetupCombat());
        turnCount++;
    }

    IEnumerator SetupCombat()
    {
        enemy = new Enemy("Ferenc", 100f, 10f);
        EnemyHealth = enemy.Health;
        EnemyDamage = enemy.Damage;

        yield return new WaitForSeconds(2f);

        combatState= CombatState.PLAYER_TURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        Debug.Log($"Turn:{turnCount} - Player Turn");
    }

    public void OnAttackButton()
    {
        if (combatState != CombatState.PLAYER_TURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        EnemyHealth -= PlayerDamage;

        yield return new WaitForSeconds(2f);

        if(EnemyHealth <= 0)
        {
            combatState= CombatState.WIN;
            EndCombat();
        }
        else
        {
            combatState= CombatState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());
        }
        Debug.Log("Enemy health " + EnemyHealth);
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log($"Turn: {turnCount} - Enemy Turn");
        PlayerHealth -= EnemyDamage;

        yield return new WaitForSeconds(2f);

        if (PlayerHealth <= 0)
        {
            combatState = CombatState.LOSE;
            EndCombat();
        }
        else
        {
            turnCount++;
            PlayerTurn();
            combatState = CombatState.PLAYER_TURN;
        }
        Debug.Log("Player health " + PlayerHealth);
    }

    private void EndCombat()
    {
        if(combatState == CombatState.WIN) {
            Debug.Log("Won");
        }
        else if(combatState == CombatState.LOSE)
        {
            Debug.Log("Lost");
        }
    }
}
