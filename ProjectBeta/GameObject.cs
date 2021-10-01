using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectBeta
{
    public class GameObject
    {
        // Positioning
        public PointStruct position;

        // Identity
        public Guid ID { get; private set; }
        public string sprite { get; private set; }
        public string cleaner = "";
        public Objects type;

        // List of objects
        public List<GameObject> objectList = new List<GameObject>();

        // Constructor
        public GameObject(PointStruct position, string sprite, Objects type)
        {
            this.type = type;
            ID = Guid.NewGuid();
            this.sprite = sprite;
            this.position = position;
            objectList.Add(this);

            for (int i = 0; i <this.sprite.Length; i++) { cleaner += " "; }
        }

        // Moves up one
        public void MoveUp()
        {
            position.Move(this, 0, -1);
        }

        // Moves down one
        public void MoveDown()
        {
            position.Move(this, 0, 1);
        }

        // Moves left one
        public void MoveLeft()
        {
            position.Move(this, -1, 0);
        }

        // Moves right one
        public void MoveRight()
        {
            position.Move(this, 1, 0);
        }
    }
}
