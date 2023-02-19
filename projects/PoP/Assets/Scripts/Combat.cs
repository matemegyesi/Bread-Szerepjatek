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

    //Egy start buttonon van rajta, el�nd�tja a combatot(k�s�bb nem gomb lesz de egyenl�re j�)
    public void StartCombat()
    { 
        combatState = CombatState.START;
        StartCoroutine(SetupCombat());

        turnCount++;
    }

    //be�ll�tjuk az enemyt �s a player turn k�vetkezik
    IEnumerator SetupCombat()
    {
        enemy = new Enemy("Ferenc", 100f, 10f);
        EnemyHealth = enemy.Health;
        EnemyDamage = enemy.Damage;

        yield return new WaitForSeconds(1f);

        combatState = CombatState.PLAYER_TURN;
        PlayerTurn();
    }

    //ki�rja hogy a player j�n, csak inform�ci�t ad
    private void PlayerTurn()
    {
        Debug.Log($"Turn: {turnCount} - Player Turn");
    }

    //az attack button, ha player turn az akt�v combat state akkor tudunk t�madni �s megh�v�dik a PlayerAttack
    public void OnAttackButton()
    {
        if (combatState != CombatState.PLAYER_TURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    //minden ide ker�l amit player t�mad�s k�zben tesz
    IEnumerator PlayerAttack()
    {
        EnemyHealth -= PlayerDamage;

        yield return new WaitForSeconds(1f);

        if(EnemyHealth <= 0)
        {
            combatState = CombatState.WIN;
            EndCombat();
        }
        else
        {
            combatState = CombatState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        PlayerHealth -= EnemyDamage;

        yield return new WaitForSeconds(1f);

        if (PlayerHealth <= 0)
        {
            combatState = CombatState.LOSE;
            EndCombat();
        }
        else
        {
            turnCount++;
            combatState = CombatState.PLAYER_TURN;
            PlayerTurn();
        }
    }

    private void EndCombat()
    {
        if(combatState == CombatState.WIN)
            Debug.Log("WON!");
        else if(combatState == CombatState.LOSE)
            Debug.Log("LOST!");
    }
}
