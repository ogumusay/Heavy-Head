using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Chapter
{
    Egypt = 2,
    Greece = 12,
    Space = 22
}

public class ChapterButton : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Chapter chapter;
    [SerializeField] GameObject lockImage;
    Button button;


    private void Start()
    {
        button = GetComponent<Button>();

        SaveObject saveObject = SaveSystem.Load();

        switch (chapter)
        {
            case Chapter.Egypt:
                button.interactable = true;
                break;
            case Chapter.Greece:
                button.interactable = saveObject.isGreeceUnlocked;
                break;
            case Chapter.Space:
                button.interactable = saveObject.isSpaceUnlocked;
                break;
            default:
                break;
        }

        if (button.interactable)
        {
            button.onClick.AddListener(() => SetSceneIndexAndLoadScene());
        }
        else
        {
            lockImage.SetActive(true);
        }
    }

    private void SetSceneIndexAndLoadScene()
    {
        int index = (int)chapter;
        sceneLoader.index = index;
        sceneLoader.LoadScene();
    }
}
