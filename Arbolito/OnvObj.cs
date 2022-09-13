using CodeWalker.GameFiles;
using SharpDX;

namespace ONV_Exporter
{
    //Original code decompiled from Navmesh to OpenFormats Exporter by LeetSombrero https://www.gta5-mods.com/tools/navmesh-to-openformats-exporter
    public class OnvObj
    {
        public List<NavMeshVertex> Vertices;
        public List<ushort> Indices;
        public List<OnvEdge> Edges;
        public List<OnvPoly> Polys;
        public OnvNavTree SectorTree;
        public Vector3 Size;

        private static void PrintSectorData(OnvNavTree st, string indent, StreamWriter sw)
        {
            sw.Write("{0}\tSectorData ", indent);
            if (st.SectorData == null)
            {
                sw.Write("null\n");
            }
            else
            {
                ushort[] numArray = st.SectorPolyIndices ?? (Array.Empty<ushort>());
                NavMeshPoint[] navMeshPointArray = st.SectorBounds ?? (Array.Empty<NavMeshPoint>());
                sw.WriteLine("\n{0}\t{{\n{0}\t\tPolyIndices {1}", indent, numArray.Length);
                if (numArray.Length != 0)
                {
                    sw.WriteLine("{0}\t\t{{", indent);
                    for (int index1 = 0; index1 < numArray.Length; index1 += 15)
                    {
                        sw.Write("{0}\t\t\t", indent);
                        for (int index2 = index1; index2 < index1 + 15 && index2 < numArray.Length; ++index2)
                            sw.Write("{0} ", numArray[index2]);
                        sw.Write('\n');
                    }
                    sw.WriteLine("{0}\t\t}}", indent);
                }
                sw.WriteLine("{0}\t\tBounds {1}", indent, navMeshPointArray.Length);
                if (navMeshPointArray.Length != 0)
                {
                    sw.WriteLine("{0}\t\t{{", indent);
                    foreach (NavMeshPoint navMeshPoint in navMeshPointArray)
                        sw.WriteLine("{0}\t\t\t{1} {2} {3} {4}", indent, navMeshPoint.X, navMeshPoint.Y, navMeshPoint.Z, navMeshPoint.Angle);
                    sw.WriteLine("{0}\t\t}}", indent);
                }
                sw.WriteLine("{0}\t}}", indent);
            }
        }

        private void PrintSectorTree(OnvNavTree st, int depth, string name, StreamWriter sw)
        {
            string indent = string.Concat(Enumerable.Repeat("\t", depth));
            if (st == null)
            {
                sw.WriteLine("{0}{1} null", indent, name);
            }
            else
            {
                Vector3 aabbMin = st.AABBMin;
                Vector3 aabbMax = st.AABBMax;
                sw.WriteLine("{0}{1}\n{0}{{", indent, name);
                sw.WriteLine("{0}\tAABBMin {1} {2} {3}", indent, aabbMin.X, aabbMin.Y, aabbMin.Z);
                sw.WriteLine("{0}\tAABBMax {1} {2} {3}", indent, aabbMax.X, aabbMax.Y, aabbMax.Z);
                PrintSectorData(st, indent, sw);
                PrintSectorTree(st.SubTree0, depth + 1, "SubTree0", sw);
                PrintSectorTree(st.SubTree1, depth + 1, "SubTree1", sw);
                PrintSectorTree(st.SubTree2, depth + 1, "SubTree2", sw);
                PrintSectorTree(st.SubTree3, depth + 1, "SubTree3", sw);
                sw.WriteLine("{0}}}", indent);
            }
        }

