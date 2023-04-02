using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpExp : MonoBehaviour {
    public Slider hp_slider;
    public Slider exp_slider;
    public Text hp_text;
    public Text exp_text;
    public GameObject player;
    void Start()
    {
       hp_slider = GameObject.Find("Slider_hp").GetComponent<Slider>();
       exp_slider = GameObject.Find("Slider_exp").GetComponent<Slider>();
    }
    void Update()
    {
        hp_slider.maxValue = player.GetComponent<Player>().Max_Hp;
        exp_slider.maxValue = player.GetComponent<Player>().Exp;
        hp_slider.value = player.GetComponent<Player>().Hp;
        exp_slider.value = player.GetComponent<Player>().Cur_Exp;
        hp_text.text = (player.GetComponent<Player>().Hp.ToString() + "/" + player.GetComponent<Player>().Max_Hp.ToString());
    }
}
