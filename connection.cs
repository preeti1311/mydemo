using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for connection
/// </summary>
public class connection
{
    public SqlConnection con = new SqlConnection();
    public SqlCommand cmd = new SqlCommand();
    public SqlDataReader sdr;
    public SqlDataAdapter adp = new SqlDataAdapter(); 
    public DataSet ds = new DataSet();
    public DataRow dr;
    public SqlCommandBuilder scb = new SqlCommandBuilder();
    public SqlCommand cmd2 = new SqlCommand();
    public SqlDataAdapter adp2 = new SqlDataAdapter(); 
	public connection()
	{
        con.ConnectionString = @"Data Source=LWDOT582\SQLEXPRESS;Database=Demodb1;User id=sa;Password=pass";
        cmd.Connection = con;
        cmd2.Connection = con;
        adp2.SelectCommand = cmd2;
        adp.SelectCommand = cmd;
		//
		// TODO: Add constructor logic here
		//
	}

    public void Gridviewbind(GridView id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public string autoid(string text, string table, string column)
    {

        int n;
        cmd.CommandText = "select max(Convert(int,substring(" + column + "," + Convert.ToInt32(text.Length + 1) + ",len(" + column + ")))) from " + table;
        con.Open();
        if (cmd.ExecuteScalar() == DBNull.Value)
        {
            n = 1;
        }
        else
        {
            n = Convert.ToInt32(cmd.ExecuteScalar());
            n = n + 1;
        }
        con.Close();
        return (text + n);
    }

    public void ddlBind(DropDownList id, string query, string text, string value)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataTextField = text;
        id.DataValueField = value;
        id.DataBind();
        id.Items.Insert(0, "--select--");
    }

    public void DataListbind(DataList id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public void Detailbind(DetailsView id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public void formviewbind(FormView id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public void Repeaterbind(Repeater id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public string MailSending(string to, string subject, string body)
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("username", "password");

        MailMessage msz = new MailMessage();
        msz.From = new MailAddress("emailid");
        msz.To.Add(new MailAddress(to));
        msz.Subject = subject;
        msz.Body = body;
        try
        {
            client.Send(msz);
            return "Send Sucessfully";
        }
        catch (SmtpException ex)
        {
            return "Sending Failed";
        }

    }
    public void datalistbind(DataList id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
        id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    public void listniewbind(ListView id, string query)
    {
        cmd.CommandText = query;
        ds.Clear();
        adp.Fill(ds, "vt");
       // id.DataSource = ds.Tables["vt"];
        id.DataBind();

    }

    private string RandomString(int size, bool lowerCase)
{
    StringBuilder builder = new StringBuilder();
    Random random = new Random();
    char ch ;
    for(int i=0; i<size;i++)    {
        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))) ;
        builder.Append(ch);
    }
    if(lowerCase)
    return builder.ToString().ToLower();
    return builder.ToString();
}




    public string GetUniqueKey(int maxSize, char[] chars)
    {
        //int maxSize for length of string  
        //char[] chars for contains value for generate our randon number  
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        data = new byte[maxSize];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(maxSize);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length - 1)]);
        }
        return result.ToString();
    }
    public void GenerateNumericNumber()
    {
        string NumericNumber = GetUniqueKey(10, "123456789".ToCharArray());
    }
    public void GenerateAlphaNumber()
    {
        string alphanumber = GetUniqueKey(10, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
    }
    public void GenerateSpecialCharNumber()
    {
        string SpecialCharNumber = GetUniqueKey(10, "!@#$%^&*()".ToCharArray());
    }  

}