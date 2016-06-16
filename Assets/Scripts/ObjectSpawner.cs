using System;
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private GameObject cube;

	// Use this for initialization
	void Start () {

	    foreach (var controller in GameController.Instance.Controllers)
	    {
	        controller.OnDpadDownDown += ControllerOnOnDpadDownDown;
	    }
	}

    private void ControllerOnOnDpadDownDown(Controller controller, Controller.VRControler controllerType)
    {
        Instantiate(cube, controller.transform.position, controller.transform.rotation);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
