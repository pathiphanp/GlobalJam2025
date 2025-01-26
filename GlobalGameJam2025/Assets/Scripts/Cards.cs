using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [SerializeField] public CardScriptableObjectScript dataCard;
    SpriteRenderer sprite;
    [SerializeField] Sprite backCard;
    [HideInInspector] public bool showCard;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        SetUpCard();
    }
    public void SetUpCard()
    {
        if (dataCard.data.cardSprite != null)
        {
            if (showCard)
            {
                sprite.sprite = dataCard.data.cardSprite;
            }
            else
            {
                sprite.sprite = backCard;
            }
        }
    }

    public void OpenShowCard()
    {
        sprite.sprite = dataCard.data.cardSprite;
    }
}
