using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Configuration;
using System.Data;
using System.Text;
using KinartiProject_ruppin.Models;

public class DBServices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBServices() { }

    //--------------------------------------------------------------------------------------------------
    // creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        //try
        //{
        con.Open();
        //}
        //catch (Exception ex)
        //{
        //    // write to log
        //    throw (ex);
        //}
        return con;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object
        cmd.Connection = con;              // assign the connection to the command object
        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
        return cmd;
    }

    //מחזיר את כל הסטטוסים ששייכים לטבלה שנשלחה כפרמטר
    public List<string> GetAllStatus(string relateTo)
    {
        SqlConnection con;
        //Status s = new Status();
        List<string> ls = new List<string>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT * FROM STATUS WHERE relateTO like '" + relateTo + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                ls.Add(Convert.ToString(dr["statusName"]));
            }
            return ls;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    public List<Route> Read()
    {
        SqlConnection con;
        List<Route> rl = new List<Route>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT * FROM Route ";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Route r = new Route();
                r.RouteName = Convert.ToString(dr["routeName"]);
                rl.Add(r);
            }
            return rl;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    public List<Project> GetAllProject()
    {
        SqlConnection con;
        List<Project> lp = new List<Project>();
        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT * FROM Project ";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Project p = new Project();
                p.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                p.ProjectName = Convert.ToString(dr["projectName"]);
                if (!DBNull.Value.Equals(dr["prodStartDate"]))
                {
                    p.ProdStartDate = Convert.ToDateTime(dr["prodStartDate"]);
                    //p.ProdStartDate = Convert.ToString(dr["prodStartDate"]);
                }
                if (!DBNull.Value.Equals(dr["supplyDate"]))
                {
                    p.SupplyDate = Convert.ToDateTime(dr["supplyDate"]);
                    //p.SupplyDate = Convert.ToString(dr["supplyDate"]);
                }
                if (!DBNull.Value.Equals(dr["projectStatus"]))
                {
                    p.ProjectStatus = Convert.ToString(dr["projectStatus"]);
                }
                if (!DBNull.Value.Equals(dr["comment"]))
                {
                    p.Comment = Convert.ToString(dr["comment"]);
                }
                if (!DBNull.Value.Equals(dr["prodEntranceDate"]))
                {
                    p.ProdEntranceDate = Convert.ToDateTime(dr["prodEntranceDate"]);
                }

                lp.Add(p);
            }
            return lp;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

    }

    public Item[] GetProjectItems(float projNum)
    {

        SqlConnection con;
        List<Item> Pi = new List<Item>();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Item where projectNum = " + projNum;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //int PID = Convert.ToInt32(cmd.ExecuteScalar());
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection

                Item I = new Item();
                I.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                I.ItemNum = Convert.ToString(dr["itemNum"]);
                I.ItemName = Convert.ToString(dr["itemName"]);
                I.ItemStatus = Convert.ToString(dr["itemStatus"]);
                //I.ItemCompletedPercentage = Convert.ToDouble(dr["completedPercent"]);
                I.ItemGroupCount = Convert.ToInt32(dr["groupCount"]);
                //I.SupplyDate = Convert.ToDateTime(dr["supplyDate"]);
                //I.ProjectStatus = Convert.ToString(dr["projectStatus "]);
                //I.Comment = Convert.ToString(dr["comment "]);
                //I.ProdEntranceDate = Convert.ToDateTime(dr["prodEntranceDate"]);
                Pi.Add(I);
            }
            return Pi.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    public Item[] GetAllProjectItems()
    {

        SqlConnection con;
        List<Item> Pi = new List<Item>();
        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT i.projectNum, i.itemNum, i.itemName, i.itemStatus, i.groupCount, p.projectName FROM item i INNER JOIN dbo.Project p ON i.projectNum = p.projectNum";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //int PID = Convert.ToInt32(cmd.ExecuteScalar());
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection

                Item I = new Item();
                I.ItemNum = Convert.ToString(dr["itemNum"]);
                I.ItemName = Convert.ToString(dr["itemName"]);
                I.ItemStatus = Convert.ToString(dr["itemStatus"]);
                //I.ItemCompletedPercentage = Convert.ToDouble(dr["completedPercent"]);
                I.ItemGroupCount = Convert.ToInt32(dr["groupCount"]);
                I.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                I.ProjectName = Convert.ToString(dr["projectName"]);
                //I.SupplyDate = Convert.ToDateTime(dr["supplyDate"]);
                //I.ProjectStatus = Convert.ToString(dr["projectStatus "]);
                //I.Comment = Convert.ToString(dr["comment "]);
                //I.ProdEntranceDate = Convert.ToDateTime(dr["prodEntranceDate"]);
                Pi.Add(I);
            }
            return Pi.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    //מחזיר את כל החלקים בפריט מסויים שלא משתייכים לקבוצה
    public Part[] GetPartFromItem(float projNumStatus, string itemNumStatus)
    {
        SqlConnection con;
        List<Part> lp = new List<Part>();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Part where projectNum=" + projNumStatus + " and itemNum='" + itemNumStatus + "' and (groupName IS NULL OR	groupName='')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Part p = new Part();
                p.PartBarCode = Convert.ToString(dr["barcode"]);
                p.PartNum = Convert.ToString(dr["partNum"]);
                p.PartName = Convert.ToString(dr["partName"]);
                p.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                p.ItemNum = Convert.ToString(dr["itemNum"]);
                p.GroupName = Convert.ToString(dr["groupName"]);
                p.PartKantim = Convert.ToString(dr["partKantim"]);
                p.PartFirstMachine = Convert.ToString(dr["PartFirstMachine"]);
                p.PartSecondMachine = Convert.ToString(dr["PartSecondMachine"]);
                p.PartSetNumber = Convert.ToInt32(dr["setNum"]);
                p.PartStatus = Convert.ToString(dr["partStatus"]);
                p.PartQuantity = Convert.ToInt32(dr["PartQuantity"]);
                p.PartMaterial = Convert.ToString(dr["PartMaterial"]);
                p.PartColor = Convert.ToString(dr["PartColor"]);
                p.PartLength = Convert.ToInt32(dr["PartLength"]);
                p.PartWidth = Convert.ToInt32(dr["PartWidth"]);
                p.PartThickness = Convert.ToInt32(dr["PartThickness"]);
                p.AdditionToLength = Convert.ToInt32(dr["AdditionToLength"]);
                p.AdditionToWidth = Convert.ToInt32(dr["AdditionToWidth"]);
                p.AdditionToThickness = Convert.ToInt32(dr["AdditionToThickness"]);
                p.PartCropType = Convert.ToString(dr["PartCropType"]);
                p.PartCategory = Convert.ToString(dr["PartCategory"]);
                p.PartComment = Convert.ToString(dr["PartComment"]);

                //if (!DBNull.Value.Equals(dr["comment"]))
                //{
                //    p.Comment = Convert.ToString(dr["comment"]);
                //}

                lp.Add(p);
            }
            return lp.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

    }

    public Part[] GetGroupParts(string GroupName, string projectNum, string itemNum)
    {

        SqlConnection con;
        List<Part> Plist = new List<Part>();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Part where groupName = '" + GroupName + "' AND projectNum =" + Convert.ToSingle(projectNum) + "  AND itemNum ='" + itemNum + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            //int PID = Convert.ToInt32(cmd.ExecuteScalar());
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection

                Part p = new Part();
                p.PartBarCode = Convert.ToString(dr["barcode"]);
                p.PartNum = Convert.ToString(dr["partNum"]);
                p.PartName = Convert.ToString(dr["partName"]);
                p.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                p.ItemNum = Convert.ToString(dr["itemNum"]);
                p.GroupName = Convert.ToString(dr["groupName"]);
                p.PartKantim = Convert.ToString(dr["partKantim"]);
                p.PartFirstMachine = Convert.ToString(dr["PartFirstMachine"]);
                p.PartSecondMachine = Convert.ToString(dr["PartSecondMachine"]);
                p.PartSetNumber = Convert.ToInt32(dr["setNum"]);
                p.PartStatus = Convert.ToString(dr["partStatus"]);
                p.PartQuantity = Convert.ToInt32(dr["PartQuantity"]);
                p.PartMaterial = Convert.ToString(dr["PartMaterial"]);
                p.PartColor = Convert.ToString(dr["PartColor"]);
                p.PartLength = Convert.ToInt32(dr["PartLength"]);
                p.PartWidth = Convert.ToInt32(dr["PartWidth"]);
                p.PartThickness = Convert.ToInt32(dr["PartThickness"]);
                p.AdditionToLength = Convert.ToInt32(dr["AdditionToLength"]);
                p.AdditionToWidth = Convert.ToInt32(dr["AdditionToWidth"]);
                p.AdditionToThickness = Convert.ToInt32(dr["AdditionToThickness"]);
                p.PartCropType = Convert.ToString(dr["PartCropType"]);
                p.PartCategory = Convert.ToString(dr["PartCategory"]);
                p.PartComment = Convert.ToString(dr["PartComment"]);

                Plist.Add(p);
            }
            return Plist.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    public Group[] GetGroups(string projectNum, string itemNum)
    {

        SqlConnection con;
        List<Group> Glist = new List<Group>();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Groups where projectNum = " + projectNum + " and itemNum='" + itemNum + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Group g = new Group();
                g.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                g.ItemNum = Convert.ToString(dr["itemNum"]);
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.CurrentGroupStation = Convert.ToString(dr["currentGroupStation"]);
                if (!DBNull.Value.Equals(dr["estCarpTime"]))
                {
                    g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["estPrepTime"]))
                {
                    g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["estColorTime"]))
                {
                    g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
                }
                if (!DBNull.Value.Equals(dr["currentCarpTime"]))
                {
                    g.CurrentCarpTime = Convert.ToInt32(dr["currentCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["currentPrepTime"]))
                {
                    g.CurrentPrepTime = Convert.ToInt32(dr["currentPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["currentColorTime"]))
                {
                    g.CurrentColorTime = Convert.ToInt32(dr["currentColorTime"]);
                }
                Glist.Add(g);
            }
            return Glist.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    //לדשבורד
    public Group[] GetGroupsFromAllProject(string projectNum)
    {

        SqlConnection con;
        List<Group> Glist = new List<Group>();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Groups where projectNum = " + projectNum;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Group g = new Group();
                g.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                g.ItemNum = Convert.ToString(dr["itemNum"]);
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.CurrentGroupStation = Convert.ToString(dr["currentGroupStation"]);
                g.ScannedPartsCount = Convert.ToInt32(dr["scannedPartsCount"]);
                if (!DBNull.Value.Equals(dr["estCarpTime"]))
                {
                    g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["estPrepTime"]))
                {
                    g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["estColorTime"]))
                {
                    g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
                }
                Glist.Add(g);
            }
            return Glist.ToArray();
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    public Group GetSpecificGroup(string GroupName, string projectNum, string itemNum)
    {

        SqlConnection con;
        Group g = new Group();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT * FROM Groups where groupName = '" + GroupName + "' AND projectNum =" + Convert.ToSingle(projectNum) + "  AND itemNum ='" + itemNum + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                g.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                g.ItemNum = Convert.ToString(dr["itemNum"]);
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.CurrentGroupStation = Convert.ToString(dr["currentGroupStation"]);
                //g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                //g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                //g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
                if (!DBNull.Value.Equals(dr["estCarpTime"]))
                {
                    g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["estPrepTime"]))
                {
                    g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["estColorTime"]))
                {
                    g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
                }
                g.GroupPartCount = Convert.ToInt32(dr["partCount"]);
            }
            return g;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }

    public string UserValidation(string department, string password)
    {
        string returnedUser = "NoUser";
        SqlConnection con;

        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        try
        {
            String selectSTR = "SELECT * FROM Person where department='" + department + "' and personPassword='" + password + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                returnedUser = Convert.ToString(dr["department"]);
            }
            return returnedUser;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //שינוי סטטוס בפרוייקט
    public void StatusChange(string projectStatus, float projectNum)
    {
        SqlConnection con = connect("KinartiConnectionString");

        String selectStr = String.Format("SELECT * FROM Project WHERE projectNum= {0}", projectNum); // create the select that will be used by the adapter to select data from the DB

        SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);

        DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB

        da.Fill(ds, "Project");       // Fill the datatable (in the dataset), using the Select command
                                      //dt = ds.Tables[0];          // point to the cars table , which is the only table in this case

        //dt.Rows[PersonId]["active"] = activity;
        ds.Tables["Project"].Rows[0]["projectStatus"] = projectStatus;
        da.Update(ds, "Project");
        con.Close();

    }

    //שינוי סטטוס בחלק
    public void StatusChange(string partStatus, float projNumStatus, string itemNumStatus, string partNumStatus)
    {
        SqlConnection con = connect("KinartiConnectionString");

        // create the select that will be used by the adapter to select data from the DB
        String selectStr = String.Format("SELECT * FROM Part WHERE projectNum= {0} and itemNum= '{1}' and partNum='{2}'", projNumStatus, itemNumStatus, partNumStatus);

        SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);

        // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
        DataSet ds = new DataSet();

        // Fill the datatable (in the dataset), using the Select command
        da.Fill(ds, "Part");

        ds.Tables["Part"].Rows[0]["partStatus"] = partStatus;
        da.Update(ds, "Part");
        con.Close();
    }

    public string BuildUpdateProjectCommand(Project project)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("UPDATE Project SET comment  = '{0}', prodStartDate = '{1}', supplyDate = '{2}' where projectNum  = '{3}'", project.Comment, project.ProdStartDateString, project.SupplyDateString, project.ProjectNum.ToString());

        command = sb.ToString();
        return command;
    }

    //הכנסת נתוני אקסל(פריט) למערכת 
    public void AddNewDataToDB(Project NewData)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildNewDataInsertCommand(NewData, IsProjectExist(Convert.ToString(NewData.ProjectNum)));      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (SqlException e)
        {
            if (e.Number == 2627)
            {
                throw new DuplicatePrimaryKeyException();
            }
            else
            {
                throw (e);
            }
        }
        catch (Exception e)
        {
            // write to log
            throw (e);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //בודק האם פרוייקט כבר קיים במערכת
    public bool IsProjectExist(string projectnum)
    {
        SqlConnection con;
        bool Exist = false;

        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT projectNum FROM dbo.project WHERE projectNum = " + projectnum;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                if(string.IsNullOrEmpty(Convert.ToString(dr["projectNum"])))
                {
                    Exist = false;
                }
                else
                {
                    Exist = true;
                }
            }
            return Exist;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    public int UpdateProject(Project project)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateProjectCommand(project);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //פקודת מסד נתונים להכנסת נתוני האקסל(פריט) למערכת
    public string BuildNewDataInsertCommand(Project NewData, bool isprojectexist)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbProj = new StringBuilder();
        StringBuilder sbItem = new StringBuilder();
        StringBuilder sbPartAtt = new StringBuilder();
        StringBuilder sbPartVal = new StringBuilder();


        // use a string builder to create the dynamic string
        if (isprojectexist)
        {
            sbProj.AppendFormat("");
        }
        else
        {
            sbProj.AppendFormat("INSERT INTO Project (projectNum, projectName, prodEntranceDate, projectStatus, relateTO) VALUES({0}, '{1}', '{2}', '{3}', '{4}')", NewData.ProjectNum, NewData.ProjectName, NewData.ProdEntranceDate, NewData.ProjectStatus, "Project");
        }
        sbItem.AppendFormat("INSERT INTO Item (projectNum, itemNum, itemName, itemStatus, relateTO) VALUES({0}, {1}, '{2}', '{3}', '{4}')", NewData.ProjectNum, NewData.Item.ItemNum, NewData.Item.ItemName, NewData.Item.ItemStatus, "Item");
        sbPartAtt.AppendFormat(@"INSERT INTO Part (partNum, partName, partStatus, setNum, barcode, partKantim, PartFirstMachine,
    PartSecondMachine, PartQuantity, PartMaterial, PartColor, PartCropType, PartCategory, PartComment, PartLength,
    PartWidth, PartThickness, AdditionToLength, AdditionToWidth, AdditionToThickness, projectNum, itemNum, relateTO)");

        command = sbProj.ToString() + sbItem.ToString() + sbPartAtt.ToString() + " VALUES";

        //פה צריכה להיות לולה שרצה ומכניסה את השורות של החלקים
        for (int i = 0; i < NewData.Item.ItemParts.Count; i++)
        {
            sbPartVal.AppendFormat("('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', '{13}', {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, '{22}')", NewData.Item.ItemParts[i].PartNum, NewData.Item.ItemParts[i].PartName, NewData.Item.ItemParts[i].PartStatus, NewData.Item.ItemParts[i].PartSetNumber, NewData.Item.ItemParts[i].PartBarCode, NewData.Item.ItemParts[i].PartKantim, NewData.Item.ItemParts[i].PartFirstMachine, NewData.Item.ItemParts[i].PartSecondMachine, NewData.Item.ItemParts[i].PartQuantity, NewData.Item.ItemParts[i].PartMaterial, NewData.Item.ItemParts[i].PartColor, NewData.Item.ItemParts[i].PartCropType, NewData.Item.ItemParts[i].PartCategory, NewData.Item.ItemParts[i].PartComment, NewData.Item.ItemParts[i].PartLength, NewData.Item.ItemParts[i].PartWidth, NewData.Item.ItemParts[i].PartThickness, NewData.Item.ItemParts[i].AdditionToLength, NewData.Item.ItemParts[i].AdditionToWidth, NewData.Item.ItemParts[i].AdditionToThickness, NewData.ProjectNum, NewData.Item.ItemNum, "Part");
            if (i < (NewData.Item.ItemParts.Count - 1))
            {
                sbPartVal.Append(",");
            }
        }
        command += sbPartVal.ToString();
        return command;
    }

    public int InsertNewGroup(Group group, int itemGroupCount)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildNewGroupCommand(group, itemGroupCount);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                return -999;
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private string BuildNewGroupCommand(Group group, int itemGroupCount)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();

        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}')", group.ProjectNum, group.ItemNum, group.GroupName, group.GroupRouteName, group.GroupPartCount.ToString(), group.EstPrepTime.ToString(), group.EstCarpTime.ToString(), group.EstColorTime.ToString(), group.GroupStatus, "Groups", 0);
        String prefix = "INSERT INTO Groups (projectNum, itemNum, groupName, routeName, partCount, estPrepTime, estCarpTime, estColorTime, groupStatus, relateTO,scannedPartsCount) ";
        String prefix2 = " Update Part SET groupName='" + group.GroupName + "' WHERE projectNum ='" + group.ProjectNum + "' AND itemNum ='" + group.ItemNum + "' AND partNum IN(";
        for (int i = 0; i < group.ArrPart.Length; i++)
        {
            sb2.AppendFormat("'" + group.ArrPart[i] + "'");
            if (i == group.ArrPart.Length - 1)
            {
                sb2.AppendFormat(")");
            }
            else
            {
                sb2.AppendFormat(", ");
            }
        }
        String IncreaseNumGroupInItem = "Update Item SET groupCount = " + (itemGroupCount + 1) + " WHERE projectNum = " + group.ProjectNum + " and itemNum ='" + group.ItemNum + "'"; ;
        command = prefix + sb.ToString() + prefix2 + sb2.ToString() + IncreaseNumGroupInItem;
        return command;
    }

    public string BuildUpdateCommand(Route route)
    {
        String command;
        StringBuilder sbMachineNum = new StringBuilder();
        //StringBuilder sbStationArr = new StringBuilder();
        //StringBuilder sbStationArrInsert = new StringBuilder();
        // use a string builder to create the dynamic string
        for (int i = 0; i < route.StationArr.Length; i++)
        {
            var pos = i + 1;
            sbMachineNum.AppendFormat(" update StationInRoute SET machineNum={0} where routeName='{1}' and position={2}", route.StationArr[i], route.RouteName, pos);

        }
        command = sbMachineNum.ToString();
        return command;
    }

    public List<Route> GetAllRoutes()
    {
        SqlConnection con;
        List<Route> rl = new List<Route>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT * FROM Route ";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Route r = new Route();
                r.RouteName = Convert.ToString(dr["routeName"]);
                rl.Add(r);
            }
            return rl;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    public List<Route> GetRouteWithoutOld_()
    {
        SqlConnection con;
        List<Route> rl = new List<Route>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT * FROM Route WHERE routeName NOT LIKE 'old_%' ";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Route r = new Route();
                r.RouteName = Convert.ToString(dr["routeName"]);
                rl.Add(r);
            }
            return rl;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    public List<Route> ReadRouteInfo(string conString, string routeName)
    {
        List<Route> lri = new List<Route>();
        SqlConnection con = null;

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select * from StationInRoute as sir inner join Machine as m on sir.machineNum=m.machineNum where sir.routeName='" + routeName + "' order by position";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Route ri = new Route();
                ri.RouteName = Convert.ToString(dr["routeName"]);
                ri.MachineNum = Convert.ToString(dr["machineNum"]);
                ri.MachineName = Convert.ToString(dr["machineName"]);
                ri.Position = Convert.ToInt32(dr["position"]);
                //ri.RouteNum = Convert.ToInt16(dr["routeNum"]);
                lri.Add(ri);
            }
            return lri;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    public List<Machine> ReadMachine(string conString)
    {
        List<Machine> lm = new List<Machine>();
        SqlConnection con = null;

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select * from Machine";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Machine m = new Machine();
                m.MachineName = Convert.ToString(dr["machineName"]);
                m.MachineNum = Convert.ToString(dr["machineNum"]);
                lm.Add(m);
            }
            return lm;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    public int UpdateRoute(Route route)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommand(route);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int InsertStation(Route route)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertStationCommand(route);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private String BuildInsertStationCommand(Route r)
    {
        String command;
        StringBuilder sbRouteName = new StringBuilder();
        StringBuilder sbStationArr = new StringBuilder();
        StringBuilder sbStationArrInsert = new StringBuilder();
        // use a string builder to create the dynamic string
        sbRouteName.AppendFormat("INSERT INTO Route (routeName) values('{0}')", r.RouteName);
        sbStationArr.AppendFormat(" INSERT INTO StationInRoute ([routeName],[machineNum],[position])");
        command = sbRouteName.ToString() + sbStationArr.ToString() + " values";
        for (int i = 1; i <= r.StationArr.Length; i++)
        {
            sbStationArrInsert.AppendFormat(" ('{0}',{1},{2})", r.RouteName, r.StationArr[i - 1], i);
            if (i < r.StationArr.Length)
            {
                sbStationArrInsert.Append(",");
            }
        }
        command += sbStationArrInsert.ToString();
        return command;
    }

    public string RouteValidation(string routeName)
    {
        string returnedRoute = "NoRoute";
        SqlConnection con;

        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        try
        {
            String selectSTR = "SELECT * FROM Route where routeName='" + routeName + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
                returnedRoute = Convert.ToString(dr["routeName"]);
            }
            return returnedRoute;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //עידכון הערכת זמנים של קבוצה
    public void UpdateGroupEstTime(string prepTime, string carpTime, string paintTime, string projectNum, string itemNum, string groupName)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateGroupEstTimeCommand(prepTime, carpTime, paintTime, projectNum, itemNum, groupName);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // בניית הפקודה של עידכון הערכת זמנים לקבוצה
    public string BuildUpdateGroupEstTimeCommand(string prepTime, string carpTime, string paintTime, string projectNum, string itemNum, string groupName)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdateGroupEstTime = new StringBuilder();

        // use a string builder to create the dynamic string
        sbUpdateGroupEstTime.AppendFormat("UPDATE Groups SET estPrepTime = {0}, estCarpTime = {1}, estColorTime = {2} WHERE projectNum = '{3}' and itemNum = '{4}' and groupName = '{5}'", prepTime, carpTime, paintTime, projectNum, itemNum, groupName);

        command = sbUpdateGroupEstTime.ToString();
        return command;
    }

    //מוחק חלק מקבוצה
    public string DeletePartFromGroup(string partBarcode, int PartCount, string projectNum, string itemNum, string GroupName)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeletePartFromGroupCommand(partBarcode, PartCount, projectNum, itemNum, GroupName);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return partBarcode;
    }

    //בניית פקודה למחיקת חלק מקבוצה
    public string BuildDeletePartFromGroupCommand(string partBarcode, int PartCount, string projectNum, string itemNum, string GroupName)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbDeletePartFromGroup = new StringBuilder();
        StringBuilder sbDecreasePartCount = new StringBuilder();

        // use a string builder to create the dynamic string
        sbDeletePartFromGroup.AppendFormat("UPDATE Part SET groupName = '' WHERE barcode = '{0}'", partBarcode);
        sbDecreasePartCount.AppendFormat("UPDATE Groups SET partCount = {0} WHERE projectNum='{1}' AND itemNum='{2}' AND groupName = '{3}'", PartCount, projectNum, itemNum, GroupName);
        //sbDeleteGroup.AppendFormat("DELETE FROM Groups WHERE groupName = '{0}'", GroupName);

        command = sbDeletePartFromGroup.ToString() + sbDecreasePartCount.ToString();
        return command;
    }

    //מוחק את הקבוצה והחלקים שיש בקבוצה זו
    public string DeleteGroup(string projNum, string itemNum, string groupName, string[] barcodes, int itemGroupCount)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteGroupCommand(projNum, itemNum, groupName, barcodes, itemGroupCount);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "";
    }

    //פקודת מסד נתונים למחיקת הקבוצה והחלקים שלה
    public string BuildDeleteGroupCommand(string projNum, string itemNum, string groupName, string[] barcodes, int itemGroupCount)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbDeletePartFromGroup = new StringBuilder();
        StringBuilder sbDeleteGroup = new StringBuilder();
        StringBuilder sbDecreasePartCount = new StringBuilder();
        StringBuilder sbDecreaseGroupCountFromItem = new StringBuilder();


        sbDeletePartFromGroup.AppendFormat("UPDATE Part SET groupName = '', partStatus='חלק טרם נסרק' WHERE barcode in(");
        for (int i = 0; i < (barcodes.Length - 1); i++)
        {
            sbDeletePartFromGroup.AppendFormat("'{0}',", barcodes[i]);
        }
        sbDeletePartFromGroup.AppendFormat("'{0}')", barcodes[barcodes.Length - 1]);

        //sbDecreasePartCount.AppendFormat("UPDATE Groups SET partCount = {0} WHERE groupName = '{1}'", PartCount, GroupName);
        sbDeleteGroup.AppendFormat("DELETE FROM Groups WHERE projectNum='{0}' AND itemNum='{1}' AND groupName = '{2}'", projNum, itemNum, groupName);

        sbDecreaseGroupCountFromItem.AppendFormat("UPDATE Item SET groupCount = {0} WHERE projectNum = '{1}' and itemNum = '{2}'", itemGroupCount - 1, projNum, itemNum);
        command = sbDeletePartFromGroup.ToString() + sbDeleteGroup.ToString() + sbDecreaseGroupCountFromItem.ToString();
        return command;
    }


    public string FirstScanPartOfGroup(string PartBarCode, string StationName, string CurrentDate, string NextGroupStationNo, int ScannedPartCount)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildFirstScanPartOfGroupCommand(PartBarCode, StationName, CurrentDate, NextGroupStationNo, ScannedPartCount);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "scanned";
    }
    //סריקה של חלק ראשון בקבוצה שעדיין לא התחילה
    public string BuildFirstScanPartOfGroupCommand(string PartBarCode, string StationName, string CurrentDate, string NextGroupStationNo, int ScannedPartCount)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdatePartStatus = new StringBuilder();
        StringBuilder sbUpdateGroupScannedParts = new StringBuilder();
        StringBuilder sbUpdateProjectStartTime = new StringBuilder();

        // use a string builder to create the dynamic string
        sbUpdatePartStatus.AppendFormat("UPDATE Part SET partStatus = '{0}', lastScanDate = '{1}' WHERE barcode = '{2}'", StationName, CurrentDate, PartBarCode);
        sbUpdateGroupScannedParts.AppendFormat(" UPDATE G SET G.scannedPartsCount = {0}, G.groupStatus = '{1}', G.currentGroupStation = '{2}' FROM dbo.Groups AS G INNER JOIN dbo.Part AS P  ON G.groupName = P.groupName WHERE barcode = '{3}' AND G.itemNum = P.itemNum AND G.projectNum = P.projectNum", ScannedPartCount, StationName, NextGroupStationNo, PartBarCode);
        sbUpdateProjectStartTime.AppendFormat(" UPDATE Pr SET Pr.prodStartDate = '{0}' FROM dbo.Project AS Pr INNER JOIN dbo.Part AS P ON Pr.projectNum = P.projectNum WHERE barcode = '{1}' AND Pr.projectNum = P.projectNum", CurrentDate, PartBarCode);

        command = sbUpdatePartStatus.ToString() + sbUpdateGroupScannedParts.ToString() + sbUpdateProjectStartTime.ToString();
        return command;
    }


    public string ScanPart(string PartBarCode, string StationName, int ScannedPartCount, string CurrentDate, int CategoryTime, string CategoryType)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildScanBarCodeCommand(PartBarCode, StationName, ScannedPartCount, CurrentDate, CategoryTime, CategoryType);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "scanned";
    }
    //סריקה
    public string BuildScanBarCodeCommand(string PartBarCode, string StationName, int ScannedPartCount, string CurrentDate, int CategoryTime, string CategoryType)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdatePartStatus = new StringBuilder();
        StringBuilder sbUpdateGroupScannedParts = new StringBuilder();
        StringBuilder sbUpdateProjectStartTime = new StringBuilder();

        // use a string builder to create the dynamic string
        sbUpdatePartStatus.AppendFormat("UPDATE Part SET partStatus = '{0}', lastScanDate = '{1}' WHERE barcode = '{2}'", StationName, DateTime.Parse(CurrentDate, new CultureInfo("en-US", true)), PartBarCode);
        sbUpdateGroupScannedParts.AppendFormat("UPDATE G SET G.scannedPartsCount = {0}, G.current" + CategoryType + "Time = {1} FROM dbo.Groups AS G INNER JOIN dbo.Part AS P  ON G.groupName = P.groupName WHERE barcode = '{2}' AND G.itemNum = P.itemNum AND G.projectNum = P.projectNum", ScannedPartCount, CategoryTime, PartBarCode);

        command = sbUpdatePartStatus.ToString() + sbUpdateGroupScannedParts.ToString();
        return command;
    }

    //סריקה של חלק ראשון בקבוצה חדשה
    public string PartScannedInNewStation(string PartBarCode, string NextGroupStationNo, string StationName, string CurrentDate, int CategoryTime, string CategoryType)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildPartScannedInNewStationCommand(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "scanned";
    }

    public string BuildPartScannedInNewStationCommand(string PartBarCode, string NextGroupStationNo, string StationName, string CurrentDate, int CategoryTime, string CategoryType)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdateScannedPartStatus = new StringBuilder();
        StringBuilder sbUpdateAllTheRestPartStatus = new StringBuilder();
        StringBuilder sbUpdateGroupScannedParts = new StringBuilder();

        // משנה את הסטטוס של החלק שנסרק בסטטוס של התחנה החדשה
        sbUpdateScannedPartStatus.AppendFormat("UPDATE Part SET partStatus = '{0}', lastScanDate = '{1}' WHERE barcode = '{2}'", StationName, CurrentDate, PartBarCode);
        //צריך לראות האם הוא מצליח להכניס פה נכון את הערך איפה שרשום בעברית.
        // שם את שאר החלקים בסטטוס ממתין לתחנה חוץ מהחלק ששינינו לו את הסטטוס פה למעלה
        sbUpdateAllTheRestPartStatus.AppendFormat("UPDATE p SET p.partStatus = 'ממתין ל- {0} בעגלה', p.lastScanDate = NULL FROM dbo.Part p WHERE p.groupName IN (SELECT p.groupName from dbo.Part p WHERE p.barcode = '{1}') AND p.itemNum  IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '{2}') AND p.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '{3}') AND p.partStatus NOT LIKE '{4}'", StationName, PartBarCode, PartBarCode, PartBarCode, StationName);
        // מחזיר את כמות החלקים שנסרקו להיות -1 ושם סטטוס קבוצה לסטטוס של המכונה שעכשיו כמו כן מעדכן את הזמן של הקטגוריה
        sbUpdateGroupScannedParts.AppendFormat("UPDATE G SET G.scannedPartsCount = 1, G.groupStatus = '{0}', G.currentGroupStation = '{1}', G.Current" + CategoryType + "Time = {2} FROM dbo.Groups AS G INNER JOIN dbo.Part AS P  ON G.groupName = P.groupName WHERE barcode = '{3}' AND G.itemNum = P.itemNum AND G.projectNum = P.projectNum", StationName, NextGroupStationNo, CategoryTime, PartBarCode);

        command = sbUpdateScannedPartStatus.ToString() + sbUpdateAllTheRestPartStatus.ToString() + sbUpdateGroupScannedParts.ToString();
        return command;
    }



    //סריקה של חלק ראשון בקבוצה חדשה אחרי שהטרד סיים
    public string PartScannedInNewStationAfterThread(string PartBarCode, string NextGroupStationNo, string StationName, string CurrentDate, int CategoryTime, string CategoryType)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildPartScannedInNewStationAfterThreadCommand(PartBarCode, NextGroupStationNo, StationName, CurrentDate, CategoryTime, CategoryType);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "scanned";
    }

    public string BuildPartScannedInNewStationAfterThreadCommand(string PartBarCode, string NextGroupStationNo, string StationName, string CurrentDate, int CategoryTime, string CategoryType)
    {// צריך לעשות רק פקודה שמשנה את הסטטוס של החלק שנסרק והקבוצה שלו, לעדכן את הזמן סריקה של החלק שנסרק ושאר החלקים
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdateScannedPartStatus = new StringBuilder();
        StringBuilder sbUpdateAllTheRestPartStatus = new StringBuilder();
        StringBuilder sbUpdateGroupScannedParts = new StringBuilder();

        // משנה את הסטטוס של החלק שנסרק בסטטוס של התחנה החדשה
        sbUpdateScannedPartStatus.AppendFormat("UPDATE Part SET partStatus = '{0}', lastScanDate = '{1}' WHERE barcode = '{2}'", StationName, CurrentDate, PartBarCode);
        //צריך לראות האם הוא מצליח להכניס פה נכון את הערך איפה שרשום בעברית.
        // שם את שאר החלקים בסטטוס ממתין לתחנה חוץ מהחלק ששינינו לו את הסטטוס פה למעלה
        sbUpdateAllTheRestPartStatus.AppendFormat("UPDATE p SET p.lastScanDate = NULL FROM dbo.Part p WHERE p.groupName IN (SELECT p.groupName from dbo.Part p WHERE p.barcode = '{1}') AND p.itemNum  IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '{2}') AND p.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '{3}') AND p.partStatus NOT LIKE '{4}'", StationName, PartBarCode, PartBarCode, PartBarCode, StationName);
        // מחזיר את כמות החלקים שנסרקו להיות -1 ושם סטטוס קבוצה לסטטוס של המכונה שעכשיו כמו כן מעדכן את הזמן של הקטגוריה
        sbUpdateGroupScannedParts.AppendFormat("UPDATE G SET G.scannedPartsCount = 1, G.groupStatus = '{0}', G.currentGroupStation = '{1}', G.Current" + CategoryType + "Time = {2} FROM dbo.Groups AS G INNER JOIN dbo.Part AS P  ON G.groupName = P.groupName WHERE barcode = '{3}' AND G.itemNum = P.itemNum AND G.projectNum = P.projectNum", StationName, NextGroupStationNo, CategoryTime, PartBarCode);

        command = sbUpdateScannedPartStatus.ToString() + sbUpdateAllTheRestPartStatus.ToString() + sbUpdateGroupScannedParts.ToString();
        return command;
    }


    //כאשר החלק האחרון בקבוצה נסרק ועבר הזמן החציוני שלו - מגיע לפה כדי לשנות לו את הסטטוסים לממתינים
    public string UpdateStatusWaitingForMachine(string PartBarCode, string NextGroupStationName, string GroupName, string CategoryType, int CategoryTime, int PartTimeAvg)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateStatusWaitingForMachineCommand(PartBarCode, NextGroupStationName, GroupName, CategoryType, CategoryTime, PartTimeAvg);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return "scanned";
    }


    public string BuildUpdateStatusWaitingForMachineCommand(string PartBarCode, string NextGroupStationName, string GroupName, string CategoryType, int CategoryTime, int PartTimeAvg)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");
        StringBuilder sbUpdateGroupStatus = new StringBuilder();
        StringBuilder sbUpdateAllPartStatus = new StringBuilder();
        StringBuilder sbUpdateCurrentCategoryTime = new StringBuilder();

        // שם את כל החלקים בסטטוס ממתין לתחנה הבאה  
        sbUpdateAllPartStatus.AppendFormat("UPDATE p SET p.partStatus = 'ממתין ל- {0} בעגלה' FROM dbo.Part p WHERE p.groupName IN (SELECT p.groupName from dbo.Part p WHERE p.barcode = '{1}') AND p.itemNum IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '{2}') AND p.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '{3}')", NextGroupStationName, PartBarCode, PartBarCode, PartBarCode);
        //  מעדכן את הקטגוריה בזמן העדכני ביותר ושם את הסטטוס של הקבוצה לממתין לתחנה הבאה
        sbUpdateGroupStatus.AppendFormat("UPDATE g SET groupStatus = 'ממתין ל- {0} בעגלה', g.Current" + CategoryType + "Time = {1} FROM dbo.groups g WHERE g.groupName = '{2}' AND g.itemNum IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '{3}') AND g.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '{4}')", NextGroupStationName, (CategoryTime + PartTimeAvg), GroupName, PartBarCode, PartBarCode);

        command = sbUpdateGroupStatus.ToString() + sbUpdateAllPartStatus.ToString() + sbUpdateCurrentCategoryTime.ToString();

        return command;
    }


    //מביא את מספר התחנה הנוכחי של הקבוצה שהחלק שייך אליו
    public string GetCurrentGroupStationNo(string barcode)
    {
        SqlConnection con;
        string CurrentGroupStationNo = null;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT G.currentGroupStation FROM dbo.Groups AS G INNER JOIN dbo.Part AS P ON G.groupName = P.groupName WHERE barcode = '" + barcode + "' AND G.itemNum = P.itemNum AND G.projectNum = P.projectNum";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                CurrentGroupStationNo = Convert.ToString(dr["currentGroupStation"]);
            }
            return CurrentGroupStationNo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מביא את המיקום של התחנה במסלול שלו
    public string GetCurrentGroupPositionNo(string barcode, string CurrentGroupStationNo)
    {
        SqlConnection con;
        string CurrentGroupPositionNo = null;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT SIR.position FROM dbo.Machine AS M INNER JOIN dbo.StationInRoute AS SIR ON M.machineNum = SIR.machineNum INNER JOIN dbo.Groups AS G ON G.routeName = SIR.routeName WHERE G.groupName in (SELECT p.groupName from dbo.Part p WHERE p.barcode = '" + barcode + "') AND M.machineNum = " + CurrentGroupStationNo;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                CurrentGroupPositionNo = Convert.ToString(dr["position"]);
            }
            return CurrentGroupPositionNo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מביא את המיקום הבא של הקבוצה השייכת לחלק לפי המסלול של הקבוצה
    public string GetNextGroupStationNo(string CurrentGroupPositionNo, string barcode)
    {
        SqlConnection con;
        string NextGroupStationNo = null;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT M.machineNum FROM dbo.Machine AS M INNER JOIN dbo.StationInRoute AS SIR ON M.machineNum = SIR.machineNum INNER JOIN dbo.Groups AS G ON G.routeName = SIR.routeName WHERE G.groupName in (SELECT p.groupName from dbo.Part p WHERE p.barcode = '" + barcode + "') AND SIR.position =" + CurrentGroupPositionNo + " AND G.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '" + barcode + "') AND G.itemNum IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '" + barcode + "')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                NextGroupStationNo = Convert.ToString(dr["machineNum"]);
            }
            return NextGroupStationNo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }


    //מביא את שם המיקום הבא של הקבוצה השייכת לחלק לפי המסלול של הקבוצה
    public string GetNextGroupStationName(string CurrentGroupPositionNo, string barcode)
    {
        SqlConnection con;
        string NextGroupStationName = null;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT M.machineName FROM dbo.Machine AS M INNER JOIN dbo.StationInRoute AS SIR ON M.machineNum = SIR.machineNum INNER JOIN dbo.Groups AS G ON G.routeName = SIR.routeName WHERE G.groupName in (SELECT p.groupName from dbo.Part p WHERE p.barcode = '" + barcode + "') AND SIR.position =" + CurrentGroupPositionNo + " AND G.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '" + barcode + "') AND G.itemNum IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '" + barcode + "')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                NextGroupStationName = Convert.ToString(dr["machineName"]);
            }
            return NextGroupStationName;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מביא את כמות התחנות שיש במסלול
    public int GetTotalStationCount(string barcode)
    {
        SqlConnection con;
        //שים לבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבב אולי לא כדאי שזה יתחיל ב-0
        int RouteStationCount = 0;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT count(*) AS TotalStations FROM dbo.Machine AS M INNER JOIN dbo.StationInRoute AS SIR ON M.machineNum = SIR.machineNum INNER JOIN dbo.Groups AS G ON G.routeName = SIR.routeName WHERE G.groupName IN (SELECT p.groupName from dbo.Part p WHERE p.barcode = '" + barcode + "') AND G.itemNum IN (SELECT p.itemNum from dbo.Part p WHERE p.barcode = '" + barcode + "') AND G.projectNum IN (SELECT p.projectNum from dbo.Part p WHERE p.barcode = '" + barcode + "')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                RouteStationCount = Convert.ToInt32(dr["TotalStations"]);
            }
            return RouteStationCount;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מחזירה את המספר תחנה עליה לחצו
    public string GetClickedStationNo(string StationName)
    {
        SqlConnection con;
        string StationNo = null;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT m.machineNum FROM dbo.Machine m WHERE m.machineName = '" + StationName + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                StationNo = Convert.ToString(dr["machineNum"]);
            }
            return StationNo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מחזיר כמות חלקים שיש בקבוצה
    public int GetpartCount(string barcode)
    {
        SqlConnection con;
        //שים לבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבב אולי לא כדאי שזה יתחיל ב-0
        int partCount = 0;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT g.partCount FROM dbo.Groups g INNER JOIN dbo.Part p ON g.groupName = p.groupName WHERE p.barcode = '" + barcode + "' AND g.itemNum = p.itemNum AND g.projectNum = p.projectNum";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                partCount = Convert.ToInt32(dr["partCount"]);
            }
            return partCount;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מחזיר כמות חלקים שנסרקו כבר בקבוצה
    public int GetScannedPartCount(string barcode)
    {
        SqlConnection con;
        //שים לבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבבב אולי לא כדאי שזה יתחיל ב-0
        int scannedPartsCount = 0;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT g.scannedPartsCount FROM dbo.Groups g INNER JOIN dbo.Part p ON g.groupName = p.groupName WHERE p.barcode = '" + barcode + "' AND g.itemNum = p.itemNum AND g.projectNum = p.projectNum";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                scannedPartsCount = Convert.ToInt32(dr["scannedPartsCount"]);
            }
            return scannedPartsCount;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מחזיר את שם הקוצה של החלק שנסרק
    public string GetGroupName(string barcode)
    {
        SqlConnection con;

        string GroupName = "";

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT p.groupName FROM dbo.Part p WHERE p.barcode = '" + barcode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                GroupName = Convert.ToString(dr["groupName"]);
            }
            return GroupName;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //מחזיר את הסטטוס של החלק שנסרק
    public string GetScannedPartStatus(string barcode)
    {
        SqlConnection con;

        string ScannedPartStatus = "";

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT p.partStatus FROM dbo.Part p WHERE p.barcode = '" + barcode + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ScannedPartStatus = Convert.ToString(dr["partStatus"]);
            }
            return ScannedPartStatus;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }


    public string GetLatScanPartDate(string PartBarCode, string StationName, string CurrentDate, string GroupName)
    {
        SqlConnection con;

        string lastScanDate = "";

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT top 1 p.lastScanDate FROM dbo.part p WHERE p.groupName = '" + GroupName + "' and p.lastScanDate is not null and p.itemNum in(select itemNum from part where barcode = '" + PartBarCode + "') and p.projectNum in(select projectNum from part where barcode = '" + PartBarCode + "') order by lastScanDate desc";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                lastScanDate = Convert.ToString(dr["lastScanDate"]);
            }
            return lastScanDate;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }


    public string GetCategoryType(string StationName)
    {
        SqlConnection con;

        string CategoryType = "";

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "select machineCategory from Machine where machineName = '" + StationName + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                CategoryType = Convert.ToString(dr["machineCategory"]);
            }
            return CategoryType;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }



    public int GetMedianTimeForPart(string PartBarCode, string GroupName, string CurrentDate)
    {
        SqlConnection con;

        List<int> Gap = new List<int>();
        int Median = 0;
        int mone = 0;
        bool FirstTime = true;
        DateTime BeforeScannedTime = new DateTime();
        DateTime CurrentScannedTime = new DateTime();

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT lastScanDate FROM dbo.part p WHERE groupName = '" + GroupName + "' and p.itemNum in(select itemNum from part where barcode = '" + PartBarCode + "') and p.projectNum in(select projectNum from part where barcode = '" + PartBarCode + "') order by lastScanDate DESC";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                if (!FirstTime)
                {
                    if (dr["lastScanDate"] is DBNull)
                    {
                        //CurrentScannedTime = Convert.ToDateTime(CurrentDate);
                        CurrentScannedTime = DateTime.Parse(CurrentDate, new CultureInfo("en-US", true));
                    }
                    else
                    {
                        CurrentScannedTime = Convert.ToDateTime(dr["lastScanDate"]);
                    }
                    Gap.Add((int)Math.Round(BeforeScannedTime.Subtract(CurrentScannedTime).TotalMinutes));
                }
                else
                {
                    CurrentScannedTime = Convert.ToDateTime(dr["lastScanDate"]);
                    FirstTime = false;
                }
                mone++;
                BeforeScannedTime = CurrentScannedTime;
            }

            Gap.Sort();
            Median = Gap[(int)Math.Round((double)(Gap.Count / 2))];

            return Median;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    public int GetLatScanCategoryDate(string PartBarCode, string StationName, string CurrentDate, string CategoryType, string GroupName)
    {
        SqlConnection con;

        int CurrentCategoryTime = 0;

        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            String selectSTR = "SELECT G.current" + CategoryType + "Time FROM dbo.Groups G WHERE G.groupName = '" + GroupName + "' and G.itemNum in(select itemNum from part where barcode = '" + PartBarCode + "') and G.projectNum in(select projectNum from part where barcode = '" + PartBarCode + "')";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                if (dr["current" + CategoryType + "Time"] is DBNull)
                {
                    CurrentCategoryTime = 0;
                }
                else
                {
                    CurrentCategoryTime = Convert.ToInt32(dr["current" + CategoryType + "Time"]);
                }
            }
            return CurrentCategoryTime;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }


    //מחזירה מחרוזת "בתהליך" במידה ועברנו תחנה 1 במסלול
    public string CheckGroupPosition(float projectNum, string itemNum, string groupName, string currentGroupStation)
    {
        string returnedGroup = "atStart";
        SqlConnection con;

        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            //SELECT currentGroupStation FROM Groups WHERE projectNum = 111.1 AND itemNum = 'N1' AND groupName = '07-06-2019_alexxxxx' AND currentGroupStation IS NOT NULL
            String selectSTR = "SELECT currentGroupStation FROM Groups WHERE projectNum ='" + projectNum + "' AND itemNum ='" + itemNum + "' AND groupName ='" + groupName + "' AND currentGroupStation IS NOT NULL";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                returnedGroup = Convert.ToString(dr["currentGroupStation"]);
            }
            return returnedGroup;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    //הוספה לקבוצה קיימת שנמצאת בתחנה הראשונה לכל היותר
    public int AddingPartToExistGroup(Group groupInfo, string[] partNumToAddArr)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildAddingPartToExistGroupCommand(groupInfo, partNumToAddArr);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                return -999;
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private string BuildAddingPartToExistGroupCommand(Group groupInfo, string[] partNumToAddArr)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        // use a string builder to create the dynamic string
        String prefix = " Update Part SET groupName='" + groupInfo.GroupName + "' WHERE projectNum ='" + groupInfo.ProjectNum + "' AND itemNum ='" + groupInfo.ItemNum + "' AND partNum IN(";
        for (int i = 0; i < partNumToAddArr.Length; i++)
        {
            sb.AppendFormat("'" + partNumToAddArr[i] + "'");
            if (i == partNumToAddArr.Length - 1)
            {
                sb.AppendFormat(")");
            }
            else
            {
                sb.AppendFormat(", ");
            }
        }
        // use a string builder to create the dynamic string
        sb2.AppendFormat(" UPDATE Groups SET partCount  = '{0}' where projectNum  = '{1}' and itemNum='{2}'", (groupInfo.GroupPartCount + partNumToAddArr.Length).ToString(), groupInfo.ProjectNum, groupInfo.ItemNum);
        command = prefix + sb.ToString() + sb2.ToString();
        return command;
    }

    //בניית קבוצת השלמה לקבוצה שקיימת ונמצאת באמצע המסלול
    public int AccomplishGroup(Group groupInfo, string[] partNumToAddArr)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("KinartiConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = AccomplishGroupCommand(groupInfo, partNumToAddArr);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //אפשר להוסיף זריקה לאקספשיונס של מספר השורות שעודכנו שווה למספר השורות שעודכנו
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                return -999;
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private string AccomplishGroupCommand(Group groupInfo, string[] partNumToAddArr)
    {
        String command;
        SqlConnection con;
        con = connect("KinartiConnectionString");

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        string dateNow = DateTime.Today.ToString("dd-MM-yyyy");
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}', '{4}', '{5}', '{6}','{7}')", groupInfo.ProjectNum, groupInfo.ItemNum, dateNow + '_' + groupInfo.GroupName, groupInfo.GroupRouteName, partNumToAddArr.Length.ToString(), groupInfo.GroupStatus, "Groups", 0);
        String prefix = "INSERT INTO Groups (projectNum, itemNum, groupName, routeName, partCount, groupStatus, relateTO,scannedPartsCount) ";
        String prefix2 = " Update Part SET groupName='" + dateNow + '_' + groupInfo.GroupName + "' WHERE projectNum ='" + groupInfo.ProjectNum + "' AND itemNum ='" + groupInfo.ItemNum + "' AND partNum IN(";
        for (int i = 0; i < partNumToAddArr.Length; i++)
        {
            sb2.AppendFormat("'" + partNumToAddArr[i] + "'");
            if (i == partNumToAddArr.Length - 1)
            {
                sb2.AppendFormat(")");
            }
            else
            {
                sb2.AppendFormat(", ");
            }
        }
        command = prefix + sb.ToString() + prefix2 + sb2.ToString();
        return command;
    }

    public List<Group> GetAllGroupsFromAllProjects()
    {
        SqlConnection con;
        List<Group> gl = new List<Group>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "select p.projectName,i.itemName,cast(scannedPartsCount as float)/cast(partCount as float) AS 'ScannedPrecent',g.* from Groups as g inner join Item as i on g.itemNum = i.itemNum inner join Project p on i.projectNum = p.projectNum";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                Group g = new Group();
                g.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                g.ProjectName = Convert.ToString(dr["projectName"]);
                g.ItemNum = Convert.ToString(dr["itemNum"]);
                g.ItemName = Convert.ToString(dr["itemName"]);
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.GroupPartCount = Convert.ToInt32(dr["partCount"]);
                g.ScannedPartsCount = Convert.ToInt32(dr["scannedPartsCount"]);

                if (!DBNull.Value.Equals(dr["currentGroupStation"]))
                {
                    g.CurrentGroupStation = Convert.ToString(dr["currentGroupStation"]);
                }
                if (!DBNull.Value.Equals(dr["estCarpTime"]))
                {
                    g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["estPrepTime"]))
                {
                    g.EstCarpTime = Convert.ToInt32(dr["estPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["estColorTime"]))
                {
                    g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
                }

                if (!DBNull.Value.Equals(dr["currentCarpTime"]))
                {
                    g.CurrentCarpTime = Convert.ToInt32(dr["currentCarpTime"]);
                }
                if (!DBNull.Value.Equals(dr["currentPrepTime"]))
                {
                    g.CurrentPrepTime = Convert.ToInt32(dr["currentPrepTime"]);
                }
                if (!DBNull.Value.Equals(dr["currentColorTime"]))
                {
                    g.CurrentColorTime = Convert.ToInt32(dr["currentColorTime"]);
                }

                gl.Add(g);
            }
            return gl;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    //מחזיר את מספר הקבוצות שקיימות בפריט מסויים
    public int GetNumGroupsInItem(string projNum, string itemNum)
    {
        Item I = new Item();
        SqlConnection con;
        try
        {
            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        try
        {
            String selectSTR = "SELECT groupCount FROM Item where projectNum= '" + projNum + "'and itemNum ='" + itemNum + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                I.ItemGroupCount = Convert.ToInt32(dr["groupCount"]);
            }
            return I.ItemGroupCount;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

    }


    public int GroupInRoutePosition(string projectNum, string itemNum, string routeName, string groupName)
    {
        SqlConnection con;
        int pos = 0;
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            //top 1זה בגלל שיש כפל שמכונה יכולה להית פעמיים באותו מסלול, כנשפטל צריך לעדכן את 
            String selectSTR = "SELECT top 1 sir.position FROM StationInRoute sir INNER JOIN Groups g ON	sir.routeName = g.routeName WHERE g.projectNum=" + projectNum + " AND g.itemNum='" + itemNum + "' AND g.groupName='" + groupName +"' AND sir.machineNum = g.currentGroupStation";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                pos = Convert.ToInt32(dr["position"]);
            }
            return pos;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
    }

    // מביאה לי אובייקט עם נתונים להצגה של התחנות בדשבורד
    public List<TempObjForDashboard> GetMachinesCurrentdetails()
    { 
        SqlConnection con;
        
        List<TempObjForDashboard> Lobj = new List<TempObjForDashboard>();
        try
        {

            con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            String selectSTR = "SELECT p.projectName,g.projectNum,i.itemName,g.itemNum, g.groupName, g.groupStatus, g.scannedPartsCount, g.partCount FROM dbo.Groups g INNER JOIN dbo.Item i ON g.projectNum = i.projectNum AND g.itemNum = i.itemNum INNER JOIN dbo.Project p ON i.projectNum = p.projectNum WHERE g.groupStatus in(SELECT m.machineName FROM dbo.Machine m)";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                TempObjForDashboard obj = new TempObjForDashboard();
                obj.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                obj.ProjectName = Convert.ToString(dr["projectName"]);
                obj.ItemNum = Convert.ToString(dr["itemNum"]);
                obj.ItemName = Convert.ToString(dr["itemName"]);
                obj.GroupName = Convert.ToString(dr["groupName"]);
                obj.MachineName = Convert.ToString(dr["groupStatus"]);
                obj.PartCount = Convert.ToInt32(dr["partCount"]);
                obj.ScannedPartCount = Convert.ToInt32(dr["scannedPartsCount"]);
                //obj.GroupStatus = Convert.ToString(dr["groupStatus"]);
                //obj.GroupRouteName = Convert.ToString(dr["routeName"]);
                //obj.GroupPartCount = Convert.ToInt32(dr["partCount"]);
                //obj.ScannedPartsCount = Convert.ToInt32(dr["scannedPartsCount"]);


                Lobj.Add(obj);
            }
            return Lobj;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
    }

    //public int NumOfStationInRoute(string projectNum, string itemNum, string groupName, string currentGroupStation, string routeName)
    //{
    //    SqlConnection con;
    //    int numStation = 0;
    //    try
    //    {

    //        con = connect("KinartiConnectionString"); // create a connection to the database using the connection String defined in the web config file
    //    }

    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);

    //    }

    //    try
    //    {
    //        //top 1זה בגלל שיש כפל שמכונה יכולה להית פעמיים באותו מסלול, כנשפטל צריך לעדכן את 
    //        String selectSTR = "SELECT	sir.position FROM StationInRoute sir INNER JOIN Groups g ON	sir.routeName = g.routeName WHERE g.projectNum=" + projectNum + " AND g.itemNum='" + itemNum + "' AND g.groupName='" + groupName + "' AND sir.machineNum = g.currentGroupStation";
    //        SqlCommand cmd = new SqlCommand(selectSTR, con);
    //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //        while (dr.Read())
    //        {
    //            numStation = Convert.ToInt32(dr["position"]);
    //        }
    //        return numStation;
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);

    //    }
    //}
}
