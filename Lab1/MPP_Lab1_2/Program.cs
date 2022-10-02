using System;
using System.IO;
using System.Xml.Schema;
using NLog;

namespace MPP_Lab1;

class Program
{
    static int copiedFilesCount = 0;
    static int Main(string[] args)
    {
        var logger = LogManager.GetCurrentClassLogger();

        try
        {
            string src = args[0];
            string dest = args[1];
            if (Directory.Exists(src))
            {
                string[] files = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories);
                TaskQueue taskQueue = new TaskQueue(files.Length);
                foreach (string file in files)
                {
                    taskQueue.taskEnqueue(() => { CopyFile(file, dest); });
                }

                taskQueue.Abort();
                taskQueue.Wait();

                Console.WriteLine($"Count of copied files is {copiedFilesCount}\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Directory doesnt exists");
                return 1;
            }
            return 0;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }  
    }

    public static void CopyFile(string file, string dest)
    {
        FileInfo fi = new FileInfo(file);
        fi.CopyTo(Path.Combine(dest, fi.Name), true);
        copiedFilesCount++;
    }
}