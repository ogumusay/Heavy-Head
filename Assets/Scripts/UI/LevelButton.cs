using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Text levelNumberText;
    Button button;
    Chapter chapter;
    int levelNumber = 0;


    private void Start()
    {
        levelNumber = transform.GetSiblingIndex() + 1;
        levelNumberText.text = levelNumber.ToString();
        button = GetComponent<Button>();

        SaveObject saveObject = SaveSystem.Load();

        chapter = (Chapter) SceneManager.GetActiveScene().buildIndex;

        switch (chapter)
        {
            case Chapter.Egypt:
                if (!saveObject.unlockedEgyptLevels.Contains(levelNumber))
                {
                    button.interactable = false;
                }
                break;
            case Chapter.Greece:
                if (!saveObject.unlockedGreeceLevels.Contains(levelNumber))
                {
                    button.interactable = false;
                }
                break;
            case Chapter.Space:
                if (!saveObject.unlockedSpaceLevels.Contains(levelNumber))
                {
                    button.interactable = false;
                }
                break;
            default:
                break;
        }

        if (button.interactable)
        {
            button.onClick.AddListener(() => FindIndexAndLoadLevel());
        }
    }

    private void FindIndexAndLoadLevel()
    {        
        int index = SceneManager.GetActiveScene().buildIndex + levelNumber;
        sceneLoader.index = index;
        sceneLoader.LoadScene();
    }
}
