using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public string GameSceneName;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
