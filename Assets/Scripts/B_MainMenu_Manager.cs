using UnityEngine;
using UnityEngine.SceneManagement;

public class B_MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private B_GameData gameData;
    [SerializeField] private int easyMoney;
    [SerializeField] private int mediumMoney;
    [SerializeField] private int hardMoney;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetDifficulty(int difficulty)
    {
        if (difficulty == 0) { gameData.currentMoney = easyMoney; }
        else if (difficulty == 1) { gameData.currentMoney = mediumMoney; }
        else if (difficulty == 2) { gameData.currentMoney = hardMoney; }
        Play();
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }
}
