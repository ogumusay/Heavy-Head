using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Animator animator;
    public int index = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync(0));
    }

    public void LoadNextScene()
    {
        index = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings - 1 >= index)
        {
            StartCoroutine(LoadSceneAsync(index));
        }
        else
        {
            StartCoroutine(LoadSceneAsync(0));
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAsync(index));
    }

    public IEnumerator LoadSceneAsync(int index)
    {
        animator.SetTrigger("Fade Out");

        yield return new WaitForSeconds(1f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
