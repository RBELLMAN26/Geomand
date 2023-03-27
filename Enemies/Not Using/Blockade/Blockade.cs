using UnityEngine;

//The purpose of the blockade is to have a higher health rate but be alot slower
public class Blockade : MonoBehaviour
{

    //Manages The Controls And Information;
    public EnemyInfo eI;
    public EnemyController eC;

    public int numberOfSplits = 2;
    public GameObject miniBlockades; //Need To Create Different Sizes but will Be that 5 minis will be the max.

    //When Death Occurs To Activate Split Block
    void SplitBlock()
    {
        for (int i = 0; i < numberOfSplits; i++)
        {

        }
    }

}
