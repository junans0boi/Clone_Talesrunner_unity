using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
    //Text 
    Text chatText;
    //RectTransform
    RectTransform rt;
    //preferredHeight
    float preferredH;
    void Awake()
    {
        chatText = GetComponent<Text>();
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // preferredH가 변경이 되면
        if(preferredH != chatText.preferredHeight)
        {
            //chatText.text의 크기에 맞게 ContetSize를 변경
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);

            preferredH = chatText.preferredHeight;
        }
    }

    //Text 셋팅, Text내용의 크기에 맞기 ContetSize를 변경
    public void SetText(string s)
    {
        chatText.text = s;
        
    }
}