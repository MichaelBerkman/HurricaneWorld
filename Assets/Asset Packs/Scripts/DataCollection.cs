using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataCollection : MonoBehaviour
{

    public static string filePath;
    
    public string PartipID;
    public string ans1, ans2, ans3;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void WriteData()
    {
        string filePath = Path.Combine(Application.persistentDataPath,"ElevatorWorldData.csv");
        //string filePath = "ElevatorWorldData.csv";

        StreamWriter write = File.AppendText(filePath);
        write.Write("," + ans1 + "," + ans2 + "," + ans3);
        //File.AppendText()
        write.Close();
    }
   
    public void InitializeID()
    {

        //create a storage txt for a int
        string idFile = Path.Combine(Application.persistentDataPath, "partipID.txt");
       // string idFile = "partipID.txt";
        if (!File.Exists(idFile))
        {
            StreamWriter write = File.AppendText(idFile);
            write.Write("1");
            write.Close();
        }
        else
        {
            StreamWriter write = new StreamWriter(idFile);
            //write.Write()
        }
    }
    


    public string GetPartipID()
    {
        int lineCount = File.ReadAllLines("ElevatorWorldData.csv").Length + 1;

        return lineCount.ToString();
         
        int i = 0;
                
        string partipID = null;
        string idFile = Path.Combine(Application.persistentDataPath, "partipID.txt");

        //string idFile = "partipID.txt";

        
        
        partipID =  File.ReadAllText("partipID.txt");
        //partipID = ((char)read.Read()).ToString();
        partipID = (int.Parse(partipID) + 1).ToString();

        Debug.Log(partipID);

        //read.Close();
        StreamWriter write = new StreamWriter("partipID.txt");
        write.Write(partipID);
        write.Close();
        return partipID;
    }


    public void NewEntry()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "ElevatorWorldData.csv");
        StreamWriter write = File.AppendText(filePath);
        write.WriteLine();
        write.Write(GetPartipID() + "Test2");
        write.Close();
    }

    

    public void InitializeData()
    {
        InitializeID();

        string filePath = Path.Combine(Application.persistentDataPath, "ElevatorWorldData.csv");
        //string filePath = "ElevatorWorldData.csv";
        if (!File.Exists(filePath))
        {
            
            StreamWriter write = new StreamWriter(filePath);
            Debug.Log("File Not Found");
            write.WriteLine("Participant ID" + "," + "Ava1Q1" + "," + "Ava1Q2" + "," + "Ava1Q3"
                + "," + "Ava2Q1" + "," + "Ava2Q2" + "," + "Ava2Q3"
                + "," + "Ava3Q1" + "," + "Ava3Q2" + "," + "Ava3Q3"
                + "," + "Ava4Q1" + "," + "Ava4Q2" + "," + "Ava4Q3"
                + "," + "Ava5Q1" + "," + "Ava5Q2" + "," + "Ava5Q3"
                + "," + "Ava6Q1" + "," + "Ava6Q2" + "," + "Ava6Q3"
                + "," + "Ava7Q1" + "," + "Ava7Q2" + "," + "Ava7Q3"
                + "," + "Ava8Q1" + "," + "Ava8Q2" + "," + "Ava8Q3" 
                + "," + "Ava9Q1" + "," + "Ava9Q2" + "," + "Ava9Q3"
                + "," + "Ava10Q1" + "," + "Ava10Q2" + "," + "Ava10Q3"
                + "," + "Ava11Q1" + "," + "Ava11Q2" + "," + "Ava11Q3"
                + "," + "Ava12Q1" + "," + "Ava12Q2" + "," + "Ava12Q3"
                + "," + "Ava13Q1" + "," + "Ava13Q2" + "," + "Ava13Q3"
                + "," + "Ava14Q1" + "," + "Ava14Q2" + "," + "Ava14Q3"
                + "," + "Ava15Q1" + "," + "Ava15Q2" + "," + "Ava15Q3"
                + "," + "Ava16Q1" + "," + "Ava16Q2" + "," + "Ava16Q3");
            write.Write(GetPartipID() + "Test");

            write.Close();
        }
        else
        {
            Debug.Log("File Found");
            NewEntry();

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //PartipID = "P." + GetPartipID();
    }
}
