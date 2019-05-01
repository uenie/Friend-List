using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class FriendList : MonoBehaviour
{
    // InputField
    [SerializeField]
    InputField m_InputField;
    bool m_InputFieldIsFocusedOld = false;

    // Список друзей (Content)
    [SerializeField]
    RectTransform userListContent;

    //Список пользователей подтягиваем из файла, список друзей по усмотрению(отдельный файл либо добавлять маркер в файле со списком пользователей).
    // Список пользователей и друзей (файлы и List-ы)
    // Parse File "Users.txt"
    static string usersPath = "#Text\\Users.txt";
    static string textUserList = File.ReadAllText(usersPath);
    static char[] separators = { '\n' };
    List<string> users = textUserList.Split(separators).ToList();
    // Parse File "Friends.txt"
    static string friendsPath = "#Text\\Friends.txt";
    static string textFriendList = File.ReadAllText(friendsPath);
    List<string> friends = textFriendList.Split(separators).ToList();

    // Список текущих пользователей в списке
    List<GameObject> currentUsers = new List<GameObject>();

    // Префабы элементов списка пользователей
    [SerializeField]
    UserPanel userPanel;
    [SerializeField]
    FriendPanel friendPanel;
    [SerializeField]
    float verticalOffset = 1.0f;

    void OnGUI()
    {
        // По нажатию на inputfield, отображать список друзей
        if (!m_InputFieldIsFocusedOld && m_InputField.isFocused)
        {
            ValueChangeProcess();
        }

        // Установить прежнее значение фокусировки на InputField
        m_InputFieldIsFocusedOld = m_InputField.isFocused;
    }

    void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        m_InputField.onValueChanged.AddListener(delegate { ValueChangeProcess(); });
    }

    void Update()
    {

    }

    // Очистка списка
    void ClearList()
    {

        for (int i = 0; i < currentUsers.Count; i++)
        {
            Destroy(currentUsers[i]);
        }
        currentUsers.Clear();

    }

    // Отображение пользователей списка
    void ShowUsers()
    {
        for (int i = 0; i < currentUsers.Count; i++)
        {
            // Расположить панель 
            RectTransform userNameRectTransform = currentUsers[i].GetComponent<RectTransform>();
            userNameRectTransform.anchoredPosition = new Vector2(userNameRectTransform.rect.width / 2,
                (-userNameRectTransform.rect.height / 2) + (-userNameRectTransform.rect.height * i));
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeProcess()
    {
        // Очистка списка
        ClearList();

        // Если ничего не введено, отобразить только список друзей
        if (m_InputField.text == "")
        {
            for (int i = 0; i < friends.Count; i++)
            {
                // Добавить друга в список
                if (friends[i].Trim() != "")
                    AddUserToList(Instantiate(friendPanel.gameObject, userListContent), friends[i]);
            }
        }
        else
        if (m_InputField.text != "")
        {
            //   отобразить фильтрованный список друзей и пользователей

            //При вводе текста в данный inputfield
            //(фильтр и поиск осуществлять по совпадению имени.
            //Например, при вводе “Pla” отфильтровывать и отображать только пользователей,
            //имена которых начинаются на “Pla” -> Player1, Player2…) (скрин 2).
            //1 - фильтруем список друзей,
            for (int i = 0; i < friends.Count; i++)
            {
                if (friends[i].StartsWith(m_InputField.text, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Добавить друга в список
                    if (friends[i].Trim() != "")
                        AddUserToList(Instantiate(friendPanel.gameObject, userListContent), friends[i]);
                }
            }
            //2 - осуществляем поиск подходящих пользователей
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].StartsWith(m_InputField.text, StringComparison.CurrentCultureIgnoreCase) && !friends.Contains(users[i]))
                {
                    // Добавить пользователя в список
                    if (users[i].Trim() != "")
                        AddUserToList(Instantiate(userPanel.gameObject, userListContent), users[i]);
                }
            }
        }

        //Отобразить пользователей
        ShowUsers();
    }

    //Добавление пользователя в список
    void AddUserToList(GameObject userGO, string name)
    {
        // Задать имя
        userGO.GetComponentInChildren<Text>().text = name;

        // Добавить друга в список текущих пользователей
        currentUsers.Add(userGO);
    }

    // Удаление друга
    public void DeleteFriend(string name)
    {
        // Найти и удалить
        friends.Remove(name);

        // Изменить файл списка друзей
        File.WriteAllLines(friendsPath, friends);

        //Обновление
        ValueChangeProcess();
    }

    // Добавление друга
    public void AddFriend(string name)
    {
        // Проверить на наличие и добавить
        if (!friends.Contains(name))
        {
            friends.Add(name);
        }

        // Изменить файл списка друзей
        File.WriteAllLines(friendsPath, friends);

        //Обновление
        ValueChangeProcess();
    }
}
