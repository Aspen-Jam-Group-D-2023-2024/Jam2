using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromTrigger : MonoBehaviour
{
    public string sceneToLoadName;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneToLoadName);
    }
}
