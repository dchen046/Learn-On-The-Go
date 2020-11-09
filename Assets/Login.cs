using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button regButton;
    // Start is called before the first frame update
    void Start()
    {
       loginButton.onClick.AddListener(() => 
       {
            /**
             * When clicked make sure both inputs are there
             * Then check the database to see if it exists
             * If exists then go to next scene
             * else error message
             */
            print("login pressed");
       });

       regButton.onClick.AddListener(() =>
       {
            /**
             * Make sure both inputs are there
             * Then check if username is already registered 
             * If its not then register the person and message
             * Else error message popup
             */
            print("register pressed");
       });
    }
    

}
