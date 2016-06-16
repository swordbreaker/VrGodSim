using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private GameObject pivot;

    [SerializeField] private float movementSpeed;

    private Rigidbody rigid;
    private HashSet<Controller> attachtedControllers = new HashSet<Controller>();
    private Vector3 movementVector;
    private float distanceBetweenController;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        foreach (var controller in GameController.Instance.Controllers)
        {
            controller.OnGrippUp += ControllerOnOnGrippUp;
            controller.OnGrippDown += ControllerOnOnGrippDown;
        }
    }

    private void ControllerOnOnGrippDown(Controller controller, Controller.VRControler controllerType)
    {
        attachtedControllers.Add(controller);
        if (attachtedControllers.Count > 0)
        {
            distanceBetweenController = Vector3.Distance(attachtedControllers.First().transform.position, controller.transform.position);
        }
        pivot.transform.position = controller.transform.position;
    }

    private void ControllerOnOnGrippUp(Controller controller, Controller.VRControler controllerType)
    {
        attachtedControllers.Clear();
    }

    void FixedUpdate()
    {
        if (attachtedControllers.Count == 1)
        {
            movementVector =  pivot.transform.position - attachtedControllers.First().transform.position;
            rigid.velocity = movementVector * movementSpeed;
        }
        else if (attachtedControllers.Count == 2)
        {
            var newDistance = Vector3.Distance(attachtedControllers.First().transform.position, attachtedControllers.Last().transform.position);
            var zoom = (newDistance - distanceBetweenController) * 10;
            distanceBetweenController = newDistance;

            transform.localScale = ClampMagnitude(
                Vector3.SmoothDamp(transform.localScale, transform.localScale + new Vector3(zoom, zoom, zoom), ref currentVelocity, 0.3f), 
                2f, 
                0.1f);
        }
    }

    public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        float magnitude = v.magnitude;
        if (magnitude > max) return v.normalized * max;
        else if (magnitude < min) return v.normalized * min;
        return v;
    }

}
