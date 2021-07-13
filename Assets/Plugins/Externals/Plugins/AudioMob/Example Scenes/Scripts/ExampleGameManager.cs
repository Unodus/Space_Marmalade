using System;
using System.Collections;
using System.Globalization;
using AudioMob.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.AudioMob.Example_Scenes.Scripts
{
    /// <summary>
    /// This class demonstrates a few aspects of the AudioMob SDK:
    ///  - using a single prefab
    ///  - error handling
    ///  - callback handling
    /// </summary>
    public class ExampleGameManager : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private BannerAdClient adClient;

        /// <summary>
        /// This text GUI displays information and error messages
        /// </summary>
        [SerializeField]
        private Text infoText;

        [SerializeField]
        private Text infoText2;

        [SerializeField]
        private Text errorText;

        [SerializeField]
        private Button playButton;
#pragma warning restore 0649

        private float adRequestTime;

        private const int TimeDelayMultiplier = 3;
        const int MaxRetries = 3;
        private int retryCount = 0;

        const float secondsAdWasAttemptedToBeRetrievedOver = ((float) MaxRetries / 2) *
                                                             ((2 * TimeDelayMultiplier) +
                                                              ((MaxRetries - 1) * TimeDelayMultiplier));


        private ErrorSubState errorState = ErrorSubState.None;

        /// <summary>
        /// register for playback events from the AudioMob SDK
        /// </summary>
        private void Awake()
        {
            adClient.AdErrorEvent += AdClient_ErrorEvent;
            adClient.AdPlaybackEvent += AdClient_PlaybackEvent;
            adClient.AdAvailable += AdClient_AdAvailableEvent;
            adClient.AdRequested += AdClient_AdRequestedEvent;

            // When developing the SDK, some errors do not send an event.
            // This requires additional work by parsing error logs from Unity.
            Application.logMessageReceived += LogCallback;
        }

        private void Start()
        {
            playButton.enabled = false;
            adClient.RequestAd();
        }

        private void OnDestroy()
        {
            adClient.AdErrorEvent -= AdClient_ErrorEvent;
            adClient.AdPlaybackEvent -= AdClient_PlaybackEvent;
            adClient.AdAvailable -= AdClient_AdAvailableEvent;
            adClient.AdRequested -= AdClient_AdRequestedEvent;
            Application.logMessageReceived -= LogCallback;
        }

        public void ResetInfoString()
        {
            infoText.text = "...";
        }

        /// <summary>
        /// Some errors that occur in  the Unity engine don't send an event; the only way it seems to track them is parse the error
        /// string.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        void LogCallback(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Error)
            {
                // More logic would go here to parse the string for specific error conditions.
                // e.g "[AudioMobSDK] No Internet Connection".
                Text textObject = errorText != null ? errorText : infoText;
                textObject.text = condition;

                Debug.LogWarning("ExampleGameManager, LogCallback " + condition);

                if (infoText2 != null)
                {
                    infoText2.text = "";
                }
            }
        }

        /// <summary>
        /// Set the error state and retry the ad request if there is an error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void AdClient_ErrorEvent(object sender, BasicAdClient.AdErrorEventArgs eventArgs)
        {
            Debug.LogWarning("ExampleGameManager, AdClient_ErrorEvent: " + eventArgs.ErrorState);

            errorState = eventArgs.ErrorState;
            // Add additional code here to handle the error case.
            // E.g.  check if the string contains "Internet"

            Text textObject = errorText != null ? errorText : infoText;

            if (string.IsNullOrEmpty(eventArgs.Error))
            {
                textObject.text = eventArgs.ErrorState.ToString();
            }
            else
            {
                textObject.text = eventArgs.Error;
            }

            if (infoText2 != null)
            {
                infoText2.text = "";
            }

            if (eventArgs.ErrorState == ErrorSubState.noAdsAvailable)
            {
                RetryAdRequest();
            }
        }

        /// <summary>
        /// Set info text and request the next ad when a Playback Event has been received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void AdClient_PlaybackEvent(object sender, BasicAdClient.AdPlaybackEventArgs eventArgs)
        {
            errorState = ErrorSubState.None;
            // IsPlaying == false means the ad has stopped playing
            if (eventArgs.IsPlaying == false)
            {
                infoText.text = eventArgs.PlaybackCompleted ? "Audio ad has finished" : "User stopped the ad";
            }
            else
            {
                // we are starting playback and should queue the next advert
                adClient.RequestAd();
            }

            if (infoText2 != null)
            {
                infoText2.text = "";
            }


        }

        /// <summary>
        /// When an ad has been loaded, this callback gets invoked.
        /// The loading process is:
        /// - load XML manifest
        /// - loads assets: audio ad & banner images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void AdClient_AdAvailableEvent(object sender, EventArgs eventArgs)
        {
            errorState = ErrorSubState.None;

            infoText.text = "Ad available";
            playButton.enabled = true;
            float duration = Time.time - adRequestTime;
            Debug.Log("Load time: " + duration.ToString(CultureInfo.CurrentCulture));
            if (infoText2 != null)
            {
                infoText2.text = "Load time: " + duration.ToString("0.00");

                // reset any displayed error messages
                if (errorText != null)
                {
                    errorText.text = "";
                }
            }
        }

        /// <summary>
        /// This handler is called when an ad request has been made.  A few seconds later the audio ad & assets should
        /// be downloaded.  Another event occurs when that has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void AdClient_AdRequestedEvent(object sender, EventArgs eventArgs)
        {
            infoText.text = "Ad requested";
            playButton.enabled = true;
            adRequestTime = Time.time;

            if (infoText2 != null)
            {
                infoText2.text = "";
            }
        }


        /// <summary>
        /// If we haven't already retried making an audio ad request using an increasing time delay, do so.
        ///
        /// </summary>
        private void RetryAdRequest()
        {
            if (retryCount < MaxRetries)
            {
                StartCoroutine(ExecuteAdRequestAfterTime());
            }
        }

        /// <summary>
        /// Using a co-routine to make continual ad requests until either maximum number of retries has happened or
        /// an audio ad is successfully downloaded.
        /// </summary>
        /// <returns></returns>
        IEnumerator ExecuteAdRequestAfterTime()
        {
            while (retryCount < MaxRetries)
            {
                retryCount++;
                float delay = retryCount * TimeDelayMultiplier;
                Debug.Log($"Waiting for {delay} seconds before requesting ad again");
                yield return new WaitForSeconds(delay);
                adClient.RequestAd();
            }

            if (errorState != ErrorSubState.None)
            {
                Debug.LogWarning("Attempted to re-request Ad " + retryCount + " times over " +
                                 secondsAdWasAttemptedToBeRetrievedOver +
                                 " seconds with no success, discontinuing retries.");
            }
        }
    }
}