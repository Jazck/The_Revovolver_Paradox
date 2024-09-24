using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandStartMusic : SequencerCommand
    { // Rename to SequencerCommand<YourCommand>

        public void Start()
        {
            AudioList.Instance.StartMusic(AudioList.Music.FirstEncounter, true);
        }

        public void Update()
        {
            // Add any update code here. When the command is done, call Stop().
            // If you've called stop above in Awake(), you can delete this method.
        }

        public void OnDestroy()
        {
            // Add your finalization code here. This is critical. If the sequence is cancelled and this
            // command is marked as "required", then only Awake() and OnDestroy() will be called.
            // Use it to clean up whatever needs cleaning at the end of the sequencer command.
            // If you don't need to do anything at the end, you can delete this method.
        }

    }

}


/**/
