using UnityEngine;

//HiScoreFiller is for placing your Score into the GUI Panel at the game over screen
public class HiScoreFiller : MonoBehaviour
{
    public string fillerName;
    public int fillerNum;

    //Fill Name Will be Ran when the player hits Restart
    public void fillName()
    {

    }

    //Fill Number will be ran when the player hits restart
    public void fillNum()
    {

    }
    //Need Function to bring up the Keyboard for Android and later on for apple
    public void OpenKeyboard(string OS)
    {

    }
    //Function gets applied when user hits the restart button
    public void UpdateInfo()
    {
        fillName();
        fillNum();


    }
}
