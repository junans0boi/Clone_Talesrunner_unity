using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.EventSystems;

public class ChatManager : MonoBehaviourPun
{
    //InputChat
    public InputField inputChat;
    //ChatItem 공장
    public GameObject chatItemFactory;
    //ScorllView의 Content Transform
    public RectTransform trContent;
    

    // 내 아이디 색
    Color nickColor;
    
    void Start()
    {
        //InputField에서 엔터를 쳤을때 호출 되는 함수
        inputChat.onSubmit.AddListener(OnSubmit);
        //마우스 커서 비활성화
        Cursor.visible = false;
        //idColor를 랜덤으로 
        nickColor = new Color(
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f)
        );

    }

    void Update()
    {
        
        //만약 esc를 누르면 커서 활성화
        
    }

    //InputField에서 엔터를 쳤을때 호출되는 함수
    public void OnSubmit(string s)
    {
        
        //<color=#FFFFFF>닉네임</color>
        string chatText = "<color=#" + ColorUtility.ToHtmlStringRGB(nickColor) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s;
        
        photonView.RPC("RpcAddChat", RpcTarget.All, chatText);
        
        //4. InputChat의 내용 초기화
        inputChat.text = "";
        //5. InputChat에 focusing을 해주자
        inputChat.ActivateInputField();
    }

    void OnSubmitBtn()
    {
        //OnSubmit();
    }

    public RectTransform rtScrollView;
    private float preContentH;
    [PunRPC]
    void RpcAddChat(string chat)
    {
        // 이전 content의 h값을 저장하자
        preContentH = trContent.sizeDelta.y;
        //1. ChatItem을 만든다(부모를 Scorllview�� Content)
        GameObject item = Instantiate(chatItemFactory, trContent); // chatItemFactory를 trContent자식에 소환
        //2. 만든 ChatItem에서 ChatItem 컴포넌트를 가져온다
        ChatItem chatItem= item.GetComponent<ChatItem>();
        //3. 가져온 컴포넌트에 s를 셋팅
        chatItem.SetText(chat);
        StartCoroutine(AutoScrollBottom());

    }

    IEnumerator AutoScrollBottom()
    {
        yield return null;
        // 4. 이전에 바닥에 닿아 있었다면
        if (trContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        {
            // (Content y >= 변경되기전 Content H - 스크롤뷰H
            if (trContent.anchoredPosition.y >= preContentH - rtScrollView.sizeDelta.y)
            {
                // 추가 된 높이만큼 content y값을 변경하겠다
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - rtScrollView.sizeDelta.y);

            }
            
        }
    }
}