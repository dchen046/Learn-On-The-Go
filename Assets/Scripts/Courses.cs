﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Courses : MonoBehaviour
{
    public Button biochemButton;
    public Button LogoutButton;
    // Start is called before the first frame update
    void Start()
    {
        biochemButton.onClick.AddListener(() =>
        {
            print("biochem pressed");
            SceneManager.LoadScene("TopicScene");
        });

        LogoutButton.onClick.AddListener(() =>
        {
            print("logout pressed");
            SceneManager.LoadScene("LoginScene");
        });


    }

}
