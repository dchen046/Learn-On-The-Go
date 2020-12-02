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
        setTopicButtons();
        
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
            print("back pressed");
            SceneManager.LoadScene("CourseScene");
        });
    }

    private void setTopicButtons()
    {
        topic1.onClick.AddListener(() =>
        {
            // TODO
            print("topic 1 pressed");
            SceneManager.LoadScene("FillInBlank2");
        });

        topic2.onClick.AddListener(() =>
        {
            // TODO
            print("topic 2 pressed");
            SceneManager.LoadScene("snake1");
        });


        // TODO set up topic 3
        
    }
}
