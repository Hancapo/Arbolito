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
        public string? GTAVPath;
        public RpfManager RpfMan = new();

        public MainWindow()
        {
            InitializeComponent();
            DefaultSettings();
        }
        private static void DefaultSettings()
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
            if (CbSplitBy.SelectedIndex == 0)
            {
                var dlg = new OpenFolderDialog();
                var result = dlg.ShowAsync(this).Result;
                if (string.IsNullOrWhiteSpace(result)) return;
                TbSplitSourceField.Text = result.ToString();
                var ytypCount = Directory.GetFiles(result, "*.ytyp").Length;
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
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            ContentMessage = "No YTYP(s) found!",
                        });
                    msBoxSourceStatus.ShowDialog(this);
                    //MessageBox.Show($"No YTYP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LbSplitSource.Content = ($"Text file loaded");
                }
            }
            else
            {
                var ofd = new OpenFileDialog
                {
                    AllowMultiple = false,
                };
                var result = ofd.ShowAsync(this).Result?[0];
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
            if (string.IsNullOrWhiteSpace(result)) return;
            TbSplitYmapField.Text = result;
            var ymapCount = Directory.GetFiles(result, "*.ymap").Length;
            if (ymapCount > 0)
            {
                SelectedYmaps = Directory.GetFiles(result, "*.ymap");
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
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        ContentMessage = "No YMAP(s) found",
                    });
                msBoxYmapStatus.ShowDialog(this);
                //MessageBox.Show($"No YMAP(s) found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }

        private void OnBtnSplitOutputBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitOutputField.Text = result;
            }
        }

        private async void OnBtnSplitMappingsClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(SplitYmaps);
        }

        private void SplitYmaps()
        {
            List<ArchetypeElement> ytypArchetypesNames;
            if (TbSplitOutputField.Text != string.Empty)
            {
                //YMAP Processing
                var timer = new Stopwatch();
                timer.Start();
                Parallel.ForEach(SelectedYmaps, ymap =>
                {
                    var ymapProgress = (int)Math.Round((double)(Array.IndexOf(SelectedYmaps, ymap) + 1) / SelectedYmaps.Length * 100);
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        //Normalize integer value from 0 to 100
                        PbSplitProgress.Value = ymapProgress;
                    });
                    YmapFile ymapFile = new();
                    ymapFile.Load(File.ReadAllBytes(ymap));
                    var ymapFileName = Path.GetFileNameWithoutExtension(ymap);
                    ytypArchetypesNames = CbSplitBy.SelectedIndex == 0 ? GetArchetypeElementList(SelectedYtyps) : GetArchetypeElementList(TbSplitSourceField.Text);
                    foreach (var ytypThing in ytypArchetypesNames)
                    {
                        List<YmapEntityDef> foundEntities = new();
                        if (ymapFile.AllEntities != null && (ymapFile.AllEntities != null || ymapFile.CarGenerators == null))
                        {
                            foreach (var archs in CollectionsMarshal.AsSpan(ymapFile.AllEntities?.ToList()))
                            {
                                foreach (var addedArch in ytypThing.archetypeNames.Where(addedArch => archs._CEntityDef.archetypeName == addedArch &&
                                             archs._CEntityDef.lodLevel is rage__eLodType.LODTYPES_DEPTH_ORPHANHD or rage__eLodType.LODTYPES_DEPTH_HD))
                                {
                                    foundEntities.Add(archs);
                                    ymapFile.RemoveEntity(archs);
                                }
                            }
                        }
                        Directory.CreateDirectory(Path.Combine(TbSplitOutputField.Text, "modified_ymaps"));
                        var newYmapBytes = ymapFile.Save();
                        File.WriteAllBytesAsync(Path.Combine(TbSplitOutputField.Text, "modified_ymaps") + $"\\{ymapFileName}.ymap", newYmapBytes);
                        if (foundEntities.Count <= 0) continue;
                        YmapFile splittedYmap = new()
                        {
                            Name = $"{ymapFileName}_{ytypThing.YtypName}",
                            //}
                            //    SplittedYmap.AddEntity(item);
                            //{
                            //foreach (var item in foundEntities)
                            AllEntities = foundEntities.ToArray()
                        };
                        if (splittedYmap.AllEntities == null) continue;
                        splittedYmap.BuildCEntityDefs();
                        splittedYmap.CalcExtents();
                        splittedYmap.CalcFlags();
                        splittedYmap.BuildCCarGens();
                        var newYmapBytes2 = splittedYmap.Save();
                        File.WriteAllBytesAsync(TbSplitOutputField.Text + $"\\{ymapFileName}_{ytypThing.YtypName}.ymap", newYmapBytes2);
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
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
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
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        ContentMessage = "Please select YTYP and YMAP files!",
                    });
                    msBoxYtypYmapMissing.ShowDialog(this);

                });
            }
        }

        private static List<ArchetypeElement> GetArchetypeElementList(IEnumerable<string> YtypsFiles)
        {
            List<ArchetypeElement> ArchetypesNames = new();

            Parallel.ForEach(YtypsFiles, ytyp =>
            {
                ArchetypeElement archetypeElement = new();

                YtypFile ytypFile = new();
                ytypFile.Load(File.ReadAllBytes(ytyp));

                archetypeElement.YtypName = Path.GetFileNameWithoutExtension(ytyp);

                List<MetaHash> metaHashes = new();

                metaHashes.AddRange(ytypFile.AllArchetypes.Where(archs => archs.Type is MetaName.CBaseArchetypeDef or MetaName.CTimeArchetypeDef).Select(archs => archs.Hash));

                if (metaHashes.Count <= 0) return;
                archetypeElement.archetypeNames = metaHashes;
                ArchetypesNames.Add(archetypeElement);
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
            catch
            {
                // ignored
            }
        }

        private async void TCSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!RpfMan.IsInited)
            {
                ExtractorControlsToggle(false);
                if (TabControlMenu != null)
                {
                    if (File.Exists("config.ini"))
                    {
                        GTAVPath = (await File.ReadAllLinesAsync("config.ini"))[0].Split("=")[1];
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
                                     SystemDecorations = SystemDecorations.None,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "Select your GTAV folder.",
                                 });

                        var buttonResult = await msBoxSourceStatus2.ShowDialog(this);

                        if (buttonResult == ButtonResult.Ok)
                        {
                            OpenFolderDialog ofd = new();
                            var selecteddir = await ofd.ShowAsync(this);
                            StringBuilder sb = new();
                            GTAVPath = selecteddir;
                            sb.AppendLine($"[GTAVPATH]={GTAVPath}");
                            await File.WriteAllTextAsync("config.ini", sb.ToString().Trim());
                        }
                    }
                }
                await Task.Run(() =>
                {
                    if (TabControlMenu is not { SelectedIndex: 5 }) return;
                    GTA5Keys.LoadFromPath(GTAVPath);
                    RpfMan.Init(GTAVPath, UpdateStatus, UpdateStatus);
                }).ConfigureAwait(false);
            }
            else
            {
                ExtractorControlsToggle(true);
            }
        }

        private void ExtractorControlsToggle(bool toggle)
        {
            if (TbExtractOutputPath == null ||
                TbExtractSourcePath == null ||
                BtnSelectOutputExtract == null ||
                BtnSelectSourceExtract == null ||
                checkBoxEnableMods == null ||
                checkBoxEnableDLCs == null ||
                BtnExtractFiles == null ||
                CbExtractType == null) return;
            TbExtractOutputPath.IsEnabled = toggle;
            TbExtractSourcePath.IsEnabled = toggle;
            BtnSelectOutputExtract.IsEnabled = toggle;
            BtnSelectSourceExtract.IsEnabled = toggle;
            checkBoxEnableMods.IsEnabled = toggle;
            checkBoxEnableDLCs.IsEnabled = toggle;
            BtnExtractFiles.IsEnabled = toggle;
            CbExtractType.IsEnabled = toggle;
            checkBoxExtractAsXml.IsEnabled = toggle;
        }

        private void LbGTALoadingChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (LbGTALoadingStatus == null) return;
            if ((string)LbGTALoadingStatus.Content == "Scan complete")
            {
                ExtractorControlsToggle(true);
            }

        }
        
        private async void BtnSelectOutputExtractClick(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new();

            var selecteddir = await ofd.ShowAsync(this);
            if (Directory.Exists(selecteddir))
            {
                TbExtractOutputPath.Text = selecteddir;

            }

        }

        private async void BtnSelectSourceExtractClick(object sender, RoutedEventArgs e)
        {

            if (CbExtractType != null)
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

                                if (buttonresult == ButtonResult.Ok)
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
                                    TbExtractSourcePath.Text = string.Empty;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }




        }

        private void CbExtractTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbExtractType != null)
            {
                TbExtractSourcePath.Text = string.Empty;

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

                        var ymapFolder = Directory.GetFiles(TbExtractSourcePath.Text, "*.ymap");

                        foreach (var ymapFile in ymapFolder)
                        {
                            YmapFile yf = new();
                            yf.Load(await File.ReadAllBytesAsync(ymapFile));


                            ExportFilesFromFiles(yf, TbExtractOutputPath.Text, (bool)checkBoxEnableMods.IsChecked, (bool)checkBoxExtractAsXml.IsChecked, (bool)checkBoxExtractAsXml.IsChecked);

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

                        await msBoxSourceStatus.ShowDialog(this);
                        ExtractorControlsToggle(true);



                        break;
                    case 2:
                        var YtypFiles = Directory.GetFiles(TbExtractSourcePath.Text, "*.ytyp");

                        foreach (var YtypFile in YtypFiles)
                        {
                            YtypFile ytf = new();
                            ytf.Load(await File.ReadAllBytesAsync(YtypFile));
                            ExportFilesFromFiles(ytf, TbExtractOutputPath.Text, (bool)checkBoxEnableMods.IsChecked, (bool)checkBoxEnableDLCs.IsChecked, (bool)checkBoxExtractAsXml.IsChecked);
                        }

                        var msBoxSourceStatus2 = MessageBoxManager
                                 .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                                 {
                                     ButtonDefinitions = ButtonEnum.Ok,
                                     ContentTitle = "Success",
                                     CanResize = false,
                                     Icon = MessageBox.Avalonia.Enums.Icon.Info,
                                     ShowInCenter = true,
                                     WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                     ContentMessage = "Assets from YTYP(s) have been extracted successfully.",
                                 });

                        await msBoxSourceStatus2.ShowDialog(this);
                        ExtractorControlsToggle(true);

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
                    }
                }
            }

        }

        public void ExportFilesFromFiles(YmapFile yf, string output, bool mods, bool dlc, bool asxml)
        {

            List<RpfFile> rpfFiles = new();
            List<string> ymapEntitiesStrings = new();
            string ymapName = JenkIndex.GetString(yf._CMapData.name);

            if (mods && dlc) { rpfFiles = RpfMan.AllRpfs; }
            else if (!mods && dlc) { rpfFiles = RpfMan.AllNoModRpfs; }
            else if (mods && !dlc) { rpfFiles.AddRange(RpfMan.ModRpfs); rpfFiles.AddRange(RpfMan.BaseRpfs); }
            else { rpfFiles = RpfMan.BaseRpfs; }

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
                        if (!entry.Name.Contains(x + ".")) continue;
                        var fentry = entry as RpfFileEntry;
                        RpfResourceFileEntry rrfe = fentry as RpfResourceFileEntry;
                        var data = entry.File.ExtractFile(fentry);

                        if (rrfe != null)
                        {
                            data = ResourceBuilder.AddResourceHeader(rrfe, data);
                        }

                        if (!asxml)
                        {
                            File.WriteAllBytes(Path.Combine(output, ymapName, entry.NameLower), data);
                        }
                        else
                        {
                            string newfn;
                            var xml = MetaXml.GetXml(fentry, fentry.File?.ExtractFile(fentry), out newfn, Path.Combine(output, ymapName));
                            if (string.IsNullOrEmpty(xml))
                            {
                                return;
                            }

                            File.WriteAllText(Path.Combine(output, ymapName, entry.NameLower + ".xml"), xml);
                        }
                    }
                }
            }


        }

        public void ExportFilesFromFiles(YtypFile ytf, string output, bool mods, bool dlc, bool asxml)
        {
            List<RpfFile> rpfFiles = new();

            List<string> AllYTYPHashes = new();
            List<string> YtypMloEntitiesHashes = new();
            var ytypName = JenkIndex.GetString(ytf._CMapTypes.name);


            switch (mods)
            {
                case true when dlc:
                    rpfFiles = RpfMan.AllRpfs;
                    break;
                case false when dlc:
                    rpfFiles = RpfMan.AllNoModRpfs;
                    break;
                case true when !dlc:
                    rpfFiles.AddRange(RpfMan.ModRpfs); rpfFiles.AddRange(RpfMan.BaseRpfs);
                    break;
                default:
                    rpfFiles = RpfMan.BaseRpfs;
                    break;
            }


            var YtypArchetypesHashes = ytf.AllArchetypes.Select(item => JenkIndex.GetString(item._BaseArchetypeDef.assetName)).ToList();

            Directory.CreateDirectory(Path.Combine(output, ytypName));

            int MLOArchetypeCount = ytf.AllArchetypes.Count(x => x.Type == MetaName.CMloArchetypeDef);

            if (MLOArchetypeCount > 0)
            {
                Directory.CreateDirectory(Path.Combine(output, ytypName, "MLO entities"));
                YtypMloEntitiesHashes.AddRange(from item in ytf.AllArchetypes where item.Type == MetaName.CMloArchetypeDef select item as MloArchetype into mloarch from mloentity in mloarch.entities select JenkIndex.GetString(mloentity._Data.archetypeName));
                YtypMloEntitiesHashes = YtypMloEntitiesHashes.Distinct().ToList();
            }

            AllYTYPHashes = AllYTYPHashes.Concat(YtypArchetypesHashes).Concat(YtypMloEntitiesHashes).Distinct().ToList();


            foreach (var rpffile in rpfFiles)
            {
                foreach (var entry in rpffile.AllEntries)
                {
                    foreach (var x in AllYTYPHashes)
                    {
                        if (!entry.Name.Contains(x + ".")) continue;
                        var fentry = entry as RpfFileEntry;
                        var data = entry.File.ExtractFile(fentry);

                        if (fentry is RpfResourceFileEntry rfentry)
                        {
                            data = ResourceBuilder.AddResourceHeader(rfentry, data);
                        }

                        if (YtypMloEntitiesHashes.Contains(x))
                        {
                            if (!asxml)
                            {
                                File.WriteAllBytes(Path.Combine(output, ytypName, "MLO entities", entry.NameLower), data);

                            }
                            else
                            {
                                string newfn;
                                var xml = MetaXml.GetXml(fentry, fentry.File?.ExtractFile(fentry), out newfn, Path.Combine(output, ytypName, "MLO entities"));
                                if (string.IsNullOrEmpty(xml))
                                {
                                    return;
                                }

                                File.WriteAllText(Path.Combine(output, ytypName, "MLO entities", entry.NameLower + ".xml"), xml);
                            }

                        }
                        else
                        {
                            if (!asxml)
                            {
                                File.WriteAllBytes(Path.Combine(output, ytypName, entry.NameLower), data);

                            }
                            else
                            {
                                string newfn;
                                var xml = MetaXml.GetXml(fentry, fentry.File?.ExtractFile(fentry), out newfn, Path.Combine(output, ytypName));
                                if (string.IsNullOrEmpty(xml))
                                {
                                    return;
                                }

                                File.WriteAllText(Path.Combine(output, ytypName, entry.NameLower + ".xml"), xml);
                            }

                        }
                    }
                }
            }


        }

        public void ExportFilesFromFiles(string filename, string output, bool mods, bool dlc, bool asxml)
        {
            List<RpfFile> rpfFiles = new();


            switch (mods)
            {
                case true when dlc:
                    rpfFiles = RpfMan.AllRpfs;
                    break;
                case false when dlc:
                    rpfFiles = RpfMan.AllNoModRpfs;
                    break;
                case true when !dlc:
                    rpfFiles.AddRange(RpfMan.ModRpfs); rpfFiles.AddRange(RpfMan.BaseRpfs);
                    break;
                default:
                    rpfFiles = RpfMan.BaseRpfs;
                    break;
            }

            var textEntitiesHashes = (from item in File.ReadAllLines(filename)
                                      let newhash = JenkHash.GenHash(item)
                                      select newhash).Distinct().ToList();


            Directory.CreateDirectory(Path.Combine(output, Path.GetFileNameWithoutExtension(filename)));
            
            foreach (var rpfFile in rpfFiles)
            {
                foreach (var entry in rpfFile.AllEntries)
                {

                    foreach (var x in textEntitiesHashes)
                    {
                        if (entry.NameHash != x) continue;
                        var fentry = entry as RpfFileEntry;
                        var data = entry.File.ExtractFile(fentry);

                        if (fentry is RpfResourceFileEntry rfentry)
                        {
                            data = ResourceBuilder.AddResourceHeader(rfentry, data);
                        }

                        File.WriteAllBytes(Path.Combine(output, Path.GetFileNameWithoutExtension(filename), entry.Name), data);
                    }

                }
            }



        }

    }
}
