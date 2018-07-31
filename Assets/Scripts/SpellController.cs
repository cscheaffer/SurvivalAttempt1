using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    public Spell spellQ;
    public Spell spellW;
    public Spell spellE;
    public Spell spellR;
    Spell ActiveSpell { get; set; }
    Spell GameSpell;
    Vector3 targetPosition;
    bool spellFired;

	// Use this for initialization
	void Start () {
        
	}

    private void Update()
    {
        if(spellFired)
        {
            GameSpell.transform.position = Vector3.MoveTowards(GameSpell.transform.position, targetPosition, Time.deltaTime * ActiveSpell.speed);
            if(GameSpell.transform.position == targetPosition)
            {
                Destroy(GameSpell.gameObject);
                spellFired = false;
            }
        }
    }
    public void CastSpell()
    {


    }
    public bool DisplaySpell(string spell)
    {
        if(spell == "Q")
        {
            ActiveSpell = spellQ;
        }
        else if(spell == "W")
        {
            ActiveSpell = spellW;
        }
        else if(spell == "E")
        {
            ActiveSpell = spellE;
        }
        else if(spell == "R")
        {
            ActiveSpell = spellR;
        }
        return true;
    }
    public bool RemoveSpell()
    {
        return false;
    }

}
