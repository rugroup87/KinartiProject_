using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using KinartiProject_ruppin.Models;

    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {

        }

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
                I.ItemNum = Convert.ToString(dr["itemNum"]);
                I.ItemName = Convert.ToString(dr["itemName"]);
                I.ItemStatus = Convert.ToString(dr["itemStatus"]);
                //I.ItemCompletedPercentage = Convert.ToDouble(dr["completedPercent"]);
                //I.ItemGroupCount = Convert.ToInt32(dr["groupCount"]);
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
            String selectSTR = "SELECT i.projectNum, i.itemNum, i.itemName, i.itemStatus, p.projectName FROM item i INNER JOIN dbo.Project p ON i.projectNum = p.projectNum";
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
                //I.ItemGroupCount = Convert.ToInt32(dr["groupCount"]);
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
                String selectSTR = "SELECT * FROM Part where projectNum=" + projNumStatus + " and itemNum='" + itemNumStatus + "' and groupName is NULL";
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

        public Part[] GetGroupParts(string GroupName)
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
                String selectSTR = "SELECT * FROM Part where groupName = '" + GroupName + "'";
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
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
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

    public Group GetSpecificGroup(string GroupName)
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
            String selectSTR = "SELECT * FROM Groups where groupName = '" + GroupName + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                g.GroupName = Convert.ToString(dr["groupName"]);
                g.GroupStatus = Convert.ToString(dr["groupStatus"]);
                g.GroupRouteName = Convert.ToString(dr["routeName"]);
                g.EstCarpTime = Convert.ToInt32(dr["estCarpTime"]);
                g.EstPrepTime = Convert.ToInt32(dr["estPrepTime"]);
                g.EstColorTime = Convert.ToInt32(dr["estColorTime"]);
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
            sb.AppendFormat("UPDATE Project SET comment  = '{0}', prodStartDate = '{1}', supplyDate = '{2}' where projectNum  = '{3}'", project.Comment, project.ProdStartDate.ToString(),project.SupplyDate.ToString(),project.ProjectNum.ToString());

            command = sb.ToString();
            return command;
        }

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

            String cStr = BuildNewDataInsertCommand(NewData);      // helper method to build the insert string

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

        public string BuildNewDataInsertCommand(Project NewData)
        {
            String command;
            SqlConnection con;
            con = connect("KinartiConnectionString");
            StringBuilder sbProj = new StringBuilder();
            StringBuilder sbItem = new StringBuilder();
            StringBuilder sbPartAtt = new StringBuilder();
            StringBuilder sbPartVal = new StringBuilder();
    
            
            // use a string builder to create the dynamic string
            sbProj.AppendFormat("INSERT INTO Project (projectNum, projectName, prodStartDate, projectStatus) VALUES({0}, '{1}', '{2}', '{3}')", NewData.ProjectNum, NewData.ProjectName, NewData.ProdStartDate, NewData.ProjectStatus );
            sbItem.AppendFormat("INSERT INTO Item (projectNum, itemNum, itemName, itemStatus) VALUES({0}, {1}, '{2}', '{3}')", NewData.ProjectNum, NewData.Item.ItemNum, NewData.Item.ItemName, NewData.Item.ItemStatus);
            sbPartAtt.AppendFormat(@"INSERT INTO Part (partNum, partName, partStatus, setNum, barcode, partKantim, PartFirstMachine,
    PartSecondMachine, PartQuantity, PartMaterial, PartColor, PartCropType, PartCategory, PartComment, PartLength,
    PartWidth, PartThickness, AdditionToLength, AdditionToWidth, AdditionToThickness, projectNum, itemNum)");

            command = sbProj.ToString() + sbItem.ToString() + sbPartAtt.ToString() + " VALUES";

            //פה צריכה להיות לולה שרצה ומכניסה את השורות של החלקים
            for (int i = 0; i < NewData.Item.ItemParts.Count; i++)
            {
                sbPartVal.AppendFormat("('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', '{13}', {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21})", NewData.Item.ItemParts[i].PartNum, NewData.Item.ItemParts[i].PartName, NewData.Item.ItemParts[i].PartStatus, NewData.Item.ItemParts[i].PartSetNumber, NewData.Item.ItemParts[i].PartBarCode, NewData.Item.ItemParts[i].PartKantim, NewData.Item.ItemParts[i].PartFirstMachine, NewData.Item.ItemParts[i].PartSecondMachine, NewData.Item.ItemParts[i].PartQuantity, NewData.Item.ItemParts[i].PartMaterial, NewData.Item.ItemParts[i].PartColor, NewData.Item.ItemParts[i].PartCropType, NewData.Item.ItemParts[i].PartCategory, NewData.Item.ItemParts[i].PartComment, NewData.Item.ItemParts[i].PartLength, NewData.Item.ItemParts[i].PartWidth, NewData.Item.ItemParts[i].PartThickness, NewData.Item.ItemParts[i].AdditionToLength, NewData.Item.ItemParts[i].AdditionToWidth, NewData.Item.ItemParts[i].AdditionToThickness, NewData.ProjectNum, NewData.Item.ItemNum);
                if (i < (NewData.Item.ItemParts.Count - 1))
                {
                    sbPartVal.Append(",");
                }
            }
            command += sbPartVal.ToString();
            return command;
        }

        public int InsertNewGroup(Group group)
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

            String cStr = BuildNewGroupCommand(group);      // helper method to build the insert string

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

        private String BuildNewGroupCommand(Group group)
        {
            String command;
            SqlConnection con;
            con = connect("KinartiConnectionString");

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
        
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", group.ProjectNum, group.ItemNum, group.GroupName, group.GroupRouteName, group.GroupPartCount.ToString(), group.EstPrepTime.ToString(), group.EstCarpTime.ToString(), group.EstColorTime.ToString());
            String prefix = "INSERT INTO Groups (projectNum, itemNum, groupName, routeName, partCount, estPrepTime, estCarpTime, estColorTime) ";
            String prefix2 = " Update Part SET groupName='" + group.GroupName + "' WHERE projectNum ='" + group.ProjectNum + "' AND itemNum ='" + group.ItemNum + "' AND partNum IN(";
            for (int i = 0; i < group.ArrPart.Length; i++)
            {
                sb2.AppendFormat("'" + group.ArrPart[i] + "'");
                if (i== group.ArrPart.Length-1)
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

    public string BuildUpdateOldRouteCommand(string routeName)
    {
        String command;
        StringBuilder sbUpDateRoutName_In_RouteTable = new StringBuilder();
        StringBuilder sbUpDateRoutName_In_StationInRouteTable = new StringBuilder();
        StringBuilder sbUpDateRoutName_In_GroupsTable = new StringBuilder();
        StringBuilder sbUpDateRoutNameFinal_In_StationInRouteTable = new StringBuilder();
        StringBuilder sbUpDateRoutNameFinal_In_GroupsTable = new StringBuilder();

        sbUpDateRoutName_In_StationInRouteTable.AppendFormat(" update StationInRoute SET routeName='tempRouteName' where routeName='{0}'", routeName);
        sbUpDateRoutName_In_GroupsTable.AppendFormat(" update Groups SET routeName='tempRouteName' where routeName='{0}'", routeName);
        sbUpDateRoutName_In_RouteTable.AppendFormat(" update Route SET routeName='{0}' where routeName='{1}'","old_"+ routeName, routeName);
        sbUpDateRoutNameFinal_In_StationInRouteTable.AppendFormat(" update StationInRoute SET routeName='{0}' where routeName='tempRouteName'", "old_" + routeName);
        sbUpDateRoutNameFinal_In_GroupsTable.AppendFormat(" update Groups SET routeName='{0}' where routeName='tempRouteName'", "old_" + routeName);
        command = sbUpDateRoutName_In_StationInRouteTable.ToString() + sbUpDateRoutName_In_GroupsTable.ToString() + sbUpDateRoutName_In_RouteTable.ToString() + sbUpDateRoutNameFinal_In_StationInRouteTable.ToString() + sbUpDateRoutNameFinal_In_GroupsTable.ToString();
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
                String selectSTR = "SELECT * FROM Route where routeName!='tempRouteName'";
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
                        ri.MachineNum = Convert.ToInt32(dr["machineNum"]);
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
                        m.MachineNum = Convert.ToInt32(dr["machineNum"]);
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

    //public int UpdateRoute(Route route)
    //{
    //    SqlConnection con;
    //    SqlCommand cmd;

    //    try
    //    {
    //        con = connect("KinartiConnectionString"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        write to log
    //                throw (ex);
    //    }

    //    String cStr = BuildUpdateCommand(route);      // helper method to build the insert string

    //    cmd = CreateCommand(cStr, con);             // create the command

    //    try
    //    {
    //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //        return numEffected;
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //        write to log
    //                throw (ex);
    //    }

    //    finally
    //    {
    //        if (con != null)
    //        {
    //            close the db connection
    //                    con.Close();
    //        }
    //    }
    //}

    //public string BuildUpdateCommand(Route route)
    //{
    //    String command;
    //    StringBuilder sbMachineNum = new StringBuilder();

    //    use a string builder to create the dynamic string
    //    for (int i = 0; i < route.StationArr.Length; i++)
    //    {
    //        var pos = i + 1;
    //        sbMachineNum.AppendFormat(" update StationInRoute SET machineNum={0} where routeName='{1}' and position={2}", route.StationArr[i], route.RouteName, pos);

    //    }
    //    command = sbMachineNum.ToString();
    //    return command;
    //}

    public int UpdateOldRoute(string routeName)
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
        String cStr = BuildUpdateOldRouteCommand(routeName);      // helper method to build the insert string

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
                sbRouteName.AppendFormat("INSERT INTO Route (routeName) values('{0}')", r.RouteName );  
                sbStationArr.AppendFormat(" INSERT INTO StationInRoute ([routeName],[machineNum],[position])");
                command = sbRouteName.ToString() + sbStationArr.ToString() + " values";
                for (int i = 1; i <= r.StationArr.Length; i++)
                {
                    sbStationArrInsert.AppendFormat(" ('{0}',{1},{2})", r.RouteName, r.StationArr[i - 1], i);
                    if (i< r.StationArr.Length)
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

    }
