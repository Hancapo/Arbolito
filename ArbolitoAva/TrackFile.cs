using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolitoAva
{
    public class TrackFile
    {
        public List<TrackNode> trackNodes { get; set; }
        public int trackNodeCount => trackNodes.Count;


        public void LoadFile(string filename)
        {

            List<TrackNode> tftn = new();
            
            string[] tracklines = File.ReadAllLines(filename);

            foreach (var item in tracklines)
            {

                string[] lineSplitted = item.Split(' ');

                if (lineSplitted.Length < 2)
                {
                    continue;
                }

                TrackNode trackNode = new()
                {
                    PosX = double.Parse(lineSplitted[0]),
                    PosY = double.Parse(lineSplitted[1]),
                    PosZ = double.Parse(lineSplitted[2]),
                    Type = int.Parse(lineSplitted[3])

                };

                tftn.Add(trackNode);



            }

            trackNodes = tftn;


        }

        public void MoveTrackNodes(double x, double y, double z)
        {
            foreach (var item in trackNodes)
            {
                item.PosX += x;
                item.PosY += y;
                item.PosZ += z;
            }
        }

        public void SaveFile(string outputfile)
        {
            StringBuilder trackFileBuilder = new();

            trackFileBuilder.AppendLine(trackNodeCount.ToString());

            foreach (var item in trackNodes)
            {
                trackFileBuilder.AppendLine($"{Math.Round(item.PosX, 4)} {Math.Round(item.PosY, 4)} {Math.Round(item.PosZ, 4)} {item.Type}");
            }

            File.WriteAllText(outputfile, trackFileBuilder.ToString());


        }
    }
}
