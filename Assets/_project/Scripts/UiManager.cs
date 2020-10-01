using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider SliderProgessQuest;
    public Image ImageObjectif;
    public Transform PlanetSpot;
  
    public float ScaleMax;
    private float _chronoScalePlanet;
    private GameObject _planetObject;
    public  static UiManager Instance;
    private bool _scalePlanet = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = Instance ?? this;
        GameManager.Instance.OnReadyEndQuest += OnReadyEndQuest;
        SetupUi();
    }

  

    // Update is called once per frame
    void Update()
    {
       
        if(_scalePlanet)
        {
            _chronoScalePlanet += Time.deltaTime;
            float scaleTime = _chronoScalePlanet / (GameManager.Instance.SelectedQuest.Duration*0.2f);
            _planetObject.transform.localScale = Vector3.one * Mathf.Lerp(0, ScaleMax, scaleTime);
        } 
    }


    private void OnReadyEndQuest()
    {
        _scalePlanet = true;
    }

    public void SetupUi()
    {
        ImageObjectif.material = GameManager.Instance.SelectedQuest.Planet.PlanetPicture;
        _planetObject = Instantiate(GameManager.Instance.SelectedQuest.Planet.PlanetModel, PlanetSpot.transform.position, Quaternion.identity, PlanetSpot.transform);
        _planetObject.transform.localScale = Vector3.zero;
    }

    public void UpdateSlider(float value)
    {
        SliderProgessQuest.value = Mathf.Min(value,1f);
    }

    //IEnumerator LerpCoroutine(float value,float start,float end)
    //{
    //    value = Mathf.Lerp()

    //    yield return new WaitForEndOfFrame();
    //}

}
