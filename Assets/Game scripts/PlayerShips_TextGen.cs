using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerShips_TextGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        create_text_player();   // starts it
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void create_text_player()
    {
        string path = Application.persistentDataPath + "/player_Ship.txt"; // creates a Path in temp memory 
        FileStream stream = new FileStream(path, FileMode.Create); // creates it as a stream
        stream.Close(); // closes
        using (stream = new FileStream(path, FileMode.Truncate))
        {
            using (var writer = new StreamWriter(stream)) // if its a new text file (it is)
            {
                writer.Write(""); // clear the file
            }
        }
        stream.Close(); // close stream so it can be accessed by other scripts.
    }
}