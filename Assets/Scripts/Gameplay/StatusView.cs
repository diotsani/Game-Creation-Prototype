using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    public TMP_Text statusText;
    public TMP_Text stressText;
    public TMP_Text healthText;
    public TMP_Text foodText;
    public int number;
    private string status = "Status";
    public void Start()
    {
        UpdateStatus(status, number);
    }

    private void Update()
    {
        
    }

    public void UpdateStatus(string name,int value)
    {
        statusText.text = $"{name} {value.ToString("+#;-#;0")}";
        statusText.transform.SetAsLastSibling();
        StartCoroutine(ShowStatus());
    }
    public IEnumerator ShowStatus()
    {
        yield return new WaitForSeconds(1);
        statusText.gameObject.SetActive(false);
    }
}
