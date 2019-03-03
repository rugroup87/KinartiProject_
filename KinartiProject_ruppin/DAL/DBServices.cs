﻿using System;
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
            con.Open();
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

        //---------------------------------------------------------------------------------
        // Create Project Table
        //---------------------------------------------------------------------------------
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
                //int PID = Convert.ToInt32(cmd.ExecuteScalar());
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {// Read till the end of the data into a row
                 // read first field from the row into the list collection
                    
                    Project p = new Project();
                    p.ProjectNum = Convert.ToSingle(dr["projectNum"]);
                    p.ProjectName = Convert.ToString(dr["projectName"]);
                    if (!DBNull.Value.Equals(dr["prodStartDate"]))
                {
                    p.ProdStartDate = Convert.ToString(dr["prodStartDate"]);
                }
                if (!DBNull.Value.Equals(dr["supplyDate"]))
                {
                    p.SupplyDate = Convert.ToString(dr["supplyDate"]);
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
                    p.ProdEntranceDate = Convert.ToString(dr["prodEntranceDate"]);
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
                I.ItemCompletedPercentage = Convert.ToDouble(dr["completedPercent"]);
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
            String selectSTR = "SELECT i.projectNum, i.itemNum, i.itemName, i.groupCount, i.completedPercent, i.itemStatus, p.projectName FROM item i INNER JOIN dbo.Project p ON i.projectNum = p.projectNum";
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
                I.ItemCompletedPercentage = Convert.ToDouble(dr["completedPercent"]);
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
    public void StatusChange(string projectStatus, float projectNum)
    {
        SqlConnection con = connect("KinartiConnectionString");

        String selectStr = String.Format("SELECT * FROM Project WHERE projectNum= {0}", projectNum); // create the select that will be used by the adapter to select data from the DB

        SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);

        DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB

        da.Fill(ds, "Project");       // Fill the datatable (in the dataset), using the Select command

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
        //dt = ds.Tables[0]; // point to the cars table , which is the only table in this case

        //dt.Rows[PersonId]["active"] = activity;
        ds.Tables["Project"].Rows[0]["projectStatus"] = projectStatus;
        da.Update(ds, "Project");
        con.Close();

    }






}