using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Controller[] Controllers
    {
        get { return controllers; }
    }

    public static GameController Instance { get; private set; }

    [SerializeField]
    private Controller[] controllers;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
