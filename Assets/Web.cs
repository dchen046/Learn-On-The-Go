using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// This class will be used to establish the connection and have some functions to interact with database
public class Web : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetRequest("http://localhost/scripts/Auth.php"));
        // StartCoroutine(Register("test", "test"));

    }

    //    IEnumerator GetRequest(string uri)
    //    {
    //        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //        {
    //            // Request and wait for the desired page.
    //            yield return webRequest.SendWebRequest();

    //            // string[] pages = uri.Split('/');
    //            // int page = pages.Length - 1;

    //            string[] users = uri.Split('')

    //            if (webRequest.isNetworkError)
    //            {
    //                Debug.Log(pages[page] + ": Error: " + webRequest.error);
    //            }
    //            else
    //            {
    //                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //            }
    //        }
    //    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/scripts/Auth.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // get message
                string message = www.downloadHandler.text;
                Debug.Log(message);
                if (message.Equals("Login Success"))
                {
                    SceneManager.LoadScene("CourseScene");
                }
            }

        }
    }


    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/scripts/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // get message
                string message = www.downloadHandler.text;
                Debug.Log(message);
                if (message.Equals("Registration Success"))
                {
                    SceneManager.LoadScene("LoginScene");
                }
            }
        }
    }
}
