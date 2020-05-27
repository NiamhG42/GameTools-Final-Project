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
        public GameObject buildingPrefab, roadTile, treePrefab;
         public BuildingProfile[] profileArray;

        public BuildingProfile wallProfile;

        private int citySize = 30;
        private int terrainSize = 42;

        private bool[,,] cityArray = new bool [30,30,30];   //increased array size to allow for larger city volume
        private bool[,,] cityBuildingArray = new bool[30, 30, 30];
        private bool[,,] cityRoadArray = new bool[30, 30, 30];

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
                for (int z = 5; z < citySize; z += 8)
                {
                   SetRoadSlot(x, 0, z, true);
                   SetRoadSlot(z, 0, x, true);
                }
            }

            
        //Block off "shortcuts" where the city road cuts through the racetrack
            for (int z = 7; z < 15; z++)
            {
                SetRoadSlot(5, 0, z, false);
            }
            for (int z = 5; z < 17; z++)
            {
                SetRoadSlot(13, 0, z, false);
            }
            for (int x = 15; x < 21; x++)
            {
                SetRoadSlot(x, 0, 5, false);
            }
            for (int x =15; x<23; x++)
            {
                SetRoadSlot(x, 0, 13, false);
            }      

            //CITY ROADS
            for (int i = -7; i < 18; i += 1)
            {
                for (int j = -7; j < 15; j += 1)
                {

                    Instantiate(roadTile, new Vector3(i, 0.05f, j), Quaternion.identity);
                }
            }
            


            //CITY BUILDINGS:

            for (int i=-9;i<18;i+=1)
            {
                for (int j=-9;j<15;j+=1)
                {
                    int random = Random.Range(0, profileArray.Length);
                    Instantiate(buildingPrefab, new Vector3(i, 0f, j), Quaternion.identity).GetComponent<DeluxeTowerBlock>().SetProfile(profileArray[random]);                 
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

        //Check if there is a ROAD in that slot
        public bool CheckRoadSlot(int x, int y, int z)
        {
            if (x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize) return true;
            else
            {
                return cityRoadArray[x, y, z];
            }

        }

        //Set it that there is a ROAD in that slot
        public void SetRoadSlot(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > citySize || y < 0 || y > citySize || z < 0 || z > citySize))
            {
                cityRoadArray[x, y, z] = occupied;
            }

        }

        void SetPathSlots()
        {
            int xPathOffSet = 4;
            int zPathOffSet = 4;

            // xPathOffSet + 0
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 0, 0, zPathOffSet + i, true); }

            // xPathOffSet + 1
            for (int i = 2; i <= 3; i++) { SetSlot(xPathOffSet + 1, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 1, 0, zPathOffSet + i, true); }

            // xPathOffSet + 2
            for (int i = 1; i <= 4; i++) { SetSlot(xPathOffSet + 2, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 2, 0, zPathOffSet + i, true); }

            // xPathOffSet + 3
            for (int i=0; i <=5; i++) { SetSlot(xPathOffSet + 3, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 3, 0, zPathOffSet + i, true); }

            // xPathOffSet + 4
            for (int i = 0; i <= 5; i++) { SetSlot(xPathOffSet + 4, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 4, 0, zPathOffSet + i, true); }
            for (int i = 13; i <= 14; i++) { SetSlot(xPathOffSet + 4, 0, zPathOffSet + i, true); }

            // xPathOffSet +5
            for (int i = 0; i <= 5; i++) { SetSlot(xPathOffSet + 5, 0, zPathOffSet + i, true);}
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 5, 0, zPathOffSet + i, true); }
            for (int i = 13; i <= 14; i++) { SetSlot(xPathOffSet + 5, 0, zPathOffSet + i, true); }

            // xPathOffSet + 6
            for (int i = 0; i <= 1; i++) { SetSlot(xPathOffSet + 6, 0, zPathOffSet + i, true); }
            for (int i = 4; i <= 5; i++) { SetSlot(xPathOffSet + 6, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 10; i++) { SetSlot(xPathOffSet + 6, 0, zPathOffSet + i, true); }
            for (int i = 13; i <= 14; i++) { SetSlot(xPathOffSet + 6, 0, zPathOffSet + i, true); }

            // xPathOffSet + 7
            for (int i = 0; i <= 1; i++) { SetSlot(xPathOffSet + 7, 0, zPathOffSet + i, true); }
            for (int i = 4; i <= 5; i++) { SetSlot(xPathOffSet + 7, 0, zPathOffSet + i, true); }
            for (int i = 8; i <= 10; i++) { SetSlot(xPathOffSet + 7, 0, zPathOffSet + i, true); }
            for (int i = 13; i <= 14; i++) { SetSlot(xPathOffSet + 7, 0, zPathOffSet + i, true); }

            // xPathOffSet + 8
            for (int i = 0; i <= 1; i++) { SetSlot(xPathOffSet + 8, 0, zPathOffSet + i, true); }
            for (int i = 4; i <= 5; i++) { SetSlot(xPathOffSet + 8, 0, zPathOffSet + i, true); }
            for (int i = 8; i <= 9; i++) { SetSlot(xPathOffSet + 8, 0, zPathOffSet + i, true); }
            for (int i = 13; i <= 14; i++) { SetSlot(xPathOffSet + 8, 0, zPathOffSet + i, true); }

            // xPathOffSet + 9
            for (int i = 0; i <= 1; i++) { SetSlot(xPathOffSet + 9, 0, zPathOffSet + i, true); }
            for (int i = 4; i <= 9; i++) { SetSlot(xPathOffSet + 9, 0, zPathOffSet + i, true); }
            for (int i = 12; i <= 14; i++) { SetSlot(xPathOffSet + 9, 0, zPathOffSet + i, true); }

            // xPathOffSet + 10
            for (int i = 0; i <= 1; i++) { SetSlot(xPathOffSet + 10, 0, zPathOffSet + i, true); }
            for (int i = 4; i <= 9; i++) { SetSlot(xPathOffSet + 10, 0, zPathOffSet + i, true); }
            for (int i = 12; i <= 14; i++) { SetSlot(xPathOffSet + 10, 0, zPathOffSet + i, true); }

            // xPathOffSet + 11
            for (int i = 0; i <= 2; i++) { SetSlot(xPathOffSet + 11, 0, zPathOffSet + i, true); }
            for (int i = 5; i <= 8; i++) { SetSlot(xPathOffSet + 11, 0, zPathOffSet + i, true); }
            for (int i = 12; i <= 13; i++) { SetSlot(xPathOffSet + 11, 0, zPathOffSet + i, true); }

            // xPathOffSet + 12
            for (int i = 0; i <= 3; i++) { SetSlot(xPathOffSet + 12, 0, zPathOffSet + i, true); }
            for (int i = 6; i <= 7; i++) { SetSlot(xPathOffSet + 12, 0, zPathOffSet + i, true); }
            for (int i = 11; i <= 13; i++) { SetSlot(xPathOffSet + 12, 0, zPathOffSet + i, true); }

            // xPathOffSet + 13
            for (int i = 1; i <= 3; i++) { SetSlot(xPathOffSet + 13, 0, zPathOffSet + i, true); }
            for (int i = 11; i <= 13; i++) { SetSlot(xPathOffSet + 13, 0, zPathOffSet + i, true); }

            // xPathOffSet + 14
            for (int i = 2; i <= 3; i++) { SetSlot(xPathOffSet + 14, 0, zPathOffSet + i, true); }
            for (int i = 10; i <= 12; i++) { SetSlot(xPathOffSet + 14, 0, zPathOffSet + i, true); }

            // xPathOffSet + 15
            for (int i = 2; i <= 4; i++) { SetSlot(xPathOffSet + 15, 0, zPathOffSet + i, true); }
            for (int i = 9; i <= 12; i++) { SetSlot(xPathOffSet + 15, 0, zPathOffSet + i, true); }

            // xPathOffSet + 16
            for (int i = 2; i <= 11; i++) { SetSlot(xPathOffSet + 16, 0, zPathOffSet + i, true); }

            // xPathOffSet + 17
            for (int i = 2; i <= 11; i++) { SetSlot(xPathOffSet + 17, 0, zPathOffSet + i, true); }

            // xPathOffSet + 18
            for (int i = 5; i <= 8; i++) { SetSlot(xPathOffSet + 18, 0, zPathOffSet + i, true); }    
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
