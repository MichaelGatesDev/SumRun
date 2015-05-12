//------------------------------------------------------------------------------
//  Author:
//       Michael Gates <michaelgatesdev@gmail.com>
//
//  Copyright (c) 2015 Michael Gates 2015
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class ThunderScript : MonoBehaviour
{
    // ========================================================================================\\

    public AudioClip thunderSound;
    public GameObject lightningLight;
    public float lowLight;
    public float highLight;
    private System.Random random;
    private Light theLight;
    private bool doing;

    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        random = new System.Random();
        
        theLight = (Light)lightningLight.GetComponent<Light>();

        
        StartCoroutine("RandomLightning");
    }

    // ========================================================================================\\

    private IEnumerator RandomLightning()
    {   
        while (true)
        {

            if (random.NextDouble() > 0.75D)
            {
                DoLightning();
            }

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator LightningFlash()
    {
        theLight.intensity = highLight;

        yield return new WaitForSeconds(0.1f);

        StartCoroutine("LightningDim"); 

        yield break;
    }

    private IEnumerator LightningDim()
    {
        do
        {
            theLight.intensity -= 0.5f;
            yield return new WaitForSeconds(0.01f);
        } while (theLight.intensity > lowLight) ;

        doing = false;
        yield break;
    }

    private void DoLightning()
    {
        if (!doing)
        {
            doing = true;
            StartCoroutine("LightningFlash");
        }
    }

    
    // ========================================================================================\\
}
