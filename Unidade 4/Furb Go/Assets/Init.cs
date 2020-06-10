using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Init : MonoBehaviour
{

    public static string qrCode;
    public Text teste;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

        foreach (TrackableBehaviour tb in tbs)
        {
            qrCode = tb.TrackableName;
            var menu = new Menu();
            menu.ChangeScene(ScenesNames.MapaFurb);
        }
    }
}
