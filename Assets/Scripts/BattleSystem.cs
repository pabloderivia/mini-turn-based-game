using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleState state;

    Unit playerUnit, enemyUnit;
    //the position of the enemy and the player
    public Transform playerBattleStation, enemyBattleStation;

    public Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public Animator playerAnimator;
    public Animator enemyAnimator;
    GameObject enemyGO, playerGO;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit =  playerGO.GetComponent<Unit>();
        playerUnit.name = "Green Goblin";

        enemyGO = Instantiate (enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        playerAnimator = playerGO.GetComponent<Animator>();
        enemyAnimator = enemyGO.GetComponent<Animator>();

        dialogueText.text = "¡Un  "+enemyPrefab.name+" apareció!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);


        yield return new WaitForSeconds(1f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    void PlayerTurn()
    {
        dialogueText.text = "Elige una acción";
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "¡Has ganado la batalla! Maldito psicópata violento";

        }
        else if (state == BattleState.WON)
        {
            dialogueText.text = "Has sido derrotado. Noob. GG IZI.";
        }
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }


    IEnumerator PlayerAttack()
    {
        //play animation and wait a little before update the HPHUD
        Debug.Log("jugador atacando");
        playerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        //deal dmg and update HUD
        bool enemyIsDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.UpdateHP(enemyUnit);
        dialogueText.text = "Has golpeado con éxito!";
        yield return new WaitForSeconds(0.5f);
        if(enemyIsDead)
        {
            yield return new WaitForSeconds(0.1f);
            enemyGO.GetComponent<Die>().ExploteAndDie();
            //end batlle
            state = BattleState.WON;
            EndBattle();
        }
        else{
            yield return new WaitForSeconds(0.6f);
            //enemy turn
            StartEnemyTurn();
        }

        enemyHUD.UpdateHP(enemyUnit);

    }

    void StartEnemyTurn()
    {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text= "¡Turno del enemigo!";
        yield return new WaitForSeconds(0.4f);
        dialogueText.text= enemyPrefab.name +" ataca!";
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        bool playerIsDead = playerUnit.TakeDamage(enemyUnit.damage);

        if(playerIsDead)
        {
            playerHUD.UpdateHP(playerUnit);
            yield return new WaitForSeconds(0.4f);
            playerGO.GetComponent<Die>().ExploteAndDie();
            //end batlle
            EndBattle(); 
            state = BattleState.LOST;

        }
        else{
            playerHUD.UpdateHP(playerUnit);
            //player turn
            yield return new WaitForSeconds(0.6f);
            dialogueText.text = "Tu turno. Elige una acción.";
            state = BattleState.PLAYERTURN;

        }



        
    }

    
    
}
