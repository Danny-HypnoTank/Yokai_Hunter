using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMY1TURN, ENEMY2TURN, ENEMY3TURN, ENEMY4TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    #region Variables and References for Players and Enemys

    [SerializeField]
    private BaseCard currentCard;
    //Player and Enemy Prefabs
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject[] enemy1Prefab;
    [SerializeField]
    private GameObject[] enemy2Prefab;
    [SerializeField]
    private GameObject[] enemy3Prefab;
    [SerializeField]
    private GameObject[] enemy4Prefab;


    //Player and Enemy Spawn Points
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private Transform enemy1SpawnPoint;
    [SerializeField]
    private Transform enemy2SpawnPoint;
    [SerializeField]
    private Transform enemy3SpawnPoint;
    [SerializeField]
    private Transform enemy4SpawnPoint;

    //References to player and enemy Data
    private Unit playerUnit;
    private Unit enemy1Unit;
    private Unit enemy2Unit;
    private Unit enemy3Unit;
    private Unit enemy4Unit;

    [SerializeField]
    private BattleHUD playerHUD;
    [SerializeField]
    private BattleHUD enemy1HUD;
    [SerializeField]
    private BattleHUD enemy2HUD;
    [SerializeField]
    private BattleHUD enemy3HUD;
    [SerializeField]
    private BattleHUD enemy4HUD;

    [SerializeField]
    private GameObject enemy1TargetingUI;
    [SerializeField]
    private GameObject enemy2TargetingUI;
    [SerializeField]
    private GameObject enemy3TargetingUI;
    [SerializeField]
    private GameObject enemy4TargetingUI;

    [SerializeField]
    private GameObject playerFocusTargetingUI;
    [SerializeField]
    private GameObject endTurnButton;

    [SerializeField]
    private GameObject[] playerCards;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private GameObject playerAbilityUI;
    [SerializeField]
    private GameObject playerHealTargetingUI;
    [SerializeField]
    private GameObject playerDefendTargetingUI;
    
    public BattleState state;

    [SerializeField]
    private CardManager cardManager;

    [SerializeField]
    private GameObject loadingUI;
    [SerializeField]
    private LoadingToBoss loadingFromScenario;

    [SerializeField]
    private GameObject loseMenu;
    [SerializeField]
    private GameObject winMenu;

    #endregion

    #region Properties - Get/Set
    public GameObject PlayerPrefab { get => playerPrefab; set => playerPrefab = value; }
    public GameObject[] Enemy1Prefab { get => enemy1Prefab; set => enemy1Prefab = value; }
    public GameObject[] Enemy2Prefab { get => enemy2Prefab; set => enemy2Prefab = value; }
    public GameObject[] Enemy3Prefab { get => enemy3Prefab; set => enemy3Prefab = value; }
    public GameObject[] Enemy4Prefab { get => enemy4Prefab; set => enemy4Prefab = value; }

    public Transform PlayerSpawnPoint { get => playerSpawnPoint; set => playerSpawnPoint = value; }
    public Transform Enemy1SpawnPoint { get => enemy1SpawnPoint; set => enemy1SpawnPoint = value; }
    public Transform Enemy2SpawnPoint { get => enemy2SpawnPoint; set => enemy2SpawnPoint = value; }
    public Transform Enemy3SpawnPoint { get => enemy3SpawnPoint; set => enemy3SpawnPoint = value; }
    public Transform Enemy4SpawnPoint { get => enemy4SpawnPoint; set => enemy4SpawnPoint = value; }

    public Unit PlayerUnit { get => playerUnit; set => playerUnit = value; }
    public Unit Enemy1Unit { get => enemy1Unit; set => enemy1Unit = value; }
    public Unit Enemy2Unit { get => enemy2Unit; set => enemy2Unit = value; }
    public Unit Enemy3Unit { get => enemy3Unit; set => enemy3Unit = value; }
    public Unit Enemy4Unit { get => enemy4Unit; set => enemy4Unit = value; }

    public BattleHUD PlayerHUD { get => playerHUD; set => playerHUD = value; }
    public BattleHUD Enemy1HUD { get => enemy1HUD; set => enemy1HUD = value; }
    public BattleHUD Enemy2HUD { get => enemy2HUD; set => enemy2HUD = value; }
    public BattleHUD Enemy3HUD { get => enemy3HUD; set => enemy3HUD = value; }
    public BattleHUD Enemy4HUD { get => enemy4HUD; set => enemy4HUD = value; }

    public GameObject Enemy1TargetingUI { get => enemy1TargetingUI; set => enemy1TargetingUI = value; }
    public GameObject Enemy2TargetingUI { get => enemy2TargetingUI; set => enemy2TargetingUI = value; }
    public GameObject Enemy3TargetingUI { get => enemy3TargetingUI; set => enemy3TargetingUI = value; }
    public GameObject Enemy4TargetingUI { get => enemy4TargetingUI; set => enemy4TargetingUI = value; }

    public GameObject PlayerAbilityUI { get => playerAbilityUI; set => playerAbilityUI = value; }
    public GameObject PlayerHealTargetingUI { get => playerHealTargetingUI; set => playerHealTargetingUI = value; }
    public GameObject PlayerDefendTargetingUI { get => playerDefendTargetingUI; set => playerDefendTargetingUI = value; }
    public GameObject PlayerFocusTargetingUI { get => playerFocusTargetingUI; set => playerFocusTargetingUI = value; }

    public GameObject EndTurnButton { get => endTurnButton; set => endTurnButton = value; }
    public Text DialogueText { get => dialogueText; set => dialogueText = value; }

    public GameObject[] PlayerCards { get => playerCards; set => playerCards = value; }

    public BaseCard CurrentCard { get => currentCard; set => currentCard = value; }
    public CardManager CardManager { get => cardManager; set => cardManager = value; }

    public GameObject LoadingUI { get => loadingUI; set => loadingUI = value; }
    public LoadingToBoss LoadingFromScenario { get => loadingFromScenario; set => loadingFromScenario = value; }
    public GameObject LoseMenu { get => loseMenu; set => loseMenu = value; }
    public GameObject WinMenu { get => winMenu; set => winMenu = value; }
    #endregion
}
