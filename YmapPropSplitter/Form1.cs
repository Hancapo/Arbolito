using CodeWalker.GameFiles;

namespace YmapPropSplitter
{
    public partial class Form1 : Form
    {

        public List<ArchetypeElement> YtypArchetypes = new();
        public string[] SelectedYmaps;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseYTYP_Click(object sender, EventArgs e)
        {

            YtypArchetypes.Clear();
            FolderBrowserDialog fbw = new();

            DialogResult dialog = fbw.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                tbYTYP.Text = fbw.SelectedPath;



                int ytypCount = Directory.GetFiles(fbw.SelectedPath, "*.ytyp").Count();

                if (ytypCount > 0)
                {
                    string[] selectedYTYPs = Directory.GetFiles(fbw.SelectedPath, "*.ytyp");

                    MessageBox.Show($"{ytypCount} YTYP(s) found!");

                    foreach (var ytyp in selectedYTYPs)
                    {
                        YtypFile ytypFile = new();
                        ytypFile.Load(File.ReadAllBytes(ytyp));

                        foreach (var archs in ytypFile.AllArchetypes)
                        {
                            if (archs.Type == MetaName.CBaseArchetypeDef || archs.Type == MetaName.CTimeArchetypeDef)
                            {
                                ArchetypeElement ae = new()
                                {
                                    archetype = archs,
                                    YtypName = Path.GetFileNameWithoutExtension(ytyp)
                                };

                                YtypArchetypes.Add(ae);

                            }

                        }
                    }

                    lbYTYPstatus.Text = $"{YtypArchetypes.Count} found in {ytypCount} YTYP(s) file(s)";
                }
                else
                {
                    MessageBox.Show($"No YTYP(s) found!");
                }
            }


            
        }

        private void btnBrowseYMAP_Click(object sender, EventArgs e)
        {
            btnBrowseYMAP.Enabled = true;
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
            if (SelectedYmaps != null && YtypArchetypes.Count != 0 && tbOutput.Text != String.Empty)
            {
                foreach (var ymap in SelectedYmaps)
                {
                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));

                    string ymapFileName = Path.GetFileNameWithoutExtension(ymap);

                    List<YmapEntityDef> foundEntities = new();

                    foreach (var archs in ymapFile.AllEntities)
                    {
                        foreach (var addedArch in YtypArchetypes)
                        {
                            if (archs._CEntityDef.archetypeName == addedArch.archetype._BaseArchetypeDef.name && 
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

                    YmapFile saitama = new()
                    {
                        Name = ymapFileName + "_splitted"
                    };

                    foreach (var item in foundEntities)
                    {
                        saitama.AddEntity(item);
                    }

                    saitama.BuildCEntityDefs();
                    saitama.CalcExtents();
                    saitama.CalcFlags();

                    byte[] newYmapBytes2 = saitama.Save();
                    File.WriteAllBytes(tbOutput.Text + $"\\{ymapFileName}_splitted.ymap", newYmapBytes2);



                }
            }
            else
            {
                MessageBox.Show("Please select YTYP and YMAP files!");
            }
            
        }
    }
}