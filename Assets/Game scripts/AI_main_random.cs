using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AI_main_random : MonoBehaviour
{

    public class UsedArrays // creates a class for our arrays (easier to manage)
    {
        public List<char> BoatXCheckArray;
        public List<char> BoatYCheckArray;

        public List<int> CurrentBoatListHorizontal;
        public List<int> CurrentBoatListVertical;

    }

    public class Values_to_check
    {

    }
    public Verify VF;
    public List<char> boatXcheck = new List<char>(); //creates arrays for when we read our text file
    public List<char> boatYcheck = new List<char>(); //creates arrays for when we read our text file
    public List<int> boathorizontal = new List<int>(); //creates arrays for our random ships
    public List<int> boatvertical = new List<int>(); //creates arrays for our random ships
    // Start is called before the first frame update
    private string writeshipcoor;
    public string[] list_of_ships = {"battleship","battleship","submarine","submarine","carrier"}; //list of ships
    public int[] ship_coor = { };

    private float xfloat;

    private int valCharx;
    private int valChary;
    private int i = 0;
    private int counter = 0;
    private UsedArrays Arrays = new UsedArrays();
    void Start() 
    {
        Arrays.BoatXCheckArray = boatXcheck;
        Arrays.BoatYCheckArray = boatYcheck;
        Arrays.CurrentBoatListHorizontal = boathorizontal;
        Arrays.CurrentBoatListVertical = boatvertical;

        create_text_AI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void generate_ships(int i)
    {
        string path = Application.persistentDataPath + "/AI_Ships.txt"; // states th string path
        for (i=0; i <= 4; i++) //loops for the number of ships we have
        {
            myLabel://label

            int x = UnityEngine.Random.Range(0, 10); //random x
            int z = UnityEngine.Random.Range(0, 10); //random y
            int a = UnityEngine.Random.Range(1, 3); //generates either a 1 or 2
            int[] ship_coor = new int[] { x, z }; //adds to array
            Debug.Log(ship_coor);
            Debug.Log("a" + a);
            if (a == 1)
            {
                Arrays.CurrentBoatListHorizontal.Clear();//clears the array from last time 
                Arrays.CurrentBoatListHorizontal.Add(ship_coor[0]); //adds all posible values to an array
                Arrays.CurrentBoatListHorizontal.Add(ship_coor[0] + 1); //|
                Arrays.CurrentBoatListHorizontal.Add(ship_coor[0] - 1); //|
                if (list_of_ships[i] == "battleship")
                {
                    Arrays.CurrentBoatListHorizontal.Add(ship_coor[0] - 2); //adds additional beacause the battleship is longer
                }
                if (list_of_ships[i] == "carrier")
                {
                    Arrays.CurrentBoatListHorizontal.Add(ship_coor[0] - 2);          //adds additional beacause the carrier is longer
                    Arrays.CurrentBoatListHorizontal.Add(ship_coor[0] - 3);          //adds all posible values to an array
                }
                if (Search_values_x(ship_coor[1]))      //checks if a coordinate hass already been used. booleon function send through y as its a constant
                {
                    Debug.Log("trying again");
                    Arrays.CurrentBoatListHorizontal.Clear(); //clears the array
                    goto myLabel; //jumps to my label so it misses incrementing the for loop
                }
                    
                File.AppendAllText(path, ((ship_coor[0] + 1) + "," + (ship_coor[1]) + ",0," + (list_of_ships[i])+ "\n"));  //adds all the checked values to the text file
                File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));  
                File.AppendAllText(path, ((ship_coor[0] - 1) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));
                if (list_of_ships[i] == "battleship")
                {
                    File.AppendAllText(path, ((ship_coor[0] - 2) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));
                }
                if (list_of_ships[i] == "carrier")
                {
                    File.AppendAllText(path, ((ship_coor[0] - 2) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));
                    File.AppendAllText(path, ((ship_coor[0] - 3) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));
                }
                   



            }
            else if(a == 2)
            {
                Arrays.CurrentBoatListVertical.Clear();
                Arrays.CurrentBoatListVertical.Add(ship_coor[1]); //adds all posible values to an array
                Arrays.CurrentBoatListVertical.Add(ship_coor[1] + 1);
                Arrays.CurrentBoatListVertical.Add(ship_coor[1] - 1);

                if (list_of_ships[i] == "battleship") //next ship to create random values for is a battleship so 1 extra value is required
                {
                    Arrays.CurrentBoatListVertical.Add(ship_coor[1] - 2);
                }
                if (list_of_ships[i] == "carrier")  //next ship to create random values for is a battleship so 2 extra value is required
                {
                    Arrays.CurrentBoatListVertical.Add(ship_coor[1] - 2);
                    Arrays.CurrentBoatListVertical.Add(ship_coor[1] - 3);          //adds all posible values to an array
                }
                if (Search_values_y(ship_coor[0]))      //checks if a coordinate hass already been used.
                {
                    Debug.Log("trying again");
                    Arrays.CurrentBoatListVertical.Clear();
                    goto myLabel;
                }
                File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1] + 1) + ",1," + (list_of_ships[i]) + "\n")); //adds all checked values to the text file
                File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1]) + "," + (list_of_ships[i]) + "\n"));
                File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1] - 1) + "," + (list_of_ships[i]) + "\n"));
                if (list_of_ships[i] == "battleship")
                {
                    File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1] - 2) + "," + (list_of_ships[i]) + "\n"));
                }
                if (list_of_ships[i] == "carrier")
                {
                    File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1] - 2) + "," + (list_of_ships[i]) + "\n"));
                    File.AppendAllText(path, ((ship_coor[0]) + "," + (ship_coor[1] - 3) + "," + (list_of_ships[i]) + "\n"));
                }

            }
        }
        end();

    }

    void create_text_AI()
    {
        string path = Application.persistentDataPath + "/AI_Ships.txt"; //states the new string path and creates a new empty text file called AI_Ships
        FileStream stream = new FileStream(path, FileMode.Create);
        stream.Close();
        using (stream = new FileStream(path, FileMode.Truncate))
        {
            using (var writer = new StreamWriter(stream))
            {
               writer.Write("");
            }
        }
        int i=0;
        stream.Close();
        generate_ships(i); //runs the generate procedure


    }
    void end() // when finished
    {
        Debug.Log("end");
    }
    private bool Search_values_x(int g) //this will return a true if it find a value that matches the ship that has been genarated
    {
        for (int i = 0; i <= Arrays.CurrentBoatListHorizontal.Count-1; i++) //this loops for the length of the currnt horizontal list
        {
            if (Arrays.CurrentBoatListHorizontal[i]>=10 || Arrays.CurrentBoatListHorizontal[i] < 0) //this sees if the the whole boat is within the boundary
            {
                Debug.Log("Found error on x" + Arrays.CurrentBoatListHorizontal[i]); // if it find that a x value is outside of the playing boundary then it returns true and try again.
                return true;
            }
        }
        string path = Application.persistentDataPath + "/AI_Ships.txt";

        StreamReader inp_stm = new StreamReader(path); //sets inp_stm as the new path and opens it.
        Debug.Log("-------------------------x-------------------------------");
        while (!inp_stm.EndOfStream) //while the file is not all read
        {
            string inp_ln = inp_stm.ReadLine(); //read the first line that hasnt been read
            Debug.Log(inp_ln);
            valCharx = (int)Char.GetNumericValue(inp_ln[0]); //sets the first number to the x
            valChary = ((int)Char.GetNumericValue(inp_ln[2])); //sets the 3rd number (misses the comma)
            //Debug.Log(valCharx);
            //Debug.Log(valChary);
            if (valChary == g) //if current y in Text file equals our Random one
            {
                counter = 0;
                Debug.Log("ding");
                for (int i = 0; i < Arrays.CurrentBoatListHorizontal.Count; i++) //loops for the length of the horizontal values (this is beacause this is the only value that changes)
                {
                    if (valCharx == Arrays.CurrentBoatListHorizontal[i]) // if the same as the current x value 
                    {
                        //Debug.Log("second ding");
                        //Debug.Log("count:"+ Arrays.CurrentBoatListHorizontal.Count);
                        //Debug.Log("Current counter pos "+counter);
                        int test = Arrays.CurrentBoatListHorizontal.Count;
                        //Debug.Log("test data:"+test);
                        
                        //Debug.Log("overlap");
                        inp_stm.Close(); //close the file
                        return true; //found a match try again
                    }
                }
            }
        }
        inp_stm.Close(); //close the file
        return false; //didn't find anything so can continue
    }

    private bool Search_values_y(int g) //This is identical to the other bt it checks y values as it is vertical so y changes.
    {
        for (int i = 0; i <= Arrays.CurrentBoatListVertical.Count-1; i++)
        {
            Debug.Log("doing the checking");
            if (Arrays.CurrentBoatListVertical[i] >= 10 || Arrays.CurrentBoatListVertical[i] < 0)
            {
                return true;
            }
        }

        string path = Application.persistentDataPath + "/AI_Ships.txt";
        StreamReader inp_stm = new StreamReader(path);
        Debug.Log("-------------------------y-------------------------------");
        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            Debug.Log(inp_ln);
            valCharx = (int)Char.GetNumericValue(inp_ln[0]);
            valChary = (int)Char.GetNumericValue(inp_ln[2]);
            //Debug.Log(valCharx);
            //Debug.Log(valChary);
            if (valCharx == g) //if a current y 
            {
                counter = 0;
                Debug.Log("ding");
                for (int i = 0; i < Arrays.CurrentBoatListVertical.Count; i++)
                {
                    if (valChary == boatvertical[i])
                    {
                        Debug.Log("second ding");
                        Debug.Log("count:" + Arrays.CurrentBoatListVertical.Count);
                        Debug.Log("Current counter pos " + counter);
                        int test = Arrays.CurrentBoatListVertical.Count;
                        Debug.Log("test data:" + test);

                        Debug.Log("overlap");
                        inp_stm.Close();
                        return true;
                    }
                }
            }
        }
        inp_stm.Close();
        return false;
    }
}
