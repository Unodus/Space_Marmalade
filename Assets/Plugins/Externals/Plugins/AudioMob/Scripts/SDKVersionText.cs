using AudioMob.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.AudioMob.Scripts
{
    public class SDKVersionText : MonoBehaviour
    {
        public Text sdkVersionText;

        // Start is called before the first frame update
        void Start()
        {
            sdkVersionText.text = BasicAdClient.VersionString;
        }
    }
}