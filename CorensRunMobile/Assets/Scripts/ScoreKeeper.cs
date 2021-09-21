using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    [SerializeField] ScoreAndLayer score;
    [SerializeField] int dodgeBonus = 2;//Multiplier for narrow dodges

    //Updates score and request the UI to change display
    public void UpdateScore(bool narrowDodge)
    {
        if (narrowDodge) { score.score += 1 * score.layer * dodgeBonus; }
        else { score.score += 1 * score.layer; }
    }

}
