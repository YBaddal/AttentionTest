using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Game1051 : MonoBehaviour
{
    Question question;
    public int level;
    public int point;
    public List<string> levelTexts;
    public List<float> levelTimes;
    public AudioSource source;
    public AudioClip correctSound, wrongSound;
    public List<WordLine> wordLines;
    public List<string> sentence;
    public List<Toggle> correctToggles, selectedToggles;

    private void OnEnable()
    {
        question = GetComponent<Question>();
        selectedToggles = new List<Toggle>();
        correctToggles = new List<Toggle>();
        Init();
        ResetLevel();
    }
    void EarnPoint()
    {
        question.point += point;
    }
    public void Init()
    {
        level = question.level;
        question.tutorialText = levelTexts[level];
        question.Init();
        question.maxQuestionTime = levelTimes[level];
        question.questionTime = 1;
    }
    void SetLevel()
    {
        switch (level)
        {
            case 0:
                PrepareLevel(4);
                break;
            case 1:
                PrepareLevel(5);
                break;
            case 2:
                PrepareLevel(6);
                break;
            case 3:
                PrepareLevel(8);
                break;
            default:
                PrepareLevel(5);
                break;
        }
    }
    void PrepareLevel(int correctCount)
    {
        int rndSentence = Random.RandomRange(0, sentence.Count);

        var splitted = sentence[rndSentence].Split(' ');

        int i = 0, j = 0, w = 0, posX = 0;
        List<int> selected = new List<int>();

        while (selected.Count < correctCount)
        {
            int rnd = Random.RandomRange(0, splitted.Length);
            if (!selected.Contains(rnd) && splitted[rnd].Length > 4)
                selected.Add(rnd);
        }

        while (i < splitted.Length)
        {
            RectTransform rect = wordLines[w].words[j].GetComponent<RectTransform>();
            Toggle word = wordLines[w].words[j];
            if (j < wordLines[w].words.Count)
            {
                word.gameObject.SetActive(true);
                if (selected.Contains(i))
                {
                    correctToggles.Add(word);
                    word.GetComponentInChildren<Text>().text = HarfleriKaristir(splitted[i]);
                }
                else
                    word.GetComponentInChildren<Text>().text = splitted[i];

                int size = 11;
                if (splitted[i].Length < 3)
                    size = 15;
                else if (splitted[i].Length > 12)
                    size = 9;
                else if (splitted[i].Length > 8)
                    size = 10;
                else if (splitted[i].Length > 6)
                    size = 9;
             


                rect.sizeDelta = new Vector2(splitted[i].Length * size, rect.sizeDelta.y);
                posX += splitted[i].Length * size;

                if (posX < 1100)
                {
                    i++;
                    j++;
                }
                else
                {
                    word.gameObject.SetActive(false);
                    j = 0;
                    w++;
                    posX = 0;
                }

                
            }
            else
            {
                j = 0;
                w++;
                posX = 0;
            }
        }
    }
    public void CheckAnswer()
    {
        int correctCount = 0;
        for (int i = 0; i < selectedToggles.Count; i++)
            if (correctToggles.Contains(selectedToggles[i]))
                correctCount++;

        if (correctCount == correctToggles.Count)
        {
            EarnPoint();
            source.PlayOneShot(correctSound);
            question.questionTime++;
            Invoke("ResetLevel", 0.25f);
        }
    }

    public void SelectToggle(Toggle selected)
    {
        if (selectedToggles.Contains(selected))
            selectedToggles.Remove(selected);
        else
            selectedToggles.Add(selected);

        CheckAnswer();
    }
    void ResetLevel()
    {
        correctToggles.Clear();
        selectedToggles.Clear();
        SetLevel();
    }
    string HarfleriKaristir(string metin)
    {
        // Noktalama iþaretlerini temizle
        string temizlenmisMetin = Regex.Replace(metin, @"[^\w\s]", "");

        if (temizlenmisMetin.Length > 4)
        {
            char[] harfDizisi = temizlenmisMetin.ToCharArray();

            // Ýlk ve son harfi deðiþtirmemek için rastgele indeksleri belirle
            int ilkIndex = UnityEngine.Random.Range(1, temizlenmisMetin.Length - 2);
            int sonIndex = UnityEngine.Random.Range(ilkIndex + 1, temizlenmisMetin.Length - 1);

            // Harfleri yer deðiþtir
            char gecici = harfDizisi[ilkIndex];
            harfDizisi[ilkIndex] = harfDizisi[sonIndex];
            harfDizisi[sonIndex] = gecici;

            return new string(harfDizisi);
        }
        else
        {
            return metin;
        }
    }
}
    [System.Serializable]
public class WordLine
{
    public List<Toggle> words;
}