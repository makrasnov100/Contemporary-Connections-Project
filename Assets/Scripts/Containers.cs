using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteElement {
    public string id = "";
    public List<Addon> addons = new List<Addon>();

    public RouteElement(string id) {
        this.id = id;
    }
}

//Event represents a choise that the user needs to do
public class Event : RouteElement {
    public string correctText;
    public string incorrectText;
    public Event parent;
    public RouteElement correctChild;
    public RouteElement incorrectChild;

    public Event(string id, string correctText, string incorrectText) : base (id) {
        this.correctText = correctText;
        this.incorrectText = incorrectText;
    }

    public void Route(Event parent, RouteElement correctChild, RouteElement incorrectChild) {
        this.parent = parent;
        this.correctChild = correctChild;
        this.incorrectChild = incorrectChild;
    }
}

public class End : RouteElement {
    public string titleText;
    public string contentText;
    public Event restartEvent;
    public Event goBackEvent;

    public End(string id, string titleText, string contentText) : base (id) {
        this.titleText = titleText;
        this.contentText = contentText;
    }

    public void Route(Event goBackEvent, Event restartEvent) {
        this.restartEvent = restartEvent;
        this.goBackEvent = goBackEvent;
    }
}

public enum AddonType {NOTE, IMAGE, BACKGROUND, HINT};

//Addons provide additional info (like info graphic to the event
public class Addon {
    public string text;
    public AddonType type;

    public Addon(string text, AddonType type) {
        this.text = text;
        this.type = type;
    }
}

public class Background : Addon
{
    public Texture image;

    public Background(Texture image, string text = "", AddonType type = AddonType.IMAGE) : base(text, type) {
        this.image = image;
    }
}

