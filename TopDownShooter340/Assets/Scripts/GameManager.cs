using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Pawn> spawnedPlayers;

    public HUD headsUpDisplay;

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
}
