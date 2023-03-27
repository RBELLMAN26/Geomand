using Unity.Mathematics;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public bool debugMode = false;
    public PlayTouchManager pTM;
    public PUM pum;
    public bool[] activeTouch;
    public bool[] activeTurrets;
    public int[] assignedTurretTouchs = new int[4];
    
    public TowerInfo[] turrets;
    //public Transform[] missles;
    //public bool onlyBlue, onlyGreen;
    //public int currentColor;
    //public Vector2 mousePOS;
    // Start is called before the first frame update
    void Start()
    {
        activeTurrets = pum.activeTurrets;
        //DontDestroyOnLoad(turrets);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        activeTouch = pTM.isTouch;
        //UpdateTurrets();
    }
    public void UpdateTurrets()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            activeTurrets[i] = turrets[i].isActive;
            assignedTurretTouchs[i] = turrets[i].touchSelected;
        }
    }
    public void PurchasedTurret(int turret)
    {
        turrets[turret].gameObject.SetActive(true);
        turrets[turret].isActive = true;
    }
    public void DestroyedTurret(int turret)
    {
        pum.activeTurrets[turret] = false;
        pum.ScanTurrets();
        pum.UpdateCosts();
    }
    public void Revive()
    {
        if (turrets == null)
        {
            turrets = FindObjectsOfType<TowerInfo>();
            Debug.Log("Turrets are missing");
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            pum.activeTurrets[i] = true;
            turrets[i].gameObject.SetActive(true);
            turrets[i].isActive = true;
            activeTurrets[i] = turrets[i].isActive;
            turrets[i].GetComponent<TowerController>().Revive();
        }
        pum.ScanTurrets();
    }
    public void DisableSounds()
    {
        if (turrets == null)
        {
            turrets = FindObjectsOfType<TowerInfo>();
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<TowerController>().DisableSound();
        }
    }
    public void UpdateTurretAssignment(int Turret)
    { 
        switch (turrets[Turret].selectedColor)
        {
            //Green
            case 0:
                {
                    if (turrets[Turret].GetComponent<TowerInfo>().blueActive || debugMode)
                    {
                        turrets[Turret].GetComponent<TowerController>().ChangeTower(0, 1);
                        //turrets[Turret].selectedColor = 1;
                    }
                    if(!turrets[Turret].GetComponent<TowerInfo>().blueActive)
                    {
                        if(turrets[Turret].GetComponent<TowerInfo>().redActive)
                        {
                            turrets[Turret].GetComponent<TowerController>().ChangeTower(0, 2);
                        }
                    }
                    break;
                }
            //Blue
            case 1:
                {
                    if (turrets[Turret].GetComponent<TowerInfo>().redActive || debugMode)
                    {
                        //turrets[Turret].selectedColor = 2;
                        turrets[Turret].GetComponent<TowerController>().ChangeTower(1, 2);
                    }
                    else
                    {
                        //turrets[Turret].selectedColor = 0;
                        turrets[Turret].GetComponent<TowerController>().ChangeTower(1, 0);
                    }
                    break;
                }
            //Red
            case 2:
                {

                    //turrets[Turret].selectedColor = 0;
                    turrets[Turret].GetComponent<TowerController>().ChangeTower(2, 0);
                    break;
                }
        }
    }
}
