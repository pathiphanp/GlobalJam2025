using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextEffectBattle : MonoBehaviour
{
    [SerializeField] GameObject showDescription;
    [SerializeField] public TMP_Text desriptionText;
    int charC = 0;
    [SerializeField] float delayRead;
    Coroutine startTextEffect;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.textEffectBattle = this;
        RestRead();
    }
    public void CallReadText(string _description)
    {
        RestRead();
        if (startTextEffect != null)
        {
            StopCoroutine(startTextEffect);
            startTextEffect = StartCoroutine(ReadText(_description));
        }
        else
        {
            startTextEffect = StartCoroutine(ReadText(_description));
        }
    }
    public IEnumerator ReadText(string suptitle)
    {
        RestRead();
        yield return new WaitForSeconds(0.5f);
        showDescription.SetActive(true);
        while (charC < suptitle.Length)
        {
            yield return new WaitForSeconds(delayRead);
            charC++;
            string text = suptitle.Substring(0, charC);
            desriptionText.text = text;
        }
        yield return new WaitForSeconds(10f);
        showDescription.SetActive(false);
        // Debug.Log("Description");
    }
    public void RestRead()
    {
        desriptionText.text = "";
        charC = 0;
    }

}
