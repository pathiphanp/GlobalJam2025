using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutScenes : MonoBehaviour
{
    [SerializeField] GameObject[] cutScenes;
    [SerializeField] float durationChecgeCutScenes;
    [SerializeField] Button btnSkipCutScenes;
    Coroutine callCutScenes;
    [SerializeField] GameObject gamePlayScenes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnSkipCutScenes.onClick.AddListener(OnClickSkipCutScenes);
        callCutScenes = StartCoroutine(PlayCutScenes());
    }

    private void OnClickSkipCutScenes()
    {

        StopCoroutine(callCutScenes);
        StartGame();
    }

    IEnumerator PlayCutScenes()
    {
        for (int i = 0; i < cutScenes.Length; i++)
        {
            cutScenes[i].SetActive(true);
            yield return new WaitForSeconds(durationChecgeCutScenes);
            yield return true;
        }
        StartGame();
    }

    void StartGame()
    {
        foreach (GameObject ctS in cutScenes)
        {
            ctS.SetActive(false);
        }
        btnSkipCutScenes.gameObject.SetActive(false);
        gamePlayScenes.SetActive(true);
    }
}
