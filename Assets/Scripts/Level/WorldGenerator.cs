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
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    // ========================================================================================\\

    private System.Random random;
    private bool addNewPieces = true; // hardcoded
    //
    private List<GameObject>possiblePieces;
    //
    public GameObject lastGenerated;
    //
    public float x_spread = 6.0f;
    public float xs_spread_x = 0.0f;
    public float xs_spread_y = 0.0f;
    public float was_xs_spread_x = 0.0f;
    public float was_xs_spread_y = 0.0f;
    public float s_spread_x = 0.0f;
    public float s_spread_y = 0.0f;
    public float was_s_spread_x = 0.0f;
    public float was_s_spread_y = 0.0f;
    //
    public GameObject[] prefabs;

    // ========================================================================================\\

    public WorldGenerator()
    {
        this.random = new System.Random();
    }


    // Use this for initialization
    void Start()
    {
        // setup the pieces
        StartCoroutine("SetupPieces");

        // start adding pieces
        StartCoroutine("AddPieces");
    }

    // ========================================================================================\\
    /*
     * 
     * World Generators really suck...
     * 
     */
    // ========================================================================================\\
    
    private IEnumerator SetupPieces()
    {
        yield return new WaitForSeconds(0.5f);

        
        possiblePieces = new List<GameObject>();

        foreach (GameObject go in prefabs)
        {
            if (go.GetComponent<LevelPiece>() == null)
                continue;
            
            LevelPiece goLP = go.GetComponent<LevelPiece>();
            
            GameObject goPrefab = goLP.GetPrefab();
            
            if (goPrefab == null)
                continue;
            
            float goWeight = goLP.GetWeight();
            
            if (goWeight <= 0.0f)
            {
                goWeight = 0.1f;
            }

            int newWeight = (int)(goWeight * 100);
            
            for (int i=0; i<newWeight; i++)
                possiblePieces.Add(go);
        }

        
        yield return null;
    }
    
    private GameObject GetNextLikelyPiece()
    {
        int ranNum = random.Next(0, possiblePieces.Count);

        GameObject selected = possiblePieces [ranNum];
        LevelPieceType nextPieceType = selected.GetComponent<LevelPiece>().pieceType;

        if (lastGenerated.GetComponent<LevelPiece>() == null)
        {
            return null;
        }

        LevelPieceType lastGeneratedType = lastGenerated.GetComponent<LevelPiece>().pieceType;

        // if last generated was a beginning piece
        if (lastGeneratedType == LevelPieceType.BEGIN)
        {
            if (nextPieceType != LevelPieceType.MID)
            {
                return GetNextLikelyPiece();
            }
        }


        // if last generated was an ending piece
        else if (lastGeneratedType == LevelPieceType.END)
        {
            if (nextPieceType != LevelPieceType.EMPTY)
            {
                return GetNextLikelyPiece();
            }
        }

        
        // if last generated was a middle piece
        else if (lastGeneratedType == LevelPieceType.MID)
        {
            if (nextPieceType == LevelPieceType.BEGIN || nextPieceType == LevelPieceType.EMPTY || nextPieceType == LevelPieceType.EXTRA_SMALL || nextPieceType == LevelPieceType.SMALL)
            {
                return GetNextLikelyPiece();
            }
        }

        
        // if last generated piece was empty
        else if (lastGeneratedType == LevelPieceType.EMPTY)
        {
            if (nextPieceType != LevelPieceType.SMALL && nextPieceType != LevelPieceType.EXTRA_SMALL && nextPieceType != LevelPieceType.BEGIN)
            {
                return GetNextLikelyPiece();
            }
        }
        
        
        // if last generated piece was a small island
        else if (lastGeneratedType == LevelPieceType.SMALL)
        {
            if (nextPieceType != LevelPieceType.EMPTY)
            {
                return GetNextLikelyPiece();
            }
        }
        
        
        // if last generated piece was an extra small island
        else if (lastGeneratedType == LevelPieceType.EXTRA_SMALL)
        {
            if (nextPieceType != LevelPieceType.EMPTY)
            {
                return GetNextLikelyPiece();
            }
        }

        return selected;
    }
    
    private IEnumerator AddPieces()
    {
        yield return new WaitForSeconds(1);

        while (addNewPieces)
        {
            AddNewPiece();
            yield return new WaitForSeconds(1.1f);
        }
        yield return null;
    }

    private void AddNewPiece()
    {
        if (lastGenerated == null)
        {
            Debug.Log("Houston, we have a problem.");
            return;
        }

        GameObject toAdd = GetNextLikelyPiece();

        if (toAdd == null)
        {
            Debug.LogError("MAJOR ISSUE! PIECE CAN'T GENERATE.");
            return;
        }

        Vector3 oldPos = lastGenerated.transform.position;
        float x = oldPos.x;
        float y = oldPos.y;
        float z = oldPos.z;
        
        bool xs = false;
        bool wasXS = false;
        if (toAdd.GetComponent<LevelPiece>().pieceType == LevelPieceType.EXTRA_SMALL)
        {
            xs = true;
        }        
        if (lastGenerated.GetComponent<LevelPiece>().pieceType == LevelPieceType.EXTRA_SMALL)
        {
            wasXS = true;
        }

        bool s = false;
        bool wasS = false;
        if (toAdd.GetComponent<LevelPiece>().pieceType == LevelPieceType.EXTRA_SMALL)
        {
            s = true;
        }        
        if (lastGenerated.GetComponent<LevelPiece>().pieceType == LevelPieceType.EXTRA_SMALL)
        {
            wasS = true;
        }

        if (xs)
        {
            x += xs_spread_x;
            y += xs_spread_y;
        }
        if (wasXS)
        {
            x += was_xs_spread_x;
            y += was_xs_spread_y;
        }
        
        if (s)
        {
            x += s_spread_x;
            y += s_spread_y;
        }
        if (wasS)
        {
            x += was_s_spread_x;
            y += was_s_spread_y;
        }

        Vector3 newPos = new Vector3(x + x_spread, y, z);

        GameObject go = (GameObject)GameObject.Instantiate(toAdd, oldPos, Quaternion.identity);
        go.transform.position = newPos;

        lastGenerated = go;
    }


    
    // ========================================================================================\\
}
