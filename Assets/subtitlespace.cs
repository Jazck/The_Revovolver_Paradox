using UnityEngine;
using PixelCrushers.DialogueSystem;
public class SeparateSubtitles : MonoBehaviour
{
    void OnConversationLine(Subtitle subtitle)
    {
        if (!string.IsNullOrEmpty(subtitle.formattedText.text))
        {
            subtitle.formattedText.text += "\n";
        }
    }
}