using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text text;
    public Text achievementText;

    private int textValue;
    // Start is called before the first frame update
    void Start()
    {
        textValue = 0;
        text.text = textValue.ToString();
        achievementText.enabled = false;
        GameEvents.instance.OnNumberAchieved += OnGoalAchieved;
        GameEvents.instance.OnNumberChanged += OnNumberChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            textValue += 1;
            GameEvents.instance.NumberChanged(textValue);
            textValue = textValue % 10;
            text.text = textValue.ToString();
        }
    }

    private void OnGoalAchieved()
    {
        achievementText.enabled = true;
    }

    private void OnNumberChanged(int number)
    {
        Debug.Log("Number has changed to " + number);
        if (number == 10)
        {
            GameEvents.instance.NumberAchieved();
        }
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnNumberAchieved -= OnGoalAchieved;
    }
}
