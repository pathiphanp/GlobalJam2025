using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField] Button doubleattackBtn;
    [SerializeField] Button randomDestroyEnemyCardBtn;
    [SerializeField] Button randomOpenHideCardBtn;

    int cooldownDoubleattack;
    int cooldownRandomDestroyEnemyCard;
    int cooldownRandomOpenHideCard;

    private void Start()
    {
        GameManager.instance.skillControl = this;
        doubleattackBtn.onClick.AddListener(Doubleattack);
        randomDestroyEnemyCardBtn.onClick.AddListener(RandomDestroyEnemyCard);
        randomOpenHideCardBtn.onClick.AddListener(RandomOpenHideCard);
    }

    private void RandomOpenHideCard()
    {
        List<Cards> getEnemyCard = new List<Cards>();
        for (int i = 0; i < GameManager.instance.controlCard.enemyCards.Count; i++)
        {
            if (!GameManager.instance.controlCard.enemyCards[i].GetComponent<Cards>().showCard)
            {
                getEnemyCard.Add(GameManager.instance.controlCard.enemyCards[i].GetComponent<Cards>());
            }
        }
        int rndCard = UnityEngine.Random.Range(0, getEnemyCard.Count);
        getEnemyCard[rndCard].showCard = true;
        getEnemyCard[rndCard].SetUpCard();
        GameManager.instance.controlCard.SetUpCard();
        randomOpenHideCardBtn.gameObject.SetActive(false);
    }

    private void RandomDestroyEnemyCard()
    {
        List<GameObject> getEnemyCard = new List<GameObject>();
        for (int i = 0; i < GameManager.instance.controlCard.enemyCards.Count; i++)
        {
            getEnemyCard.Add(GameManager.instance.controlCard.enemyCards[i]);
        }
        int rndCard = UnityEngine.Random.Range(0, getEnemyCard.Count);
        GameManager.instance.controlCard.enemyCards.Remove(getEnemyCard[rndCard]);
        Destroy(getEnemyCard[rndCard].gameObject);
        GameManager.instance.controlCard.SetUpCard();
        randomDestroyEnemyCardBtn.gameObject.SetActive(false);
    }

    private void Doubleattack()
    {
        GameManager.instance.controlEnemy.isDoubleDamageTake = true;
        doubleattackBtn.gameObject.SetActive(false);
    }

    public void RegenCooldownSkill()
    {
        if (!doubleattackBtn.gameObject.activeSelf)
        {
            cooldownDoubleattack++;
            if (cooldownDoubleattack == 3)
            {
                cooldownDoubleattack = 0;
                doubleattackBtn.gameObject.SetActive(true);
            }
        }

        if (!randomDestroyEnemyCardBtn.gameObject.activeSelf)
        {
            cooldownRandomDestroyEnemyCard++;
            if (cooldownRandomDestroyEnemyCard == 5)
            {
                cooldownRandomDestroyEnemyCard = 0;
                randomDestroyEnemyCardBtn.gameObject.SetActive(true);
            }
        }

        if (!randomOpenHideCardBtn.gameObject.activeSelf)
        {
            cooldownRandomOpenHideCard++;
            if (cooldownRandomOpenHideCard == 4)
            {
                cooldownRandomDestroyEnemyCard = 0;
                randomOpenHideCardBtn.gameObject.SetActive(true);
            }
        }
    }
}
