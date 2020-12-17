using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    //Storage elements
    public static StoryManager instance;
    public List<Texture> eventImages;
    public List<Texture> endImages;

    //Instance Variables
    List<Event> events = new List<Event>();
    List<End> ends = new List<End>();
    public HashSet<string> visitedRouteElements = new HashSet<string>();

    GameObject currentEventGO;

    //Reference Variables
    //- prefabs
    public GameObject eventPrefab;
    public GameObject endPrefab;
    // - in scene
    public TMP_Text completionText;
    public GameObject routeHolder;
    public AudioSource soundPlayer;

    void Awake() {
        if (!instance){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {

        //Setup all events & ends in the story
        // - events
        events.Add(new Event("intro_evt",
            "Let's Begin!",
            "I dont want to play!"
        ));
        events.Add(new Event("0_evt",
            "Call for the help of religious and education groups in presenting the case to Americans that Muslim and Islam extremists (terrorists) are not the same.",
            "Refer to the acts of a republic president George W. Bush in the aftermath of the 9/11 attacks and call Americans to follow the same advice of unity rather than a division from the Muslim religion."
        ));
        events.Add(new Event("1_evt",
            "Prioritize addressing the issue of untrue affiliation of Muslims with terrorism first.",
            "Prioritize addressing the large need for a safe haven created by oppression in foreign countries first."
        ));
        events.Add(new Event("2_evt",
            "Argue through boundary penetrating mediums such as news articles and social media that to continue diversifying the American nation we need to let outside voices contribute to the American values by gaining a safe haven from persecution and entering the country.",
            "Argue that because the US was closed to Non Christian points of view for much of its growth as a country, it needs to gain back the lost perspectives. Do this by sending letters to congresspeople and other politicians."
        ));

        // - ends
        ends.Add(new End("intro_end",
            "Too bad :(",
            "If you ever want to learn about history come back later! If you want to exit the game just look at the top right corner."
        ));
        ends.Add(new End("0_end",
            "You chose wrong!",
            "Although mentioning George W. Bush’s actions can help get the attention of the Republican base, the artifact at the beginning of the game tells us that the majority of these people would favor the current president’s rhetoric surrounding the bans. This detail makes George W. Bush a less favorable authority to speak on the issue."
        ));
        ends.Add(new End("1_end",
            "Try again!",
            "Historical evidence suggests that misinformation about a religious group’s affiliation and agendas in the United States caused the most tensions among citizens. The other option more closely addresses this issue."
        ));
        ends.Add(new End("2_end",
            "Not quite!",
            "Globalization was defined by the context section as the progress which makes the world “a single place.” The United States was also quoted to be enriched by many different cultures throughout its existence. Both of these points contradict this selection."
        ));
        ends.Add(new End("4_end",
            "Congratulations! You made it!",
            "You have chosen the correct historically valid approach and arguments for promoting the end to the “Muslim Ban” executive order. Providing us with the tools to synthesize and correctly select the best solutions from all available options is why history plays an important role in politics and the formation of our own worldviews."
        ));

        //Setup the story line with all pathways
        events[0].Route(null, events[1], ends[0]);
        events[1].Route(events[0], events[2], ends[1]);
        events[2].Route(events[1], events[3], ends[2]);
        events[3].Route(events[2], ends[4], ends[3]);

        ends[0].Route(events[0], events[0]);
        ends[1].Route(events[1], events[0]);
        ends[2].Route(events[2], events[0]);
        ends[3].Route(events[3], events[0]);
        ends[4].Route(events[3], events[0]);


        //Setup the addons (context, hints and images) for events and ends
        // - background
        events[0].addons.Add(new Addon(
            "\tIn 2017, the 45th president of the United States, Donald J. Trump, enacted an executive order banning entry for refugees from 6 Muslim majority countries. In an editorial for the Boston Globe, Jeffrey Sachs calls this action a direct discrimination of refugees on the “basis of their religion.”  Sachs states that the president was spreading misinformation when saying that he is signing the order to protect the country from terrorism. In actuality, Sach argues that it was to further improve his appeal among his base, the white working class men. In this historical question-answer game, you will be tasked with choosing the correct answer out of the given options based on the provided historical context. The final result of your correct answers will form a historical argument and approach for helping the refugees enter America. For each event in the game, there will be two options to choose from and a historical hint highlighting the importance history plays given the context provided. Good luck on your short journey.",
            AddonType.BACKGROUND
        ));
        events[1].addons.Add(new Addon(
            "\tAfter the 9/11 terrorist attacks, in which several commercial planes were hijacked and crashed into prominent buildings around the United States, a wave of outrage has hiked the number of hate crimes done on Muslims by 15 fold from the previous year. In his paper on the unfolding of these events, Jeffrey Kaplan writes that law enforcement’s efforts to prosecute hate crimes and President Bush’s use of his position as a bully pulpit to unite the nation helped alleviate an even larger potential outrage. Additionally, the outpouring of support from various religious and educational organizations towards endangered Muslim communities also helped to lower the rates of hate crimes. Use this historical perspective in combination with the information from the initial context slide to choose a good approach to convince the United States public that they should not be concerned about incoming Muslim refugees being terrorists.",
            AddonType.BACKGROUND
        ));
        events[2].addons.Add(new Addon(
            "\tStarting by the second quarter of the 19th century, the United States was still a young nation with many tensions influencing the ideologies of those inside. One of these significant issues was the topic of Catholic immigration. At the time, Samuel Morse, a leader among the Nativist Party, wrote on the dangers he perceives immigrants of this particular affiliation were dangerous to the country itself. He lists election interference, foreign militia formation, and large social unrest as some observations that pose a threat to the newly established US constitution. A large contributor to such a perspective was the idea that the Catholics’ alliance was not with America because of their religion but with a foreign power such as the Pope. Additionally, falsified content such as the “Awful Disclosures of Maria Monk” at the time contributed to the negative view of the incoming Catholic immigrants.Excerpts from this work told of untrue horrors of events occurring inside isolated catholic monasteries, such as the murder of infants and adultery among priests.It took decades for false statements and accusations to vanish from the social mainstream and catholic individuals to be more widely accepted in politics and culture.",
            AddonType.BACKGROUND
        ));
        events[3].addons.Add(new Addon(
            "\tIn her article for the Journal of Theological Studies, Jeannine H. Fletcher refers to the globalization of the world becoming “a single place” through developments such as trade, migration, and information travel. At first, the impact on pluralistic religious thinking was limited to forming hierarchical systems in which Christianity often rested at the top of all other religions. Fletcher continues to describe how increased globalization by mid 20th century lead to the creation of “theology of religions” in which supporters of the movement began to unify different religious theologies and treating them as respected neighbors rather than something to stand in opposition off. Although a move in the right direction, Fletcher argues that the hybrid approach in which multiple cultural entities form a dynamic identity by means of negotiation and reasoning is the best approach for the free flow of knowledge and experience. The words of David Moberg reflect this perspective of the blending of cultures in his paper on American religious pluralism. Moberg affirms that for the past 350 years, the United States has borrowed and modified from the world’s traditions, beliefs, and cultures to form a unique religious space inside the country.",
            AddonType.BACKGROUND
        ));

        // - hint
        events[0].addons.Add(new Addon(
            "\tTry selecting one of the choises below!",
            AddonType.HINT
        ));
        events[1].addons.Add(new Addon(
            "\tIn this case, historical evidence shows various methods by which the United States was able to quickly unite the nation and end the wave of unjust violence against the peaceful domestic Muslim population. Focus on the methods that worked in the past to guide your selection. Teaching us about what worked in similar situations is one way history can play a significant role in society.",
            AddonType.HINT
        ));
        events[2].addons.Add(new Addon(
            "\tThe way Catholic immigrants were treated in the 19th century paints a picture of how the American public may perceive Muslims as the new religion trying to invade the American way of life. Knowing the reasons people believed catholic immigrants were a threat may provide powerful insight into what themes need to be addressed in order for Muslim refugees to become accepted by the American public.",
            AddonType.HINT
        ));
        events[3].addons.Add(new Addon(
            "\tThe history of globalization points us to the integration of many cultures into one as the progression in which the United States is heading. Use this significant historical analysis to pick an approach on how to suggest that Muslim refugees should be allowed to enter the country.",
            AddonType.HINT
        ));

        // - images 
        for (int i = 0; i < eventImages.Count; i++) {
            if (eventImages[i] != null) {
                events[i].addons.Add(new Background(eventImages[i]));
            }
        }
        for (int i = 0; i < endImages.Count; i++) {
            if (endImages[i] != null) { 
                ends[i].addons.Add(new Background(endImages[i]));
            }
        }

        //Setup the initial step in the story
        ShowElement(events[0]);
    }

    public void ShowElement(RouteElement element)
    {
        if (!visitedRouteElements.Contains(element.id)) {
            visitedRouteElements.Add(element.id);
        }

        //Update the UI
        // - completion text
        int total_routes = events.Count + ends.Count;
        float completion = visitedRouteElements.Count / (float)total_routes;
        completionText.text = "Completed " + ((int)(completion * 100)).ToString() + "%";

        GameObject nextEventGO = null;
        if (element.GetType().ToString() == "Event"){ 
            nextEventGO = Instantiate(eventPrefab); 
        }
        else { 
            nextEventGO = Instantiate(endPrefab); 
        }

        SetupRouteElement nextEvent = nextEventGO.GetComponent<SetupRouteElement>();
        nextEvent.Construct(element);
        nextEventGO.transform.SetParent(routeHolder.transform);
        nextEventGO.transform.localScale = Vector3.one;
        RectTransform nextEventRectTransform = nextEventGO.GetComponent<RectTransform>();
        nextEventRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 1920);
        nextEventRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 1080);


        //TODO: perform a cool transition between two event gameobjects
        Destroy(currentEventGO);
        currentEventGO = nextEventGO;
    }

}
