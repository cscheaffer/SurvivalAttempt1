using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHUDScreen : MonoBehaviour {

    public PlayerTargetting Player;
    public PlayerTargetting Enemy;
	// Use this for initialization
	void Start () {
		
	}
	
    public void DisplayTarget(Enemy target)
    {
        Enemy.Health.text = target.Health.ToString();
        Enemy.gameObject.SetActive(true);
    }
    public void DisableTarget()
    {
        Enemy.gameObject.SetActive(false);
    }
}
