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

    //Egy start buttonon van rajta, elíndítja a combatot(késõbb nem gomb lesz de egyenlõre jó)
    public void StartCombat()
    { 
        combatState = CombatState.START;
        StartCoroutine(SetupCombat());

        turnCount++;
    }

    //beállítjuk az enemyt és a player turn következik
    IEnumerator SetupCombat()
    {
        enemy = new Enemy("Ferenc", 100f, 10f);
        EnemyHealth = enemy.Health;
        EnemyDamage = enemy.Damage;

        yield return new WaitForSeconds(1f);

        combatState = CombatState.PLAYER_TURN;
        PlayerTurn();
    }

    //kiírja hogy a player jön, csak információt ad
    private void PlayerTurn()
    {
        Debug.Log($"Turn: {turnCount} - Player Turn");
    }

    //az attack button, ha player turn az aktív combat state akkor tudunk támadni és meghívódik a PlayerAttack
    public void OnAttackButton()
    {
        if (combatState != CombatState.PLAYER_TURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    //minden ide kerül amit player támadás közben tesz
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
