using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    public Sprite ATKA;
    public Sprite ATKB;
    public Sprite ATKC;
    public Sprite ATKS;

    public Sprite DEFA;
    public Sprite DEFB;
    public Sprite DEFC;
    public Sprite DEFS;

    public Sprite SPEA;
    public Sprite SPEB;
    public Sprite SPEC;
    public Sprite SPES;

    public Sprite buyNormal;
    public Sprite buyGold;
    public Sprite buyATK;
    public Sprite buyDEF;
    public Sprite buySPE;

    public Sprite setCardImage(string imageName)
    {
        switch(imageName)
        {
            case "ATKA":
                return ATKA;
                break;
            case "ATKB":
                return ATKB;
                break;
            case "ATKC":
                return ATKC;
                break;
            case "ATKS":
                return ATKS;
                break;
            case "DEFA":
                return DEFA;
                break;
            case "DEFB":
                return DEFB;
                break;
            case "DEFC":
                return DEFC;
                break;
            case "DEFS":
                return DEFS;
                break;
            case "SPEA":
                return SPEA;
                break;
            case "SPEB":
                return SPEB;
                break;
            case "SPEC":
                return SPEC;
                break;
            case "SPES":
                return SPES;
                break;
            case "buyNormal":
                return buyNormal;
                break;
            case "buyGold":
                return buyGold;
                break;
            case "buyATK":
                return buyATK;
                break;
            case "buyDEF":
                return buyDEF;
                break;
            case "buySPE":
                return buySPE;
                break;
        }
        return ATKA;
        
    }
}
