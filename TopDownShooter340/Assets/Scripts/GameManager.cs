using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Pawn> spawnedPlayers;

    [Header("Number of player lives")]
    public int playerLives;
    
    [Header("HUD")]
    public HUD headsUpDisplay;

    [Header("Pause")]
    public bool isPaused;
    public GameObject PauseMenu;

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
        var pawnsInGame = FindObjectsOfType<Pawn>();
        foreach (Pawn pawn in pawnsInGame)
        {
            spawnedPlayers.Add(pawn);
        }
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
}
