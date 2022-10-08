using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Globalization;
using CodeWalker.GameFiles;
using System.IO;
using System;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;
using Avalonia;

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
        public string? gtavpath;
        public RpfManager rpfman = new();
        
        public MainWindow()
        {
            InitializeComponent();
            DefaultSettings();
        }
        private void DefaultSettings()
        { 
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
                var ofd = new OpenFileDialog
                {
                    AllowMultiple = false,
                };
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
                var timer = new Stopwatch();
                timer.Start();
                Parallel.ForEach(SelectedYmaps, ymap =>
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
                    foreach (ArchetypeElement? ytypThing in YtypArchetypesNames)
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
                        Directory.CreateDirectory(Path.Combine(TbSplitOutputField.Text, "modified_ymaps"));

                        byte[] newYmapBytes = ymapFile.Save();
                        File.WriteAllBytesAsync(Path.Combine(TbSplitOutputField.Text, "modified_ymaps") + $"\\{ymapFileName}.ymap", newYmapBytes);
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
                                File.WriteAllBytesAsync(TbSplitOutputField.Text + $"\\{ymapFileName}_{ytypThing.YtypName}.ymap", newYmapBytes2);
                            }
                        }
                    }

                });

                timer.Stop();
                Debug.WriteLine($"Elapsed time: {timer.ElapsedMilliseconds / 1000.0} second(s)");
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

            Parallel.ForEach(ytypsFiles, ytyp =>
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
            });

            return ArchetypesNames;
        }

        private static List<ArchetypeElement> GetArchetypeElementList(string textFile)
        {
            List<ArchetypeElement> ArchetypesNames = new();
            ArchetypeElement archetypeElement = new();
            List<MetaHash> fileElements = new();

            Parallel.ForEach(File.ReadAllLines(textFile), item =>
            {
                fileElements.Add(JenkHash.GenHash(item.ToLower().Trim()));

            });

            archetypeElement.archetypeNames = fileElements;
            archetypeElement.YtypName = Path.GetFileNameWithoutExtension(textFile);
            ArchetypesNames.Add(archetypeElement);
            return ArchetypesNames;
        }

        private async void UpdateStatus(string text)
        {
            try
            {

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    LbGTALoadingStatus.Content = text;
                    
                });
               
                
            }
            catch (Exception)
            {
            }

        }

        private async void TCSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (!rpfman.IsInited)
            {
                ExtractorControlsToggle(false);
                if (TabControlMenu != null)
                {
                    if (File.Exists("config.ini"))
                    {
                        gtavpath = File.ReadAllLines("config.ini")[0].Split("=")[1];
                    }
                    else
                    {
                        var msBoxSourceStatus2 = MessageBoxManager
                                 .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                                 {
                                     ButtonDefinitions = ButtonEnum.Ok,
                                     ContentTitle = "Warning",
                                     CanResize = false,
                                     Icon = MessageBox.Avalonia.Enums.Icon.Setting,
                                     ShowInCenter = true,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "Select your GTAV folder.",
                                 });

                        var buttonResult = await msBoxSourceStatus2.ShowDialog(this);

                        if (buttonResult == ButtonResult.Ok)
                        {
                            OpenFolderDialog ofd = new();
                            string? selecteddir = await ofd.ShowAsync(this);
                            StringBuilder sb = new();
                            gtavpath = selecteddir;
                            sb.AppendLine($"[GTAVPATH]={gtavpath}");
                            File.WriteAllText("config.ini", sb.ToString().Trim());
                        }
                    }
                }
                await Task.Run(() =>
                {
                    if (TabControlMenu != null)
                    {
                        if (TabControlMenu.SelectedIndex == 5)
                        {
                            GTA5Keys.LoadFromPath(gtavpath);
                            rpfman.Init(gtavpath, UpdateStatus, UpdateStatus);
                            
                        }
                    }
                }).ConfigureAwait(false);
            }
            else
            {
                ExtractorControlsToggle(true);
            }
        }

        private void ExtractorControlsToggle(bool toggle)
        {
            if (TbExtractOutputPath != null && 
                TbExtractSourcePath != null && 
                BtnSelectOutputExtract != null && 
                BtnSelectSourceExtract != null && 
                checkBoxEnableMods != null &&
                checkBoxEnableDLCs != null && 
                BtnExtractFiles != null &&
                CbExtractType != null)
            {
                TbExtractOutputPath.IsEnabled = toggle;
                TbExtractSourcePath.IsEnabled = toggle;
                BtnSelectOutputExtract.IsEnabled = toggle;
                BtnSelectSourceExtract.IsEnabled = toggle;
                checkBoxEnableMods.IsEnabled = toggle;
                checkBoxEnableDLCs.IsEnabled = toggle;
                BtnExtractFiles.IsEnabled = toggle;
                CbExtractType.IsEnabled = toggle;
            }
            

        }
        
        private async void LbGTALoadingChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if(LbGTALoadingStatus != null)
            {
                if (LbGTALoadingStatus.Content == "Scan complete")
                {
                    ExtractorControlsToggle(true);
                }
            }
            
        }

        private async void BtnSelectOutputExtractClick(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new();

            string? selecteddir = await ofd.ShowAsync(this);
            if (Directory.Exists(selecteddir))
            {
                TbExtractOutputPath.Text = selecteddir;

            }

        }

        private async void BtnSelectSourceExtractClick(object sender, RoutedEventArgs e)
        {

            if(CbExtractType != null)
            {

                switch (CbExtractType.SelectedIndex)
                {
                    case 0:
                        OpenFileDialog ofiled = new()
                        {
                            AllowMultiple = false,
                            Filters = new List<FileDialogFilter>()
                            {
                                new FileDialogFilter() { Name = "Text Files", Extensions = new List<string>() { "txt" } }
                            }
                        };
                        string? selectedfile = ofiled.ShowAsync(this).Result?[0];
                        if (File.Exists(selectedfile) && selectedfile.EndsWith(".txt"))
                        {
                            TbExtractSourcePath.Text = selectedfile;
                        }
                        break;
                    case 1:
                        OpenFolderDialog ofd = new();
                        string? selecteddirymap = await ofd.ShowAsync(this);
                        if (!Directory.Exists(selecteddirymap))
                        {
                            TbExtractSourcePath.Text = String.Empty;
                        }
                        else
                        {
                            TbExtractSourcePath.Text = selecteddirymap;
                            if (Directory.GetFiles(selecteddirymap, "*.ymap").Length < 1)
                            {
                                var msBoxSourceStatus2 = MessageBoxManager
                                 .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                                 {
                                     ButtonDefinitions = ButtonEnum.Ok,
                                     ContentTitle = "Error",
                                     CanResize = false,
                                     Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                                     ShowInCenter = true,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "No YMAP(s) have been detected, select another folder.",
                                 });

                                var buttonresult = await msBoxSourceStatus2.ShowDialog(this);

                                if(buttonresult == ButtonResult.Ok)
                                {
                                    TbExtractSourcePath.Text = String.Empty;
                                }
                            }
                        }
                        break;
                    case 2:
                        OpenFolderDialog ofd2 = new();
                        string? selecteddirytyp = await ofd2.ShowAsync(this);
                        if (!Directory.Exists(selecteddirytyp))
                        {
                            TbExtractSourcePath.Text = String.Empty;
                        }
                        else
                        {
                            TbExtractSourcePath.Text = selecteddirytyp;
                            if (Directory.GetFiles(selecteddirytyp, "*.ytyp").Length < 1)
                            {
                                var msBoxSourceStatus2 = MessageBoxManager
                                 .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                                 {
                                     ButtonDefinitions = ButtonEnum.Ok,
                                     ContentTitle = "Error",
                                     CanResize = false,
                                     Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                                     ShowInCenter = true,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "No YTYP(s) have been detected, select another folder.",
                                 });
                                
                                var buttonresult = await msBoxSourceStatus2.ShowDialog(this);

                                if (buttonresult == ButtonResult.Ok)
                                {
                                    TbExtractSourcePath.Text = String.Empty;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }


            

        }

        private async void CbExtractTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CbExtractType != null)
            {
                TbExtractSourcePath.Text = String.Empty;

            }
        }

        private async void BtnExtractFilesClick(object sender, RoutedEventArgs e)
        {
            if (CbExtractType.SelectedIndex != -1 && TbExtractSourcePath.Text != null && TbExtractOutputPath.Text != null)
            {
                ExtractorControlsToggle(false);
                switch (CbExtractType.SelectedIndex)
                {
                    case 1:

                        string[] YmapFolder = Directory.GetFiles(TbExtractSourcePath.Text, "*.ymap");

                        foreach (var YmapFile in YmapFolder)
                        {
                            YmapFile yf = new();
                            yf.Load(File.ReadAllBytes(YmapFile));
                            

                            ExportFilesFromFiles(yf, TbExtractOutputPath.Text, (bool)checkBoxEnableMods.IsChecked, (bool)checkBoxEnableDLCs.IsChecked);
                            
                        }
                        
                        var msBoxSourceStatus = MessageBoxManager
                                 .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                                 {
                                     ButtonDefinitions = ButtonEnum.Ok,
                                     ContentTitle = "Success",
                                     CanResize = false,
                                     Icon = MessageBox.Avalonia.Enums.Icon.Info,
                                     ShowInCenter = true,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "Assets from YMAP(s) have been extracted successfully.",
                                 });

                        msBoxSourceStatus.ShowDialog(this);
                        ExtractorControlsToggle(true);



                        break;
                    default:
                        break;
                }
            }
            else
            {
                {
                    var msBoxSourceStatus = MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Error",
                            CanResize = false,
                            Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                            ShowInCenter = true,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            ContentMessage = "Please select a source path, output path.",
                        });

                    var buttonresult = await msBoxSourceStatus.ShowDialog(this);

                    if (buttonresult == ButtonResult.Ok)
                    {
                        return;
                    }
                }
            }
                
        }

        public void ExportFilesFromFiles(YmapFile yf, string output, bool mods, bool dlc)
        {
            List<RpfFile> rpfFiles = new();
            List<string> ymapEntitiesStrings = new();
            string ymapName = JenkIndex.GetString(yf._CMapData.name);

            RpfManager r = new();
            r.Init(gtavpath, UpdateStatus, UpdateStatus);

            if (mods && dlc) { rpfFiles = r.AllRpfs; }
            else if (!mods && dlc) { rpfFiles = r.AllNoModRpfs; }
            else if (mods && !dlc) { rpfFiles.AddRange(r.ModRpfs); rpfFiles.AddRange(r.BaseRpfs); }
            else { rpfFiles = r.BaseRpfs; }

            foreach (var item in yf.AllEntities)
            {
                ymapEntitiesStrings.Add(JenkIndex.GetString(item._CEntityDef.archetypeName));
            }

            ymapEntitiesStrings = ymapEntitiesStrings.Distinct().ToList();

            Directory.CreateDirectory(Path.Combine(output, ymapName));

            foreach (RpfFile rpffile in rpfFiles)
            {
                foreach (RpfEntry entry in rpffile.AllEntries)
                {

                    foreach (var x in ymapEntitiesStrings)
                    {
                        if (entry.Name.Contains(x + "."))
                        {
                            var fentry = entry as RpfFileEntry;
                            RpfResourceFileEntry rrfe = fentry as RpfResourceFileEntry;
                            byte[] data = entry.File.ExtractFile(fentry);

                            if (rrfe != null)
                            {
                                data = ResourceBuilder.AddResourceHeader(rrfe, data);
                            }


                            File.WriteAllBytes(Path.Combine(output, ymapName, entry.Name), data);
                        }
                    }
                }
            }


        }

        public bool ExportFilesFromFiles(YtypFile ytf, string output, bool mods, bool dlc)
        {
            List<RpfFile> rpfFiles = new();

            List<uint> AllYTYPHashes = new();
            List<uint> YtypMloEntitiesHashes = new();
            List<uint> YtypArchetypesHashes = new();
            bool Success = false;


            if (mods && dlc) { rpfFiles = rpfman.AllRpfs; }
            else if (!mods && dlc) { rpfFiles = rpfman.AllNoModRpfs; }
            else if (mods && !dlc) { rpfFiles.AddRange(rpfman.ModRpfs); rpfFiles.AddRange(rpfman.BaseRpfs); }
            else { rpfFiles = rpfman.BaseRpfs; }

            YtypArchetypesHashes = (from item in ytf.AllArchetypes
                                    let newhash = item._BaseArchetypeDef.assetName.Hash
                                    select newhash).ToList();

            Directory.CreateDirectory(Path.Combine(output, ytf.Name));

            int MLOArchetypeCount = ytf.AllArchetypes.Where(x => x.Type == MetaName.CMloArchetypeDef).Count();

            if (MLOArchetypeCount > 0)
            {
                Directory.CreateDirectory(Path.Combine(output, ytf.Name, "MLO entities"));

                foreach (var item in ytf.AllArchetypes)
                {
                    if (item.Type == MetaName.CMloArchetypeDef)
                    {
                        MloArchetype? mloarch = item as MloArchetype;
                        foreach (var mloentity in mloarch.entities)
                        {
                            YtypMloEntitiesHashes.Add(mloentity._Data.archetypeName.Hash);
                        }
                    }
                }

                YtypMloEntitiesHashes = YtypMloEntitiesHashes.Distinct().ToList();
            }

            AllYTYPHashes = AllYTYPHashes.Concat(YtypArchetypesHashes).Concat(YtypMloEntitiesHashes).Distinct().ToList();


            foreach (RpfFile rpffile in rpfFiles)
            {
                foreach (RpfEntry entry in rpffile.AllEntries)
                {
                    AllYTYPHashes.ForEach(x =>
                    {
                        if (entry.NameHash == x)
                        {
                            var fentry = entry as RpfFileEntry;
                            byte[] data = entry.File.ExtractFile(fentry);

                            if (fentry is RpfResourceFileEntry rfentry)
                            {
                                data = ResourceBuilder.AddResourceHeader(rfentry, data);
                            }

                            if (YtypMloEntitiesHashes.Contains(x))
                            {
                                File.WriteAllBytes(Path.Combine(output, ytf.Name, "MLO entities", entry.Name), data);
                                Success = true;

                            }
                            else
                            {
                                File.WriteAllBytes(Path.Combine(output, ytf.Name, entry.Name), data);
                                Success = true;

                            }

                        }
                    });
                }
            }

            return Success;

        }

        public bool ExportFilesFromFiles(string filename, string output, bool mods, bool dlc)
        {
            List<RpfFile> rpfFiles = new();

            bool Success = false;

            if (mods && dlc) { rpfFiles = rpfman.AllRpfs; }
            else if (!mods && dlc) { rpfFiles = rpfman.AllNoModRpfs; }
            else if (mods && !dlc) { rpfFiles.AddRange(rpfman.ModRpfs); rpfFiles.AddRange(rpfman.BaseRpfs); }
            else { rpfFiles = rpfman.BaseRpfs; }

            var textEntitiesHashes = (from item in File.ReadAllLines(filename)
                                      let newhash = JenkHash.GenHash(item)
                                      select newhash).Distinct().ToList();


            Directory.CreateDirectory(Path.Combine(output, Path.GetFileNameWithoutExtension(filename)));

            foreach (RpfFile rpfFile in rpfFiles)
            {
                foreach (RpfEntry entry in rpfFile.AllEntries)
                {

                    foreach (var x in textEntitiesHashes)
                    {
                        if (entry.NameHash == x)
                        {
                            var fentry = entry as RpfFileEntry;
                            byte[] data = entry.File.ExtractFile(fentry);

                            if (fentry is RpfResourceFileEntry rfentry)
                            {
                                data = ResourceBuilder.AddResourceHeader(rfentry, data);
                            }

                            File.WriteAllBytes(Path.Combine(output, Path.GetFileNameWithoutExtension(filename), entry.Name), data);
                            Success = true;

                        }
                    }

                }
            }

            return Success;


        }

    }
}
