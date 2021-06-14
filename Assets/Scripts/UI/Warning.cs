using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField] GameObject warningObject;
    [SerializeField] Chapter chapter;
    GameObject warningText;
    GameObject collectedText;
    Dictionary<string, string> locale;
    public BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        warningText = warningObject.transform.Find("Not Finished").gameObject;
        collectedText = warningObject.transform.Find("Finished").gameObject;

        Localization localization = new Localization();
        locale = localization.GetLocale();

        collectedText.GetComponent<Text>().text = locale["finished_id"];
        Text text = warningText.GetComponent<Text>();

        switch (chapter)
        {
            case Chapter.Egypt:
                text.text = locale["egypt_not_finished_id"];
                break;
            case Chapter.Greece:
                text.text = locale["greece_not_finished_id"];
                break;
            case Chapter.Space:
                text.text = locale["space_not_finished_id"];
                break;
        }

    }

    void OnEnable()
    {
        CollectableHandler.onAllCollected += DisplayExitWarning;
    }

    private void OnDisable()
    {
        CollectableHandler.onAllCollected -= DisplayExitWarning;
    }

    private void DisplayExitWarning()
    {       
        collectedText.SetActive(true);
        warningObject.SetActive(true);

        boxCollider.enabled = false;

        StopAllCoroutines();
        StartCoroutine(RemoveWarning(5f));
    }

    public void DisplayCollectWarning()
    {
        collectedText.SetActive(false);
        warningText.SetActive(true);
        warningObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(RemoveWarning(2f));
    }

    private IEnumerator RemoveWarning(float sec)
    {
        yield return new WaitForSeconds(sec);
        warningText.SetActive(false);
        warningObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisplayCollectWarning();
    }
}
