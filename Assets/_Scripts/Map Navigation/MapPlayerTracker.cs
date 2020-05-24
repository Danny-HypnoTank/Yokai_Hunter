using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        [SerializeField]
        private GameObject restText;
        [SerializeField]
        private ScriptableObjectPlayerStats playerStats;

        [SerializeField]
        private GameObject[] mysteryOptions;
        private int newMystery;
        [SerializeField]
        private GameObject treasureOptions;
        [SerializeField]
        private GameObject storeOptions;
        [SerializeField]
        private GameObject[] storeCards;


        private void Awake()
        {
            Instance = this;
            lockAfterSelecting = false;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    restText.SetActive(false);
                    mapManager.SaveMap();
                    SceneManager.LoadScene(UnityEngine.Random.Range(5, 8));
                    break;
                case NodeType.EliteEnemy:
                    restText.SetActive(false);
                    mapManager.SaveMap();
                    SceneManager.LoadScene(UnityEngine.Random.Range(8,10));
                    break;
                case NodeType.RestSite:
                    restText.SetActive(true);
                    playerStats.curHP = playerStats.maxHP;
                    break;
                case NodeType.Treasure:
                    restText.SetActive(false);
                    treasureOptions.SetActive(true);
                    break;
                case NodeType.Store:
                    restText.SetActive(false);
                    for (int i = 0; i < storeCards.Length; i++)
                    {
                        storeCards[i].SetActive(true);
                    }
                    storeOptions.SetActive(true);
                    break;
                case NodeType.Boss:
                    restText.SetActive(false);
                    mapManager.SaveMap();
                    SceneManager.LoadScene(10);
                    break;
                case NodeType.Mystery:
                    newMystery = UnityEngine.Random.Range(0, mysteryOptions.Length);
                    restText.SetActive(false);
                    mysteryOptions[newMystery].SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}