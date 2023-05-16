using System;
using System.Net;

class HttpClient
{
    static void Main(string[] args)
    {
        WebClient client = new WebClient();

        string responseString = client.DownloadString("http://localhost:8080/"); // Envia uma solicitação GET para o servidor

        Console.WriteLine(responseString); // Exibe a resposta do servidor

        Console.ReadLine();
    }
}
