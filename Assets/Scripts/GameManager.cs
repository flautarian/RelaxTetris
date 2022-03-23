using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    // walls container
    public Transform GOsContainer;
    // pools dictionary
    public  Dictionary<string, GameObjectsPool> GMPools = new Dictionary<string, GameObjectsPool>();

    private CanvasController.GameState gameState;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #region Pool GM Manager
        public void ReturnToPool(string name, GameObject gm){
            var nameFiltered = name.Replace("(Clone)", "").Trim();
            if(GMPools.ContainsKey(nameFiltered)){
                GMPools[nameFiltered].ReturnToPool(gm);
            }
        }

        public GameObject RequestAndExecuteGameObject(string nameAndPath, Vector3 position){
            if(GOsContainer == null){
                Debug.Log("finding container");
                var go = GameObject.FindGameObjectWithTag("SpecialGO/Walls");
                if(go != null)
                GOsContainer = go.transform;
            }
            var names = nameAndPath.Split('/');
            if(names.Length > 0){
                var name = names[names.Length-1];
                if(!GMPools.ContainsKey(name)){
                    //Debug.LogFormat("Creating pool for : {0}, actual Count {1}", name, GMPools.Count);
                    UnityEngine.Object PartObject = (UnityEngine.Object)Resources.Load(nameAndPath);
                    if(PartObject != null){
                        GameObjectsPool GmPool = new GameObjectsPool();
                        GmPool.objects = new Queue<GameObject>();
                        GameObject prefab = (GameObject)GameObject.Instantiate(PartObject);
                        prefab.transform.parent = GOsContainer.transform;
                        prefab.SetActive(false);
                        GmPool.prefab = prefab;
                        GMPools.Add(name, GmPool);
                        
                    }
                }
                GameObject go = GMPools[name].Get();
                if(go != null){
                    go.SetActive(true);
                    go.transform.position = position;
                    return go;
                }
            }
            return null;
        }
    #endregion

    public void OnChangeState(CanvasController.GameState newState){
        gameState = newState;
        switch(gameState){
            case CanvasController.GameState.MAINMENU:
            // go to main menu
            LoadScene(0);
            break;

            case CanvasController.GameState.RETRY:
            // retry game
            LoadScene(1);
            break;
        }
    }
    private void LoadScene(int scene){
        if(GMPools != null){
            foreach(KeyValuePair<string, GameObjectsPool>  pool in GMPools){
                Destroy(pool.Value);
            }
            GMPools.Clear();
        }
        SceneManager.LoadScene(scene);
    }
    public GameObject LoadPlayerPiece(){
        //TODO: diversificate for more than one piece type
        UnityEngine.Object PartObject = (UnityEngine.Object)Resources.Load("Prefabs/Piece_L");
        if(PartObject != null){
            GameObject prefab = (GameObject)GameObject.Instantiate(PartObject);
            return prefab;
        }
        return null;
    }

    public bool IsInGame(){
        return gameState == CanvasController.GameState.INGAME;
    }

}
