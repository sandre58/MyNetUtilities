// -----------------------------------------------------------------------
// <copyright file="SmtpHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Net.Sockets;

namespace MyNet.Utilities.Mail.Smtp;

public static class SmtpHelper
{
    public static bool TestSmtpConnection(string? server, int port)
    {
        if (server == null)
        {
            return false;
        }

        try
        {
            var hostEntry = System.Net.Dns.GetHostEntry(server);
            var endPoint = new System.Net.IPEndPoint(hostEntry.AddressList[0], port);
            using var tcpSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // try to connect and test the response for code 220 = success
            tcpSocket.Connect(endPoint);
            if (!CheckResponse(tcpSocket, 220))
            {
                return false;
            }

            // send HELLO and test the response for code 250 = proper response
            SendData(tcpSocket, string.Format(CultureInfo.InvariantCulture, "HELLO {0}\r\n", System.Net.Dns.GetHostName()));
            return CheckResponse(tcpSocket, 250);

            // if we got here it's that we can connect to the smtp server
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static void SendData(Socket socket, string data)
    {
        var dataArray = System.Text.Encoding.ASCII.GetBytes(data);
        _ = socket.Send(dataArray, 0, dataArray.Length, SocketFlags.None);
    }

    private static bool CheckResponse(Socket socket, int expectedCode)
    {
        while (socket.Available == 0)
        {
            System.Threading.Thread.Sleep(100);
        }

        var responseArray = new byte[1024];
        _ = socket.Receive(responseArray, 0, socket.Available, SocketFlags.None);
        var responseData = System.Text.Encoding.ASCII.GetString(responseArray);
        var responseCode = Convert.ToInt32(responseData[..3], CultureInfo.InvariantCulture);
        return responseCode == expectedCode;
    }
}
