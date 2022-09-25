using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Globalization;
using CodeWalker.GameFiles;
using System.IO;
using System;
using Avalonia.Styling;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace ArbolitoAva
{
    public partial class MainWindow : Window
    {

        public string[] SelectedYmaps;
        public string[] SelectedYtyps;
        public string[] SelectedYmapsToMerge;
        public string[] SelectedYmapsToReplaceProps;
        public string[] SelectedTrainTracks;
        public string[] YnvFiles;
        public List<PropReplacer>? PropReplacersList = new();
        public MainWindow()
        {
            InitializeComponent();
            DefaultSettings();
        }
        private void DefaultSettings()
        {
            CbSplitBy.SelectedIndex = 0;
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            //dgvPropReplaceList.ForeColor = Color.Black;
            //PositionMode.SelectedIndex = 0;
            //RotationMode.SelectedIndex = 0;
        }
        private void OnBtnSplitSourceBrowseClick(object sender, RoutedEventArgs e)
        {

            if(CbSplitBy.SelectedIndex == 0)
            {
                var dlg = new OpenFolderDialog();
                var result = dlg.ShowAsync(this).Result;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    TbSplitSourceField.Text = result.ToString();
                    int ytypCount = Directory.GetFiles(result, "*.ytyp").Length;

                    if (ytypCount > 0)
                    {
                        SelectedYtyps = Directory.GetFiles(result, "*.ytyp");

                        LbSplitSource.Content = ($"{ytypCount} YTYP(s) found!");


                    }
                    else
                    {
                        var msBoxSourceStatus = MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Warning",
                            CanResize = false,
                            Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                            ShowInCenter = true,
                            ContentMessage = "No YTYP(s) found!",
                        });
                        msBoxSourceStatus.ShowDialog(this);
                        //MessageBox.Show($"No YTYP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LbSplitSource.Content = ($"Text file loaded");

                    }
                }

            }
            else
            {
                var ofd = new OpenFileDialog();
                ofd.AllowMultiple = false;
                string? result;
                result = ofd.ShowAsync(this).Result?[0];
                if (!string.IsNullOrWhiteSpace(result))
                {
                    TbSplitSourceField.Text = result;
                }
            }
            
            
        }

        private void OnBtnSplitYmapBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitYmapField.Text = result.ToString();
                int ymapCount = Directory.GetFiles(result.ToString(), "*.ymap").Length;

                if(ymapCount > 0)
                {
                    SelectedYmaps = Directory.GetFiles(result.ToString(), "*.ymap");
                    
                    LbSplitYmap.Content = $"{ymapCount} YMAP(s) found!";
                }
                else
                {
                    var msBoxYmapStatus = MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = ButtonEnum.Ok,
                        ContentTitle = "Warning",
                        CanResize = false,
                        Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                        ShowInCenter = true,
                        ContentMessage = "No YMAP(s) found",
                    });
                    msBoxYmapStatus.ShowDialog(this);
                    //MessageBox.Show($"No YMAP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }

            }
        }

        private void OnBtnSplitOutputBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitOutputField.Text = result.ToString();
            }
        }

        private async void OnBtnSplitMappingsClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(SplitYmaps);
        }

        private void SplitYmaps()
        {
            List<ArchetypeElement> YtypArchetypesNames = new();
            

            if (SelectedYmaps != null && TbSplitOutputField.Text != String.Empty)
            {

                //YMAP Processing
                foreach (var ymap in SelectedYmaps)
                {
                    int ymapProgress = (int)Math.Round((double)(Array.IndexOf(SelectedYmaps, ymap) + 1) / SelectedYmaps.Length * 100);

                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        //Normalize integer value from 0 to 100

                        PbSplitProgress.Value = ymapProgress;

                        
                    });

                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));

                    string ymapFileName = Path.GetFileNameWithoutExtension(ymap);

                    
                    if (CbSplitBy.SelectedIndex == 0)
                    {
                        YtypArchetypesNames = GetArchetypeElementList(SelectedYtyps);
                    }
                    else
                    {
                        YtypArchetypesNames = GetArchetypeElementList(TbSplitSourceField.Text);

                    }


                    foreach (ArchetypeElement? ytypThing in CollectionsMarshal.AsSpan(YtypArchetypesNames))
                    {
                        List<YmapEntityDef> foundEntities = new();

                        if (ymapFile.AllEntities != null && (ymapFile.AllEntities != null || ymapFile.CarGenerators == null))
                        {

                            foreach (YmapEntityDef archs in CollectionsMarshal.AsSpan(ymapFile.AllEntities?.ToList()))
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
                        }
                        else
                        {
                            continue;
                        }
                        Directory.CreateDirectory(Path.Combine(TbSplitOutputField.Text, "modified_ymaps"));

                        byte[] newYmapBytes = ymapFile.Save();
                        File.WriteAllBytes(Path.Combine(TbSplitOutputField.Text, "modified_ymaps") + $"\\{ymapFileName}.ymap", newYmapBytes);
                        if (foundEntities.Count > 0)
                        {
                            YmapFile SplittedYmap = new()
                            {
                                Name = $"{ymapFileName}_{ytypThing.YtypName}"
                            };

                            //foreach (var item in foundEntities)
                            //{
                            //    SplittedYmap.AddEntity(item);
                            //}

                            SplittedYmap.AllEntities = foundEntities.ToArray();

                            if (SplittedYmap.AllEntities != null)
                            {
                                SplittedYmap.BuildCEntityDefs();
                                SplittedYmap.CalcExtents();
                                SplittedYmap.CalcFlags();
                                SplittedYmap.BuildCCarGens();
                                byte[] newYmapBytes2 = SplittedYmap.Save();
                                File.WriteAllBytes(TbSplitOutputField.Text + $"\\{ymapFileName}_{ytypThing.YtypName}.ymap", newYmapBytes2);
                            }
                        }
                    }
                }

                
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    var msBoxYmapSuccess = MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = ButtonEnum.Ok,
                        ContentTitle = "Information",
                        CanResize = false,
                        Icon = MessageBox.Avalonia.Enums.Icon.Info,
                        ShowInCenter = true,
                        ContentMessage = "Ymaps splitted successfully!",
                    });

                    msBoxYmapSuccess.ShowDialog(this);

                    PbSplitProgress.Value = 0;


                });
                

            }
            else
            {
                
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    var msBoxYtypYmapMissing = MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = ButtonEnum.Ok,
                        ContentTitle = "Warning",
                        CanResize = false,
                        Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                        ContentMessage = "Please select YTYP and YMAP files!",
                    });
                    msBoxYtypYmapMissing.ShowDialog(this);

                });
            }
        }

        private static List<ArchetypeElement> GetArchetypeElementList(string[] ytypsFiles)
        {
            List<ArchetypeElement> ArchetypesNames = new();

            foreach (var ytyp in ytypsFiles)
            {
                ArchetypeElement archetypeElement = new();

                YtypFile ytypFile = new();
                ytypFile.Load(File.ReadAllBytes(ytyp));

                archetypeElement.YtypName = Path.GetFileNameWithoutExtension(ytyp);

                List<MetaHash> metaHashes = new();

                metaHashes.AddRange(ytypFile.AllArchetypes.Where(archs => archs.Type == MetaName.CBaseArchetypeDef || archs.Type == MetaName.CTimeArchetypeDef).Select(archs => archs.Hash));
                
                if (metaHashes.Count > 0)
                {
                    archetypeElement.archetypeNames = metaHashes;
                    ArchetypesNames.Add(archetypeElement);

                }


            }

            return ArchetypesNames;
        }

        private static List<ArchetypeElement> GetArchetypeElementList(string textFile)
        {
            List<ArchetypeElement> ArchetypesNames = new();
            ArchetypeElement archetypeElement = new();
            List<MetaHash> fileElements = new();

            foreach (var item in File.ReadAllLines(textFile))
            {
                fileElements.Add(JenkHash.GenHash(item.ToLower().Trim()));
            }

            archetypeElement.archetypeNames = fileElements;
            archetypeElement.YtypName = Path.GetFileNameWithoutExtension(textFile);
            ArchetypesNames.Add(archetypeElement);
            return ArchetypesNames;
        }
    }
}
