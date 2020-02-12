using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private readonly PlayerController player;
    [SerializeField, Range(0f, 100f), Tooltip("Current health")] public int currentHealth;
    [SerializeField, Range(0f, 100f), Tooltip("Current max health")] private int maxHealth;


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
        currentHealth = maxHealth;
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
