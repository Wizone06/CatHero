using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    private Player player;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5f,0);
    }
}
