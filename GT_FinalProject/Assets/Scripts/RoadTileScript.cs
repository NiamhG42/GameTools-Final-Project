using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class RoadTileScript : MonoBehaviour
    {
        private CityManager cityManager;

        // Start is called before the first frame update
        void Start()
        {
            int x = Mathf.RoundToInt(transform.position.x + 7.0f);
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 7.0f);

            cityManager = CityManager.Instance;

            if (!cityManager.CheckSlot(x, y, z))
            {
                Destroy(gameObject);
            }
            else
            {
                //Road Tile is Placed            
            }
        }
    }
}
