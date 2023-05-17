using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleStamina : MonoBehaviour
{
    public GameObject[] staminaPrefabs;
    [SerializeField]
    public static float stamina = 100;
    public Slider slider;

    void Update()
    {
        slider.value = stamina;

        if (stamina < 90)
        {
            staminaPrefabs[9].SetActive(false);
        } else
        {
            staminaPrefabs[9].SetActive(true);
        }

        if (stamina < 80)
        {
            staminaPrefabs[8].SetActive(false);
        }
        else
        {
            staminaPrefabs[8].SetActive(true);
        }

        if (stamina < 70)
        {
            staminaPrefabs[7].SetActive(false);
        }
        else
        {
            staminaPrefabs[7].SetActive(true);
        }

        if (stamina < 60)
        {
            staminaPrefabs[6].SetActive(false);
        }
        else
        {
            staminaPrefabs[6].SetActive(true);
        }

        if (stamina < 50)
        {
            staminaPrefabs[5].SetActive(false);
        }
        else
        {
            staminaPrefabs[5].SetActive(true);
        }

        if (stamina < 40)
        {
            staminaPrefabs[4].SetActive(false);
        }
        else
        {
            staminaPrefabs[4].SetActive(true);
        }

        if (stamina < 30)
        {
            staminaPrefabs[3].SetActive(false);
        }
        else
        {
            staminaPrefabs[3].SetActive(true);
        }

        if (stamina < 20)
        {
            staminaPrefabs[2].SetActive(false);
        }
        else
        {
            staminaPrefabs[2].SetActive(true);
        }

        if (stamina < 10)
        {
            staminaPrefabs[1].SetActive(false);
        }
        else
        {
            staminaPrefabs[1].SetActive(true);
        }

        if (stamina < 1)
        {
            staminaPrefabs[0].SetActive(false);
        } else
        {
            staminaPrefabs[0].SetActive(true);
        }

    }
    
}
