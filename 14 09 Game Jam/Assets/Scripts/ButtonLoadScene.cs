using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public string sceneToLoad;
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
