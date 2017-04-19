using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCardPanel : MonoBehaviour {

    public Text newCardName;
    public Image newCardImage;
    public Text newCardDescription;

    public IconController iconControl;

    public void showGetCardPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void setCardName(string name)
    {
        newCardName.text = name;
    }

    public void setCardImage(string imageName)
    {
        newCardImage.sprite = iconControl.setCardImage(imageName);
    }

    public void setCardDescription(string description)
    {
        newCardDescription.text = description;
    }

    public void dismissGetCardPanel()
    {
        this.gameObject.SetActive(false);
    }
}
