using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// This class will be used to establish the connection and have some functions to interact with database
public class Web : MonoBehaviour
{

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
