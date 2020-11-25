using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button regButton;
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        setRegisterButton();
        setBackButton();
    }

    private void setRegisterButton()
    {
        regButton.onClick.AddListener(() =>
        {

            /**
             * Make sure both inputs are there
             * Then check if username is already registered 
             * If its not then register the person and message
             * Else error message popup
             */
            StartCoroutine(Main.Instance.Web.Register(usernameInput.text, passwordInput.text));
            print("register pressed");
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
            SceneManager.LoadScene("LoginScene");
        });
    }
}
