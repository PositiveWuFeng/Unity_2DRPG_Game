using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;
using System.Data;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;

public class SqlManage
{
    // 连接字符串  第一个是你的服务器地址  第二个使我们刚才创建的数据库 三 四 就是我们刚才设置的 SA
    private string address = "server=127.0.0.1;database=RPG2D;uid=sa;pwd=shao123123";
    SqlConnection con=null;
    SqlCommand command = null;
    public SqlManage()
    {
        con = new SqlConnection(address);
        Debug.Log("连接成功");
    }
    public SqlManage(string address)
    {
        this.address = address;
        con = new SqlConnection(address);
        Debug.Log("连接成功");
    }
    /// <summary>
    /// 数据库的查询
    /// </summary>
    public Data SqlSelect(string username)
    {
        SQLOpen();
        Data data = new Data();
        //设置CommandText，设置其执行SQL语句
        command.CommandText = "select * from Users where username='"+username+"'";
        SqlDataReader st = command.ExecuteReader();
        if (!st.Read())
        {
            SQLOpen();
            return data;
        }
        //st.Read();
        data.username = st[0].ToString().Trim();
        data.password = st[1].ToString().Trim();
        SQLOpen();
        return data;
    }
    /// <summary>
    /// 数据库的插入
    /// </summary>
    public bool SqlInsert(string username,string password)
    {
        if(username.Length>=5 && username.Length<=15 && password.Length >= 5 && password.Length <= 15)
        {
            //判断是否已有用户名
            if (SqlSelect(username).username != null)
            {
                return false;
            }
            SQLOpen();
            //设置CommandText，设置其执行SQL语句
            command.CommandText = "Insert Into Users Values ('" + username +
                "','" + password + "')";
            int lin = command.ExecuteNonQuery();
            Debug.Log("注册成功，受影响" + lin);
            SQLOpen();
            return true;
        }
        else
        {
            Debug.Log("用户名或密码 长度不符合");
            return false;
        }
    }
    /// <summary>
    /// 判断数据库是否已经打开
    /// </summary>
    public void SQLOpen()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
            //创建SqlCommand对象，并指定其使用con连接数据库
            command = new SqlCommand();
            command.Connection = con;
        }
        else if (con != null || con.State == ConnectionState.Open)
        {
            command.Dispose();
            con.Close();
        }
    }
    ~SqlManage()
    {
        command.Dispose();
        con.Close();
    }

}
public struct Data // 我们在数据库中创建的表结构
{
    public string username, password;
    public Data(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
