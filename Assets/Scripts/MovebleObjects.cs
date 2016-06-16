using System;
using UnityEngine;
using System.Collections;

public class MovebleObjects : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider collider;
    private Controller touchingController;
    private Transform pivotPoint;
    private bool isGrabbed;
    private Vector3 movementDelta;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        pivotPoint = new GameObject().transform;

        foreach (var controller in GameController.Instance.Controllers)
        {
            controller.OnTriggerDown += ControllerOnOnTriggerDown;
            controller.OnTriggerUp += ControllerOnOnTriggerUp;
        }
    }

    private void Update()
    {
        if (isGrabbed && touchingController != null)
        {
            float angle;
            Vector3 axis;
            Quaternion rotationDelata = touchingController.transform.rotation*Quaternion.Inverse(pivotPoint.rotation);
            rotationDelata.ToAngleAxis(out angle, out axis);
            rigid.angularVelocity = (Time.fixedDeltaTime*angle*axis)*10;
            rigid.velocity = (touchingController.transform.position - pivotPoint.position) * 5000 * Time.fixedDeltaTime;
        }
        
    }

    private void ControllerOnOnTriggerUp(Controller controller, Controller.VRControler controllerType)
    {
        if (touchingController == controller)
        {
            isGrabbed = false;
            touchingController = null;
        }
    }

    private void ControllerOnOnTriggerDown(Controller controller, Controller.VRControler controllerType)
    {
        if (touchingController == controller)
        {
            pivotPoint.position = controller.transform.position;
            pivotPoint.rotation = controller.transform.rotation;
            pivotPoint.SetParent(transform, true);
            isGrabbed = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller" && !isGrabbed)
        {
            touchingController = other.gameObject.GetComponent<Controller>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //touchingController = null;
    }
}
