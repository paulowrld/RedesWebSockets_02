using System;
using System.Net;
using System.IO;

class HttpServer
{
    static void Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");

        listener.Start();

        Console.WriteLine("Servidor HTTP iniciado...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            //verifica se a solicitação é uma requisição GET para o diretório raiz do servidor
            if (request.HttpMethod == "GET" && request.Url.LocalPath == "/")
            {
                HttpListenerResponse response = context.Response;

                //gera a lista de arquivos no diretório raiz do servidor
                string[] files = Directory.GetFiles(Environment.CurrentDirectory);
                string fileList = "<h1>Lista de arquivos no diretorio do projeto.</h1>";
                fileList += "<ul>";
                foreach (string file in files)
                {
                    fileList += "<li>" + Path.GetFileName(file) + "</li>";
                }
                fileList += "</ul>";

                // Cria a resposta HTML
                string responseString = "<html><body>" + fileList + "</body></html>";

                // Configura a resposta com o conteúdo HTML e os cabeçalhos apropriados
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                // Se a solicitação não é uma requisição GET para o diretório raiz, retorna um erro 404
                context.Response.StatusCode = 404;
                context.Response.Close();
            }
        }
    }
}
