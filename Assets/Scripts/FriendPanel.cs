using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanel : MonoBehaviour
{
    // Ссылки на кнопки и текст
    [SerializeField]
    Button buttonInvite, buttonDelete;
    [SerializeField]
    Text textName;

    void Start()
    {
        //Вызывает эти методы, когда нажимаются соответствующие кнопки
        buttonInvite.onClick.AddListener(OnClickButtonInvite);
        buttonDelete.onClick.AddListener(OnClickButtonDelete);
    }

    //Друзей можно удалить из списка друзей или отправить приглашение(сообщение в лог) (скрин 3).
    void OnClickButtonInvite()
    {
        Debug.Log("OnClickButtonInvite - "+ textName.text);
    }

    void OnClickButtonDelete()
    {
        //Debug.Log("OnClickButtonDelete");
        FindObjectOfType<FriendList>().DeleteFriend(textName.text);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

    }
}
