using CodeWalker.GameFiles;
using CodeWalker.World;
using ONV_Exporter;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace YmapPropSplitter
{
    public partial class Form1 : Form
    {

        public string[] SelectedYmaps;
        public string[] SelectedYtyps;
        public string[] SelectedYmapsToMerge;
        public string[] SelectedTrainTracks;
        public string[] YnvFiles;
        public Form1()
        {
            InitializeComponent();
            cbSplitType.SelectedIndex = 0;
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private void btnBrowseYTYP_Click(object sender, EventArgs e)
        {

            if (cbSplitType.SelectedIndex == 0)
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
                        MessageBox.Show($"No YTYP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new();

                DialogResult dialog = openFileDialog.ShowDialog();

                if(dialog == DialogResult.OK)
                {
                    tbYTYP.Text = openFileDialog.FileName;
                }
            }

            



        }

        private void btnBrowseYMAP_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();

            DialogResult dialogResult = fbw.ShowDialog();

            if(dialogResult == DialogResult.OK)
            {
                tbYMAP.Text = fbw.SelectedPath;

                int ymapCount = Directory.GetFiles(fbw.SelectedPath, "*.ymap").Length;

                if (ymapCount > 0)
                {
                    SelectedYmaps = Directory.GetFiles(fbw.SelectedPath, "*.ymap");

                    lbYmap.Text = $"{ymapCount} YMAP(s) found!";


                }
                else
                {
                    MessageBox.Show($"No YMAP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            if (SelectedYmaps != null && tbOutput.Text != String.Empty)
            {
                List<ArchetypeElement> YtypArchetypesNames = new();


                //YMAP Processing
                foreach (var ymap in SelectedYmaps)
                {
                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));

                    string ymapFileName = Path.GetFileNameWithoutExtension(ymap);


                    if (cbSplitType.SelectedIndex == 0)
                    {
                        YtypArchetypesNames = GetArchetypeElementList(SelectedYtyps);
                    }
                    else
                    {
                        YtypArchetypesNames = GetArchetypeElementList(tbYTYP.Text);

                    }


                    foreach (var ytypThing in YtypArchetypesNames)
                    {
                        List<YmapEntityDef> foundEntities = new();

                        foreach (var archs in ymapFile.AllEntities)
                        {
                            foreach (var addedArch in ytypThing.archetypeNames)
                            {
                                if (archs._CEntityDef.archetypeName == addedArch &&
                                    (archs._CEntityDef.lodLevel == rage__eLodType.LODTYPES_DEPTH_ORPHANHD ||
                                    archs._CEntityDef.lodLevel == rage__eLodType.LODTYPES_DEPTH_HD))
                                {
                                    foundEntities.Add(archs);
                                    ymapFile.RemoveEntity(archs);
                                }

                            }
                        }

                        Directory.CreateDirectory(Path.Combine(tbOutput.Text, "modified_ymaps"));

                        byte[] newYmapBytes = ymapFile.Save();
                        File.WriteAllBytes(Path.Combine(tbOutput.Text, "modified_ymaps") + $"\\{ymapFileName}.ymap", newYmapBytes);
                        if (foundEntities.Count > 0)
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
                MessageBox.Show($"Processing Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Please select YTYP and YMAP files!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        // Ymap merger =>

        private void btnBrowseYmapM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();


            DialogResult dr = fbw.ShowDialog();

            if (dr == DialogResult.OK)
            {
                SelectedYmapsToMerge = Directory.GetFiles(fbw.SelectedPath, "*.ymap");

                tbYmapM.Text = fbw.SelectedPath;

                if (SelectedYmapsToMerge.Length > 0)
                {
                    lbYmapMerg.Text = $"{SelectedYmapsToMerge.Length} YMAP(s) found!";
                }
                else
                {
                    MessageBox.Show($"No YMAP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                if (SelectedYmapsToMerge.Length > 0)
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

                    MessageBox.Show($"Merge Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select YMAP files!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBrowseOutputM_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();


            DialogResult dr = fbw.ShowDialog();
            if (dr == DialogResult.OK) { tbOutputM.Text = fbw.SelectedPath; }





        }





        private void btnMoveTracks_Click(object sender, EventArgs e)
        {
            double OffsetX;
            double OffsetY;
            double OffsetZ;

            if (tbOutputTracks.Text != string.Empty && tbTrainTracksIn.Text != string.Empty)
            {
                if (tbMoveX.Text != string.Empty && tbMoveY.Text != string.Empty && tbMoveZ.Text != string.Empty)
                {
                    if (double.TryParse(tbMoveX.Text, out OffsetX) &&
                       double.TryParse(tbMoveY.Text, out OffsetY) &&
                       double.TryParse(tbMoveZ.Text, out OffsetZ))
                    {
                        if (SelectedTrainTracks.Length > 0)
                        {
                            foreach (var trainTrackFile in SelectedTrainTracks)
                            {
                                string trainTrackName = Path.GetFileName(trainTrackFile);
                                TrackFile trackFile = new();
                                trackFile.LoadFile(trainTrackFile);
                                trackFile.MoveTrackNodes(OffsetX, OffsetY, OffsetZ);
                                trackFile.SaveFile(Path.Combine(tbOutputTracks.Text, trainTrackName));
                            }

                            MessageBox.Show($"{SelectedTrainTracks.Length} Train Track(s) has been moved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }


                }
            }
            else
            {
                MessageBox.Show("Invalid input and ouput", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnTrackOutputBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();


            DialogResult dr = fbw.ShowDialog();
            if (dr == DialogResult.OK) { tbOutputTracks.Text = fbw.SelectedPath; }

        }

        private void btnBrowseTracks_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbw = new();

            DialogResult dialogResult = fbw.ShowDialog();


            if (dialogResult == DialogResult.OK)
            {
                tbTrainTracksIn.Text = fbw.SelectedPath;

                int trainTracksCount = Directory.GetFiles(fbw.SelectedPath, "*.dat").Length;

                if (trainTracksCount > 0)
                {
                    SelectedTrainTracks = Directory.GetFiles(fbw.SelectedPath, "*.dat");

                    lbTrainTrack.Text = $"{trainTracksCount} Train Track(s) found!";


                }
                else
                {
                    MessageBox.Show($"No Train Track(s) found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void tbMoveX_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbMoveX.Text, out _) || tbMoveX.Text.Contains(',')) { tbMoveX.Text = string.Empty; }

        }

        private void tbMoveY_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbMoveY.Text, out _) || tbMoveY.Text.Contains(',')) { tbMoveY.Text = string.Empty; }
        }

        private void tbMoveZ_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbMoveZ.Text, out _) || tbMoveZ.Text.Contains(',')) { tbMoveZ.Text = string.Empty; }
        }

        private void btnYnvInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new()
            {
                ShowNewFolderButton = false,
                Description = "Select the input folder"
            };
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tbYNVs.Text = folderBrowser.SelectedPath;
            }
        }
        
        private void btnOnvOutput_Click(object sender, EventArgs e)
        {
            //Folder browser for output path
            FolderBrowserDialog folderBrowser = new()
            {
                ShowNewFolderButton = false,
                Description = "Select the output folder"
            };
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tbOnvOutput.Text = folderBrowser.SelectedPath;
            }
        }

        private void btnNavConvert_Click(object sender, EventArgs e)
        {

            if(tbYNVs.Text != String.Empty && tbOnvOutput.Text != String.Empty)
            {
                YnvFiles = Directory.GetFiles(tbYNVs.Text, "*.ynv");
                if (YnvFiles.Length > 0)
                {
                    foreach (string YnvFile in YnvFiles)
                    {
                        OnvObj onvObj = new();

                        byte[] data = File.ReadAllBytes(YnvFile);
                        YnvFile ynvFile = new();
                        ynvFile.Load(data);

                        NavMesh nav = ynvFile.Nav;

                        onvObj.Indices = ynvFile.Indices;
                        onvObj.Edges = ynvFile.Edges.ConvertAll(new Converter<YnvEdge, OnvObj.OnvEdge>(OnvObj.OnvEdge.YnvToOnvEdge));
                        onvObj.Polys = ynvFile.Polys.ConvertAll(new Converter<YnvPoly, OnvObj.OnvPoly>(OnvObj.OnvPoly.YnvToOnvPoly));
                        NavMeshSector sectorTree = nav.SectorTree;
                        onvObj.SectorTree = new OnvObj.OnvNavTree(sectorTree);
                        onvObj.Size = nav.AABBSize;
                        onvObj.Vertices = nav.Vertices.GetFullList();
                        onvObj.Export($"{tbOnvOutput.Text}/{Path.GetFileNameWithoutExtension(YnvFile)}.onv");
                    }

                    MessageBox.Show($"All done, {YnvFiles.Length} ynv(s) processed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No YNV files found in the input folder", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            else
            {
                MessageBox.Show("Input or/and Output fields are empty or invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private List<ArchetypeElement> GetArchetypeElementList(string[] ytypsFiles)
        {
            List<ArchetypeElement> ArchetypesNames = new();

            foreach (var ytyp in ytypsFiles)
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
                    ArchetypesNames.Add(archetypeElement);

                }


            }

            return ArchetypesNames;
        }

        private List<ArchetypeElement> GetArchetypeElementList(string textFile)
        {
            List<ArchetypeElement> ArchetypesNames = new();
            ArchetypeElement archetypeElement = new();
            List<MetaHash> fileElements = new();

            foreach (var item in File.ReadAllLines(textFile))
            {
                fileElements.Add(JenkHash.GenHash(item));
            }

            archetypeElement.archetypeNames = fileElements;
            archetypeElement.YtypName = Path.GetFileNameWithoutExtension(textFile);
            ArchetypesNames.Add(archetypeElement);
            return ArchetypesNames;
        }

        private void cbSplitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbSplitType.SelectedIndex == 0)
            {
                label1.Text = "YTYP folder";
            }
            else
            {
                label1.Text = "Text file";
            }
        }
    }
}