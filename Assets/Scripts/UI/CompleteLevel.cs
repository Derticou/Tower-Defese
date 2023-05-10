using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    private void Start()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }
    public SceneFader sceneFader;
    
    public void Continue()
    {

        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
