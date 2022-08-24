using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YmapPropSplitter
{
    public class TrackFile
    {
        public List<TrackNode> trackNodes = new();
        public int TrackNodeCount => trackNodes.Count;


        List<TrackNode> LoadFile(string filename)
        {
            string[] tracklines = File.ReadAllLines(filename);

            foreach (var item in tracklines)
            {
                if(item.Split(' ').Length < 0)
                {
                    continue;
                }

                Console.WriteLine(item);


            }

            return new List<TrackNode>();
        }
    }
}
