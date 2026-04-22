/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-20 21:04:52
 * @modify date 2026-04-20 21:04:52
 * @desc [description]
 */

using UnityEngine;
using TMPro;

public class StartTextController : MonoBehaviour
{
[SerializeField] private float blinkSpeed = 2f;

    private TMP_Text text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        originalColor = text.color;
    }

    void Update()
    {
        //el alpha (transparencia) esta controlada variando entre 0 y 1
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
