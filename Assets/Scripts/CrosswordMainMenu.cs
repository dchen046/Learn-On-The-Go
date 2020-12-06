using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class CrosswordMainMenu : MonoBehaviour
{
    void Start()
    {  
    }
    public void btnTest(Button btn) {
        if(btn.name == "BioChemistry"){
            SceneManager.LoadScene("Crossword");
        }
        else if(btn.name == "Biology"){
            SceneManager.LoadScene("Crossword_1");
        }
        else if(btn.name == "Mainmenu") {
            SceneManager.LoadScene("TopicScene");
        }
    }
    void Update()
    {
    }
}
