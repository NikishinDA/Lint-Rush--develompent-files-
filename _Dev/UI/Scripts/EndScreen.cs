using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _Internal._Dev.Management.Scripts;

namespace _Internal._Dev.UI.Scripts
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private Slider toyPB;
        [SerializeField] private GameObject[] winTexts;
        private float endProgress;
        bool showToy = false;

        private void Awake()
        {
            nextButton.onClick.AddListener(OnNextButtonClick);
        }
        private void OnEnable()
        {
            winTexts[Random.Range(0, winTexts.Length)].SetActive(true);
            StartCoroutine(ProgressCor(1f));
        }

        private IEnumerator ProgressCor(float time)
        {
            float endProgress = PlayerPrefs.GetFloat("ToyProgress", 0);
            if (endProgress >= 1f)
            {
                endProgress = 1f;
                PlayerPrefs.SetFloat("ToyProgress", 0);
                PlayerPrefs.SetInt("ToyNumber", (VarSaver.ToyNumber + 1) % 3);
                PlayerPrefs.Save();
                showToy = true;
            }
            float startProgress = VarSaver.ToyOldProgress;
            toyPB.value = startProgress;
            yield return new WaitForSeconds(0.5f);
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                toyPB.value = Mathf.Lerp(startProgress, endProgress, t / time);
                yield return null;
            }
            toyPB.value = endProgress;
        }
        private void OnNextButtonClick()
        {
            int level = PlayerPrefs.GetInt("Level", 1);
            PlayerPrefs.SetInt("Level", level + 1);
            PlayerPrefs.Save();
            if (showToy) 
            {
                EventManager.Broadcast(GameEventsHandler.ShowToyEvent);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
