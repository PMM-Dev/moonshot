using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapMaking : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _mapCandidate = new GameObject[3];
        [SerializeField]
        private GameObject _mapStart;
        [SerializeField]
        private GameObject _connectorCandidate;
        [SerializeField]
        private List<GameObject> _wholeMapOrder = new List<GameObject>();
        public List<GameObject> WholeMapOrder
        {
            get
            {
                return _wholeMapOrder;
            }            
        }
        private bool _isMapCreate;
        public bool IsMapCreate
        {
            get
            {
                return _isMapCreate;
            }
        }

        private int[] _mapFrequency = new int[20];//not two times more selected when first random check
        private int[] _mapFrequencyCheck = new int[20];// check the frequency when locating
        private int _mapCount;//count candidate map*2 including duplicate 
        private List<int> _mapOrderIndex = new List<int>();//random order maplist
        private int[] _stagePerMapCount = new int[3]{ 2, 3, 3 };
        private float[] _startPointY = new float[3]{ 26.7f, 230.6f, 503.8f };
        private float[] _connectorPointY = new float[3] { 60.6f, 264.8f, 537.8f };
        private float _mapYlength = 68.2f;
        private float _connectorYlength = 68.2f;
        private List<int> _connectOrderIndex = new List<int>();
        private bool[] _connectFrequency = new bool[30];
        private List<GameObject> _mapOrder = new List<GameObject>();
        private List<GameObject> _connectOrder = new List<GameObject>();

        public void CreateStage()
        {
            /*INITIALIZING*/
            _mapOrder.Clear();
            _wholeMapOrder.Clear();
            _connectOrderIndex.Clear();
            _connectOrder.Clear();
            for (int i = 0; i < 30; i++)
            {
                _connectFrequency[i] = false;
            }
            for (int i = 0; i<3;i++)
            {
                CreateMap(i);
            }
            /*INITIALIZING*/

            CreateConnector();
            Merge();
            _isMapCreate = true;
            BackgroundController.Instance.Initialize();
        }

        void CreateMap(int stagenum) 
        {
            /*INITIALIZING*/
            for(int i = 0; i<20;i++)
            {
                _mapFrequency[i] = 0;
                _mapFrequencyCheck[i] = 0;
                _mapOrderIndex.Clear();
            }
            /*INITIALIZING*/

            _mapCount = _mapCandidate[stagenum].transform.childCount;
            _mapOrder.Add(_mapStart.transform.GetChild(stagenum).gameObject);

            while (true)
            {
                if (_mapOrderIndex.Count == _stagePerMapCount[stagenum])
                    break;

                int ran = Random.Range(0, _mapCount / 2);

                if (_mapFrequency[ran] >= 2)
                    continue;

                _mapOrderIndex.Add(ran);
                _mapFrequency[ran]++;
            }

            //Code it after make map prefab
            for (int i = 0; i < _stagePerMapCount[stagenum]; i++)
            {
                int idx = _mapOrderIndex[i];
                Vector3 targetPosition = new Vector3(0, _startPointY[stagenum] + _mapYlength * (i + 1), 0);
                if (_mapFrequencyCheck[idx] == 0)
                {
                    GameObject game = _mapCandidate[stagenum].transform.GetChild(idx).gameObject;
                    game.transform.position = targetPosition;
                    _mapOrder.Add(game);
                    _mapFrequencyCheck[idx]++;
                }
                else if (_mapFrequencyCheck[idx] == 1)
                {
                    GameObject game = _mapCandidate[stagenum].transform.GetChild(idx + _mapCount / 2).gameObject;
                    game.transform.position = targetPosition;
                    _mapOrder.Add(game);
                    _mapFrequencyCheck[idx]++;
                }
                else
                    continue;
            }
        }
        void CreateConnector()
        {
            for(int i = 0; i<_mapOrder.Count;i++)
            {
                if(_mapOrder[i].gameObject.GetComponent<MapAttribute>().MapEnd == MapEndType.Left)
                {
                    _connectOrderIndex.Add(0);
                }
                else if(_mapOrder[i].gameObject.GetComponent<MapAttribute>().MapEnd == MapEndType.Middle)
                {
                    _connectOrderIndex.Add(1);
                }
                else if (_mapOrder[i].gameObject.GetComponent<MapAttribute>().MapEnd == MapEndType.Right)
                {
                    _connectOrderIndex.Add(2);
                }
            }

            for (int i = 0; i < _connectOrderIndex.Count; i++)
            {
                int index = 0;
                int Yindex = 0;
                if(0<=i && i<3)
                {
                    Yindex = i;
                    index = 0;
                }
                else if (3 <= i && i < 7)
                {
                    Yindex = i - 3;
                    index = 1;
                }
                else if (7 <= i && i < 11)
                {
                    Yindex = i - 7;
                    index = 2;
                }
                    
                int connector = _connectOrderIndex[i];
                int connectorcount = connector*8;
                Vector3 targetPosition = new Vector3(0, _connectorPointY[index] + _connectorYlength * Yindex, 0);
                while (_connectFrequency[connectorcount])
                {
                    connectorcount++;
                }
                _connectorCandidate.transform.GetChild(connectorcount).gameObject.transform.position = targetPosition;
                _connectOrder.Add(_connectorCandidate.transform.GetChild(connectorcount).gameObject);
                _connectFrequency[connectorcount] = true;
            }
        }

        void Merge()
        {
            for(int i = 0;i<_mapOrder.Count ;i++)
            {
                _wholeMapOrder.Add(_mapOrder[i]);
                _wholeMapOrder.Add(_connectOrder[i]);
            }
        }
    }

}
