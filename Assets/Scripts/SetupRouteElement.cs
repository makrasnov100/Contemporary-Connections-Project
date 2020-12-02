using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetupRouteElement : MonoBehaviour
{
    //Reference variables
    GameObject optionOneGO;
    GameObject optionTwoGO;
    TMP_Text optionOne;
    TMP_Text optionTwo;

    //Instace variables
    bool firstIsCorrect;

    public void Construct(RouteElement element) {

        if (element.GetType().ToString() == "Event") {
            //Randomly assign correct answer
            firstIsCorrect = Random.Range(0, 2) == 1;

        }
        else if (element.GetType().ToString() == "End") {
            
        }

    }
}
