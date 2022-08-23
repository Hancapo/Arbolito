using CodeWalker.GameFiles;

namespace YmapPropSplitter
{
    public partial class Form1 : Form
    {

        public List<ArchetypeElement> YtypArchetypes = new();
        public string[] SelectedYmaps;
        public string[] SelectedYtyps;
        public string[] SelectedYmapsToMerge;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseYTYP_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbw = new();

            DialogResult dialog = fbw.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                tbYTYP.Text = fbw.SelectedPath;
                int ytypCount = Directory.GetFiles(fbw.SelectedPath, "*.ytyp").Length;

                if (ytypCount > 0)
                {
                    SelectedYtyps = Directory.GetFiles(fbw.SelectedPath, "*.ytyp");

                    lbYTYPstatus.Text = ($"{ytypCount} YTYP(s) found!");

                    
                }
                else
                {
                    MessageBox.Show($"No YTYP(s) found!");
                }
            }



        }

        private void btnBrowseYMAP_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();
            fbw.ShowDialog();

            tbYMAP.Text = fbw.SelectedPath;

            int ymapCount = Directory.GetFiles(fbw.SelectedPath, "*.ymap").Count();

            if (ymapCount > 0)
            {
                SelectedYmaps = Directory.GetFiles(fbw.SelectedPath, "*.ymap");

                lbYmap.Text = $"{ymapCount} YMAP(s) found!";


            }
            else
            {
                MessageBox.Show($"No YMAP(s) found!");
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbw = new();
            fbw.ShowDialog();

            tbOutput.Text = fbw.SelectedPath;
        }

        private void tbYTYP_TextChanged(object sender, EventArgs e)
        {


        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (SelectedYmaps != null && SelectedYtyps.Length != 0 && tbOutput.Text != String.Empty)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = SelectedYmaps.Length * 100;
                //YTYP Processing
                foreach (var ytyp in SelectedYtyps)
                {
                    ArchetypeElement archetypeElement = new();
                    
                    YtypFile ytypFile = new();
                    ytypFile.Load(File.ReadAllBytes(ytyp));

                    archetypeElement.YtypName = Path.GetFileNameWithoutExtension(ytyp);

                    List<MetaHash> metaHashes = new();

                    foreach (var archs in ytypFile.AllArchetypes)
                    {


                        if (archs.Type == MetaName.CBaseArchetypeDef || archs.Type == MetaName.CTimeArchetypeDef)
                        {
                            metaHashes.Add(archs.Hash);
                        }
                        
                    }

                    if (metaHashes.Count > 0)
                    {
                        archetypeElement.archetypeNames = metaHashes;
                        YtypArchetypes.Add(archetypeElement);
                        
                    }


                }
                
                //YMAP Processing
                foreach (var ymap in SelectedYmaps)
                {
                    
                    progressBar1.Value += 100;

                    
                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));

                    string ymapFileName = Path.GetFileNameWithoutExtension(ymap);


                    foreach (var ytypThing in YtypArchetypes)
                    {
                        List<YmapEntityDef> foundEntities = new();
                        
                        foreach (var archs in ymapFile.AllEntities)
                        {
                            foreach (var addedArch in ytypThing.archetypeNames)
                            {
                                if (archs._CEntityDef.archetypeName == addedArch &&
                                    archs._CEntityDef.lodLevel == rage__eLodType.LODTYPES_DEPTH_ORPHANHD)
                                {
                                    foundEntities.Add(archs);
                                    ymapFile.RemoveEntity(archs);
                                }
                                
                            }
                        }
                        
                        Directory.CreateDirectory(Path.Combine(tbOutput.Text, "modified_ymaps"));

                        byte[] newYmapBytes = ymapFile.Save();
                        File.WriteAllBytes(Path.Combine(tbOutput.Text, "modified_ymaps") + $"\\{ymapFileName}.ymap", newYmapBytes);
                        if(foundEntities.Count > 0)
                        {
                            YmapFile SplittedYmap = new()
                            {
                                Name = $"{ymapFileName}_{ytypThing.YtypName}"
                            };

                            foreach (var item in foundEntities)
                            {
                                SplittedYmap.AddEntity(item);
                            }

                            if (SplittedYmap.AllEntities != null)
                            {
                                SplittedYmap.BuildCEntityDefs();
                                SplittedYmap.CalcExtents();
                                SplittedYmap.CalcFlags();
                                byte[] newYmapBytes2 = SplittedYmap.Save();
                                File.WriteAllBytes(tbOutput.Text + $"\\{ymapFileName}_{ytypThing.YtypName}.ymap", newYmapBytes2);
                            }
                        }


                        
                    }
                }
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    MessageBox.Show($"Processing Complete!");
                }

            }
            else
            {
                MessageBox.Show("Please select YTYP and YMAP files!");
            }

        }


        // Ymap merger =>
    
        private void btnBrowseYmapM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();


            DialogResult dr = fbw.ShowDialog();

            if(dr == DialogResult.OK)
            {
                SelectedYmapsToMerge = Directory.GetFiles(fbw.SelectedPath, "*.ymap");

                tbYmapM.Text = fbw.SelectedPath;

                if (SelectedYmapsToMerge.Length > 0)
                {
                    lbYmapMerg.Text = $"{SelectedYmapsToMerge.Length} YMAP(s) found!";
                }
                else
                {
                    MessageBox.Show($"No YMAP(s) found!");
                }
            }

            

        }

        

        private void btnMerge_Click(object sender, EventArgs e)
        {

            YmapFile yfhola = new();
            yfhola.Name = tbYmapName.Text;
            
            if (tbOutputM.Text != String.Empty && tbYmapName.Text != String.Empty)
            {
                SelectedYmapsToMerge = Directory.GetFiles(tbYmapM.Text, "*.ymap");

                if(SelectedYmapsToMerge.Length > 0)
                {
                    List<YmapEntityDef> AllEntsFromYmaps = new();
                    foreach (var ymap in SelectedYmapsToMerge)
                    {
                        YmapFile ymapFile = new();
                        ymapFile.Load(File.ReadAllBytes(ymap));

                        ymapFile.AllEntities.ToList().ForEach(x => yfhola.AddEntity(x));




                    }
                    
                    yfhola.BuildCEntityDefs();
                    yfhola.CalcExtents();
                    yfhola.CalcFlags();
                    
                    byte[] mergedYmapBytes = yfhola.Save();

                    File.WriteAllBytes(tbOutputM.Text + $"\\{tbYmapName.Text}.ymap", mergedYmapBytes);
                }
            }
            else
            {
                MessageBox.Show("Please select YMAP files!");
            }
        }

        private void btnBrowseOutputM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();


            DialogResult dr = fbw.ShowDialog();

            tbOutputM.Text = fbw.SelectedPath;
            

            
        }
    }
}