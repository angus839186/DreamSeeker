using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;
using System;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Reflection;

public class UserDataExcelSaveLoad : MonoBehaviour
{
    public static UserDataExcelSaveLoad instance;
    public DataManager dataManager;
    public PlayerDataList playerDataList = new PlayerDataList();

    string excelPath = Path.Combine(Application.streamingAssetsPath, "ExcelTest.xlsx");
    string excelSheetName = "工作表1";
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);

        var excelRowData = ReadExcel(excelPath, excelSheetName);

        playerDataList.data = ParseDataToJson(excelRowData);
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        //LoadExcel(excelPath, excelSheetName,playerDataList.data);
    }

    DataRowCollection ReadExcel(string excelPath, string excelSheet)
    {
        using (FileStream fs = File.Open(excelPath, FileMode.Open, FileAccess.Read))
        { 
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            
            var result = excelReader.AsDataSet();

            return result.Tables[excelSheet].Rows;
        }
    }

    List<PlayerData> ParseDataToJson(DataRowCollection excelData)
    {
        List<PlayerData> playerDataList = new List<PlayerData>();
        PlayerData playerData;

        for (int i = 1; i < excelData.Count; i++)
        {
            playerData = new PlayerData();
            playerData.userID = Int32.Parse(excelData[i][0].ToString());
            playerData.userName = excelData[i][1].ToString();
            playerData.useraccountID = excelData[i][2].ToString();
            playerData.userpassword = excelData[i][3].ToString();

            string a = excelData[i][4].ToString().Replace("(", "").Replace(")","");
            string[] s = a.Split(new char[] { ',' });
            playerData.playerPosition = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));

            playerData.itemList = excelData[i][5].ToString();
            playerData.itemNumber = excelData[i][6].ToString();

            playerData.survivePoint = Int32.Parse(excelData[i][7].ToString());
            playerData.interest = Int32.Parse(excelData[i][8].ToString());
            playerData.forgotPassword = excelData[i][9].ToString();

            playerDataList.Add(playerData);
        }

        return playerDataList;
    }

    private void LoadExcel(string excelPath, string excelSheet,List<PlayerData> playerDatas)
    {
        if (File.Exists(excelPath))
        {
            File.Delete(excelPath);
        }

        FileInfo newfile = new FileInfo(excelPath);
        using (ExcelPackage package = new ExcelPackage(newfile))
        {
            //創建工作表
            //ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets.Add(excelSheet);
            //針對指定格新增內容
            //excelWorkSheet.Cells[1, 0].Value = "aa";
            //excelWorkSheet.Cells[A5].Value = "bb";
            
            package.Save();
        }
    }

    public void SavePlayerData(string excelPath, string excelSheet, Vector3 playerDataPosition)
    {
        /*if (File.Exists(excelPath))
        {
            File.Delete(excelPath);
        }*/

        FileInfo newfile = new FileInfo(excelPath);
        using (ExcelPackage package = new ExcelPackage(newfile))
        {
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets[0];

            for (int i = 0; i < playerDataList.data.Count; i++)
            {
                if (dataManager.currentPlayerData.userID == playerDataList.data[i].userID)
                {
                    excelWorkSheet.Cells[i + 2, 5].Value = playerDataPosition.ToString();
                    break;
                }
            }
            
            package.Save();
        }
    }

    public void SavePlayerDataIO()
    {
        SavePlayerData(excelPath, excelSheetName, dataManager.currentPlayerData.playerPosition);
    }

    public void CreateAccountPlayerData(string _name, string _account, string _password,string _forgotpassword)
    {
        FileInfo newfile = new FileInfo(excelPath);
        using (ExcelPackage package = new ExcelPackage(newfile))
        {
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets[0];
            
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 1].Value = playerDataList.data.Count + 1;
            
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 2].Value = _name;
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 3].Value = _account;
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 4].Value = _password;
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 5].Value = new Vector3(0,0,0);
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 6].Value = new Vector3(0, 0, 0);
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 7].Value = "1";
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 8].Value = "100";
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 9].Value = "100";
            excelWorkSheet.Cells[playerDataList.data.Count + 2, 10].Value = _forgotpassword;
            package.Save();
        }

        var excelRowData = ReadExcel(excelPath, excelSheetName);
        playerDataList.data = ParseDataToJson(excelRowData);
    }

    public void DeletePlayerData(int _index)
    {
        FileInfo newfile = new FileInfo(excelPath);
        using (ExcelPackage package = new ExcelPackage(newfile))
        {
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets[0];

            excelWorkSheet.DeleteRow(_index);

            package.Save();
        }
        var excelRowData = ReadExcel(excelPath, excelSheetName);
        playerDataList.data = ParseDataToJson(excelRowData);
    }

    public void ChangePasswordPlayerData(int _index,string _password)
    {
        FileInfo newfile = new FileInfo(excelPath);
        using (ExcelPackage package = new ExcelPackage(newfile))
        {
            ExcelWorksheet excelWorkSheet = package.Workbook.Worksheets[0];

            excelWorkSheet.Cells[_index + 2, 4].Value = _password;

            package.Save();
        }
    }

    [Serializable]
    public class PlayerDataList
    {
        public List<PlayerData> data = new List<PlayerData>();
    }

    [Serializable]
    public class PlayerData
    {
        public int userID;
        public string userName;
        public string useraccountID;
        public string userpassword;
        public Vector3 playerPosition;
        public string itemList;
        public string itemNumber;
        public int survivePoint;
        public int interest;
        public string forgotPassword;
    }
}
