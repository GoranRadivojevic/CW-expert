//=================================================================
// Telnet.cs
//=================================================================
// Copyright (C) 2011,2012 S56A YT7PWR
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//=================================================================


using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace CWExpert
{
    #region Telnet Server

    public class TelnetServer
    {
        #region Variable

        public bool run_server = false;
        Stream s;
        StreamWriter sw;
        bool connected = false;
        byte[] receive_buffer = new byte[2048];
        Socket sock;
        Socket WorkingSocket;
        AutoResetEvent server_event = new AutoResetEvent(false);
        public bool IPv6 = false;
        bool login = false;
        string op_name = "";
        string qth = "";

        #endregion

        #region constructor/destructor

        public TelnetServer(string host, int port, bool ipv6)
        {
            IPv6 = ipv6;

            try
            {
                IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse(host), port);

                if (!IPv6)
                {
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                else
                {
                    sock = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    sock.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, 0);  // allow IPV4 and IPV6
                    ipServer = new IPEndPoint(IPAddress.IPv6Any, port);
                }

                sock.Bind(ipServer);
                sock.Listen(4);
                run_server = true;
                Thread t = new Thread(new ThreadStart(Service));
                t.Start();
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
                run_server = false;
            }
        }

        ~TelnetServer()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region misc function

        public bool SendMessage(string message)
        {
            try
            {
                if (connected)
                    sw.Write(message + "\n");

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public bool Close()
        {
            try
            {                
                sock.Close(1000);
                run_server = false;
                server_event.Set();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private void AsyncAcceptCallback(IAsyncResult result)
        {
            try
            {
                if (WorkingSocket == null)
                {
                    if (IPv6)
                        WorkingSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    else
                        WorkingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

                if (sock != null)
                {
                    if (WorkingSocket != null && !WorkingSocket.Connected)
                    {
                        Socket listener = (Socket)result.AsyncState;
                        WorkingSocket = listener.EndAccept(result);

                        StateObject state = new StateObject();
                        state.workSocket = WorkingSocket;

                        WorkingSocket.BeginReceive(receive_buffer, 0, receive_buffer.Length, SocketFlags.None,
                            new AsyncCallback(ReceiveCallback), WorkingSocket);

                        connected = true;
                        s = new NetworkStream(WorkingSocket);
                        sw = new StreamWriter(s);
                        sw.Write("login:\n");
                        sw.Flush();
                        login = true;
                        Debug.Write("Client connected!");
                    }
                }
            }
            catch (ObjectDisposedException exception)
            {
                if (WorkingSocket != null)
                    WorkingSocket.Close();
                if (sock != null)
                    sock.Close();
                server_event.Set();
                Debug.Write(exception.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (WorkingSocket != null && WorkingSocket.Connected)
                {
                    try
                    {
                        int num_read = WorkingSocket.EndReceive(result);
                        sw.AutoFlush = true;
                        ASCIIEncoding asen = new ASCIIEncoding();
                        string name = asen.GetString(receive_buffer, 0, num_read);
                        name = name.Replace("\0", "");
                        name = name.Replace("\n", "");
                        name = name.Replace("\r", "");

                        if (name == "" || name == null) return;
                        // process input data here!!!!

                        switch (name.ToUpper())
                        {
                            case "HELP":        // list of all commands
                                sw.Write("* Commands are:\n" + "* HELP - show list of commands\n" +
                                    "* DX - shows last 5 spots\n" + "* BYE(or QUIT) - disconnect\n");
                                break;
                            case "DX":          // last 5 spots
                                sw.Write("DX DE YT7PWR 14070.0 S56A RTTY CQ    1026Z \n");
                                break;
                            case "BYE":
                            case "QUIT":
                                sw.Write("Bye!\n");
                                sw.Flush();
                                sw.Close();
                                connected = false;
                                WorkingSocket.Disconnect(true);
                                WorkingSocket.Close(1000);
                                s.Close();
                                server_event.Set();
                                break;

                            default:
                                if (login)
                                {
                                    sw.Write(name + "\n" + "Welcome to DX Cluster Server CWExpert 2.0.0 " +
                                        "YT7PWR S56A\n");
                                    login = false;
                                }
                                else
                                {
                                    string[] val = new string[1];
                                    val[0] = "";
                                    string[] delimiter = new string[1];
                                    delimiter[0] = "SET/";

                                    string[] delimiter_1 = new string[3];
                                    delimiter_1[0] = "SET ";

                                    if (name.StartsWith("set/"))
                                        val = name.ToUpper().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                                    else if (name.StartsWith("set "))
                                        val = name.ToUpper().Split(delimiter_1, StringSplitOptions.RemoveEmptyEntries);

                                    foreach (string vals in val)
                                    {
                                        if (vals.StartsWith("NAME"))
                                        {
                                            string[] q;
                                            q = vals.Split(' ');
                                            op_name = q[1];
                                            sw.Write("Name set to: " + op_name + "\n");
                                        }
                                        else if (vals.StartsWith("QTH"))
                                        {
                                            string[] q;
                                            q = vals.Split(' ');
                                            qth = q[1];
                                            sw.Write("QTH set to: " + qth + "\n");
                                        }
                                        else
                                        {
                                            sw.Write("Unknow command: " + name + "\n" + "* Commands are:\n" + "* HELP - show list of commands\n" +
                                                "* DX - shows last 5 spots\n" + "* BYE(or QUIT) - disconnect\n");
                                        }
                                    }
                                }
                                break;
                        }

                        if (connected)
                            WorkingSocket.BeginReceive(receive_buffer, 0, receive_buffer.Length, SocketFlags.None,
                                new AsyncCallback(ReceiveCallback), null);
                    }
                    catch (IOException ex)
                    {
                        Debug.Write(ex.ToString());
                        connected = false;
                        sock.Disconnect(true);
                        server_event.Set();
                    }
                }
                else
                {
                    Debug.Write("Disconnected!\n");
                    WorkingSocket.Close();
                    WorkingSocket = null;
                    server_event.Set();
                }
            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    if (WorkingSocket != null)
                    {
                        WorkingSocket.Close(1000);
                        WorkingSocket = null;
                    }

                    server_event.Set();
                }
            }
            catch (ObjectDisposedException)
            {
                // The socket was closed out from under me
                if (WorkingSocket != null)
                {
                    WorkingSocket.Close(1000);
                    WorkingSocket = null;
                }

                server_event.Set();
            }
        }

        public void Service()
        {
            try
            {

                while (run_server)
                {
                    if (sock != null)
                        sock.BeginAccept(new AsyncCallback(AsyncAcceptCallback), sock);
                    else
                        run_server = false;

                    server_event.WaitOne();
                }

                WorkingSocket.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }

    #endregion

    #region Telnet Client

    public class TelnetClient
    {
        #region Variable

        public TcpClient ClusterClient;
        Stream sr;
        Stream sw;
        delegate void CrossThreadCallback(int command, byte[] data, int count);
        DXClusterClient ClusterForm;
        string CALL = "";
        string remote_addr = "";
        string remote_port = "";
        public bool IPv6 = false;

        #endregion

        #region constructor/destructor

        public TelnetClient(DXClusterClient form)
        {
            try
            {
                ClusterForm = form;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        ~TelnetClient()
        {
        }

        #endregion

        #region misc function

        public bool SendMessage(int type, string message)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        {                                               // regular message
                            if (ClusterClient.Connected)
                            {
                                ASCIIEncoding asen = new ASCIIEncoding();
                                byte[] ba = asen.GetBytes(message);
                                byte[] data = new byte[ba.Length + 2];

                                for (int i = 0; i < data.Length - 2; i++)
                                    data[i] = ba[i];

                                data[data.Length - 2] = 0x0d;
                                data[data.Length-1] = 0x0a;
                                sw.Write(data, 0, data.Length);
                            }
                        }
                        break;
                        
                    case 1:
                        {                                               // bye
                            if (ClusterClient.Connected)
                            {
                                ASCIIEncoding asen = new ASCIIEncoding();
                                byte[] ba = asen.GetBytes(message);
                                byte[] data = new byte[5];

                                for (int i = 0; i < message.Length; i++)
                                    data[i] = ba[i];

                                data[3] = 0x0d;
                                data[4] = 0x0a;
                                sw.Write(data, 0, data.Length);
                                Thread.Sleep(100);
                                ClusterClient.Close();
                            }
                        }
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void Start(string host, string remote, string call)
        {
            try
            {
                CALL = call;
                string[] address = remote.Split(':');
                remote_addr = address[0];
                remote_port = address[1];
                ClusterClient = new TcpClient();
                Thread t = new Thread(new ThreadStart(ClientServiceLoop));
                t.Start();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                MessageBox.Show("Error creating client!", "DX Cluster error");                
            }
        }

        public bool Close()
        {
            try
            {
                sr.Flush();
                sr.Close();
                sr.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void ClientServiceLoop()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(remote_addr);
                IPAddress ipAddress;
                IPEndPoint ipepRemote = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 100);

                if (remote_addr == "127.0.0.1")
                    ipepRemote = new IPEndPoint(IPAddress.Parse(remote_addr), int.Parse(remote_port));
                else
                    {
                        ipAddress = ipHostInfo.AddressList[0];
                        ipepRemote = new IPEndPoint(ipAddress, int.Parse(remote_port));
                    }

                byte[] buffer = new byte[2048];
                int count = 0;
                string text = "";
                ASCIIEncoding buf = new ASCIIEncoding();

                text = "DXClusterClient - Connecting to " + remote_addr.ToString();
                buf.GetBytes(text, 0, text.Length, buffer, 0);
                ClusterForm.Invoke(new CrossThreadCallback(ClusterForm.CrossThreadCommand), 1, buffer, text.Length);
                ClusterClient.Connect(ipepRemote);

                if (ClusterClient.Connected)
                {
                    sw = ClusterClient.GetStream();
                    sr = ClusterClient.GetStream();
                    text = "DXClusterClient - Connected to " + remote_addr.ToString();
                    buf.GetBytes(text, 0, text.Length, buffer, 0);
                    ClusterForm.Invoke(new CrossThreadCallback(ClusterForm.CrossThreadCommand), 1, buffer, text.Length);
                }

                while (ClusterClient.Connected)
                {
                    count = sr.Read(buffer, 0, 2048);

                    if (count == 0)
                    {
                        ClusterClient.Close();
                    }
                    else
                        ClusterForm.Invoke(new CrossThreadCallback(ClusterForm.CrossThreadCommand), 0, buffer, count);
                }

                text = "DXClusterClient - Disconnected";
                buf = new ASCIIEncoding();
                buf.GetBytes(text, 0, text.Length, buffer, 0);
                ClusterForm.Invoke(new CrossThreadCallback(ClusterForm.CrossThreadCommand), 1, buffer, text.Length);

                ClusterClient.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                byte[] buffer = new byte[100];
                string text = "DXClusterClient - Disconnected";
                ASCIIEncoding buf = new ASCIIEncoding();
                buf.GetBytes(text, 0, text.Length, buffer, 0);

                try
                {
                    ClusterForm.Invoke(new CrossThreadCallback(ClusterForm.CrossThreadCommand), 1, buffer, text.Length);
                }
                catch
                {
                }
            }
        }

        #endregion
    }

    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 128;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    #endregion
}