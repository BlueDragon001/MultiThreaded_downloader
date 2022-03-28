using System;
using System.Net;
using System.IO;
using System.Threading;

namespace MultiThreaded_downloader
{
    class NewDownloader
    {
        string _SourceUrl;
        string _DestinationPath;
        Int64 ContentLength;
        volatile bool _allowed = true;
        volatile bool _allowed0 = true;
        long min = 0;
        long mid = 15000;
        String[] disposeablePaths;

        
        public NewDownloader(string SourceURL, String[] disposablePaths, String Destination)
        {
            _SourceUrl = SourceURL;
            this.disposeablePaths = disposablePaths;
            this._DestinationPath = Destination +"/"+ FileInfo().Item2.Substring(FileInfo().Item2.IndexOf("/") + 1);
        }

        public Tuple<long, String> FileInfo()
        {
            var webRequest = WebRequest.Create(_SourceUrl);
            webRequest.Method = "HEAD";
            WebResponse response = webRequest.GetResponse();
            String type = response.ContentType;
            ContentLength = response.ContentLength;
            response.Close();
            return new Tuple<long, String>(ContentLength, type);
        }

        
        public void Download()
        {
            object fileStream1 = null;
            object fileStream2 = null;



            Thread thread1 = new(() =>
           {
               var Data = new DownloadableData(_SourceUrl, min, FileInfo().Item1 / 4);
               WebResponse webResponse = Data.request();
               var responseStream = webResponse.GetResponseStream();
               var fileStream = new FileStream(disposeablePaths[0], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
               var buffer = new byte[1000 * 1024];
               
               while (_allowed)
               {
                   var d1 = responseStream.Read(buffer, 0, buffer.Length);
                   Console.Write($"File downloaded " + d1);
                   fileStream.Write(buffer, 0, d1);
                   if (d1 == 0)
                   {
                       fileStream.Close();
                       break;
                   }
               }

               fileStream1 = fileStream;
           });

            Thread thread2 = new(() =>
           {
               var Data = new DownloadableData(_SourceUrl, FileInfo().Item1 / 4, FileInfo().Item1 * 3 / 4);
               WebResponse webResponse = Data.request();
               var responseStream = webResponse.GetResponseStream();
               var fileStream = new FileStream(disposeablePaths[1], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
               var buffer2 = new byte[1000 * 1024];
               while (_allowed0)
               {
                   var d2 = responseStream.Read(buffer2, 0, buffer2.Length);
                   fileStream.Write(buffer2, 0, d2);
                   if (d2 == 0)
                   {
                       Console.Write($"File downloaded " + d2);
                       fileStream.Close();
                       break;
                   }
               }

               fileStream2 = fileStream;
           });
            Thread thread3 = new(() =>
            {
                var Data = new DownloadableData(_SourceUrl, FileInfo().Item1 * 3 / 4, FileInfo().Item1);
                WebResponse webResponse = Data.request();
                var responseStream = webResponse.GetResponseStream();
                var fileStream = new FileStream(disposeablePaths[2], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                var buffer3 = new byte[1000 * 1024];
                while (_allowed0)
                {
                    var d3 = responseStream.Read(buffer3, 0, buffer3.Length);
                    fileStream.Write(buffer3, 0, d3);
                    Console.Write($"File downloaded " + d3);
                    if (d3 == 0)
                    {
                        fileStream.Close();
                        break;
                    }
                }


            });
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            try
            {
                using var outputStream = File.Create(_DestinationPath);
                for (int i = 0; i < disposeablePaths.Length; i++)
                {
                    var buffer = new byte[1024 * 1000];

                    using var input = File.Open(disposeablePaths[i], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    while (true)
                    {
                        var bus = input.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bus);
                        Console.Write($"File downloaded " + bus);
                        Console.WriteLine(disposeablePaths[i] + bus);
                        if(bus == 0)
                        {
                            outputStream.Close();
                            break;
                        }
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("Error Copying " + e.Message);
            }

        }
        

    }




}
