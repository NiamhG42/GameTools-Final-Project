﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class DeluxeTowerBlock : MonoBehaviour
    {

        #region Fields
        public BuildingProfile myProfile;
        public Transform basePrefab;
        public int recursionLevel = 0;
        private int maxLevel = 3;
        private CityManager cityManager;
        private Renderer myRenderer;
        private MeshFilter myMeshFilter;
        private Mesh myMesh;
        private Material myMaterial;
        private Quaternion roofRotation;

        #endregion

        #region Properties	
        #endregion

        #region Methods

        public void SetProfile(BuildingProfile profile)
        {
            myProfile = profile;
            myProfile.maxHeight = Random.Range(4, 7);
            maxLevel = myProfile.maxHeight;
        }

        public void Initialize(int recLevel, Material mat, Mesh mesh)
        {
            recursionLevel = recLevel;
            myMesh = mesh;
            myMaterial = mat;
            maxLevel = myProfile.maxHeight;

        }

        #region Unity Methods

        // Use this for internal initialization
        void Awake()
        {
            myRenderer = GetComponent<MeshRenderer>();
            myMeshFilter = GetComponent<MeshFilter>();
        }

        // Use this for external initialization
        void Start()
        {
            int x = Mathf.RoundToInt(transform.position.x + 7.0f);
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 7.0f);

            cityManager = CityManager.Instance;

            Transform child;
            if (recursionLevel == 0)
            {
                if (!cityManager.CheckSlot(x, y, z) && !cityManager.CheckRoadSlot(x, y, z))
                {
                    int meshNum = myProfile.groundBlocks.Length;
                    int matNum = myProfile.groundMaterials.Length;
                    myMesh = myProfile.groundBlocks[Random.Range(0, meshNum)];
                    myMaterial = myProfile.groundMaterials[Random.Range(0, matNum)];
                    cityManager.SetSlot(x, y, z, true);
                    cityManager.SetBuildingSlot(x, y, z, true);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            
            myMeshFilter.mesh = myMesh;
            myRenderer.material = myMaterial;

            if (recursionLevel < maxLevel)
            {
                if (recursionLevel == maxLevel - 1)
                {
                    if (!cityManager.CheckSlot(x, y + 1, z))
                    {
                        //Randomise roof rotation
                        int random = Random.Range(0, 3);
                        switch (random)
                        {
                            case 0:
                            roofRotation = Quaternion.Euler(0, 0, 0);
                            break;

                            case 1:
                            roofRotation = Quaternion.Euler(0, 90, 0);
                            break;

                            case 2:
                            roofRotation = Quaternion.Euler(0, 180, 0);
                            break;

                            case 3:
                            roofRotation = Quaternion.Euler(0, 270, 0);
                            break;

                        }

                        child = Instantiate(basePrefab, transform.position + Vector3.up*1f, roofRotation, this.transform);
                        int meshNum = myProfile.roofBlocks.Length;
                        int matNum = myProfile.roofMaterials.Length;
                        child.GetComponent<DeluxeTowerBlock>().Initialize(recursionLevel + 1, myProfile.roofMaterials[Random.Range(0, matNum)], myProfile.roofBlocks[Random.Range(0, meshNum)]);

                        cityManager.SetSlot(x, y + 1, z, true);
                        cityManager.SetBuildingSlot(x, y+1, z, true);
                    }
                }
                else
                {
                    if (!cityManager.CheckSlot(x, y + 1, z))
                    {
                        child = Instantiate(basePrefab, transform.position + Vector3.up * 1f, Quaternion.identity, this.transform);
                        int meshNum = myProfile.mainBlocks.Length;
                        int matNum = myProfile.mainMaterials.Length;
                        child.GetComponent<DeluxeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);

                        cityManager.SetSlot(x, y + 1, z, true);
                        cityManager.SetBuildingSlot(x, y+1, z, true);
                    }
                }
            }

            
            //If the block is surrounded on all sides, turn off the Renderer
            if ((cityManager.CheckBuildingSlot(x, y + 1, z) && cityManager.CheckBuildingSlot(x, y - 1 , z)
                && cityManager.CheckBuildingSlot(x + 1, y, z) && cityManager.CheckBuildingSlot(x + 1, y, z)
                && cityManager.CheckBuildingSlot(x, y, z + 1) && cityManager.CheckBuildingSlot(x, y, z - 1)))
            {
                //if the building is beside the road, don't turn off Renderer
                if (!(cityManager.CheckRoadSlot(x + 1, 1, z) || cityManager.CheckRoadSlot(x + 1, 1, z)
                || cityManager.CheckRoadSlot(x, 1, z + 1) || cityManager.CheckRoadSlot(x, 1, z - 1)))
                {              
                    if (x != 0 && y != 1 && z != 0)
                    {
                            myRenderer.enabled = false;

                    }
                }
            }
            //Fix random invisble walls
            if ( x == 17 || x == 22 ||x == 23)
            {
                myRenderer.enabled = true;
            }
           
            


            }

            // Update is called once per frame
            void Update()
        {
            if (transform.position.y < -5f)
            {
                Destroy(gameObject);
            }
        }

        #endregion
        #endregion

    }
}

