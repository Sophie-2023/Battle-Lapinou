using UnityEngine;
using UnityEngine.SceneManagement;

public class B_MainMenu_Manager : MonoBehaviour
{
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
        B_GameData gameData = B_GameData.Instance;
        if (difficulty == 0) { gameData.currentMoney = easyMoney; gameData.currentDifficulty = 0; gameData.initialMoney = easyMoney; }
        else if (difficulty == 1) { gameData.currentMoney = mediumMoney; gameData.currentDifficulty = 1; gameData.initialMoney = mediumMoney; }
        else if (difficulty == 2) { gameData.currentMoney = hardMoney; gameData.currentDifficulty = 2; gameData.initialMoney = hardMoney; }
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ChooseLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }

}
