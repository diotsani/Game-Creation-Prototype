using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : MonoBehaviour
{
    public Button updateStatus;
    public TMP_Text statusText;
    public List<TMP_Text> statusTexts;
    public int amountStatus = 5;
    
    public int number;
    private string status = "Status";

    private void OnEnable()
    {
        PlayerStatusData.OnStatusChange += UpdateStatus;
    }

    private void OnDisable()
    {
        PlayerStatusData.OnStatusChange -= UpdateStatus;
    }

    public void Start()
    {
        //updateStatus.onClick.AddListener(()=>UpdateStatus(status,number));
        Init();
    }

    private void Update()
    {
        
    }

    void Init()
    {
        for (int i = 0; i < amountStatus; i++)
        {
            var text = Instantiate(statusText, transform);
            text.gameObject.SetActive(false);
            statusTexts.Add(text);
        }
    }

    public void UpdateStatus(string name,int value)
    {
        TMP_Text text = GetStatusText();
        if(text != null)
        {
            text.gameObject.SetActive(true);
            text.text = $"{name} {value.ToString("+#;-#;0")}";
            text.transform.SetAsLastSibling();
            StartCoroutine(ShowStatus(text));
        }
        // statusText.text = $"{name} {value.ToString("+#;-#;0")}";
        // statusText.transform.SetAsLastSibling();
        // StartCoroutine(ShowStatus());
    }
    public IEnumerator ShowStatus(TMP_Text text)
    {
        yield return new WaitForSeconds(1);
        text.gameObject.SetActive(false);
    }
    private TMP_Text GetStatusText()
    {
        foreach (var text in statusTexts)
        {
            if (!text.gameObject.activeInHierarchy)
            {
                return text;
            }
        }
        return null;
    }
}
