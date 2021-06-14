using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject levelFailedUI;
    [SerializeField] GameObject levelCompleteUI;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject winText;

    private void Start()
    {
        Text text = winText.GetComponent<Text>();
        SystemLanguage systemLanguage = Application.systemLanguage;

        switch (systemLanguage)
        {
            case SystemLanguage.Turkish:
                text.text = "TAMAMLANDI!";
                break;
            case SystemLanguage.Spanish:
                text.text = "TERMINADO!";
                break;
            case SystemLanguage.German:
                text.text = "ABGESCHLOSSEN!";
                break;
            case SystemLanguage.French:
                text.text = "TERMINÉ!";
                break;
            default:
                text.text = "COMPLETED!";
                break;

        }
    }

    private void OnEnable()
    {
        //PlayerController.onDie += SetLevelFailedUIActive;
        ExitLevel.onPassThroughTheDoor += SetLevelCompleteUIActive;
        ExitLevel.onPassThroughTheDoor += UnlockNextLevel;

    }

    private void OnDisable()
    {
        //PlayerController.onDie -= SetLevelFailedUIActive;
        ExitLevel.onPassThroughTheDoor -= SetLevelCompleteUIActive;
        ExitLevel.onPassThroughTheDoor -= UnlockNextLevel;
    }

    private void SetLevelFailedUIActive()
    {
        levelFailedUI.SetActive(true);
    }

    private void SetLevelCompleteUIActive()
    {
        levelCompleteUI.SetActive(true);
        UI.SetActive(false);
    }

    private void UnlockNextLevel()
    {
        SaveObject saveObject = SaveSystem.Load();

        int index = SceneManager.GetActiveScene().buildIndex;
        int levelNumber;

        if (index < (int) Chapter.Greece)
        {
            levelNumber = index - (int) Chapter.Egypt;

            if (index + 1 == (int) Chapter.Greece)
            {
                saveObject.isGreeceUnlocked = true;
            }
            else
            {
                if (!saveObject.unlockedEgyptLevels.Contains(levelNumber + 1))
                {
                    saveObject.unlockedEgyptLevels.Add(levelNumber + 1);            

                }
            }
        }
        else if ((int) Chapter.Greece < index && index < (int) Chapter.Space)
        {            
            levelNumber = index - (int)Chapter.Greece;

            if (index + 1 == (int) Chapter.Space)
            {
                saveObject.isSpaceUnlocked = true;
            }
            else
            {
                if (!saveObject.unlockedGreeceLevels.Contains(levelNumber + 1))
                {
                    saveObject.unlockedGreeceLevels.Add(levelNumber + 1);
                }
            }
        }
        else
        {
            levelNumber = index - (int)Chapter.Space;

            if (!saveObject.unlockedSpaceLevels.Contains(levelNumber + 1))
            {
                saveObject.unlockedSpaceLevels.Add(levelNumber + 1);
            }
        }

        SaveSystem.Save(saveObject);
    }
}
