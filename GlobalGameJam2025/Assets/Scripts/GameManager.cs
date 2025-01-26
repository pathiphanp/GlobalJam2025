using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public CameraManager cameraManager;
    [HideInInspector] public TextEffectBattle textEffectBattle;
    [HideInInspector] public ControlCard controlCard;
    [HideInInspector] public ControlEnemy controlEnemy;
    [HideInInspector] public SkillControl skillControl;

    [SerializeField] public GameObject chooseTargetText;

    [Header("BtnQusetion")]
    public GameObject btnQusetion;
    public Button btnWhatHappened;
    public Button btnWhatDidYouDoBeforeTheIncident;
    public Button btnWhatAreyouDoingOnThisBoat;
    public Button bynHowIsYourRelationshipWithEveryone;

    [Header("Humman")]
    public HummanDescription hummanDescription;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        btnWhatHappened.onClick.AddListener(OnClickShowWhatHappenedDescription);
        btnWhatDidYouDoBeforeTheIncident.onClick.AddListener(OnClickShowWhatDidYouDoBeforeTheIncident);
        btnWhatAreyouDoingOnThisBoat.onClick.AddListener(OnClickShowWhatAreyouDoingOnThisBoat);
        bynHowIsYourRelationshipWithEveryone.onClick.AddListener(OnClickShowHowIsYourRelationshipWithEveryone);
    }
    private void OnClickShowWhatHappenedDescription()
    {
        SetBtnQusetion(false);
        textEffectBattle.CallReadText(hummanDescription.description[0].description.description);
    }
    private void OnClickShowWhatDidYouDoBeforeTheIncident()
    {

    }
    private void OnClickShowWhatAreyouDoingOnThisBoat()
    {

    }
    private void OnClickShowHowIsYourRelationshipWithEveryone()
    {

    }

    public void SetBtnQusetion(bool _SetInput)
    {
        btnQusetion.SetActive(_SetInput);
    }
}
