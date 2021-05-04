using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointFollow : MonoBehaviour
{


    [SerializeField] ScriptableGameEvents EventProfiler;
    [SerializeField] ScriptableElias EliasThemeNames;

    [SerializeField]
    GameObject EliasObject;
    EliasPlayer EliasComponent;


    [SerializeField]GameObject Projector; 


    [SerializeField]  GameObject PlayerPrefab; // Refference to all the Ship prefabs, for instantiation
    [SerializeField]  GameObject SmallEnemyPrefab;
    [SerializeField]  GameObject LargeEnemyPrefab;


    [SerializeField] PostSwitcher postSwitcher;

    [SerializeField] GameObject PointPrefab; // Refference for each navigable point on the map

    GridMath gridMath;          // Ref to the grid values
    MyProjector projectorMath; // Ref to the generated point values

    [SerializeField]
    GameObject TimerPrefab; // ref to the countdown slider
    Slider TimerObject; 

    int NumOfPoints; // The total amount of points in the grid

    int MaxGridX, MinGridX, ColumnsPerManifold;
    
    public enum NodeMode // Each point can have three states- whether it's empty, able to be selected, or has a ship on it
    {
        Empty,
        Selectable,
        Occupied
    }

    public class MyPoint // Each point uses this Struct. The lp-rp points are part of an unimplemented feature due to scope
    {
        public int Column, Row;
        public GameObject p;
        public NodeMode Mode;
    }

    public class SpaceshipBase // Ship Base - All ships inherit from this
    {
        public string Name;

        public GameObject ShipPrefab;
        public float Movement;
        public float MaxMovement;
        public float Reach;
        public float Shots;
        public int ShipType;

        public MyPoint CurrentNode;

        public void Initiate(MyPoint StartingNode, GameObject prefab) // Creates the Ship and makes sure all variables are set
        {
            
            ShipPrefab = Instantiate(prefab);

            ShipPrefab.name = "Ship:" + ShipType;
            ShipPrefab.tag = "Player";

            CurrentNode = StartingNode;
            ShipPrefab.transform.SetParent(CurrentNode.p.transform);
            ShipPrefab.transform.localPosition = Vector3.zero;

            ShipPrefab.transform.LookAt(Vector3.zero);

        }
    }


    public class Player : SpaceshipBase // The Players Ship. Has long reach and mobility
    {
        public Player(MyPoint StartingNode, GameObject prefab)
        {
            ShipType = 0;
            Movement = 2;
            MaxMovement = 2;
            Reach = 3;
            Shots = 1;

            Initiate(StartingNode, prefab);
        }
    }
    public class SmallEnemy : SpaceshipBase // The "smaller" enemy ship
    {
        public SmallEnemy(MyPoint StartingNode, GameObject prefab)
        {
            ShipType = 1;
            Movement = 2;
            MaxMovement = 2;
            Reach = 2;
            Shots = 1;

            Initiate(StartingNode, prefab); 
        }
    }
    public class BigEnemy : SpaceshipBase // The larger enemy ship, acts as a carrier for smaller ships
    {
        public BigEnemy(MyPoint StartingNode, GameObject prefab)
        {
            ShipType = 2;
            Movement = 5;
            MaxMovement = 5;
            Reach = 3;
            Shots = 1;

            Initiate(StartingNode, prefab);
        }
    }

    // At some point, convert these to the profiler ^


    public List<MyPoint> MyPoints = new List<MyPoint>();
    public List<SpaceshipBase> SpaceshipBases = new List<SpaceshipBase>();


    public ScriptableGameEvents.TurnPhase CurrentTurn;

    Slider InstantiateTimer() // Creates the timer at the start of each turn
    {
        GameObject Canvas = GameObject.FindGameObjectWithTag("Canvas");
        GameObject Timer = Instantiate(TimerPrefab, Canvas.transform);
        return Timer.GetComponent<Slider>();
    }

    void Start()// Instantiate grid and ships in random positions
    {
        if (!EliasComponent)
            EliasObject.TryGetComponent(out EliasComponent);

        CurrentTurn = ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting;


        projectorMath = Projector.GetComponent<MyProjector>();
        gridMath = projectorMath.gridMath;
        InitiatePoints();
        PositionPoints(MyPoints);

        ColumnsPerManifold = gridMath.Columns / gridMath.Manifolds;

        MaxGridX = 2 * ColumnsPerManifold;//(int)Mathf.Floor( Mathf.Lerp(0, gridMath.Columns, 0.5f ));
        MinGridX = 3 * ColumnsPerManifold;//(int)Mathf.Ceil(Mathf.Lerp(0, gridMath.Columns, 0.5f)); ;


        List<MyPoint> TempList = new List<MyPoint>( MyPoints);
       
        int RandomNumber;
        RandomNumber = Random.Range(0, TempList.Count);

        MyPoints[RandomNumber] = CheckNodeIsWithinBounds(MyPoints[RandomNumber]);


        SpaceshipBases.Add(new Player(MyPoints[RandomNumber], PlayerPrefab));
        TempList.RemoveAt(RandomNumber);

        RandomNumber = Random.Range(0, TempList.Count);


        MyPoints[RandomNumber] = CheckNodeIsWithinBounds(MyPoints[RandomNumber]);

        SpaceshipBases.Add(new SmallEnemy(MyPoints[RandomNumber], SmallEnemyPrefab));
        TempList.RemoveAt(RandomNumber);

        ReadyPoints();

    }

    void CallEvent(ScriptableGameEvents.TurnPhase turnPhase)
    {

        ScriptableGameEvents.EventSettings currentEvent = EventProfiler.GetEventByPhase(turnPhase);
        if (currentEvent == null) return;
        Debug.Log(currentEvent.Name);
        CurrentTurn = turnPhase;

        if (!string.IsNullOrEmpty( currentEvent.TriggerEliasProfiler ))
            EliasThemeNames.ChangeElias(EliasComponent, currentEvent.TriggerEliasProfiler);

        switch (turnPhase)
        {
            case ScriptableGameEvents.TurnPhase.EnemyTurnMoving:
                break;
            case ScriptableGameEvents.TurnPhase.EnemyTurnShooting:
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Moving:
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting:
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Start:
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_End:

                Destroy(GameObject.FindGameObjectWithTag("Timer"));

                SpaceshipBases[0].Movement = SpaceshipBases[0].MaxMovement;
                //CurrentTurn = ScriptableGameEvents.TurnPhase.Transition;
                LaunchMissiles(SpaceshipBases[0], ScriptableGameEvents.TurnPhase.EnemyTurnMoving);


                break;
            default:
                Debug.Log("Event Logic is not programmed");
                break;
        }

        }

    void AddEnemyShip()
    {
        MyPoint SpawnZone = GetRandNode();
        int RandomNumber = Random.Range(0, 2);
        if (RandomNumber == 0)
        {
            SpaceshipBases.Add(new SmallEnemy(SpawnZone, SmallEnemyPrefab));
        }
        else
        {
            SpaceshipBases.Add(new BigEnemy(SpawnZone, LargeEnemyPrefab));
        }
    }

    void TimeSelf()
    {
        if (TimerObject == null)
        {
            TimerObject = InstantiateTimer();
        }
        else
        {
            TimerObject.value -= Time.deltaTime;
            if (TimerObject.value <= 0)
            {
                postSwitcher.SetMove();

                EndTurn();
                Destroy(TimerObject.gameObject);
            }
        }
    }

    void UpdateByEvent()
    {
        ScriptableGameEvents.EventSettings currentEvent = EventProfiler.GetEventByPhase(CurrentTurn);
        if (currentEvent == null) return;



        switch (CurrentTurn)
        {
            case ScriptableGameEvents.TurnPhase.EnemyTurnMoving:
                    //do enemy stuff

                    foreach (SpaceshipBase p in SpaceshipBases)
                    {
                        if (p.ShipType == 1)
                        {
                            FindSelectableNodes(p.Movement * gridMath.Size, p);
                            ChangeNode(p, GetClosestNode(SpaceshipBases[0].ShipPrefab.transform.position, p, 0));
                            p.Movement = p.MaxMovement;
                        }

                        if (p.ShipType == 2)
                        {
                            FindSelectableNodes(p.Movement * gridMath.Size, p);
                            ChangeNode(p, GetClosestNode(GetRandNode().p.transform.position, p, 0));
                            p.Movement = p.MaxMovement;
                        }

                    }

                    postSwitcher.SetAim();
                    ReadyPoints();
                
                break;
            case ScriptableGameEvents.TurnPhase.EnemyTurnShooting:
               
                    //do enemy stuff

                    List<MyPoint> ShipsToMake = new List<MyPoint>();

                    foreach (SpaceshipBase p in SpaceshipBases)
                    {
                        if (p.ShipType == 1)
                        {
                            FindSelectableNodes(p.Reach * gridMath.Size, p);
                            p.ShipPrefab.GetComponent<PlayerMovement>().Shoot(GetClosestNode(SpaceshipBases[0].ShipPrefab.transform.position, p, 0).p.transform.position);
                            LaunchMissiles(p, ScriptableGameEvents.TurnPhase.PlayerTurn_Moving);

                        }
                        if (p.ShipType == 2)
                        {
                            FindSelectableNodes(p.Reach * gridMath.Size, p);
                            ShipsToMake.Add(p.CurrentNode);

                        }
                    }

                    foreach (MyPoint p in ShipsToMake)
                    {
                        SpaceshipBases.Add(new SmallEnemy(p, SmallEnemyPrefab));

                    }

                    postSwitcher.SetMove();
                    ReadyPoints();
                    FindSelectableNodes(SpaceshipBases[0].Reach * 100, SpaceshipBases[0]);

                    


        
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Moving:
                TimeSelf();

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    ChangeNode(SpaceshipBases[0], GetClosestNode(worldPosition, SpaceshipBases[0], 1));
                }

                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting:
                TimeSelf();

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;


                    if (Vector3.Distance(SpaceshipBases[0].ShipPrefab.transform.position, worldPosition) < SpaceshipBases[0].Reach)
                        {
                            MyPoint FirePoint = GetClosestNode(worldPosition, SpaceshipBases[0], 1);
                            if (FirePoint.p != null)
                            {
                                SpaceshipBases[0].ShipPrefab.GetComponent<PlayerMovement>().Shoot(FirePoint.p.transform.position);
                                postSwitcher.SetMove();
                                ReadyPoints();
                            }


                        }

                    
                }

                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_Start:
                break;
            case ScriptableGameEvents.TurnPhase.PlayerTurn_End:
                break;
            default:
                Debug.Log("Event Logic is not programmed");
                break;
        }

    }


    MyPoint CheckNodeIsWithinBounds(MyPoint NewPoint)// Moves a ship to a different part of the grid
    {
        if (NewPoint.Column < MinGridX)
        {
            foreach (MyPoint i in MyPoints)
            {
                if (i.Row == NewPoint.Row)
                {
                    if (i.Column == NewPoint.Column + ColumnsPerManifold)
                    {
                        if (NewPoint != i)
                        {
                            NewPoint = i;
               //        CheckNodeIsWithinBounds(NewPoint);
                        }
                    }
                }
            }
        }

        if (NewPoint.Column > MaxGridX)
        {
            foreach (MyPoint i in MyPoints)
            {
                if (i.Row == NewPoint.Row)
                {
                    if (i.Column == NewPoint.Column - ColumnsPerManifold)
                    {
                        if (NewPoint != i)
                        {
                            NewPoint = i;
                  //                                      CheckNodeIsWithinBounds(NewPoint);
                        }
                        
                    }
                }
            }
        }
        Debug.Log("After Coordinates:" + NewPoint.Column + " by " + NewPoint.Row + ". Between " + MinGridX + " and " + MaxGridX + " with a threshold of " + ColumnsPerManifold);
        //       

        if (NewPoint.Column >= MinGridX && NewPoint.Column <= MaxGridX)
        {
            return NewPoint;

        }
        else
        {
            if (NewPoint.Column < MinGridX)
            {
                foreach (MyPoint i in MyPoints)
                {
                    if (i.Row == NewPoint.Row)
                    {
                        if (i.Column == NewPoint.Column + ColumnsPerManifold)
                        {
                            if (NewPoint != i)
                            {
                                NewPoint = i;
                                //        CheckNodeIsWithinBounds(NewPoint);
                            }


                        }
                    }
                }
            }

            if (NewPoint.Column > MaxGridX)
            {
                foreach (MyPoint i in MyPoints)
                {
                    if (i.Row == NewPoint.Row)
                    {
                        if (i.Column == NewPoint.Column - ColumnsPerManifold)
                        {
                            if (NewPoint != i)
                            {
                                NewPoint = i;
                                //                                      CheckNodeIsWithinBounds(NewPoint);
                            }

                        }
                    }
                }
            }
            Debug.Log("After Coordinates:" + NewPoint.Column + " by " + NewPoint.Row);

            return NewPoint;
        }
            

    }

    void ChangeNode(SpaceshipBase Ship, MyPoint NewPoint)// Moves a ship to a different part of the grid
    { 
        
        if (NewPoint.p == null)
            return;

        NewPoint = CheckNodeIsWithinBounds(NewPoint);

        Ship.CurrentNode = NewPoint;
        Ship.ShipPrefab.transform.SetParent(NewPoint.p.transform);

        Ship.Movement -= Vector3.Magnitude(Ship.ShipPrefab.transform.localPosition) *0.25f* gridMath.Size;
        if (Ship.Movement <0)
        {
            Ship.Movement = 0;
        }
    }


    void ChangeNodeType(MyPoint Point, NodeMode newMode) // Used to calculate whether a node on the map is selectable
    {
        Point.Mode = newMode;

        if(newMode == NodeMode.Empty)
        {
            Point.p.GetComponent<SpriteRenderer>().material.color = new Color(1,1,1,0.1f);
        }
        else if (newMode == NodeMode.Occupied)
        {
            Point.p.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else if (newMode == NodeMode.Selectable)
        {
            Point.p.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

    }

    public void EndTurn() // When the end of turn button is pressed, delete the existing turn timer, reset movement and make the missiles move
    {
        if (CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnShooting || CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnMoving)
            return;
        CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_End);
    }

    void LaunchMissiles(SpaceshipBase Sender, ScriptableGameEvents.TurnPhase NextTurn) // For each unmoved missile, moves them
    {

        GameObject[] Missiles = GameObject.FindGameObjectsWithTag("Missile");
        if (Missiles.Length == 0)
        {
            CallEvent(NextTurn);
        }
        else
        {
            foreach (GameObject p in Missiles)
            {
                StartCoroutine(MissileCorourtine(p, Sender, NextTurn));
            }
        }
    }
    IEnumerator MissileCorourtine(GameObject MissileObject, SpaceshipBase Sender, ScriptableGameEvents.TurnPhase NextTurn) // Animates the moving of missiles at the end of each turn
    {
        GameObject Missile = MissileObject.transform.GetChild(0).gameObject;
        LineRenderer Path =  MissileObject.transform.GetComponentInChildren<LineRenderer>();
        float Count = 0;
        while (Count < 1)
        {
            int CurrentPos = (int)Mathf.Lerp(0, Path.positionCount, Count);
             Missile.transform.position = Path.GetPosition(CurrentPos);
            for(int i = 0; i < CurrentPos; i++)
            {
                Path.SetPosition(i, Path.GetPosition(CurrentPos));
            }

            yield return new WaitForSeconds(gridMath.TransitionSpeed * Time.deltaTime); //Count is the amount of time in seconds that you want to wait.
            Count += gridMath.TransitionSpeed * Time.deltaTime;
            foreach(SpaceshipBase p in SpaceshipBases)
            {
                if (p != Sender)
                {
                    if (Vector3.Distance(p.ShipPrefab.transform.position, Missile.transform.position) < 0.5f)
                    {
                        p.ShipPrefab.GetComponent<PlayerMovement>().DestroyMe();
                    }
                }
            }
        }
        Destroy(MissileObject);

        CallEvent(NextTurn);
        yield return null;

    }

    void InitiatePoints() // Instantiate Point gameobjects, based on gridmath data 
    {
        NumOfPoints = gridMath.Columns * gridMath.Rows;

        for (int i = 0; i < gridMath.Columns; i++)
        {

            MyPoint pp;
            for (int j = 0; j < gridMath.Rows; j++)
            {
                pp = new MyPoint();
                pp.Column = i;
                pp.Row = j;
                pp.p = Instantiate(PointPrefab);

                pp.p.transform.SetParent(transform);
                
                pp.p.name = i + ", " + j;
                pp.p.tag = "Point";
                ChangeNodeType(pp, NodeMode.Empty);

                pp.p.transform.position = gridMath.SetPosition(i, j);

                Vector3 Displacement = new Vector3((2 * ((1.25f + gridMath.Columns) / gridMath.Columns)) * gridMath.Size * gridMath.ScreenRatio.x, 0, 0);

                MyPoints.Add(pp);
            }
        }
    }
    
    void DeinitialisePoints(List<MyPoint> ThesePoints) // When a grid is resized/deleted, this function clears the gameobjects that have been instantiated
    {
        foreach (MyPoint p in ThesePoints)
        {
            Destroy(p.p);
        }
        ThesePoints.Clear();
        InitiatePoints();
    }

    void FindSelectableNodes(float Range, SpaceshipBase Player) // Makes nodes within a certain range selectable
    {
        foreach (MyPoint p in MyPoints)
        {
            
            if (p.Mode == NodeMode.Selectable)
            {
                ChangeNodeType(p, NodeMode.Empty);
            }
            
        }


        Collider[] hitColliders = Physics.OverlapSphere(Player.ShipPrefab.transform.position, Range);




        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Point")
            {
                foreach (MyPoint p in MyPoints)
                {
                    if (p.p == hitCollider.gameObject)
                    {
                        ChangeNodeType(p, NodeMode.Selectable);
                    }
                }

                
            }
        }

    }

    MyPoint GetClosestNode(Vector3 worldPosition, SpaceshipBase Player, float CutOff) // Function for finding the closest node, usually for when a node has been clicked
    {
        MyPoint ThisNode = Player.CurrentNode;
        GameObject ClosestPoint = MyPoints[0].p;
        float distance = Vector3.Distance(worldPosition, Player.ShipPrefab.transform.position);
        distance *= 2.1f;
        
        Collider[] hitColliders = Physics.OverlapSphere(Player.ShipPrefab.transform.position, distance);

       
        if (hitColliders.Length == 0)
        {
            return Player.CurrentNode;
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Point")
            {
                
                if(Vector3.Distance(hitCollider.transform.position, worldPosition) < Vector3.Distance(ThisNode.p.transform.position, worldPosition))
                {
                    ClosestPoint = hitCollider.gameObject;

                    foreach (MyPoint p in MyPoints)
                    {
                        if (p.p == ClosestPoint)
                        {
                            if (p.Mode == NodeMode.Selectable)
                            {
                                ThisNode = p;
                            }
                        }
                    }
                }
            }
        }

        if(CutOff == 0)
        {

            return ThisNode;
        }

        if (Vector3.Distance( ThisNode.p.transform.position, worldPosition ) < CutOff)
        {
            return ThisNode;
        }
        else
        {
            return Player.CurrentNode;
        }

    }

    MyPoint GetRandNode() // Function for finding the furest node, usually for ships trying to flee
    {
        int TempNumber = Random.Range(0, MyPoints.Count);
        MyPoint ThisNode = MyPoints[TempNumber];
        return ThisNode;
    }


    void PositionShips(List<SpaceshipBase> TheseShips) // moves ships to the point they should be on, smoothly
    {
        foreach (SpaceshipBase p in TheseShips)
        {

            p.ShipPrefab.transform.LookAt(gridMath.SetPosition(p.CurrentNode.Column, -0.5f)); //Vector3.zero   );

            if (p.ShipPrefab.transform.localPosition == Vector3.zero)
            {
            }
            else if(Vector3.Distance(p.ShipPrefab.transform.localPosition, Vector3.zero) < 1.0f)
            {
                FindSelectableNodes(p.Movement * gridMath.Size, p);
                float MovementTime = gridMath.TransitionSpeed * Time.deltaTime;
                p.ShipPrefab.transform.localPosition = Vector3.MoveTowards(p.ShipPrefab.transform.localPosition, Vector3.zero, MovementTime);


            }
            else
            {
                float MovementTime = Vector3.Distance(p.ShipPrefab.transform.localPosition, Vector3.zero) * gridMath.TransitionSpeed * Time.deltaTime;
                p.ShipPrefab.transform.localPosition = Vector3.MoveTowards(p.ShipPrefab.transform.localPosition, Vector3.zero, MovementTime);
            }


        }
    }


    void PositionPoints(List<MyPoint> ThesePoints) // moves points to positions they should be on, smoothly
    {
        foreach (MyPoint p in ThesePoints)
        {
            float MovementTime = Vector3.Distance(p.p.transform.position, gridMath.SetPosition(p.Column, p.Row)) * gridMath.TransitionSpeed * Time.deltaTime;

            Vector3 TargetPosition = Vector3.MoveTowards(p.p.transform.position, gridMath.SetPosition(p.Column, p.Row), MovementTime);
            Vector3 TargetSize = Vector3.one * gridMath.Size * 0.1f;
            p.p.transform.localScale = TargetSize;
            p.p.transform.position = TargetPosition;

        }
    }

    public void ReadyPoints()// When transitioning between modes, the points need time to settle before calculating where the active nodes should be
    {
        if(CurrentTurn == ScriptableGameEvents.TurnPhase.EnemyTurnMoving)
        {
            CallEvent(ScriptableGameEvents.TurnPhase.Transition);
//            CurrentTurn = ScriptableGameEvents.TurnPhase.Transition;
            StartCoroutine("Reset", (0));
        }
        else
        {
            CallEvent(ScriptableGameEvents.TurnPhase.Transition);

//            CurrentTurn = ScriptableGameEvents.TurnPhase.Transition;
            StartCoroutine("Reset", (gridMath.TransitionSpeed * 1.0f));
        }
    }
    IEnumerator Reset(float Count) // This function waits for the appropriate time before allowing the selection to happen
    {
        yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.
     
        if(Count == 0)
        {
            yield return new WaitForSeconds(gridMath.TransitionSpeed * 2.0f);

            CallEvent(ScriptableGameEvents.TurnPhase.EnemyTurnShooting);

//            CurrentTurn = ScriptableGameEvents.TurnPhase.EnemyTurnShooting;
        }
        else
        {
            if (gridMath.PolarActive)
            {
                CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Moving);
                FindSelectableNodes(SpaceshipBases[0].Movement * gridMath.Size, SpaceshipBases[0]);


            }
            else
            {
                CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting);
                FindSelectableNodes(SpaceshipBases[0].Reach * gridMath.Size, SpaceshipBases[0]);
            }


        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (NumOfPoints == gridMath.Columns * gridMath.Rows)
            PositionPoints(MyPoints);
        else
            DeinitialisePoints(MyPoints);
        
        //foreach (SpaceshipBase p in SpaceshipBases) // if there is only one ship remaining (ie, player has won), return to the menu
        //{
        //    if (p.ShipPrefab == null)
        //    {
        //        SpaceshipBases.Remove(p);

        //        if (SpaceshipBases[0].ShipType != 0)
        //        {
        //            SceneManager.LoadScene(0);
        //        }

        //        return;
        //    }
        //}

       

        PositionShips(SpaceshipBases);



        //handles the turn-changing logic

        if (CurrentTurn == ScriptableGameEvents.TurnPhase.PlayerTurn_Moving && !gridMath.PolarActive)
        {
            CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting);

//            CurrentTurn = ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting;
        }
        if (CurrentTurn == ScriptableGameEvents.TurnPhase.PlayerTurn_Shooting && gridMath.PolarActive)
        {
            CallEvent(ScriptableGameEvents.TurnPhase.PlayerTurn_Moving);

//            CurrentTurn = ScriptableGameEvents.TurnPhase.PlayerTurn_Moving;
        }



        UpdateByEvent();

        // Handles inputs
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
    }
}   
