using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
namespace PixelCrushers.DialogueSystem.SequencerCommands
{
    public class AudioList : SequencerCommand
    {
        private enum SourceType
        {
            Music,
            Ambiance
        }

        public enum Music
        {
            Main,
            Menu,
            TheLightIsOn,
            FirstEncounter,
            SecondEncounter
        }

        public enum Ambiance
        {
            Rain,
            Tower
        }


        public static AudioList Instance;

        [SerializeField] private GameObject camera;

        [SerializeField] private AudioSource audioSourceOneShot;
        [Header("Music")] 
        [SerializeField] private AudioSource musicAudioSource0;
        [SerializeField] private AudioSource musicAudioSource1;
        [SerializeField] private AudioSource musicAudioSource2;

        [SerializeField] private float fadeMusicDuration = 2;
        private bool _playMusicOn1, _notMusicFirstCall;
        private float _targetMusicVolume;
        private AudioSource _currentMusicSource;

        [Header("Ambiance")] [SerializeField] private AudioSource ambianceAudioSource0;
        [SerializeField] private AudioSource ambianceAudioSource1;
        [SerializeField] private float fadeAmbianceDuration = 2;
        private bool _playAmbianceOn1, _notAmbianceFirstCall;
        private float _targetAmbianceVolume;
        private AudioSource _currentAmbianceSource;

        [Header("Global")] [SerializeField] private AudioClip mainTheme;
        [SerializeField] [Range(0, 1)] private float mainVolume;
        [SerializeField] private AudioClip menuTheme;
        [SerializeField] [Range(0, 1)] private float menuVolume;
        [SerializeField] private AudioClip theLightIsOn;
        [SerializeField] [Range(0, 1)] private float theLightIsOnVolume;
        [SerializeField] private AudioClip firstEncounter;
        [SerializeField] [Range(0, 5)] private float firstEncounterV;
        [SerializeField] private AudioClip secondEncounter;
        [SerializeField] [Range(0, 5)] private float secondEncounterV;
        [SerializeField] private AudioClip rainAmbiance;
        [SerializeField] [Range(0, 1)] private float rainAmbianceVolume;
        [SerializeField] private AudioClip windAmbiance;
        [SerializeField] [Range(0, 1)] private float windAmbianceVolume;
        [SerializeField] private AudioClip towerAmbiance;
        [SerializeField] [Range(0, 1)] private float towerV;


        [Header("UI")] public AudioClip uiClick;
        public AudioClip uiClick2;
        public AudioClip startClick;

