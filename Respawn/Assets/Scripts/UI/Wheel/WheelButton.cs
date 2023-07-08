using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelButton : MonoBehaviour
{
    public BonusBlock bonusBlock;
    public Image image;

    public void SetNewBonus()
    {
        bonusBlock.mainWheel.GetComponent<MainWheel>().CloseAllWheel();
        image.sprite = gameObject.GetComponent<Image>().sprite;
        image.color = gameObject.GetComponent<Image>().color;

        bonusBlock.SelectedSprite = image;

        bonusBlock.bFinished = bonusBlock.SelectedSprite.sprite.name == bonusBlock.WantedSprite.name;
    }
}
