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

    //Instace Variables
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

    void Awake() {
        if (!instance){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {

        //Setup all events & ends in the story
        events.Add(new Event("0_evt", "yes", "no"));
        events.Add(new Event("1_evt", "yes", "no"));
        events.Add(new Event("2_evt", "yes", "no"));
        events.Add(new Event("3_evt", "yes", "no"));

        ends.Add(new End("0_end", "lose_a", "a"));
        ends.Add(new End("1_end", "lose_b", "b"));
        ends.Add(new End("2_end", "lose_c", "c"));
        ends.Add(new End("3_end", "lose_d", "d"));
        ends.Add(new End("4_end", "win", "cool"));

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


        //Setup the addons (notes and images) for events and ends
        // -  notes 
        events[1].addons.Add(new Addon("Interesting Note", AddonType.NOTE));
        events[3].addons.Add(new Addon("Cool Note", AddonType.NOTE));
        ends[3].addons.Add(new Addon("You Not Win", AddonType.NOTE));
        ends[4].addons.Add(new Addon("You Win", AddonType.NOTE));
        // - images 
        for(int i = 0; i < eventImages.Count; i++) {
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
        int total_routes = events.Count + events.Count;
        float completion = visitedRouteElements.Count / (float)total_routes;
        completionText.text = "Completed " + (completion * 100).ToString() + "%";

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
