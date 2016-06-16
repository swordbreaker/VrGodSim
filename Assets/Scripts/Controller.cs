using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

    public enum VRControler { Left, Right };
    public delegate void VRControlerEvent(Controller controller, VRControler controllerType);
    public event VRControlerEvent OnGrippDown;
    public event VRControlerEvent OnGrippUp;
    public event VRControlerEvent OnGrippPressed;
    public event VRControlerEvent OnTriggerDown;
    public event VRControlerEvent OnTriggerUp;
    public event VRControlerEvent OnDpadDownUp;
    public event VRControlerEvent OnDpadDownDown;

    [SerializeField]
    private VRControler controllerType;

    private SteamVR_TrackedObject trackedObj;


    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {

        

        if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (OnGrippDown != null) OnGrippDown(this, controllerType);
        }

        if (controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (OnGrippUp != null) OnGrippUp(this, controllerType);
        }

        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (OnTriggerDown != null) OnTriggerDown(this, controllerType);
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (OnTriggerUp != null) OnTriggerUp(this, controllerType);
        }

        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if (OnDpadDownDown != null) OnDpadDownDown(this, controllerType);
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if (OnDpadDownUp != null) OnDpadDownUp(this, controllerType);
        }
    }
}
