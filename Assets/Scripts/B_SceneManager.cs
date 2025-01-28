using UnityEngine;
using UnityEngine.SceneManagement;

public class B_SceneManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        B_GameData.Instance.currentMoney = B_GameData.Instance.initialMoney;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChooseLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

}
