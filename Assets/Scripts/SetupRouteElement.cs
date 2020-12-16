using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupRouteElement : MonoBehaviour
{
    //Reference variables
    public RawImage background;
    // - event specific
    public TMP_Text optionOne;
    public TMP_Text optionTwo;
    // - end specific
    public TMP_Text endOptionTitle;
    public TMP_Text endOptionContent;

    //Instace variables
    bool firstIsCorrect;
    RouteElement element;

    public void Construct(RouteElement element) {

        this.element = element;

        if (element.GetType().ToString() == "Event") {

            Event curEvent = (Event)element;

            //Randomly assign correct answer
            firstIsCorrect = Random.Range(0, 2) == 1;

            if (firstIsCorrect)
            {
                optionOne.text = curEvent.correctText;
                optionTwo.text = curEvent.incorrectText;
            }
            else
            {
                optionOne.text = curEvent.incorrectText;
                optionTwo.text = curEvent.correctText;
            }

        } else if (element.GetType().ToString() == "End") {

            End curEnd = (End)element;

            endOptionTitle.text = curEnd.titleText;
            endOptionContent.text = curEnd.contentText;
        }


        //Apply all avaliable addons
        foreach (Addon addon in element.addons) {
            switch (addon.type) {
                case AddonType.IMAGE:
                    Background bgAddon = (Background)addon;
                    background.texture = bgAddon.image;
                    break;
                default:
                    Debug.LogError("Unsuported Addon Provided - " + AddonType.IMAGE);
                    break;
            }
        }
    }

    public void OnPressOption(bool isFirst) {
        Event curEvent = (Event) element;

        if (firstIsCorrect == isFirst) {
            StoryManager.instance.ShowElement(curEvent.correctChild);
        } else { 
            StoryManager.instance.ShowElement(curEvent.incorrectChild);
        }
    }

    public void OnPressGoBack() { 

        End curEnd = (End) element;

        StoryManager.instance.ShowElement(curEnd.goBackEvent);

    }

    public void OnPressRestart() {

        End curEnd = (End)element;

        StoryManager.instance.ShowElement(curEnd.restartEvent);
    }
}
