using System;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// throw this on the player
public class ResetSceneOnKeypress : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.T;
    private string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
