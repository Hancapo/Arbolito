using CodeWalker.GameFiles;

namespace YmapPropSplitter
{
    public partial class Form1 : Form
    {

        public List<ArchetypeElement> YTYPsArchetypes = new();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseYTYP_Click(object sender, EventArgs e)
        {

            YTYPsArchetypes.Clear();
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

                                YTYPsArchetypes.Add(ae);

                            }

                        }
                    }

                    lbYTYPstatus.Text = $"{YTYPsArchetypes.Count} found in {ytypCount} YTYP(s) file(s)";
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
                string[] selectedYMAPs = Directory.GetFiles(fbw.SelectedPath, "*.ymap");

                MessageBox.Show($"{ymapCount} YMAP(s) found!");

                foreach (var ymap in selectedYMAPs)
                {
                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));

                    foreach (var archs in ymapFile.AllEntities)
                    {

                    }
                }
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
    }
}