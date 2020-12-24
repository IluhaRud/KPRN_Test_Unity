using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] List<PossibleAnswerButton> buttonsState;
    [SerializeField] TestData testData;


    private void Awake()
    {
        for (int i = 0; i < 4; i++)
            buttonsState.Add(GameObject.Find($"Variant{i + 1}Button").GetComponent<PossibleAnswerButton>());

        testData = transform.parent.parent.gameObject.GetComponent<TestData>();
    }

    public void ClickNextButton()
    {
        for (int i = 0; i < buttonsState.Count; i++)
        {
            if (buttonsState[i].isClicked && buttonsState[i].isTrue)
            {
                testData.points++;
                break;
            }
        }

        testData.LoadQuestion();
    }
}
