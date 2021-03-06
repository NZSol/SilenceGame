using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField]
    GameObject Player = null;

    CharMovement playerMover = null;


    [Range(-50, 50)]
    public float sliderX, sliderY;

    public Vector2 sliderPos = Vector2.zero;
    
    RectTransform transformer = null;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = Player.GetComponent<CharMovement>();
        sliderX = 0;
        sliderY = 0;
        transformer = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderX = transformer.anchoredPosition.x;
        sliderY = transformer.anchoredPosition.y;

        sliderX = Mathf.Clamp(sliderX, -50, 50);
        sliderY = Mathf.Clamp(sliderY, -50, 50);

        sliderPos = new Vector2(sliderX, sliderY);
        if (sliderPos.magnitude > 50)
        {
            sliderPos = sliderPos.normalized * 50;
        }
        Vector2 ValToSend = sliderPos / 50;
        playerMover.UIMoveInput(ValToSend);
    }


}
