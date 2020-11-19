using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaking : MonoBehaviour
{
    [SerializeField]
    private int stagePerMapCount;//need map per stage
    [SerializeField]
    private GameObject mapCandidate;
    [SerializeField]
    private float mapYlength;
    [SerializeField]
    private float startPointY;
    
    private int[] mapFrequency = new int[20];//not two times more selected when first random check
    private int[] mapFrequencyCheck = new int[20];// check the frequency when locating
    private int mapCount;//count candidate map*2 including duplicate 
    List<int> mapOrder = new List<int>();//random order map


    void Start()
    {
        mapCount = mapCandidate.transform.childCount; 
       
        while(true)
        {
            if (mapOrder.Count == stagePerMapCount)
                break;

            int ran = Random.Range(0, mapCount/2);

            if (mapFrequency[ran] >= 2)
                continue;

            mapOrder.Add(ran);
            mapFrequency[ran]++;
        }

        //Code it after make map prefab
        for(int i = 0;i< stagePerMapCount; i++)
        {
            int idx = mapOrder[i];
            Vector3 targetPosition = new Vector3(0, startPointY + mapYlength * (i + 1)+4.2f, 0);
            Debug.Log(targetPosition);
            if (mapFrequencyCheck[idx]==0)
            {
                GameObject game = mapCandidate.transform.GetChild(idx).gameObject;
                game.transform.position = targetPosition;
                mapFrequencyCheck[idx]++;
            }
            else if(mapFrequencyCheck[idx] == 1)
            {
                GameObject game = mapCandidate.transform.GetChild(idx+mapCount/2).gameObject;
                game.transform.position = targetPosition;
                mapFrequencyCheck[idx]++;
            }
            else
                continue;
        }
    }
}
