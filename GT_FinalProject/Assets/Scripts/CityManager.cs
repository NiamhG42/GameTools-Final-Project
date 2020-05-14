using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public enum blockType { Block, Arches, Columns, Dishpivot, DomeWithBase, HalfDome, SlitDome, Slope, Tile};

	public class CityManager : MonoBehaviour
    {

        #region Fields
        private static CityManager _instance;
        public Mesh[] meshArray;
        public Material[] materialArray;
        public GameObject buildingPrefab;
        public GameObject treePrefab;
        public BuildingProfile[] profileArray;

        public BuildingProfile wallProfile;

        private int citySize = 30;
        private int terrainSize = 42;

        private bool[,,] cityArray = new bool [30,30,30];   //increased array size to allow for larger city volume
        private bool[,,] cityBuildingArray = new bool[30, 30, 30];

        public GameObject[] terrainObjects;

        public static CityManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Properties	
        #endregion

        #region Methods
        #region Unity Methods

        // Use this for internal initialization
        void Awake () {
            //Checks to see if there's already a CityManager in scene, destroys if there is
            if (_instance == null)
            {
                _instance = this;
            }

            else
            {
                Destroy(gameObject);
                Debug.LogError("Multiple CityManager instances in Scene. Destroying clone!");
            };
        }

		
		// Use this for external initialization
		void Start ()
        {
            SetPathSlots();
            MakeOuterTerrain();


        //Make "roads" to divide the city up in a grid
            for (int x = 0; x < citySize; x++) {
                for (int z = 5; z < citySize; z += 5)
                {
                    SetSlot(x, 0, z, true);
                    SetSlot(z, 0, x, true);
                }
            }

       /*
           //BUILD CITY WALLS - add your code below
           
           for (int j = -6; j <= 6; j += 12)
           {
               for (int i = -6; i <= 6; i++)
               {
                   Instantiate(buildingPrefab, new Vector3(i, 0.05f, j), Quaternion.identity).GetComponent<DeluxeTowerBlock>().SetProfile(wallProfile);
                   if (j == -6 && i == 6 || j == -6 && i == -6 || j == 6 && i == 6 || j == 6 && i == -6) {}
                   else { Instantiate(buildingPrefab, new Vector3(j, 0.05f, i), Quaternion.identity).GetComponent<DeluxeTowerBlock>().SetProfile(wallProfile); }
               }
           }
*/



            //CITY BUILDINGS:

            for (int i=-9;i<18;i+=1)
            {
                for (int j=-9;j<15;j+=1)
                {
                    int random = Random.Range(0, profileArray.Length);
                    Instantiate(buildingPrefab, new Vector3(i, 0.05f, j), Quaternion.identity).GetComponent<DeluxeTowerBlock>().SetProfile(profileArray[random]);                 
                }
            }
            
            
		}
		
		#endregion

        public bool CheckSlot(int x, int y, int z)
        {
            if (x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize) return true;
            else
            {
                return cityArray[x, y, z];
            }

        }

        public void SetSlot(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize))
            {
                cityArray[x, y, z] = occupied;
            }

        }

        //Check if there is a BUILDING in that slot
        public bool CheckBuildingSlot(int x, int y, int z)
        {
            if (x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize) return true;
            else
            {
                return cityBuildingArray[x, y, z];
            }

        }

        //Set it that there is a BUILDING in that slot
        public void SetBuildingSlot(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize))
            {
                cityBuildingArray[x, y, z] = occupied;
            }

        }

        void SetPathSlots()
        {
            int xPathOffSet = 4;
            int zPathOffSet = 4;

            SetSlot(xPathOffSet, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet, 0, zPathOffSet + 10, true);

            SetSlot(xPathOffSet + 1, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet + 1, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 1, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 1, 0, zPathOffSet + 10, true);

            SetSlot(xPathOffSet + 2, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 2, 0, zPathOffSet + 10, true);

            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 3, 0, zPathOffSet + 10, true);

            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 4, 0, zPathOffSet + 8, true);

            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 5, 0, zPathOffSet + 13, true);

            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 13, true);
            SetSlot(xPathOffSet + 6, 0, zPathOffSet + 14, true);

            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 0, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 13, true);
            SetSlot(xPathOffSet + 7, 0, zPathOffSet + 14, true);

            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 0, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 13, true);
            SetSlot(xPathOffSet + 8, 0, zPathOffSet + 14, true);

            SetSlot(xPathOffSet + 9, 0, zPathOffSet + 0, true);
            SetSlot(xPathOffSet + 9, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 9, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 9, 0, zPathOffSet + 13, true);
            SetSlot(xPathOffSet + 9, 0, zPathOffSet + 14, true);

            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 0, true);
            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 13, true);
            SetSlot(xPathOffSet + 10, 0, zPathOffSet + 14, true);

            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 0, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 11, 0, zPathOffSet + 13, true);

            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 1, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 11, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 12, 0, zPathOffSet + 13, true);

            SetSlot(xPathOffSet + 13, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 13, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 13, 0, zPathOffSet + 11, true);
            SetSlot(xPathOffSet + 13, 0, zPathOffSet + 12, true);
            SetSlot(xPathOffSet + 13, 0, zPathOffSet + 13, true);

            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 11, true);
            SetSlot(xPathOffSet + 14, 0, zPathOffSet + 12, true);

            SetSlot(xPathOffSet + 15, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 15, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 15, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 15, 0, zPathOffSet + 11, true);
            SetSlot(xPathOffSet + 15, 0, zPathOffSet + 12, true);

            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 16, 0, zPathOffSet + 11, true);

            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 2, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 3, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 4, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 5, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 6, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 7, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 8, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 9, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 10, true);
            SetSlot(xPathOffSet + 17, 0, zPathOffSet + 11, true);
        }

        void MakeOuterTerrain()
        {
            float xPosition, yPosition, zPosition;
            int terrainObjectIndex;
            int terrainObjectsAmount = 50;

            for (int i = 0; i < terrainObjectsAmount; i++)
            {
                //Randomise X & Z positions
                xPosition = Random.Range(-terrainSize, terrainSize);
                yPosition = 2;
                zPosition = Random.Range(-terrainSize, terrainSize);

                //Randomise which terrain object spawns
                terrainObjectIndex = Random.Range(0, terrainObjects.Length);

                //Spawn terrain if its position is outside the city
                if (!(xPosition > -15 && xPosition < 22
                    && xPosition >-15 && zPosition <22))
                {
                    Instantiate(terrainObjects[terrainObjectIndex], new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                }

                else
                {
                    //Get new position and spawn there if original pos is in the city
                    xPosition = Random.Range(-terrainSize, terrainSize);
                    zPosition = Random.Range(-terrainSize, terrainSize);

                    if (!(xPosition > -15 && xPosition < 22
                    && xPosition > -15 && zPosition < 22))
                    {                   
                        Instantiate(terrainObjects[terrainObjectIndex], new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
                    }
                }
               
            }
        }

        #endregion

    }
}
