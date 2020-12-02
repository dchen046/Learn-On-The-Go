using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button regButton;
    // Start is called before the first frame update
    void Start()
    {
        setLoginButton();
        setRegisterButton();
    }
    
    private void setLoginButton()
    {
        loginButton.onClick.AddListener(() =>
        {
            print("login pressed");
            StartCoroutine(Main.Instance.Web.Login(usernameInput.text, passwordInput.text));
        });
    }

    private void setRegisterButton()
    {
        regButton.onClick.AddListener(() =>
        {
            print("register pressed");
            SceneManager.LoadScene("RegisterScene");
        });
    }

}
