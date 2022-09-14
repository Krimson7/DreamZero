using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{

    public bool canTriggerTutorial = true;

    public List<GameObject> tutorialScreens = new List<GameObject>();
    public int currentScreen = 0;
    
    public List<BoxCollider2D> tutorialTriggerZone = new List<BoxCollider2D>();

    public GameObject player;
    Collider2D checkResults;

    public void enterTutorialZone(int zone)
    {
        // print(zone);
        tutorialScreens[zone].SetActive(true);
        tutorialTriggerZone[zone].gameObject.SetActive(false);
        if(zone == 3) canTriggerTutorial = false;
    }


    //Triggered by event handler in case of first time in this level
    void tutorialUntriggered(){
        foreach(BoxCollider2D bc in tutorialTriggerZone){
            bc.gameObject.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tutorialUntriggered();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
