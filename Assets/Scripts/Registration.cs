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
            print("register pressed");
            StartCoroutine(Main.Instance.Web.Register(usernameInput.text, passwordInput.text));
        });
    }

    private void setBackButton()
    {
        backButton.onClick.AddListener(() =>
        {
            print("back pressed");
            SceneManager.LoadScene("LoginScene");
        });
    }
}
