using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LoadDelay = 2.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LoadDelay);
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = SceneIndex + 1;

        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        FindObjectOfType<LevelPersistent>().ResetLevelPersist();
        SceneManager.LoadScene(NextSceneIndex);
    }
}
