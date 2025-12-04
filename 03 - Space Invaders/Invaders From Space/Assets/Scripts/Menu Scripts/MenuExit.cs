using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuExit : MonoBehaviour
{
    public void ExitPressed()
    {
        SceneManager.LoadScene(0);
    }
}
