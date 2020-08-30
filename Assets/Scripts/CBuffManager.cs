using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBuffManager : MonoBehaviour
{
    private static int _numProbAB = 0;
    public static int NumProbAB
    {
        get => _numProbAB;
        set
        {
            _numProbAB = value;
            if (_numProbAB > 0)
            {
                GameObject.Find("Buffs").transform.Find("ProbAB").gameObject.SetActive(true);
                GameObject.Find("Buffs").transform.Find("ProbAB/Text").GetComponent<Text>().text =
                    value.ToString();
            }

            if (_numProbAB <= 0)
            {
                GameObject.Find("Buffs").transform.Find("ProbAB").gameObject.SetActive(false);
                _numProbAB = 0;
            }
        }
    }

    private static int _numProbCDE = 0;

    public static int NumProbCDE
    {
        get => _numProbCDE;
        set
        {
            _numProbCDE = value;
            if (_numProbCDE > 0)
            {
                GameObject.Find("Buffs").transform.Find("ProbCDE").gameObject.SetActive(true);
                GameObject.Find("Buffs").transform.Find("ProbCDE/Text").GetComponent<Text>().text =
                    value.ToString();
            }

            if (_numProbCDE <= 0)
            {
                GameObject.Find("Buffs").transform.Find("ProbCDE").gameObject.SetActive(false);
                _numProbCDE = 0;
            }
        }
    }
    
    //TODO : 화면 오른쪽 위에서 택배왔습니다 이런거 뜨면서 누르면 마일리지버프, 골드버프, 혹은 재화를 얻을 수 있게 해야한다.
}
