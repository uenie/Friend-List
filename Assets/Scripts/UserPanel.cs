using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    // Ссылки на кнопки и текст
    [SerializeField]
    Button buttonAdd;
    [SerializeField]
    Text textName;

    void Start()
    {
        //Вызывает эти методы, когда нажимаются соответствующие кнопки
        buttonAdd.onClick.AddListener(OnClickButtonAdd);
    }

    //Пользователя можно добавить в друзья(скрин 4).
    void OnClickButtonAdd()
    {
        //Debug.Log("OnClickButtonAdd");
        FindObjectOfType<FriendList>().AddFriend(textName.text);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

    }
}
