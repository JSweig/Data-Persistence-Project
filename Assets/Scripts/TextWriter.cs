using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Writes text letter by letter
public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;  //creates an instance of this class (Important if we have multiple dialogues going at once)

    private List<TextWriterSingle> textWriterSingleList;  //list of textwriters (see class below)

    private void Awake()
    {
        instance = this;  //sets a variable of THIS script
        textWriterSingleList = new List<TextWriterSingle>();  //creates the list of textwriters
    }


    //We have a static function so that it can be called from the controller
    public static void AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, int place)
    {
        instance.AddWriter(uiText, textToWrite, timePerCharacter, place);  //calls upon an AddWriter from THIS instance that calls other functions
    }
    private void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, int place)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, place));  //Adds a new writer function to the list
    }

    //Main update, called regardless so long as this script is active
    private void Update()
    {
        //cycles through the list each frame, calling the update in another class to update each
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            //Updates that text instance and tests if the text is complete upon return
            bool destroyInstance = textWriterSingleList[i].Update();

            //if the update returned true for that item in the list
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    //Represents a single TextWriter instance
    private class TextWriterSingle
    {
        TextMeshProUGUI uiText;
        string textToWrite;
        private int characterIndex;
        float timePerCharacter;
        private float timer;
        int place; 
        //class constructor
        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, int place)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.place = place;
            characterIndex = 0;
        }

        //returns true on complete. This Update doesn't really update once per frame... But it's called once per frame
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                //Continuously decreasing timer controls the timing of the text
                timer += timePerCharacter;
                characterIndex++;
                //Writes the text to writes from position 0 to current character index
                string text = place + "  " + textToWrite.Substring(0, characterIndex);
                //Adds invisible characters from the current character to the end of the text to write (so the characters don't move)
                text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    //If entire string is displayed returns true
                    return true;
                }

            }

            return false;
        }

    }
}