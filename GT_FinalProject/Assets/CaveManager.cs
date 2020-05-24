using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    #region Fields
    public GameObject columnPrefab;
    private Quaternion horizontalRotation = Quaternion.Euler(0, 0, 90);
    private float xPos, yPos, zPos;
    private int randomPlacement, lastPlacement;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        yPos = 6;
        xPos = 84;
        float zStartPos = -34.5f;
        lastPlacement = 0;

        for (int zOffset =0; zOffset < 50; zOffset+=10)
        {
            zPos = zStartPos + zOffset;

            //Randomise column placement
            randomPlacement = Random.Range(0, 5);

            //Don't use the same placement twice in a row
            int tempPlacement = lastPlacement;
            while (randomPlacement == tempPlacement)
            {
                randomPlacement = Random.Range(0, 5);
            }
            lastPlacement = randomPlacement;

            switch (randomPlacement)
            {
                case 0:
                    ColumnsRight(zPos);
                    break;

                case 1:
                    ColumnsLeft(zPos);
                    break;

                case 2:
                    ColumnsMiddleGapVertical(zPos);
                    break;

                case 3:
                    ColumnsTopGap(zPos);
                    break;

                case 4:
                    ColumnsBottomGap(zPos);
                    break;

                case 5:
                    ColumnsMiddleGapHorizontal(zPos);
                    break;
            }
           
        }
    }
    #endregion

    #region Methods
    void ColumnsRight(float z)
    {
        Instantiate(columnPrefab, new Vector3(84.66f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(88.66f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(92.66f, yPos, z), Quaternion.identity);
    }

    void ColumnsLeft(float z)
    {
        Instantiate(columnPrefab, new Vector3(84.05f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(79.87f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(75.9f, yPos, z), Quaternion.identity);
    }

    void ColumnsMiddleGapVertical(float z)
    {
        Instantiate(columnPrefab, new Vector3(88.66f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(92.66f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(79.87f, yPos, z), Quaternion.identity);
        Instantiate(columnPrefab, new Vector3(75.9f, yPos, z), Quaternion.identity);
    }

    void ColumnsTopGap(float z)
    {
        Instantiate(columnPrefab, new Vector3(xPos, 5.4f, z), horizontalRotation);
        Instantiate(columnPrefab, new Vector3(xPos, 2.2f, z), horizontalRotation);
    }

    void ColumnsBottomGap(float z)
    {
        Instantiate(columnPrefab, new Vector3(xPos, 6.6f, z), horizontalRotation);
        Instantiate(columnPrefab, new Vector3(xPos, 9.9f, z), horizontalRotation);
    }

    void ColumnsMiddleGapHorizontal(float z)
    {
        Instantiate(columnPrefab, new Vector3(xPos, 2.2f, z), horizontalRotation);
        Instantiate(columnPrefab, new Vector3(xPos, 9.9f, z), horizontalRotation);
    }
    #endregion
}
