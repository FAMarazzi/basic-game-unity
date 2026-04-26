/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-23 11:15:53
 * @modify date 2026-04-23 11:15:53
 * @desc [description]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // carga la escena número 1 (nivel 1)
            SceneManager.LoadScene(1);
        }
    }
}
