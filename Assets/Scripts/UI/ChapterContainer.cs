using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterContainer : MonoBehaviour
{

    RectTransform rectTransform;
    List<RectTransform> childTransforms;
    int screenWidth;
    int screenHeight;
    float spaceBetweenChilds;
    [SerializeField] float widthRatio = 2.4f;
    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        spaceBetweenChilds = screenWidth / widthRatio + screenWidth / 10;

        int index = 0;

        foreach (RectTransform child in transform)
        {
            child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenWidth / widthRatio);

            float xPos = (screenWidth / 2) + (index * spaceBetweenChilds);

            child.localPosition = new Vector2(xPos, child.transform.localPosition.y);

            index++;
        }

        float containerWidth = (screenWidth / 2) + (((screenWidth / widthRatio) + (screenWidth / 10)) * (index));

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, containerWidth);

        transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, screenHeight / 10);
    }
}
