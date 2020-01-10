using System;
using System.IO;


namespace ActorNameMetrics
{
    internal class DMOpenFile
    {
        String [] textData;

        // will use a list. Not the most efficient as arrays are faster
        // yet must be able to handle large datasets. 
      
  
        public string[] openFile()
        {
            string[] lines;
            try
            {   // Open the text file using a stream reader.
#if DEBUG
                using (StreamReader sr = new StreamReader("yesware-test-data-short-list.txt"))
#else
                using (StreamReader sr = new StreamReader("yesware-test-data-v1-8.txt"))
#endif
                {
                    // Read the stream.
                    String allData = sr.ReadToEnd();
                    //Console.WriteLine(allData);
                    lines = allData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    return lines;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}