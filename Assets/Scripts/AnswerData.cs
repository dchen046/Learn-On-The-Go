using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerData : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject = null;
    [SerializeField] Image toggle = null;

    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    [Header("References")]
    [SerializeField] GameEvents events = null;

    private RectTransform rect = null;
    public RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                //set rect: if GetComponent<RectTransform>() is null then add one
                rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return rect;
        }
    }
    private int answerIndex = -1;
    public int AnswerIndex { get { return answerIndex; } }
    private bool Checked = false;
    //update UI of the answer selection
    void UpdateUI()
    {
        if (toggle == null) return;
        if (Checked)
        {
            toggle.sprite = checkedToggle;
        }
        else toggle.sprite = uncheckedToggle;
    }
    public void UpdateData (string info, int index)
    {
        infoTextObject.text = info;
        answerIndex = index;
    }
   //reset answer selection
    public void Reset ()
    {
        Checked = false;
        UpdateUI();
    }
    //flip answer check status and update UI
    public void SwitchState ()
    {
        Checked = !Checked;
        UpdateUI();
        if (events.UpdateQuestionAnswer != null)
        {
            events.UpdateQuestionAnswer(this);
        }
    }
}