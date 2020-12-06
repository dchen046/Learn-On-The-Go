using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class Crossword1 : MonoBehaviour
{
    public Text currenttextbox;
    public Text Timetaken;
    public GameObject GameoverPanel;
    public string buttonclicked;
    public string[] question;
    public string[] player_answer;
    public float timetaken;
    public GameObject DownHintPanel;
    public GameObject AcrossHintPanel;
    public int down_panel_status;
    public int across_panel_status;
    public Text finaltime;
    public Text gamename;
    public double r;
    public double g;
    public double b;
    public double a;
    public int rbool;
    public int gbool;
    public int bbool;
    public GameObject Tiles;
    void Start(){
        question = new string[124] {"C","O","N","S","E","N","S","U","S","S","E","Q","U","E","N","C","E","S","P","E","R","M","A","T","O","G","E",
        "N","E","S","I","S","M","E","T","A","P","H","A","S","E","A","M","Y","L","A","S","E","B","I","N","A","R","Y","F","I","S","S","I","O","N",
        "S","E","P","T","A","V","I","R","O","I","D","R","O","T","N","P","U","P","S","A","S","I","N","D","O","P","O","R","E","P","T","M",
        "I","L","E","T","M","U","T","T","O","N","E","C","T","O","D","R","M","R","T","I","N","C","Y","M","E","P","S","I",
        "O","E","O"};
        player_answer = new string[124];
        timetaken = 0.0f;
        down_panel_status = 0;
        across_panel_status = 0;
        buttonclicked = "";
        Time.timeScale = 1;
        r = 0.0;
        g = 0.0;
        b = 0.0;
        a = 1.0;
        rbool = 1;
        gbool = -1;
        bbool = -1;
    }
    public void textTest(Text text){
        currenttextbox = text;
    }
    public void btnTest(Button btn) {
        if(btn.name == "Clear"){
            currenttextbox.text = "";
            player_answer[int.Parse(currenttextbox.name)] = "";
        }
        else if(btn.name == "DownHint"){
            if(down_panel_status == 0) {
                if(across_panel_status == 1){
                    AcrossHintPanel.SetActive(false);
                    across_panel_status = 0;
                }
                DownHintPanel.SetActive(true);
                down_panel_status = 1;
            }
            else {
                DownHintPanel.SetActive(false);
                down_panel_status = 0;
            } 
        }
        else if(btn.name == "AcrossHint"){
            if(across_panel_status == 0) {
                if(down_panel_status == 1){
                    DownHintPanel.SetActive(false);
                    down_panel_status = 0;
                }
                AcrossHintPanel.SetActive(true);
                across_panel_status = 1;
            }
            else {
                AcrossHintPanel.SetActive(false);
                across_panel_status = 0;
            } 
        }
        else if(btn.name == "ExitHintAcross") {
            AcrossHintPanel.SetActive(false);
            across_panel_status = 0;
        }
        else if(btn.name == "ExitHintDown"){
            DownHintPanel.SetActive(false);
            down_panel_status = 0;
        }
        else if(btn.name == "Restart"){
            Component[] texts;
            texts = Tiles.GetComponentsInChildren<Text>();
            foreach(Text component in texts) {
            }
            GameoverPanel.SetActive(false);
            Start();
        }
        else if(btn.name == "Menu" || btn.name == "Back"){
            SceneManager.LoadScene("Crossword_mainmenu");
        }
        else {
            currenttextbox.text = btn.name;
            player_answer[int.Parse(currenttextbox.name)] = btn.name;
        }
    }
    void Update(){
        if(rbool == 1) {
            r += Time.deltaTime * 0.25;
            if(r >= 1.0) {
                rbool -= 1;
            }
        }
        else if(rbool == 0) {
            r -= Time.deltaTime * 0.25;
            if(r <= 0.0) {
                r = 0.0f;
                rbool -= 1;
                gbool = 1;
            }
        }
        else if(gbool == 1) {
            g += Time.deltaTime * 0.25;
            if(g >= 1.0) {
                gbool -= 1;
            }
        }
        else if(gbool == 0) {
            g -= Time.deltaTime * 0.25;
            if(g <= 0.0) {
                g = 0.0f;
                gbool -= 1;
                bbool = 1;
            }
        }
        else if(bbool == 1) {
            b += Time.deltaTime * 0.25;
            if(b >= 1.0) {
                bbool -= 1;
            }
        }
        else if(bbool == 0) {
            b -= Time.deltaTime * 0.25;
            if(b <= 0.0) {
                b = 0.0f;
                bbool -= 1;
                rbool = 1;
            }
        }
        gamename.color = new Color((float)r,(float)g,(float)b,(float)a);
        timetaken += Time.deltaTime;
        if(timetaken >= 60) {
            float minutes = timetaken/60;
            float seconds = timetaken%60;
            Timetaken.text = (Math.Floor(minutes)).ToString("0")+"m "+(Math.Floor(seconds)).ToString("0") + "s";
        }
        else {
            Timetaken.text = (Math.Floor(timetaken)).ToString("0") + "s";
        }
        if(Enumerable.SequenceEqual(question,player_answer)) {
            finaltime.text = Timetaken.text;
            GameoverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