        public void Export(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            using StreamWriter text = File.CreateText(path);
            text.WriteLine("Version 1 1");
            text.WriteLine("Sizes {0} {1} {2}", Size.X, Size.Y, Size.Z);
            text.WriteLine("Flags 3");
            text.WriteLine("Vertices {0}\n{{", Vertices.Count);
            foreach (NavMeshVertex vertex in Vertices)
                text.WriteLine("\t{0} {1} {2}", vertex.X, vertex.Y, vertex.Z);
            text.WriteLine("}}\nIndices {0}\n{{", Indices.Count);
            for (int index1 = 0; index1 < Indices.Count; index1 += 15)
            {
                text.Write('\t');
                for (int index2 = index1; index2 < index1 + 15 && index2 < Indices.Count; ++index2)
                    text.Write("{0} ", Indices[index2]);
                text.Write('\n');
            }
            text.WriteLine("}}\nEdges {0}\n{{", Edges.Count);
            foreach (OnvEdge edge in Edges)
                text.WriteLine("\t{0}, 0, {1}, {2}, {3}, 0", edge.AreaID1, edge.PolyID1, edge.AreaID2, edge.PolyID2);
            uint num = 0;
            text.WriteLine("}}\nPolys {0}\n{{", Polys.Count);
            foreach (OnvPoly poly in Polys)
            {
                text.WriteLine("\t{0} {1} {2} 0 0", num, poly.EdgeCount, poly.Flags);
                num += poly.EdgeCount;
            }
            text.WriteLine("}");
            this.PrintSectorTree(SectorTree, 0, "SectorTree", text);
            text.WriteLine("Portals 0\nSectorID {0}", Edges[0].AreaID1);
        }

        public class OnvEdge
        {
            private int _PolyID1;
            private int _PolyID2;
            private int _AreaID1;
            private int _AreaID2;

            public int PolyID1
            {
                get => _PolyID1;
                set => _PolyID1 = LimitRange(value);
            }

            public int PolyID2
            {
                get => _PolyID2;
                set => _PolyID2 = LimitRange(value);
            }

            public int AreaID1
            {
                get => _AreaID1;
                set => _AreaID1 = LimitRange(value);
            }

            public int AreaID2
            {
                get => _AreaID2;
                set => _AreaID2 = LimitRange(value);
            }

            public OnvEdge(int PolyID1, int PolyID2, int AreaID1, int AreaID2)
            {
                this.PolyID1 = PolyID1;
                this.PolyID2 = PolyID2;
                this.AreaID1 = AreaID1;
                this.AreaID2 = AreaID2;
            }

            public static OnvEdge YnvToOnvEdge(YnvEdge e) => new((int)e.PolyID1, (int)e.PolyID2, (int)e.AreaID1, (int)e.AreaID2);

            private static int LimitRange(int value) => value > 16382 ? -1 : value;
        }

        public class OnvPoly
        {
            public uint Flags;
            public uint EdgeCount;

            public OnvPoly(uint Flags, uint EdgeCount)
            {
                this.Flags = Flags;
                this.EdgeCount = EdgeCount;
            }

            public static OnvPoly YnvToOnvPoly(YnvPoly p) => new(p.RawData.PolyFlags0, (uint)p.Edges.Length);
        }

        public class OnvNavTree
        {
            public Vector3 AABBMin;
            public Vector3 AABBMax;
            public NavMeshSectorData SectorData;
            public ushort[] SectorPolyIndices;
            public NavMeshPoint[] SectorBounds;
            public OnvNavTree SubTree0;
            public OnvNavTree SubTree1;
            public OnvNavTree SubTree2;
            public OnvNavTree SubTree3;

            public OnvNavTree(NavMeshSector tree)
            {
                AABBMin = (Vector3)tree.AABBMin;
                AABBMax = (Vector3)tree.AABBMax;
                SectorData = tree.Data;
                if (SectorData != null)
                {
                    SectorPolyIndices = SectorData.PolyIDs;
                    SectorBounds = SectorData.Points;
                }
                SubTree0 = tree.SubTree1 == null ? null : new OnvNavTree(tree.SubTree1);
                SubTree1 = tree.SubTree2 == null ? null : new OnvNavTree(tree.SubTree2);
                SubTree2 = tree.SubTree3 == null ? null : new OnvNavTree(tree.SubTree3);
                SubTree3 = tree.SubTree4 == null ? null : new OnvNavTree(tree.SubTree4);
            }
        }
    }
}
