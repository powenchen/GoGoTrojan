using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLosePose : MonoBehaviour {

    private readonly string[] win_animations = { "PickupFast", "Throw", "Win", "Wave", "Yes", "Dance" };
    private readonly string[] lose_animations = { "Lose", "Lose2", "No" };
    private Animator[] m_animators;
    [SerializeField]
    private CameraLogic m_cameraLogic;

    public bool debug = false;
    public int myDebugRanking = -1;

    private void Start()
    {
        m_animators = FindObjectsOfType<Animator>();
        //Debug.Log("there are " + m_animators.Length + " animators");
        if (debug)
        {
            StaticVariables.ranking = myDebugRanking;
        }
        if (StaticVariables.ranking == 1)
        {
            int idx = Random.Range(0,win_animations.Length);
            for (int j = 0; j < m_animators.Length; j++)
            {
                m_animators[j].SetTrigger(win_animations[idx]);
            }
        }
        else
        {
            int idx = Random.Range(0, lose_animations.Length);
            for (int j = 0; j < m_animators.Length; j++)
            {
                m_animators[j].SetTrigger(lose_animations[idx]);
            }

        }
    }
    
}
