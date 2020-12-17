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
    public Button backgroundBtn;
    public Button hintBtn;
    public TMP_Text optionOne;
    public TMP_Text optionTwo;
    public TMP_Text backgroundInfo;
    public TMP_Text hintInfo;
    public AudioClip winSound;
    public AudioClip loseSound;
    // - end specific
    public TMP_Text endOptionTitle;
    public TMP_Text endOptionContent;
    public AudioClip buttonClickSound;



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
                case AddonType.BACKGROUND:
                    backgroundInfo.text = addon.text;
                    break;
                case AddonType.HINT:
                    hintInfo.text = addon.text;
                    break;
                default:
                    Debug.LogError("Unsuported Addon Provided - " + addon.type.ToString());
                    break;
            }
        }

        if (element.GetType().ToString() == "Event")
        {
            OnPressTab(true);
        }
    }

    public void OnPressOption(bool isFirst) {
        Event curEvent = (Event) element;

        if (firstIsCorrect == isFirst) {

            StoryManager.instance.soundPlayer.clip = winSound;
            StoryManager.instance.soundPlayer.Play();

            StoryManager.instance.ShowElement(curEvent.correctChild);

        } else {

            StoryManager.instance.soundPlayer.clip = loseSound;
            StoryManager.instance.soundPlayer.Play();

            StoryManager.instance.ShowElement(curEvent.incorrectChild);

        }
    }

    public void OnPressGoBack() {

        StoryManager.instance.soundPlayer.clip = buttonClickSound;
        StoryManager.instance.soundPlayer.Play();

        End curEnd = (End) element;

        StoryManager.instance.ShowElement(curEnd.goBackEvent);

    }

    public void OnPressRestart() {

        StoryManager.instance.soundPlayer.clip = buttonClickSound;
        StoryManager.instance.soundPlayer.Play();

        End curEnd = (End)element;

        StoryManager.instance.ShowElement(curEnd.restartEvent);
    }

    public void OnPressTab(bool isBackground) {

        StoryManager.instance.soundPlayer.clip = buttonClickSound;
        StoryManager.instance.soundPlayer.Play();

        backgroundBtn.interactable = !isBackground;
        hintBtn.interactable = isBackground;

        backgroundInfo.gameObject.SetActive(isBackground);
        hintInfo.gameObject.SetActive(!isBackground);

    }
}
