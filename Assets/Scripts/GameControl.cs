using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public void CambiarPantalla()
    {
        SceneManager.LoadScene("BombermanLv1");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
