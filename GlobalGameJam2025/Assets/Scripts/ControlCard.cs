using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class ControlCard : MonoBehaviour
{
    [SerializeField] CardScriptableObjectScript[] baseDataCard;
    [SerializeField] public List<CardScriptableObjectScript> dataCard = new List<CardScriptableObjectScript>();
    [SerializeField] GameObject cardPrefab;

    [SerializeField] GameObject pointPlayerSpawnCard;
    [SerializeField] GameObject pointEnemySpawnCard;

    [SerializeField] public List<GameObject> playerCards;
    [SerializeField] public List<GameObject> enemyCards;
    [Header("Player")]
    [SerializeField] TMP_Text pointPlayerText;
    [SerializeField] public int pointPlayer;
    [SerializeField] public int pointPlayerShow;
    bool playerOnStand;
    public bool playerHideAll;
    [Header("Enemy")]
    [SerializeField] TMP_Text pointEnemyText;
    [SerializeField] public int pointEnemy;
    [SerializeField] public int pointEnemyShow;
    bool enemyOnStand;

    [SerializeField] float cardDistance;
    [Header("Btn")]
    [SerializeField] GameObject allPlayerAction;
    [SerializeField] Button drawCardBtn;
    [SerializeField] Button stayBtn;

    [SerializeField] TMP_Text showStateTurn;

    [SerializeField] public GameObject backjack;

    [SerializeField] int countGame;
    int rndSkillUse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.controlCard = this;
        drawCardBtn.onClick.AddListener(PlayerDrawCard);
        stayBtn.onClick.AddListener(PlayerStand);
        RandomUseSkill();
        RandomDeckCard();
    }
    void RandomUseSkill()
    {
        rndSkillUse = UnityEngine.Random.Range(1, 6);
    }
    void RandomDeckCard()
    {
        dataCard.Clear();
        for (int i = 0; i < baseDataCard.Length; i++)
        {
            dataCard.Add(baseDataCard[i]);
        }
        pointPlayer = 0;
        pointPlayerText.text = "0 / 21";
        pointEnemy = 0;
        pointEnemyText.text = "0 / 21";
        dataCard = dataCard.OrderBy(x => UnityEngine.Random.value).ToList();
    }
    CardScriptableObjectScript RandomCard()
    {
        int rndCard = UnityEngine.Random.Range(0, dataCard.Count);
        CardScriptableObjectScript newCard = dataCard[rndCard];
        dataCard.Remove(dataCard[rndCard]);
        return newCard;
    }
    public void CallStartGame()
    {
        //Check Skill Player
        GameManager.instance.skillControl.RegenCooldownSkill();
        countGame++;
        if (countGame == rndSkillUse)
        {
            GameManager.instance.textEffectBattle.CallReadText("What's wrong? Can't you see? HaHaHaHaHaHaHa");
            RandomUseSkill();
            countGame = 0;
            playerHideAll = true;
        }
        else
        {
            playerHideAll = false;
        }
        StartCoroutine(SpawnStartGame());
    }
    IEnumerator SpawnStartGame()
    {
        yield return new WaitForSeconds(3f);
        SpawnCard(pointPlayerSpawnCard.transform, true, true);
        SpawnCard(pointEnemySpawnCard.transform, false, false);
        yield return new WaitForSeconds(1.5f);
        SpawnCard(pointPlayerSpawnCard.transform, true, true);
        SpawnCard(pointEnemySpawnCard.transform, true, false);
        yield return new WaitForSeconds(1.5f);
        Invoke("StartPlayerTurn", 2f);
    }
    public void SetUpCard()
    {
        pointPlayerShow = 0;
        pointEnemyShow = 0;
        pointPlayer = 0;
        float playerRoll = (-cardDistance / 2) * playerCards.Count;
        for (int i = 0; i < playerCards.Count; i++)
        {
            Vector2 newX = playerCards[i].transform.localPosition;
            newX.x = playerRoll;
            playerCards[i].transform.transform.localPosition = newX;
            playerRoll += cardDistance;
            if (i > 0)
            {
                pointPlayerShow += playerCards[i].GetComponent<Cards>().dataCard.data.poin;
            }
            pointPlayer += playerCards[i].GetComponent<Cards>().dataCard.data.poin;
            if (!playerHideAll)
            {
                pointPlayerText.text = pointPlayer.ToString() + " / 21";
            }
            else
            {
                pointPlayerText.text = "0 / 21";
            }
        }

        float enemyRoll = (-cardDistance / 2) * enemyCards.Count;
        string q = "";

        bool haveHideCard = false;
        pointEnemy = 0;
        for (int i = 0; i < enemyCards.Count; i++)
        {
            Vector2 newX = enemyCards[i].transform.localPosition;
            newX.x = enemyRoll;
            enemyCards[i].transform.transform.localPosition = newX;
            enemyRoll += cardDistance;
            if (!enemyCards[i].GetComponent<Cards>().showCard)
            {
                haveHideCard = true;
            }
            if (enemyCards[i].GetComponent<Cards>().showCard)
            {
                pointEnemyShow += enemyCards[i].GetComponent<Cards>().dataCard.data.poin;
            }
            pointEnemy += enemyCards[i].GetComponent<Cards>().dataCard.data.poin;
        }
        if (haveHideCard)
        {
            q = "? + ";
        }
        pointEnemyText.text = q + pointEnemyShow.ToString() + " / 21";

    }
    void SpawnCard(Transform _poinSpawn, bool _ShowCard, bool _isPlayer)
    {
        Cards _newCard = Instantiate(cardPrefab, _poinSpawn.transform).GetComponent<Cards>();
        _newCard.dataCard = RandomCard();
        _newCard.gameObject.SetActive(false);
        if (_isPlayer)
        {
            if (!playerHideAll)
            {
                _newCard.showCard = _ShowCard;
            }
            else
            {
                _newCard.showCard = false;
            }
            playerCards.Add(_newCard.gameObject);
            pointPlayer += _newCard.dataCard.data.poin;
        }
        else
        {
            _newCard.showCard = _ShowCard;
            enemyCards.Add(_newCard.gameObject);
            pointEnemy += _newCard.dataCard.data.poin;
        }
        SetUpCard();
        _newCard.gameObject.SetActive(true);

    }
    #region State
    public void PlayerDrawCard()
    {
        SpawnCard(pointPlayerSpawnCard.transform, true, true);
        Invoke("EndTurnPlayer", 1f);
    }
    public void PlayerStand()
    {
        playerOnStand = true;
        allPlayerAction.SetActive(false);
        if (enemyOnStand)
        {
            Invoke("ChooseWinner", 1f);
        }
        else
        {
            Invoke("EndTurnPlayer", 1f);
        }
    }
    public void EnemyDrawCard()
    {
        SpawnCard(pointEnemySpawnCard.transform, true, false);
        Invoke("EndTurnEnemy", 1f);
    }
    public void EnemyStand()
    {
        enemyOnStand = true;
        if (playerOnStand)
        {
            Invoke("ChooseWinner", 1f);
        }
        else
        {
            Invoke("EndTurnEnemy", 1f);
        }
    }
    void EndTurnEnemy()
    {
        Invoke("StartPlayerTurn", 1f);
    }
    void StartPlayerTurn()
    {
        showStateTurn.text = "Player";
        playerOnStand = false;
        allPlayerAction.SetActive(true);
    }
    void EndTurnPlayer()
    {
        allPlayerAction.SetActive(false);
        showStateTurn.text = "Enemy";
        Invoke("StartEnemyTurn", 1f);
    }
    void StartEnemyTurn()
    {
        enemyOnStand = false;
        GameManager.instance.controlEnemy.StarEnemyAction();
    }
    #endregion
    #region Check Win
    void ChooseWinner()
    {
        playerOnStand = false;
        enemyOnStand = false;
        StartCoroutine(StartChooseWinner());
    }
    IEnumerator StartChooseWinner()
    {
        //Show all card
        CallOpenAllCard();
        yield return new WaitForSeconds(5f);
        if (pointPlayer == pointEnemy)
        {
            // Debug.Log("Draw");
            GameManager.instance.textEffectBattle.CallReadText("lucky little mermaid");

        }
        else if (pointPlayer > pointEnemy)
        {
            if (pointPlayer <= 21)
            {
                PlayerWin();
            }
            else if (pointPlayer > 21)
            {
                if (pointEnemy > pointPlayer)
                {
                    PlayerWin();
                }
                else
                {
                    EnemyWin();
                }
            }
        }
        else
        {
            if (pointEnemy <= 21)
            {
                EnemyWin();
            }
            else if (pointEnemy > 21)
            {
                if (pointPlayer > pointEnemy)
                {
                    EnemyWin();
                }
                else
                {
                    PlayerWin();
                }
            }

        }
        yield return new WaitForSeconds(1f);
        Debug.Log("Reset Game");
        ResetGame();
    }
    void PlayerWin()
    {
        Debug.Log("Player Win");
        showStateTurn.text = "Player \n Win";
        GameManager.instance.controlEnemy.Takedamage(GameManager.instance.controlEnemy.UseBet());
    }
    void EnemyWin()
    {
        Debug.Log("Enemy Win");
        showStateTurn.text = "Enemy \n Win";
        GameManager.instance.controlEnemy.AddPointBubble();
    }
    #endregion
    #region ResetGame
    void ResetGame()
    {
        foreach (GameObject apc in playerCards)
        {
            Destroy(apc);
        }
        playerCards.Clear();
        foreach (GameObject aec in enemyCards)
        {
            Destroy(aec);
        }
        enemyCards.Clear();
        RandomDeckCard();
        CallStartGame();
    }
    #endregion
    void CallOpenAllCard()
    {

        foreach (GameObject apc in playerCards)
        {
            apc.GetComponent<Cards>().OpenShowCard();
        }
        foreach (GameObject aec in enemyCards)
        {
            aec.GetComponent<Cards>().OpenShowCard();
        }
        pointEnemyText.text = pointEnemy + " / 21";
    }
}
