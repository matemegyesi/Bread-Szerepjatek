using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public static TextController Instance;

    public TextMeshProUGUI tmp = new TextMeshProUGUI();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    IEnumerator WriteText(TextMeshProUGUI tmp, string text)
    {
        string result = "";
        foreach (char c in text)
        {
            result += c;
            tmp.text= result;

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnTextButton()
    {
        StartCoroutine(WriteText(tmp, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer tookt"));
    }
}
