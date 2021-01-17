using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Pawn> spawnedPlayers = new List<Pawn>();
    public List<GameObject> enemies = new List<GameObject>();

    [Header("Number of player lives")]
    public GameObject player;
    public Stats playerStats;
    public static int PlayerLives;
    public int playerLives;
    public Text livesText;
    public UnityEngine.UI.Slider healthFill;

    [Header("HUD")]
    public HUD headsUpDisplay;

    [Header("Pause")]
    public bool isPaused;
    public GameObject PauseMenu;

    public float spawnPreference;
    public UnityEngine.UI.Slider prefSlider;
    [Header("Spawnpoints for player")]
    public List<GameObject> spawnpoints = new List<GameObject>();

    //Awake runs before all Starts
    private void Awake()
    {
        #region
        //Setup the Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        headsUpDisplay = FindObjectOfType<HUD>();
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerLives = playerLives;
        UpdateLivesUI();
        var pawnsInGame = FindObjectsOfType<Pawn>();
        foreach (Pawn pawn in pawnsInGame)
        {
            spawnedPlayers.Add(pawn);
        }
        LoadPreferences();
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        PauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DecreaseLives()
    {
        PlayerLives--;
        UpdateLivesUI();
    }
    public void UpdateLivesUI()
    {
        livesText.text = PlayerLives.ToString();
    }
    //Filling the health ui to match the players health
    public void HealthUIUpdate()
    {
        if (healthFill != null)
        {
            //fill amount is equal to current health
            healthFill.value = playerStats.currentHealth / playerStats.maxHealth;
        }
    }
    public static void SetPlayer(PlayerPawn pawn)
    {
        instance.player = pawn.gameObject;
        instance.playerStats = pawn.stats;
    }
    //Save prefs for spawning
    public void SavePreferences()
    {
        spawnPreference = prefSlider.value;
        PlayerPrefs.SetFloat("preference", spawnPreference);
        Debug.Log(spawnPreference);
    }
    //Load prefs for spawning
    public void LoadPreferences()
    {
        if (PlayerPrefs.HasKey("preference"))
        {
            spawnPreference = PlayerPrefs.GetFloat("preference");
            UpdateUI();
            Debug.Log(spawnPreference);
        }
        else Debug.Log("my shit is broke");
    }
    //Update Pref UI
    public void UpdateUI()
    {
        if(prefSlider != null)
        {
            prefSlider.value = spawnPreference;
        }
    }

}
