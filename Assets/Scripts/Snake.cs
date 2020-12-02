using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Snake : MonoBehaviour
{
    // Current Movement Direction
    // (by default it moves to the right)
    public float movement = 0.7f;
    Vector2 dir = Vector2.right * 0.7f;
    public int rotate = 0;
    public int currQuestion = 0;
    public string[] qnaList;
    public string[] eachquestion;
    List<Transform> tail = new List<Transform>();
    public int direction = 1;
    public bool ate = false;
    public int currlives = 3;
    public int currScore = 0;

    // Tail Prefab
    public GameObject tailPrefab;

    // Food Prefab
    public GameObject foodPrefab;
    public GameObject foodPrefab2;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public bool endgame = false;
    public GameObject apple1;
    public GameObject apple2;

    //texts
    public GameObject Score;
    public GameObject Question;
    public GameObject Answer1;
    public GameObject Answer2;
    public GameObject EndGame;

  

    IEnumerator dbConn()
    {
        WWWForm form = new WWWForm();
        form.AddField("topic", "Glycolysis");
        UnityWebRequest request = UnityWebRequest.Post("http://localhost/scripts/dbConn.php", form);
        yield return request.SendWebRequest();
        qnaList = request.downloadHandler.text.Split('\n');
        eachquestion = qnaList[currQuestion].Split('\t');
        Question.GetComponent<Text>().text = eachquestion[0];
        Answer1.GetComponent<Text>().text = "(A) " + eachquestion[1];
        Answer2.GetComponent<Text>().text = "(B) " + eachquestion[2];
        Score.GetComponent<Text>().text = "Score: "+ currScore;
        Question.GetComponent<Text>().color = Color.black;
        Answer1.GetComponent<Text>().color = Color.green;
        Answer2.GetComponent<Text>().color = Color.red;


    }
    // Use this for initialization
    
    void Start()
    {
        StartCoroutine(dbConn());
        // Move the Snake every 100ms
        InvokeRepeating("Move", 0.15f, 0.15f);
        apple1 = SpawnApple(foodPrefab);
        apple2 = SpawnApple(foodPrefab2);
    }


    // Update is called once per Frame
    void Update()
    {
        // Move in a new Direction?

        
        if (Input.GetKey(KeyCode.RightArrow) && direction != -1)
        {
            dir = (Vector2.right) * movement;
            if (direction == 2)
            {
                rotate = 1;
            }
            if (direction == -2)
            {
                rotate = -1;
            }
            direction = 1;
            
        }
        else if (Input.GetKey(KeyCode.DownArrow) && direction != 2)
        {
            dir = -(Vector2.up) * movement;    // '-up' means 'down'
            if (direction == 1)
            {
                rotate = 1;
            }
            if (direction == -1)
            {
                rotate = -1;
            }
            direction = -2;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && direction != 1)
        {
            dir = -(Vector2.right) * movement; // '-right' means 'left'
            if (direction == 2)
            {
                rotate = -1;
            }
            if (direction == -2)
            {
                rotate = 1;
            }
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && direction != -2)
        {
            dir = (Vector2.up) * movement;
            if (direction == 1)
            {
                rotate = -1;
            }
            if (direction == -1)
            {
                rotate = 1;
            }
            direction = 2;
        }
    }

    void Move()
    {

        if (endgame)
        {
            dir = dir * 0f;
            return;
        }
        Vector2 v = transform.position;
        transform.Translate(dir);
        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            currScore++;
            Score.GetComponent<Text>().text = "Score: " + currScore;
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Green Apple
        if ((coll.name.StartsWith("GreenFood") && (eachquestion[1] == eachquestion[3])) || ((coll.name.StartsWith("RedFood")) && (eachquestion[2] == eachquestion[3])))
        {
            // Get longer in next Move call
            ate = true;
            Destroy(apple1);
            Destroy(apple2);
            apple1 = SpawnApple(foodPrefab);
            apple2 = SpawnApple(foodPrefab2);
            currQuestion++;
            print(qnaList.Length);
            if (currQuestion < qnaList.Length-1)
            {
                eachquestion = qnaList[currQuestion].Split('\t');
                Question.GetComponent<Text>().text = eachquestion[0];
                Answer1.GetComponent<Text>().text = "(A) " + eachquestion[1];
                Answer2.GetComponent<Text>().text = "(B) " + eachquestion[2];
            }
            else
            {
                EndGame.GetComponent<Text>().text = "Level Completed!";
                Application.Quit();
                print("Colliding here");
                endgame = true;
            }
        }

        // Collided with Tail or Border
        else
        {
            EndGame.GetComponent<Text>().text = "You Lost!";
            Application.Quit();
            print("Colliding here");
            endgame = true;
        }
    }
        // Spawn one piece of food
    public GameObject SpawnApple(GameObject currPrefab)
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        

        // Instantiate the food at (x, y)
        var currinstance = Instantiate(currPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
        return currinstance;
    }


}