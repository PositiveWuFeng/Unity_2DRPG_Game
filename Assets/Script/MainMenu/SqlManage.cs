using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;
using System.Data;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;

public class SqlManage
{
    // �����ַ���  ��һ������ķ�������ַ  �ڶ���ʹ���ǸղŴ��������ݿ� �� �� �������Ǹղ����õ� SA
    private string address = "server=127.0.0.1;database=RPG2D;uid=sa;pwd=shao123123";
    SqlConnection con=null;
    SqlCommand command = null;
    public SqlManage()
    {
        con = new SqlConnection(address);
        Debug.Log("���ӳɹ�");
    }
    public SqlManage(string address)
    {
        this.address = address;
        con = new SqlConnection(address);
        Debug.Log("���ӳɹ�");
    }
    /// <summary>
    /// ���ݿ�Ĳ�ѯ
    /// </summary>
    public Data SqlSelect(string username)
    {
        SQLOpen();
        Data data = new Data();
        //����CommandText��������ִ��SQL���
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
    /// ���ݿ�Ĳ���
    /// </summary>
    public bool SqlInsert(string username,string password)
    {
        if(username.Length>=5 && username.Length<=15 && password.Length >= 5 && password.Length <= 15)
        {
            //�ж��Ƿ������û���
            if (SqlSelect(username).username != null)
            {
                return false;
            }
            SQLOpen();
            //����CommandText��������ִ��SQL���
            command.CommandText = "Insert Into Users Values ('" + username +
                "','" + password + "')";
            int lin = command.ExecuteNonQuery();
            Debug.Log("ע��ɹ�����Ӱ��" + lin);
            SQLOpen();
            return true;
        }
        else
        {
            Debug.Log("�û��������� ���Ȳ�����");
            return false;
        }
    }
    /// <summary>
    /// �ж����ݿ��Ƿ��Ѿ���
    /// </summary>
    public void SQLOpen()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
            //����SqlCommand���󣬲�ָ����ʹ��con�������ݿ�
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
public struct Data // ���������ݿ��д����ı�ṹ
{
    public string username, password;
    public Data(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
