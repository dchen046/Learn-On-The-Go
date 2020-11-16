using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Topics : MonoBehaviour
{
    public Button topic1;
    public Button topic2;
    public Button topic3;
    public Button LogoutButton;
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {

        setLogoutButton();
        setBackButton();
        
    }

    private void setLogoutButton()
    {
        LogoutButton.onClick.AddListener(() =>
        {
            // TODO
            print("logout pressed");
            SceneManager.LoadScene("LoginScene");
        });
    }

    private void setBackButton()
    {
        backButton.onClick.AddListener(() =>
        {

            /**
             * Make sure both inputs are there
             * Then check if username is already registered 
             * If its not then register the person and message
             * Else error message popup
             */
            print("back pressed");
            SceneManager.LoadScene("CourseScene");
        });
    }

    private void setTopicButtons()
    {

    }
}
