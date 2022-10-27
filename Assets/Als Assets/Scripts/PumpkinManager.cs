using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManager : MonoBehaviour
{
    public static PumpkinManager Instance;

    public List<PumpkinTrajectory> pumpkinList;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// returns -1 if no match found
    /// </summary>
    /// <param name="pumpkin"></param>
    /// <returns></returns>
    public int GetPumpkinIndex(PumpkinTrajectory pumpkin)
    {

        for(int i = 0; i < pumpkinList.Count; i++)
        {
            if(pumpkinList[i] == pumpkin)
            {
                return i;
            }
        }

        return -1;

    }


}
