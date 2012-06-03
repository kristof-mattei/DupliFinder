using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DupliFinder.Properties;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using DupliFinder.Processing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace DupliFinder
{
    public partial class frmMain : Form
    {
        BackgroundWorker bw;
        List<string> workingDirectories;

        public frmMain()
        {
            InitializeComponent();
            btSearch.Text = Resources.strSearch;
            btRemOrig.Visible = btRemDup.Visible = false;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                tbPath.Text = fd.SelectedPath;
                workingDirectories = new List<string>();
                workingDirectories.Add(tbPath.Text);
                btSearch.Enabled = true;
                restoreDefaults();
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            workingDirectories = getData(e.Data);
            if (workingDirectories.Count > 0)
            {
                btSearch.Enabled = true;
                tbPath.Text = workingDirectories[0];
                restoreDefaults();
            }
            else
            {
                btSearch.Enabled = false;
            }
        }

        void restoreDefaults()
        {
            this.MinimumSize = new Size(390, 130);
            this.Size = this.MinimumSize;
            btRemOrig.Visible = btRemDup.Visible = false;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (getData(e.Data).Count > 0)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (btSearch.Text == Resources.strSearch)
            {
                startSearch();
            }
            else
            {
                cancelSearch();
            }
        }

        List<string> getData(IDataObject obj)
        {
            List<string> resp = new List<string>();
            if (obj.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = obj.GetData(DataFormats.FileDrop) as string[];
                if (str != null && str.Length > 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (Directory.Exists(str[i]))
                        {
                            resp.Add(str[i]);
                        }
                    }
                }
            }
            return resp;
        }
      
        private void cancelSearch()
        {
            if (bw != null && bw.IsBusy)
                bw.CancelAsync();
        }

        void startSearch()
        {
            lvResults.Items.Clear();
            btRemOrig.Visible = btRemDup.Visible = false;
            OnNewDuplicateFound += new EventHandler(frmMain_OnNewDuplicateFound);
            lblWorking.Visible = pbWorkingAll.Visible = true;
            pbWorkingAll.Value = 0;
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        void frmMain_OnNewDuplicateFound(object sender, EventArgs e)
        {
            fillData();
        }

        void fillData()
        {
            this.BeginInvoke((SendOrPostCallback)delegate
            {
                ListView.SelectedIndexCollection rIdx = lvResults.SelectedIndices;
                ListView.SelectedIndexCollection dIdx = lvDuplicates.SelectedIndices;
                int iR = rIdx.Count > 0 ? rIdx[0] : -1;
                int iD = dIdx.Count > 0 ? dIdx[0] : -1;

                this.MinimumSize = new Size(539, 442);
                lvResults.Items.Clear();
                lvDuplicates.Items.Clear();
                foreach (ComparableBitmap cb in comparisions.Keys)
                {
                    List<ComparableBitmap> val;
                    if (comparisions.TryGetValue(cb, out val) && val.Count > 0)
                    {
                        string ar = cb.Path.Substring(cb.Path.LastIndexOf('\\') + 1);
                        lvResults.Items.Add(ar);
                    }
                }
                if (lvResults.Items.Count <= iR)
                    iR = lvResults.Items.Count - 1;

                if (lvDuplicates.Items.Count <= iD)
                    iD = lvDuplicates.Items.Count - 1;

                if (iR != -1 & lvResults.Items.Count > 0)
                {
                    
                    lvResults.SelectedIndices.Add(iR);
                }
                if (iD != -1 & lvDuplicates.Items.Count > 0)
                {
                    
                    lvDuplicates.SelectedIndices.Add(iD);
                }

                if (lvResults.Items.Count <= 0)
                    restoreDefaults();

            }, new object[] { null });
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblWorking.Visible = pbWorkingAll.Visible = false;
            if (comparisions == null || lvResults.Items.Count <= 0)
                MessageBox.Show(this, Resources.msgNoDups, "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            OnNewDuplicateFound -= new EventHandler(frmMain_OnNewDuplicateFound);
            bitmaps.Clear();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbWorkingAll.Value = e.ProgressPercentage;
            if (e.UserState.GetType() == typeof(string))
            {
                lblWorking.Text = (string)e.UserState;
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            bitmaps = new List<ComparableBitmap>();
            for (int i = 0; i < workingDirectories.Count; i++)
            {
                if (bw.CancellationPending)
                    break;
                enumFiles(workingDirectories[i], (int)((double)(i/2) / (double)workingDirectories.Count * 100));
                compareFiles((int)((double)(i) / (double)workingDirectories.Count * 100));
            }
        }

        

        void pbWorking_VisibleChanged(object sender, EventArgs e)
        {
            btBrowse.Enabled = cbIncSubfolders.Enabled = lblStart.Enabled = tbPath.Enabled = lblNote.Enabled = !lblWorking.Visible;
            btSearch.Text = lblWorking.Visible ? Resources.setCancel : Resources.strSearch;
        }

        string[] getImgFiles(string path, string[] extentions, bool includeSubdirs)
        {
            List<string> files = new List<string>();
            for (int i = 0; i < extentions.Length; i++)
            {
                files.AddRange(Directory.GetFiles(path, string.Format("*.{0}", extentions[i]), includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            }
            return files.ToArray();
        }

        void enumFiles(string path, int progress)
        {
            string[] files = getImgFiles(path, Settings.Default.SupporterFileExtentions.Split(';'), cbIncSubfolders.Checked);
            for (int i = 0; i < files.Length; i++)
            {
                if (bw.CancellationPending)
                    break;

                bitmaps.Add(new ComparableBitmap(files[i]));
                bw.ReportProgress((int)((double)i / (double)files.Length * 100),Resources.strLoading);
            }
            
        }
        
        void compareFiles(int progress)
        {
            if (bitmaps == null)
                return;
            comparisions = new Dictionary<ComparableBitmap, List<ComparableBitmap>>();
            processed = new List<int>();
            double p = 0;
            

            for (int i = 0; i < bitmaps.Count; i++)
            {
                if (bw.CancellationPending)
                    break;
                
                if (!processed.Contains(i) && !comparisions.ContainsKey(bitmaps[i]))
                {

                    comparisions.Add(bitmaps[i], new List<ComparableBitmap>());
                    processed.Add(i);
                    ThreadQueue q = new ThreadQueue(bitmaps.Count / 10);
                    Levenshtein alg = new Levenshtein();

                    for (int j = 0; j < bitmaps.Count; j++)
                    {
                        if (bw.CancellationPending)
                            break;
                        q.QueueUserWorkItem((WaitCallback)delegate(object a)
                        {
                            int r = (int)a;
                            if (i != r && !processed.Contains(r))
                            {
                                int k = alg.GetDistance<byte>(bitmaps[i], bitmaps[r]);
                                if (k <= Settings.Default.SimilarityTreshold)
                                {
                                    bitmaps[r].Similarity = k;
                                    comparisions[bitmaps[i]].Add(bitmaps[r]);
                                    processed.Add(r);
                                    if (OnNewDuplicateFound != null)
                                    {

                                        OnNewDuplicateFound(this, null);
                                    }
                                }
                            }


                        }, j);
                        bw.ReportProgress((int)(++p / (double)(bitmaps.Count * bitmaps.Count) * 100), string.Format(Resources.strProceessing, 
                            bitmaps[i].Path.Substring(bitmaps[i].Path.LastIndexOf('\\')+1)));

                    }
                    q.WaitAll();
                        
                }
                
            }

            

        }

        

        event EventHandler OnNewDuplicateFound;
        
        List<int> processed;
        List<ComparableBitmap> bitmaps;
        Dictionary<ComparableBitmap, List<ComparableBitmap>> comparisions;

        private void pbOrig_SizeChanged(object sender, EventArgs e)
        {
            pbDup.Location = new Point(pbOrig.Location.X + pbOrig.Width + 6, pbDup.Location.Y);
            lvDuplicates.Location = new Point(lvDuplicates.Location.X, pbDup.Location.Y + pbDup.Height + 3);
            btRemOrig.Location = new Point(pbOrig.Location.X + pbOrig.Width - btRemOrig.Width, pbOrig.Location.Y + pbOrig.Height - btRemOrig.Height);
            btRemDup.Location = new Point(pbDup.Location.X + pbDup.Width - btRemDup.Width, pbDup.Location.Y + pbDup.Height - btRemDup.Height);
            
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pbOrig.Width = pbOrig.Height = pbDup.Width = pbDup.Height = (gb1.Width - lvResults.Width - 24) / 2;
            lvDuplicates.Height = gb1.Height - pbDup.Height - 24;
            
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            displayPreviewImages();
            if (lvDuplicates.SelectedIndices.Count > 0)
                displayPreviewDuplicate(lvDuplicates.SelectedIndices[0]);
        }

        

        KeyValuePair<ComparableBitmap,List<ComparableBitmap>>getItemByParticalKeyName(string name)
        {
            foreach (ComparableBitmap b in comparisions.Keys)
            {
                if (b.Path.Contains(name))
                    return new KeyValuePair<ComparableBitmap, List<ComparableBitmap>>(b, comparisions[b]);
            }
            return new KeyValuePair<ComparableBitmap,List<ComparableBitmap>>();
        
        }

        double getRatio(Size s)
        {
            double r = 0;
            if (s.Width < s.Height)
                r = (double)pbOrig.Width / (double)s.Width;
            else
                r = (double)pbOrig.Height / (double)s.Height;
            return r;
        }
        KeyValuePair<ComparableBitmap, List<ComparableBitmap>> kvp;
        private void lvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvDuplicates.Items.Clear();
            if (lvResults.SelectedItems.Count > 0)
            {
                string str = lvResults.SelectedItems[0].Text;
                kvp = getItemByParticalKeyName(str);

                Size s = displayPreviewImages();
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new string[] {kvp.Value[i].Path.Substring(kvp.Value[i].Path.LastIndexOf('\\') + 1),
                        string.Format("{0}X{1}", s.Width, s.Height),
                        string.Format("{0:N2}%",100 - ((double)kvp.Value[0].Similarity / (double)Settings.Default.SimilarityTreshold * 100))});
                    lvDuplicates.Items.Add(item);
                }

            }
        }

        private void lvDuplicates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kvp.Key != null && lvDuplicates.SelectedIndices.Count > 0)
            {
                displayPreviewDuplicate(lvDuplicates.SelectedIndices[0]);
            }
        }

        private Size displayPreviewDuplicate(int idx)
        {
            if (kvp.Key != null)
            {
                Image b = Image.FromFile(kvp.Value[idx].Path);
                if (pbDup.Image != null)
                {
                    pbDup.Image.Dispose();
                    pbDup.Image = null;
                }
                pbDup.Image = b.GetThumbnailImage((int)(b.Width * getRatio(b.Size)), (int)(b.Height * getRatio(b.Size)), null, IntPtr.Zero);
                pbDup.Tag = kvp.Value[idx].Path;
                btRemDup.Visible = true;
                return b.Size;
            }
            return Size.Empty;
        }

        private Size displayPreviewImages()
        {
            if (kvp.Key != null)
            {
                Image b = Image.FromFile(kvp.Key.Path);
                if (pbOrig.Image != null)
                {
                    pbOrig.Image.Dispose();
                    pbOrig.Image = null;
                }
                pbOrig.Image = b.GetThumbnailImage((int)(b.Width * getRatio(b.Size)), (int)(b.Height * getRatio(b.Size)), null, IntPtr.Zero);
                pbOrig.Tag = kvp.Key.Path;
                if (pbDup.Image != null)
                {
                    pbDup.Image.Dispose();
                    pbDup.Image = null;
                    btRemDup.Visible = false;
                }
                btRemOrig.Visible = true;
                return b.Size;
            }
            return Size.Empty;
        }

        private void btRemOrig_Click(object sender, EventArgs e)
        {
            if (pbOrig.Tag is string && 
                MessageBox.Show(this,
                string.Format(Resources.msgSure,pbOrig.Tag.ToString().Substring(pbOrig.Tag.ToString().LastIndexOf('\\')+1)),"Delete image",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (pbOrig.Image != null)
                {
                    pbOrig.Image.Dispose();
                    pbOrig.Image = null;
                }
                if (pbDup.Image != null)
                {
                    pbDup.Image.Dispose();
                    pbDup.Image = null;
                }
                comparisions.Remove(kvp.Key);
                kvp.Key.Dispose();
                

                btRemOrig.Visible = false;
                btRemDup.Visible = false;
                fillData();
                GC.Collect();
                try
                {
                    File.Delete(pbOrig.Tag.ToString());
                }
                catch { }
            }
        }

        private void btRemDup_Click(object sender, EventArgs e)
        {
            if (pbDup.Tag is string &&
                MessageBox.Show(this,
                string.Format(Resources.msgSure, pbDup.Tag.ToString().Substring(pbDup.Tag.ToString().LastIndexOf('\\') + 1)), "Delete image",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (pbDup.Image != null)
                {
                    pbDup.Image.Dispose();
                    pbDup.Image = null;
                }
                kvp.Value[lvDuplicates.SelectedIndices[0]].Dispose();
                kvp.Value.RemoveAt(lvDuplicates.SelectedIndices[0]);
                btRemDup.Visible = false;
                
                fillData();
                GC.Collect();
                try
                {
                    File.Delete(pbDup.Tag.ToString());
                }
                catch { }
            }
        }

        private void pbOrig_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null && pb.Tag is string)
            {
                Process.Start(pb.Tag.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://blogs.microsoft.co.il/blogs/tamir");
        }
        
    }
}