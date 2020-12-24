using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossibleAnswerButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] bool isTrue;
    [SerializeField] bool isClicked;

    [SerializeField] List<Button> buttons;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();

        for (int i = 0; i < 4; i++)
        {
            buttons.Add(GameObject.Find($"Variant{i + 1}Button").GetComponent<Button>());
        }
    }
    public void EnableClick()
    {
        DisableClick();
        isClicked = true;
    }
    void DisableClick()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<PossibleAnswerButton>().isClicked = false;
        }
    }

}
