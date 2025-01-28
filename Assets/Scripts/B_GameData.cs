using UnityEngine;

public class B_GameData : MonoBehaviour
{

    public static B_GameData Instance;

    public int currentMoney;
    public int currentDifficulty;
    public int initialMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