        [Header("Player")] [SerializeField] private float walkDelay;
        [SerializeField] private bool isWalking = true;
        [Header("Combat")] public AudioClip staticPunch;
        [SerializeField] [Range(0, 1)] private float staticPunchV;
        public AudioClip movingPunch;
        [SerializeField] [Range(0, 1)] private float movingPunchV;
        public AudioClip hit;
        [SerializeField] [Range(0, 1)] private float hitV;
        public AudioClip damaged;
        [SerializeField] [Range(0, 1)] private float damagedV;
        public AudioClip death;
        [SerializeField] [Range(0, 1)] private float deathV;
        [Header("Hook")] public AudioClip hookSwing;
        [SerializeField] [Range(0, 1)] private float hookSwingV;
        [SerializeField] private float hookSwingD;
        public AudioClip hookLaunch;
        [SerializeField] [Range(0, 1)] private float hookLaunchV;
        public AudioClip hooked;
        [SerializeField] [Range(0, 1)] private float hookedV;
        public AudioClip pullpush;
        [SerializeField] [Range(0, 1)] private float pullpushV;
        public AudioClip hookReturn;
        [SerializeField] [Range(0, 1)] private float hookReturnV;
        [SerializeField] private bool canPlay = false;
        [Header("P&T")] public AudioClip pickUp;
        [SerializeField] [Range(0, 1)] private float pickUpV;
        public AudioClip andThrow;
        [SerializeField] [Range(0, 1)] private float andThrowV;
        public AudioClip putDown;
        [SerializeField] [Range(0, 1)] private float putDownV;


        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                StartMusic(Music.FirstEncounter, true);
                StartAmbiance(Ambiance.Rain, true);
            }
            else
            {
                StopMusic();
            }

        }

        private void Awake()
        {  
      
            if (Instance != null && Instance != this) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            if (audioSourceOneShot == null)
            {
                audioSourceOneShot = GetComponent<AudioSource>();
            }
        }

        public void StartMusic(Music music, bool loop)
        {
            if (_notMusicFirstCall) FadeOut(SourceType.Music);
            else _notMusicFirstCall = true;

            _currentMusicSource = _playMusicOn1 ? musicAudioSource1 : musicAudioSource0;
            _playMusicOn1 = !_playMusicOn1;

            switch (music)
            {
                case Music.Main:
                    _currentMusicSource.clip = mainTheme;
                    _targetMusicVolume = mainVolume;
                    break;
                case Music.TheLightIsOn:
                    _currentMusicSource.clip = theLightIsOn;
                    _targetMusicVolume = theLightIsOnVolume;
                    break;
                case Music.FirstEncounter:
                    _currentMusicSource.clip = firstEncounter;
                    _targetMusicVolume = firstEncounterV;
                    break;
                case Music.SecondEncounter:
                    _currentMusicSource.clip = secondEncounter;
                    _targetMusicVolume = secondEncounterV;
                    break;
                case Music.Menu:
                    _currentMusicSource.clip = menuTheme;
                    _targetMusicVolume = menuVolume;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(music), music, null);
            }

            _currentMusicSource.loop = loop;
            _currentMusicSource.Play();

            FadeIn(SourceType.Music);
        }

        public void StartAmbiance(Ambiance ambiance, bool loop)
        {
            if (_notAmbianceFirstCall) FadeOut(SourceType.Ambiance);
            else _notAmbianceFirstCall = true;

            _currentAmbianceSource = _playAmbianceOn1 ? ambianceAudioSource1 : ambianceAudioSource0;
            _playAmbianceOn1 = !_playAmbianceOn1;

            switch (ambiance)
            {
                case Ambiance.Rain:
                    _currentAmbianceSource.clip = rainAmbiance;
                    _targetAmbianceVolume = rainAmbianceVolume;
                    break;
                case Ambiance.Tower:
                    _currentAmbianceSource.clip = towerAmbiance;
                    _targetAmbianceVolume = towerV;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ambiance), ambiance, null);
            }

            _currentAmbianceSource.loop = loop;
            _currentAmbianceSource.Play();

            FadeIn(SourceType.Ambiance);
        }

        public void StopMusic()
        {
            FadeOut(SourceType.Music);
        }

        public void StopAmbiance()
        {
            FadeOut(SourceType.Ambiance);
        }

        private void FadeIn(SourceType type)
        {
            switch (type)
            {
                case SourceType.Music:
                    _currentMusicSource.DOFade(_targetMusicVolume, fadeMusicDuration);
                    break;
                case SourceType.Ambiance:
                    _currentAmbianceSource.DOFade(_targetAmbianceVolume, fadeAmbianceDuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void FadeOut(SourceType type)
        {
            switch (type)
            {
                case SourceType.Music:
                    _currentMusicSource.DOFade(0, fadeMusicDuration);
                    break;
                case SourceType.Ambiance:
                    _currentAmbianceSource.DOFade(0, fadeAmbianceDuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        //Other///
   

        public void UIClick()
        {
            PlayOneShot(uiClick, 1);
        }

        public void UIClick2()
        {
            PlayOneShot(uiClick2, 1);
        }

        public void StartClick()
        {
            PlayOneShot(startClick, 1);
            StopMusic();
        }

        //Combat///
        public void staticPunch_PlaySound()
        {
            PlayOneShot(staticPunch, staticPunchV);
        }

        public void movingPunch_PlaySound()
        {
            PlayOneShot(movingPunch, movingPunchV);
        }

        public void hit_PlaySound()
        {
            PlayOneShot(hit, hitV);
        }

        public void damaged_PlaySound()
        {
            PlayOneShot(damaged, damagedV);
        }

        public void death_PlaySound()
        {
            PlayOneShot(death, deathV);
        }

        //Hook///
        public void HookSwing_PlaySound()
        {
            StartCoroutine(WaitBeforeHookSwing(hookSwingD));
        }

        public void hookLaunch_PlaySound()
        {
            PlayOneShot(hookLaunch, hookLaunchV);
        }

        public void hooked_PlaySound()
        {
            PlayOneShot(hooked, hookedV);
            canPlay = true;
        }

        public void pullpush_PlaySound()
        {
            PlayOneShot(pullpush, pullpushV);
        }

        public void hookReturn_PlaySound()
        {
            if (canPlay)
            {
                PlayOneShot(hookReturn, hookReturnV);
                canPlay = false;
            }
        }

        //P&T///
        public void pickUp_PlaySound()
        {
            PlayOneShot(pickUp, pickUpV);
        }

        public void andThrow_PlaySound()
        {
            Debug.Log("Hey");
            PlayOneShot(andThrow, andThrowV);
        }

        public void drop_PlaySound()
        {
            Debug.Log("Hoe");
            PlayOneShot(putDown, putDownV);
        }

        public void PlayOneShot(AudioClip clip, float volumeScale)
        {
            audioSourceOneShot.PlayOneShot(clip, volumeScale);
        }

        //Delay///
        IEnumerator WaitBeforeHookSwing(float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayOneShot(hookSwing, hookSwingV);

        }

   

    }
}
