using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject m_reference;
    [SerializeField] private Texture m_imageDestination;
    [SerializeField] private Texture m_imageSource;
    [SerializeField] private GameObject m_textDestinationReference;
    [SerializeField] private GameObject m_textSourceReference;

    private void Awake()
    {
        DeleteChilds();
        CreateChilds();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeleteChilds();
            CreateChilds();
        }
    }


    private void DeleteChilds()
    {
        int childCount = transform.childCount;

        if (childCount == 0)
        {
            return;
        }

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    private void CreateChilds()
    {
        int blendModeCount = Enum.GetNames(typeof(BlendMode)).Length;
        float middle = (float)(blendModeCount - 1) / 2;

        GameObject cache = null;
        Material materialCache = null;

        for (int y = 0; y < blendModeCount; y++)
        {
            if (y == 0)
            {
                cache = Instantiate<GameObject>(m_textDestinationReference);
                cache.SetActive(true);
                cache.transform.SetParent(transform);
                cache.transform.localScale = Vector3.one;
                cache.transform.localPosition = new Vector3((-1 - middle) * 100 - 100, (y - middle) * -100 + 25, 0);
                cache.transform.localEulerAngles = new Vector3(0, 0, 30);

                cache.GetComponent<Text>().text = "Destination Factors";
            }

            cache = Instantiate<GameObject>(m_textDestinationReference);
            cache.SetActive(true);
            cache.transform.SetParent(transform);
            cache.transform.localScale = Vector3.one;
            cache.transform.localPosition = new Vector3((-1 - middle) * 100, (y - middle) * -100 + 25, 0);
            cache.transform.localEulerAngles = new Vector3(0, 0, 30);

            cache.GetComponent<Text>().text = ((BlendMode)y).ToString();
        }

        for (int x = 0; x < blendModeCount; x++)
        {
            if (x == 0)
            {
                cache = Instantiate<GameObject>(m_textSourceReference);
                cache.SetActive(true);
                cache.transform.SetParent(transform);
                cache.transform.localScale = Vector3.one;
                cache.transform.localPosition = new Vector3((x - middle) * 100 - 25, (-1 - middle) * -100 + 100, 0);
                cache.transform.localEulerAngles = new Vector3(0, 0, 30);

                cache.GetComponent<Text>().text = "Source Factors";
            }

            cache = Instantiate<GameObject>(m_textSourceReference);
            cache.SetActive(true);
            cache.transform.SetParent(transform);
            cache.transform.localScale = Vector3.one;
            cache.transform.localPosition = new Vector3((x - middle) * 100 - 25, (-1 - middle) * -100, 0);
            cache.transform.localEulerAngles = new Vector3(0, 0, 30);

            cache.GetComponent<Text>().text = ((BlendMode)x).ToString();
        }

        for (int y = 0; y < blendModeCount; y++)
        {
            for (int x = 0; x < blendModeCount; x++)
            {
                cache = Instantiate<GameObject>(m_reference);
                cache.SetActive(true);
                cache.transform.SetParent(transform);
                cache.transform.localScale = Vector3.one;
                cache.transform.localPosition = new Vector3((x - middle) * 100, (y - middle) * -100, 0);

//                materialCache = new Material(Shader.Find("Unlit/Blend"));
//                materialCache.SetInt("_BlendSrcFactor", x);
//                materialCache.SetInt("_BlendDstFactor", y);
//                materialCache.SetTexture("_MainTex", m_imageDestination);
//
//                cache.GetComponent<Image>().material = materialCache;

                materialCache = new Material(Shader.Find("Unlit/Blend"));
                materialCache.SetInt("_BlendSrcFactor", x);
                materialCache.SetInt("_BlendDstFactor", y);
                materialCache.SetTexture("_MainTex", m_imageSource);

                cache.transform.GetChild(0).GetComponent<Image>().material = materialCache;
            }
        }
    }
}
