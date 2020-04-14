using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PC2D;

public class Cutscene : MonoBehaviour
{
    public Dialogue dialogue;
    public Animator UI;
    public Animator textUI;
    public Animator Title;
    public TextMeshProUGUI textBox;
    private int textBoxCount;

    public bool startCutscene;
    public bool canInput;
    public bool endcutscene;

    private void Start()
    {

        canInput = false;
        textBoxCount = 0;
        StartCoroutine(cutsceneDelay(0.5f));
        textBox.text = dialogue.texte[textBoxCount];
        UI.SetTrigger("Start");
        textUI.SetTrigger("Start");


    }

    private void FixedUpdate()
    {
        if (startCutscene && !endcutscene)
        {

            if (canInput)
            {
                if (UnityEngine.Input.GetButtonDown(PC2D.Input.JUMP))
                {
                    canInput = false;
                    if (textBoxCount < dialogue.texte.Length - 1)
                    {
                        textBoxCount++;
                        textUI.SetTrigger("Fade");
                        textBox.text = dialogue.texte[textBoxCount];
                        textUI.SetTrigger("Start");
                        StartCoroutine(cutsceneDelay(0.5f));

                    }
                    else
                    {
                        startCutscene = false;
                        endcutscene = true;
                        UI.SetTrigger("Fade");
                    }
                }
            }
        }
        if (endcutscene)
        {
            UI.SetTrigger("Fade");
            Title.SetTrigger("Start");
        }
    }


    IEnumerator cutsceneDelay(float time)
    {

        yield return new WaitForSeconds(time);
        canInput = true;
        startCutscene = true;
    }
}
