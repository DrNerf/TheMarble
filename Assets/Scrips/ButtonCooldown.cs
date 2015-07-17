using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;

public class ButtonCooldown : MonoBehaviour 
{
    public int Cooldown;

    private Button Btn;
    private Image Img;
    ObscuredBool IsOnCooldown = false;
    private ObscuredFloat CooldownPassed = 0f;

	// Use this for initialization
	void Start () 
    {
        Btn = GetComponent<Button>();
        Img = GetComponent<Image>();

        Btn.onClick.AddListener(() => 
        {
            IsOnCooldown = true;
            Btn.interactable = false;
            CooldownPassed = 0f;
            Img.fillAmount = 0f;
        });
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsOnCooldown)
        {
            CooldownPassed += Time.deltaTime;
            if (CooldownPassed >= Cooldown)
            {
                IsOnCooldown = false;
                Btn.interactable = true;
                Img.fillAmount = 1;
            }
            else
            {
                Img.fillAmount = CooldownPassed / Cooldown;
            }
        }
	}
}
