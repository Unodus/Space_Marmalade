using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointFollow : MonoBehaviour
{
    //    public ScriptableGrid gridSettings;



    //    [SerializeField] SoundHelpers SoundManager;

    //    [SerializeField] GameObject[] CameraObjects = new GameObject[2];


    //    [SerializeField] ScriptableGameEvents EventProfiler;
    //    [SerializeField] ScriptableElias EliasThemeNames;


    //    EliasPlayer EliasComponent;



    //    [SerializeField]  GameObject PlayerPrefab; // Refference to all the Ship prefabs, for instantiation
    //    [SerializeField]  GameObject SmallEnemyPrefab;
    //    [SerializeField]  GameObject LargeEnemyPrefab;


    //  //  [SerializeField] PostSwitcher postSwitcher;

    //    [SerializeField] GameObject PointPrefab; // Refference for each navigable point on the map


    //    [SerializeField]
    //    GameObject TimerPrefab; // ref to the countdown slider
    //    Slider TimerObject; 

    //    int NumOfPoints; // The total amount of points in the grid

    //    //public enum NodeMode // Each point can have three states- whether it's empty, able to be selected, or has a ship on it
    //    //{
    //    //    Empty,
    //    //    Selectable,
    //    //    Occupied
    //    //}

    //    //public class MyPoint // Each point uses this Struct. The lp-rp points are part of an unimplemented feature due to scope
    //    //{
    //    //    public int Column, Row;
    //    //    public GameObject p;
    //    //    public NodeMode Mode;
    //    //}

    //    public class SpaceshipBase // Ship Base - All ships inherit from this
    //    {
    //        public string Name;

    //        public GameObject ShipPrefab;
    //        public float Movement;
    //        public float MaxMovement;
    //        public float Reach;
    //        public float Shots;
    //        public int ShipType;

    //        public PointObject CurrentNode;

    //        public void Initiate(PointObject StartingNode, GameObject prefab) // Creates the Ship and makes sure all variables are set
    //        {

    //            ShipPrefab = Instantiate(prefab);

    //            ShipPrefab.name = "Ship:" + ShipType;
    //            ShipPrefab.tag = "Player";

    //            CurrentNode = StartingNode;
    //            ShipPrefab.transform.SetParent(CurrentNode.p.transform);
    //            ShipPrefab.transform.localPosition = Vector3.zero;

    //            ShipPrefab.transform.LookAt(Vector3.zero);

    //        }
    //    }


    //    public class Player : SpaceshipBase // The Players Ship. Has long reach and mobility
    //    {
    //        public Player(PointObject StartingNode, GameObject prefab)
    //        {
    //            ShipType = 0;
    //            Movement = 2;
    //            MaxMovement = 2;
    //            Reach = 3;
    //            Shots = 1;

    //            Initiate(StartingNode, prefab);
    //        }
    //    }
    //    public class SmallEnemy : SpaceshipBase // The "smaller" enemy ship
    //    {
    //        public SmallEnemy(PointObject StartingNode, GameObject prefab)
    //        {
    //            ShipType = 1;
    //            Movement = 2;
    //            MaxMovement = 2;
    //            Reach = 2;
    //            Shots = 1;

    //            Initiate(StartingNode, prefab); 
    //        }
    //    }
    //    public class BigEnemy : SpaceshipBase // The larger enemy ship, acts as a carrier for smaller ships
    //    {
    //        public BigEnemy(PointObject StartingNode, GameObject prefab)
    //        {
    //            ShipType = 2;
    //            Movement = 5;
    //            MaxMovement = 5;
    //            Reach = 3;
    //            Shots = 1;

    //            Initiate(StartingNode, prefab);
    //        }
    //    }

    //    // At some point, convert these to the profiler ^


    //    public List<PointObject> MyPoints = new List<PointObject>();
    //    public List<SpaceshipBase> SpaceshipBases = new List<SpaceshipBase>();


    //    public ScriptableGameEvents.TurnPhase CurrentTurn;

    //    Slider InstantiateTimer() // Creates the timer at the start of each turn
    //    {
    //        GameObject Canvas = GameObject.FindGameObjectWithTag("Canvas");
    //        GameObject Timer = Instantiate(TimerPrefab, Canvas.transform);
    //        return Timer.GetComponent<Slider>();
    //    }

    //    public void SetMove()
    //    {
    //  //      GridExtensions.ChangeBool(true);
    //    }
    //    public void SetAim()
    //    {
    ////        GridExtensions.ChangeBool(false);
    //    }

    //    public void ToggleState()
    //    {

    //        //if (GridExtensions.GetBool())
    //        //{
    //        //    CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting);
    //        //}
    //        //else
    //        //{
    //        //    CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Moving);
    //        //}
    //    }


    //    public void SwitchOpenObject(int SelectedObject, GameObject[] Objects)
    //    {
    //        for (int i = 0; i < Objects.Length; i++)
    //        {

    //            if (i == SelectedObject)            { Objects[i].SetActive(true); }
    //            else            { Objects[i].SetActive(false); }
    //        }
    //    }



    //    void Start()// Instantiate grid and ships in random positions
    //    {

    //        if (!EliasComponent) EliasComponent = FindObjectOfType<EliasPlayer>();

    //        // Replaced by "Get Random Points"
    //      //  int RandomNumber;
    //        //RandomNumber = Random.Range(0, TempList.Count);

    ////        MyPoints[RandomNumber] = MyPoints[RandomNumber];


    //  //      SpaceshipBases.Add(new Player(MyPoints[RandomNumber], PlayerPrefab));
    //    //    TempList.RemoveAt(RandomNumber);

    //      //  RandomNumber = Random.Range(0, TempList.Count);


    //        //MyPoints[RandomNumber] = MyPoints[RandomNumber];

    //        //SpaceshipBases.Add(new SmallEnemy(MyPoints[RandomNumber], SmallEnemyPrefab));
    //        //TempList.RemoveAt(RandomNumber);



    //        CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Start);


    //    }

    //    void CallEvent(ScriptableGameEvents.TurnPhase turnPhase)
    //    {

    //        ScriptableGameEvents.TurnEventSettings currentEvent = EventProfiler.GetEventByPhase(turnPhase);
    //        if (currentEvent == null) return;



    //        CurrentTurn = turnPhase;


    //        switch (currentEvent.CameraMode)
    //        {
    //            case 0:
    //                SetMove();
    //                break;
    //            case 1:
    //                SetAim();
    //                break;

    //            default:
    //                Debug.Log(currentEvent.CameraMode + " isn't a registered camera mode");
    //                break;

    //        }
    // //       if ( currentEvent.CameraMode ==0)


    //            Debug.Log("Current Event: " + turnPhase + " with Elias String:  " + currentEvent.TriggerEliasProfiler );


    //        if (!string.IsNullOrEmpty( currentEvent.TriggerEliasProfiler ))

    //        {
    //            EliasThemeNames.ChangeElias(EliasComponent, currentEvent.TriggerEliasProfiler);
    //        }

    //        SwitchOpenObject(currentEvent.CameraMode, CameraObjects);

    //        switch (turnPhase)
    //        {
    //            case ScriptableGameEvents.TurnPhase.Transition:
    //                break;
    //            case ScriptableGameEvents.TurnPhase.EnemyTurnMoving:
    //                break;
    //            case ScriptableGameEvents.TurnPhase.EnemyTurnShooting:
    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Moving:


    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting:

    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Start:

    //                SpaceshipBases[0].Movement = SpaceshipBases[0].MaxMovement;
    //                SpaceshipBases[0].ShipPrefab.GetComponent<PlayerMovement>().ShieldSize = SpaceshipBases[0].Movement;

    //                ReadyPoints();


    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_End:

    //                Destroy(GameObject.FindGameObjectWithTag("Timer"));

    //                LaunchMissiles(SpaceshipBases[0], ScriptableGameEvents.TurnPhase.EnemyTurnMoving);


    //                break;
    //            default:
    //                Debug.Log("Event Logic is not programmed: " + turnPhase);
    //                break;
    //        }

    //        }

    //    void AddEnemyShip()
    //    {
    //        //MyPoint SpawnZone = GetRandNode();
    //        int RandomNumber = Random.Range(0, 2);
    //        if (RandomNumber == 0)
    //        {
    //    //        SpaceshipBases.Add(new SmallEnemy(SpawnZone, SmallEnemyPrefab));
    //        }
    //        else
    //        {
    //    //        SpaceshipBases.Add(new BigEnemy(SpawnZone, LargeEnemyPrefab));
    //        }
    //    }

    //    void TimeSelf()
    //    {
    //        if (TimerObject == null)
    //        {
    //            TimerObject = InstantiateTimer();
    //        }
    //        else
    //        {
    //            TimerObject.value -= Time.deltaTime;
    //            if (TimerObject.value <= 0)
    //            {
    //                SetMove();

    //                EndTurn();
    //                Destroy(TimerObject.gameObject);
    //            }
    //        }
    //    }

    //    void UpdateByEvent()
    //    {
    //        ScriptableGameEvents.TurnEventSettings currentEvent = EventProfiler.GetEventByPhase(CurrentTurn);

    //        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //        if (currentEvent == null) return;



    //        switch (CurrentTurn)
    //        {
    //            case ScriptableGameEvents.TurnPhase.Transition:
    //                break;
    //            case ScriptableGameEvents.TurnPhase.EnemyTurnMoving:
    //                    //do enemy stuff

    //                    foreach (SpaceshipBase p in SpaceshipBases)
    //                    {
    //                        if (p.ShipType == 1)
    //                        {
    //                      //      FindSelectableNodes(p.Movement * myGrid.Size, p);
    //                      //      ChangeNode(p, GetClosestNode(SpaceshipBases[0].ShipPrefab.transform.position, p, 0));
    //                            p.Movement = p.MaxMovement;
    //                        }

    //                        if (p.ShipType == 2)
    //                        {
    //                 //           FindSelectableNodes(p.Movement * myGrid.Size, p);
    //                   //         ChangeNode(p, GetClosestNode(GetRandNode().p.transform.position, p, 0));
    //                            p.Movement = p.MaxMovement;
    //                        }

    //                    }

    //                    SetAim();
    //                    ReadyPoints();

    //                break;
    //            case ScriptableGameEvents.TurnPhase.EnemyTurnShooting:

    //                    //do enemy stuff

    //            //        List<MyPoint> ShipsToMake = new List<MyPoint>();

    //                    foreach (SpaceshipBase p in SpaceshipBases)
    //                    {
    //                        if (p.ShipType == 1)
    //                        {
    //              //              FindSelectableNodes(p.Reach * myGrid.Size, p);
    //               //             p.ShipPrefab.GetComponent<PlayerMovement>().Shoot(GetClosestNode(SpaceshipBases[0].ShipPrefab.transform.position, p, 0).p.transform.position);
    //                            LaunchMissiles(p, ScriptableGameEvents.TurnPhase.PlayerTurn_Start);

    //                        }
    //                        if (p.ShipType == 2)
    //                        {
    //                 //           FindSelectableNodes(p.Reach * myGrid.Size, p);
    //                  //          ShipsToMake.Add(p.CurrentNode);

    //                        }
    //                    }

    //                    //foreach (MyPoint p in ShipsToMake)
    //                    //{
    //                    //    SpaceshipBases.Add(new SmallEnemy(p, SmallEnemyPrefab));

    //                    //}

    //                // SetMove();
    //                // ReadyPoints();
    //                // FindSelectableNodes(SpaceshipBases[0].Reach * 100, SpaceshipBases[0]);

    //                CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Start);



    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Moving:
    //                TimeSelf();

    //                if (Input.GetMouseButtonDown(0))
    //                {
    //                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                    worldPosition.z = 0;
    //                 //   ChangeNode(SpaceshipBases[0], GetClosestNode(worldPosition, SpaceshipBases[0], 1));
    //                }

    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting:
    //                TimeSelf();

    //                if (Input.GetMouseButtonDown(0))
    //                {
    //                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                    worldPosition.z = 0;


    //                    if (Vector3.Distance(SpaceshipBases[0].ShipPrefab.transform.position, worldPosition) < SpaceshipBases[0].Reach)
    //                        {
    //                 //           MyPoint FirePoint = GetClosestNode(worldPosition, SpaceshipBases[0], 1);
    //                            //if (FirePoint.p != null)
    //                            //{
    //                            //    SpaceshipBases[0].ShipPrefab.GetComponent<PlayerMovement>().Shoot(FirePoint.p.transform.position);
    //                            //        SetMove();
    //                            //    ReadyPoints();
    //                            //}


    //                        }


    //                }

    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_Start:
    //                break;
    //            case ScriptableGameEvents.TurnPhase.PlayerTurn_End:
    //                break;
    //            default:
    //                Debug.Log("Event Logic is not programmed");
    //                break;
    //        }

    //    }

    //    //MyPoint CheckNodeIsWithinBounds(MyPoint NewPoint)// Moves a ship to a different part of the grid
    //    //{
    //    //    if (NewPoint.Column < MinGridX || NewPoint.Column > MaxGridX)
    //    //    {
    //    //        int properColumn = (NewPoint.Column ) % (GridMath.Columns / GridMath.Manifolds);
    //    //        properColumn = properColumn + MinGridX;

    //    //        foreach(MyPoint i in MyPoints)
    //    //        {
    //    //            if (i.Column == properColumn && i.Row == NewPoint.Row)
    //    //            {
    //    //                return i;
    //    //            }
    //    //        }
    //    //        return NewPoint;
    //    //    }
    //    //    else
    //    //    {
    //    //        return NewPoint;
    //    //    }


    //    //}

    //    void ChangeNode(SpaceshipBase Ship, PointObject NewPoint)// Moves a ship to a different part of the grid
    //    { 

    //        if (NewPoint.p == null)
    //            return;

    //        if (NewPoint == Ship.CurrentNode)
    //            return;

    //        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();


    //        Ship.CurrentNode = NewPoint;
    //        Ship.ShipPrefab.transform.SetParent(NewPoint.p.transform);

    //        Ship.Movement -= Vector3.Magnitude(Ship.ShipPrefab.transform.localPosition) *0.25f* myGrid.Size;



    //        if (Ship.Movement <0)
    //        {
    //            Ship.Movement = 0;
    //        }

    //        Ship.ShipPrefab.GetComponent<PlayerMovement>().ShieldSize = Ship.Movement;




    //        if(CurrentTurn == ScriptableGameEvents.TurnPhase.PlayerTurn_Moving)
    //        {
    //            SoundManager.PlaySound(ScriptableSounds.GameSounds.ShipMovePlayer);
    //        }
    //        else
    //        {
    //            SoundManager.PlaySound(ScriptableSounds.GameSounds.ShipMoveEnemy);
    //        }

    //    }


    //    public void EndTurn() // When the end of turn button is pressed, delete the existing turn timer, reset movement and make the missiles move
    //    {
    //        if (CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnShooting || CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnMoving)
    //            return;
    //        CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_End);
    //    }

    //    void LaunchMissiles(SpaceshipBase Sender, ScriptableGameEvents.TurnPhase NextTurn) // For each unmoved missile, moves them
    //    {

    //        GameObject[] Missiles = GameObject.FindGameObjectsWithTag("Missile");
    //        if (Missiles.Length == 0)
    //        {
    //            CallEvent(NextTurn);
    //        }
    //        else
    //        {
    //            foreach (GameObject p in Missiles)
    //            {
    //                StartCoroutine(MissileCorourtine(p, Sender, NextTurn));
    //            }
    //        }
    //    }
    //    IEnumerator MissileCorourtine(GameObject MissileObject, SpaceshipBase Sender, ScriptableGameEvents.TurnPhase NextTurn) // Animates the moving of missiles at the end of each turn
    //    {

    //        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //        GameObject Missile = MissileObject.transform.GetChild(0).gameObject;
    //        LineRenderer Path =  MissileObject.transform.GetComponentInChildren<LineRenderer>();
    //        float Count = 0;
    //        while (Count < 1)
    //        {
    //            int CurrentPos = (int)Mathf.Lerp(0, Path.positionCount, Count);
    //             Missile.transform.position = Path.GetPosition(CurrentPos);
    //            for(int i = 0; i < CurrentPos; i++)
    //            {
    //                Path.SetPosition(i, Path.GetPosition(CurrentPos));
    //            }

    //            yield return new WaitForSeconds(myGrid.TransitionSpeed * Time.deltaTime); //Count is the amount of time in seconds that you want to wait.
    //            Count += myGrid.TransitionSpeed * Time.deltaTime;
    //            foreach(SpaceshipBase p in SpaceshipBases)
    //            {
    //                if (p != Sender)
    //                {
    //                    if (Vector3.Distance(p.ShipPrefab.transform.position, Missile.transform.position) < 0.5f)
    //                    {
    //                        p.ShipPrefab.GetComponent<PlayerMovement>().DestroyMe();
    //                    }
    //                }
    //            }
    //        }
    //        Destroy(MissileObject);

    //        CallEvent(NextTurn);
    //        yield return null;

    //    }






    //    void PositionShips(List<SpaceshipBase> TheseShips) // moves ships to the point they should be on, smoothly
    //    {
    //        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //        foreach (SpaceshipBase p in TheseShips)
    //        {

    //          //  p.ShipPrefab.transform.LookAt(gridSettings.SetPosition(p.CurrentNode.Column, -0.5f)); //Vector3.zero   );

    //            if (p.ShipPrefab.transform.localPosition == Vector3.zero)
    //            {
    //            }
    //            else if(Vector3.Distance(p.ShipPrefab.transform.localPosition, Vector3.zero) < 1.0f)
    //            {
    //             //   FindSelectableNodes(p.Movement * myGrid.Size, p);
    //                float MovementTime = myGrid.TransitionSpeed * Time.deltaTime;
    //                p.ShipPrefab.transform.localPosition = Vector3.MoveTowards(p.ShipPrefab.transform.localPosition, Vector3.zero, MovementTime);


    //            }
    //            else
    //            {
    //                float MovementTime = Vector3.Distance(p.ShipPrefab.transform.localPosition, Vector3.zero) * myGrid.TransitionSpeed * Time.deltaTime;
    //                p.ShipPrefab.transform.localPosition = Vector3.MoveTowards(p.ShipPrefab.transform.localPosition, Vector3.zero, MovementTime);
    //            }


    //        }
    //    }




    //    public void ReadyPoints()// When transitioning between modes, the points need time to settle before calculating where the active nodes should be
    //    {
    //        if(CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnMoving)
    //        {
    //            CallEvent(ScriptableGameEvents.TurnPhase.Transition);

    //            StartCoroutine("Reset", (0));
    //        }
    //        else
    //        {
    //            //  CallEvent(ScriptableGameEvents.TurnPhase.Transition);

    //            ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //            StartCoroutine("Reset", (myGrid.TransitionSpeed * 1.0f));
    //        }
    //    }
    //    IEnumerator Reset(float Count) // This function waits for the appropriate time before allowing the selection to happen
    //    {
    //        yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.

    //        if (Count == 0)
    //        {
    //            ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //            yield return new WaitForSeconds(myGrid.TransitionSpeed * 2.0f);

    //            CallEvent(ScriptableGameEvents.TurnPhase.EnemyTurnShooting);

    //        }
    //        else
    //        {
    //            ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //            if (CurrentTurn == ScriptableGameEvents.TurnPhase.PlayerTurn_Moving || CurrentTurn == ScriptableGameEvents.TurnPhase.PlayerTurn_Start)
    //            {

    //                CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Moving);
    //          //      FindSelectableNodes(SpaceshipBases[0].Movement * myGrid.Size, SpaceshipBases[0]);


    //            }
    //            else
    //            {
    //                CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting);
    //           //     FindSelectableNodes(SpaceshipBases[0].Reach * myGrid.Size, SpaceshipBases[0]);
    //            }


    //        }

    //        yield return null;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

    //    //    if (NumOfPoints == myGrid.Columns * myGrid.Rows)
    //     //       PositionPoints(MyPoints);
    //    //    else
    //    //        DeinitialisePoints(MyPoints);

    //        //foreach (SpaceshipBase p in SpaceshipBases) // if there is only one ship remaining (ie, player has won), return to the menu
    //        //{
    //        //    if (p.ShipPrefab == null)
    //        //    {
    //        //        SpaceshipBases.Remove(p);

    //        //        if (SpaceshipBases[0].ShipType != 0)
    //        //        {
    //        //            SceneManager.LoadScene(0);
    //        //        }

    //        //        return;
    //        //    }
    //        //}



    //        PositionShips(SpaceshipBases);




    //        UpdateByEvent();

    //        // Handles inputs
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            SceneManager.LoadScene(0);
    //        }

    //    }
}
