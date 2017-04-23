using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxjzgcjczyQyb.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Zyry
{
    public partial class Zczyry_List : BasePage
    {
        public ZcryBLL zcryBll;
        private AppUser WorkUser = new AppUser();


        protected void Page_Load(object sender, EventArgs e)
        {
            zcryBll = new ZcryBLL(this.WorkUser);
            if (!IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.ssdq, true);
                //数据绑定  
                SearData(0);
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();
            string tblb = "";
            foreach (ListItem item in this.DBCheckBoxList1.Items)
            {
                if (item.Selected)
                    tblb += item.Value + ",";
            }
            tblb = tblb.Trim(',');
            WebCommon.AddDataItem(list, "ryzyzglxID", tblb);

            DataTable dt = new DataTable();

            dt = zcryBll.RetrieveZyryJbxx("zczyry", list, this.gridView.PageSize, pageIndex, "", out allRecordCount).Result;

            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        //分页
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SearData(e.NewPageIndex);
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCellCollection cells = e.Row.Cells;
                foreach (TableCell cell in cells)
                {
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        cell.Text = Server.HtmlDecode(cell.Text);
                    }
                }
            }
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            SearData(0);
        }





        //public static int StartClient(string IP, int port)//启动客户端  
        //{
        //    // Connect to a remote device.       
        //    try
        //    {
        //        IPAddress ipAddress = IPAddress.Parse(IP);//Ip地址  
        //        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);//将网络终点表示为IP地址和端口  
        //        // Create a TCP/IP socket.       
        //        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //        DateTime dtStard = DateTime.Now;//开始时间  
        //        // Connect to the remote endpoint.       
        //        client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);//开始对主机进行异步请求  
        //        connectDone.WaitOne();//阻止的线程  
        //        // Send test data to the remote device.       
        //        Send(client, "kxd send data");//向服务器发数据  
        //        sendDone.WaitOne();//阻止当前线程，直到收到信号  
        //        // Receive the response from the remote device.       
        //        Receive(client);//回复  
        //        //receiveDone.WaitOne();//阻止当前线程，直到收到信号  
        //        DateTime dtEnd = DateTime.Now;//终止时间  
        //        int dt = Convert.ToInt32((dtEnd - dtStard).TotalMilliseconds);//服务器端口响应时间  
        //        // Write the response to the console.       
        //        //Console.WriteLine("Response received : {0}", response);  
        //        // Release释放 the socket.       
        //        client.Shutdown(SocketShutdown.Both);//为发送和接收禁用  
        //        client.Close();
        //        return dt;
        //        //Console.ReadLine();  
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return -1;
        //    }
        //}

        ///// <summary>  
        ///// 链接返回  
        ///// </summary>  
        ///// <param name="ar">异步操作状态</param>  
        //private static void ConnectCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.       
        //        Socket client = (Socket)ar.AsyncState;//获取用户定义对象，包含异步操作的信息  
        //        // Complete the connection.       
        //        client.EndConnect(ar);//接受异步操作挂起的信息  
        //        //Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());  
        //        // Signal that the connection has been made.       
        //        connectDone.Set();//将事件状态设置为终止状态，允许一个或多个等待线程继续。  
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}



        //private static void Receive(Socket client)//接收回复  
        //{
        //    try
        //    {
        //        // Create the state object.       
        //        StateObject state = new StateObject();//创建一个状态对象  
        //        state.workSocket = client;
        //        // Begin receiving the data from the remote device.       
        //        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        //private static void ReceiveCallback(IAsyncResult ar)//回调函数  
        //{
        //    try
        //    {
        //        // Retrieve检索/恢复 the state object and the client socket       
        //        // from the asynchronous state object.       
        //        StateObject state = (StateObject)ar.AsyncState;
        //        Socket client = state.workSocket;
        //        // Read data from the remote device.       
        //        int bytesRead = client.EndReceive(ar);
        //        if (bytesRead > 0)
        //        {
        //            // There might be more data, so store the data received so far.  

        //            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        //            // Get the rest of the data.       
        //            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        //        }
        //        else
        //        {
        //            // All the data has arrived; put it in response.       
        //            if (state.sb.Length > 1)
        //            {
        //                response = state.sb.ToString();
        //            }
        //            // Signal that all bytes have been received.       
        //            receiveDone.Set();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}



        //private static void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.       
        //        Socket client = (Socket)ar.AsyncState;//获取用户定义对象，它限定或包含关于异步操作信息  
        //        // Complete sending the data to the remote device.       
        //        int bytesSent = client.EndSend(ar);
        //        Console.WriteLine("Sent {0} bytes to server.", bytesSent);
        //        // Signal that all bytes have been sent.       
        //        sendDone.Set();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}

        //private static void Send(Socket client, String data)//向服务器发数据  
        //{
        //    // Convert the string data to byte data using ASCII encoding.       
        //    byte[] byteData = Encoding.ASCII.GetBytes(data);//将数据用ASCLL编码  
        //    // Begin sending the data to the remote device.       
        //    client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        //}


    }

    //public class StateObject
    //{
    //    // Client socket.       
    //    public Socket workSocket = null;
    //    // Size of receive buffer.       
    //    public const int BufferSize = 256;
    //    // Receive buffer.       
    //    public byte[] buffer = new byte[BufferSize];
    //    // Received data string.       
    //    public StringBuilder sb = new StringBuilder();
    //}

}