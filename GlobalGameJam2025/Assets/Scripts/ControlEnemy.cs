using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEnemy : MonoBehaviour
{
    Animator anim;

    public int hp;
    public int pointWin;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text pointWinText;

    public bool isDoubleDamageTake = false;

    int countTurn;
    private void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.controlEnemy = this;
    }
    void Opening()
    {
        GameManager.instance.textEffectBattle.CallReadText
        ("Hello, little mermaid. Lift the damn Legendary Bubble and hand it over to me, now!");
    }
    void StartGame()
    {
        GameManager.instance.controlCard.CallStartGame();
    }
    public void StarEnemyAction()
    {
        int enemyPoint = GameManager.instance.controlCard.pointEnemy;
        List<CardScriptableObjectScript> allPointHave = new List<CardScriptableObjectScript>();
        List<int> savePoint = new List<int>();
        foreach (CardScriptableObjectScript allcard in GameManager.instance.controlCard.dataCard)
        {
            allPointHave.Add(allcard);
        }
        //basic
        for (int i = 0; i < allPointHave.Count; i++)
        {
            int nowPoint = enemyPoint;
            if ((nowPoint += allPointHave[i].data.poin) <= 21)
            {
                savePoint.Add(allPointHave[i].data.poin);
                Debug.Log("Save Point : " + allPointHave[i].data.poin);
            }
        }
        float percenDraw = (float)savePoint.Count / allPointHave.Count * 100;
        Debug.Log("percenDraw : " + percenDraw);

        if (percenDraw > 65)
        {
            Debug.Log("Enemy Draw");
            GameManager.instance.controlCard.EnemyDrawCard();
        }
        else
        {
            Debug.Log("Enemy Stand");
            GameManager.instance.controlCard.EnemyStand();
        }
    }

    public void Takedamage(int damage)
    {
        if (isDoubleDamageTake)
        {
            isDoubleDamageTake = false;
            damage *= 2;
        }
        hp -= damage;
        hpText.text = "Oxygen = " + hp.ToString();
        if (isDoubleDamageTake)
        {
            isDoubleDamageTake = false;
            pointWin -= 10;
        }
        else
        {
            pointWin -= 5;
        }
        if (pointWin <= 0)
        {
            pointWin = 0;
        }
        pointWinText.text = "Bubble = " + pointWin.ToString();
        if (hp <= 0)
        {
            //Game Over
            GameManager.instance.controlCard.backjack.SetActive(false);
            GameManager.instance.textEffectBattle.
            CallReadText("Wait! Hold on! We can negotiate, can't we? I have treasures—so many treasures to offer you, little mermaid—WAIT! AARGHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH!");
            Invoke("GameOver", 15);
        }
    }
    void GameOver()
    {
        SceneManager.LoadScene("PlayerWin");
    }
    public void AddPointBubble()
    {
        int rejenpoint = 0;
        if (UseBet() == 15)
        {
            GameManager.instance.textEffectBattle.CallReadText("It's not that easy to take me down.");
        }
        if (hp < 100)
        {
            if (isDoubleDamageTake)
            {
                isDoubleDamageTake = false;
                rejenpoint = UseBet() * 2;
            }
            else
            {
                rejenpoint = UseBet();
            }
            hp += rejenpoint;
        }
        hpText.text = "Oxygen = " + hp.ToString();
        if (isDoubleDamageTake)
        {
            isDoubleDamageTake = false;
            pointWin += 10;
        }
        else
        {
            pointWin += 5;
        }
        pointWinText.text = "Bubble = " + pointWin.ToString();
        if (pointWin == 50)
        {
            GameManager.instance.textEffectBattle.CallReadText("Just a little longer, and everything shall be mine!");
        }
        if (pointWin == 100)
        {
            GameManager.instance.controlCard.backjack.SetActive(false);
            GameManager.instance.textEffectBattle.CallReadText("HAHAHAHA! The power of life... it's overflowing! Magnificent! With this, I’ll have everything I’ve ever desired!");
            Invoke("EnemyWin", 15);
        }
    }
    void EnemyWin()
    {
        SceneManager.LoadScene("EnemyWin");
    }
    public int UseBet()
    {
        if (hp > 80)
        {
            return 5;
        }
        else if (hp < 80 && hp > 50)
        {
            return 10;
        }
        else
        {
            return 15;
        }
    }
}
