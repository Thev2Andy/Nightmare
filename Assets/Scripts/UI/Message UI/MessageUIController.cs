using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUIController : MonoBehaviour
{
    public Text HeaderText;
    public Text NotificationText;
    public Text PromptText;
    public Text SubtitleText;

    private float HeaderTimer;
    private float NotificationTimer;
    private float PromptTimer;
    private float SubtitleTimer;

    public static MessageUIController Instance;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void ShowHeader(string Message, float Duration)
    {
        HeaderText.gameObject.SetActive(true);
        HeaderText.text = Message;
        HeaderTimer = Duration;
    }
    
    public void ShowNotification(string Message, float Duration)
    {
        NotificationText.gameObject.SetActive(true);
        NotificationText.text = Message;
        NotificationTimer = Duration;
    }

    public void ShowPrompt(string Message, float Duration)
    {
        PromptText.gameObject.SetActive(true);
        PromptText.text = Message;
        PromptTimer = Duration;
    }

    public void ShowSubtitle(string Message, float Duration)
    {
        SubtitleText.gameObject.SetActive(true);
        SubtitleText.text = Message;
        SubtitleTimer = Duration;
    }

    private void Update()
    {
       HeaderTimer -= Time.deltaTime;
       if(HeaderTimer < 0) HeaderTimer = 0;
       if(HeaderTimer <= 0 || NotificationText.gameObject.activeInHierarchy)
       {
           HeaderText.text = "";
           HeaderText.gameObject.SetActive(false);
       }

       NotificationTimer -= Time.deltaTime;
       if(NotificationTimer < 0) NotificationTimer = 0;
       if(NotificationTimer <= 0 || HeaderText.gameObject.activeInHierarchy)
       {
           NotificationText.text = "";
           NotificationText.gameObject.SetActive(false);
       }

       PromptTimer -= Time.deltaTime;
       if(PromptTimer < 0) PromptTimer = 0;
       if(PromptTimer <= 0)
       {
           PromptText.text = "";
           PromptText.gameObject.SetActive(false);
       }

       SubtitleTimer -= Time.deltaTime;
       if(SubtitleTimer < 0) SubtitleTimer = 0;
       if(SubtitleTimer <= 0)
       {
           SubtitleText.text = "";
           SubtitleText.gameObject.SetActive(false);
       }
    }
}
