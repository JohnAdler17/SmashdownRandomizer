using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterRandomizer : MonoBehaviour
{
    [SerializeField] private List<FighterSO> listOfFighters = new List<FighterSO>();
    FighterSO currentFighter;

    [SerializeField] private Image fighterImage;
    [SerializeField] private TextMeshProUGUI fighterNameText;

    [SerializeField] private TextMeshProUGUI percentCompletedText;

    [SerializeField] private List<Image> remainingFighters = new List<Image>();

    [SerializeField] private bool randomizingMultipleFighters = false;
    [SerializeField] private List<Image> fighterImages = new List<Image>();
    [SerializeField] private Sprite defaultSprite;

    private float charactersCompleted = 0;
    private float numCharacters;

    void Start() {
      numCharacters = listOfFighters.Count;
      UpdateRemainingFightersList();
      if (!randomizingMultipleFighters) {
        CycleCharacters(4);
      }
    }

    private void UpdateRemainingFightersList() {
      for (int i = 0; i < remainingFighters.Count; i++) {
        if (listOfFighters.Count <= i) {
          remainingFighters[i].gameObject.SetActive(false);
        }
        else {
          remainingFighters[i].sprite = listOfFighters[i].fighterIcon;
        }
      }
    }

    public void ChooseRandomFighter() {
      if (listOfFighters.Count <= 0) {
        Debug.Log("No more fighters in list to choose from");
        return;
      }
      int randomIndex = Random.Range(0, listOfFighters.Count);
      //Debug.Log(randomIndex);
      //Debug.Log(listOfFighters.Count);
      currentFighter = listOfFighters[randomIndex];

      if(listOfFighters.Contains(currentFighter)) {
          listOfFighters.Remove(currentFighter);
      }

      charactersCompleted += 1;
    }

    public void DisplayCurrentFighter() {
      //Debug.Log(currentFighter.fighterName);
      fighterImage.sprite = currentFighter.fighterIcon;
      fighterNameText.text = currentFighter.fighterName;

      percentCompletedText.text = charactersCompleted.ToString() + " / " + numCharacters.ToString() + " : " + ((charactersCompleted/numCharacters) * 100).ToString("F0") + "%";
    }

    public void CycleCharacters(int cycles) {
      StartCoroutine(RandomCycler(cycles));
    }

    private float randomCycleDelay = 0.2f;
    IEnumerator RandomCycler(int numCycles) {
      fighterNameText.text = "";
      percentCompletedText.text = charactersCompleted.ToString() + " / " + numCharacters.ToString() + " : " + ((charactersCompleted/numCharacters) * 100).ToString("F0") + "%";
      for (int i = 0; i < numCycles; i++) {
        int randomIndex = Random.Range(0, listOfFighters.Count);
        if (listOfFighters.Count <= 0) {
          Debug.Log("No more fighters");
          fighterNameText.text = "Iron Man Complete!";
          yield break;
        }
        FighterSO tempFighter = listOfFighters[randomIndex];
        fighterImage.sprite = tempFighter.fighterIcon;
        yield return new WaitForSeconds(randomCycleDelay);
      }
      ChooseRandomFighter();
      DisplayCurrentFighter();
      UpdateRemainingFightersList();
    }

    public void CycleMultipleCharacters(int cycles) {
      ResetFighterImages();
      StartCoroutine(CycleMultipleFighters(cycles));
    }

    private IEnumerator CycleMultipleFighters(int numCycles) {
      for (int i = 0; i < fighterImages.Count; i++) {
        for (int j = 0; j < numCycles; j++) {
          int randomIndex = Random.Range(0, listOfFighters.Count);
          if (listOfFighters.Count <= 0) {
            Debug.Log("No more fighters");
            yield break;
          }
          FighterSO tempFighter = listOfFighters[randomIndex];
          fighterImages[i].sprite = tempFighter.fighterIcon;
          yield return new WaitForSeconds(randomCycleDelay);
        }
        ChooseRandomFighter();
        fighterImages[i].sprite = currentFighter.fighterIcon;
        UpdateRemainingFightersList();
        yield return new WaitForSeconds(randomCycleDelay);
      }
    }

    private void ResetFighterImages() {
      for (int i = 0; i < fighterImages.Count; i++) {
        fighterImages[i].sprite = defaultSprite;
      }
    }

}
