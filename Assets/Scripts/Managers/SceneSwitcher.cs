using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        SubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.ChangeSceneAction += SwitchScene;
    }

    private void SwitchScene(int level)
    {
        Application.LoadLevel(level);
    }
}
