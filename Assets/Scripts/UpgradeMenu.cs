using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject panel;
    public Button[] buttons;
    public Text[] buttonLabels;
    public List<Upgrade> upgrades;

    Upgrade[] available;

    public void Awake()
    {
        //set menu off initially
        if (panel) panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
        available = new Upgrade[buttons.Length];
        List<Upgrade> choices = new List<Upgrade>(upgrades);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (choices.Count == 0)
            {
                buttons[i].gameObject.SetActive(false);
                continue;
            }
            int pick = Random.Range(0, choices.Count);
            available[i] = choices[pick];
            choices.RemoveAt(pick);

            buttonLabels[i].text = available[i].title + "\n" + available[i].description;

            int index =i;
            buttons[i].gameObject.SetActive(true);
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => Pick(index));
        }
    }

    public void Pick(int index)
    {
        panel.SetActive(false);
        GameManager.instance.UpgradePicked(available[index]);
    }
}
