// ***********************************************************************
// Solution         : MyselfTools
// Project          : SFVersionTool
// File             : Form1.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace SFVersionTool
{
    public partial class Form1 : Form
    {
        private string _projectPath;
        private string _applicationTypeVersion;
        private int _choosedActorCount;

        private Dictionary<string, List<string>> _actorVersions = new Dictionary<string, List<string>>();
        private List<ActorInfo> _updatedActorInfos = new List<ActorInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_SelectProjectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = @"SF Server项目文件|*.sfproj"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                string projectPath = fileName.Replace(fileName.Split('\\').Last(), "");
                projectPath = projectPath.Remove(projectPath.Length - 1, 1);

                _projectPath = txb_ProjectPath.Text = projectPath;

                InitDataGridView();
            }
        }

        private void InitDataGridView()
        {
            _choosedActorCount = 0;

            // 清空 DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            Dictionary<string, string> actorInfos = GetActors();

            lbl_ActorCount.Text = actorInfos.Keys.Count.ToString();

            _actorVersions = new Dictionary<string, List<string>>();

            List<ActorInfo> views = new List<ActorInfo>();

            // 应用程序版本号
            string mainPath = $"{_projectPath}\\ApplicationPackageRoot\\ApplicationManifest.xml";
            _applicationTypeVersion = txb_ApplicationTypeVersion.Text = GetApplicationTypeVersion(mainPath);

            foreach (KeyValuePair<string, string> keyValuePair in actorInfos)
            {
                string path = _projectPath.Replace(_projectPath.Split('\\').Last(), "") + $"{keyValuePair.Key}\\PackageRoot\\ServiceManifest.xml";

                XmlDocument xml = LoadXmlDoc(path);

                string pkgVersion = xml.SelectSingleNode("//*[local-name()='ServiceManifest']")?.Attributes?["Version"].Value;
                string codeVersion = xml.SelectSingleNode("//*[local-name()='ServiceManifest']/*[local-name()='CodePackage']")?.Attributes?["Version"].Value;
                string configVersion = xml.SelectSingleNode("//*[local-name()='ServiceManifest']/*[local-name()='ConfigPackage']")?.Attributes?["Version"].Value;

                _actorVersions.Add(keyValuePair.Key, new List<string> { pkgVersion, codeVersion, configVersion });

                views.Add(new ActorInfo
                {
                    ActorName = keyValuePair.Key,
                    CurrentPkgVersion = pkgVersion,
                    CurrentCodeVersion = codeVersion,
                    CurrentConfigVersion = configVersion,
                    NextPkgVersion = pkgVersion,
                    NextCodeVersion = codeVersion,
                    NextConfigVersion = configVersion
                });
            }

            views = views.OrderBy(t => t.ActorName).ToList();

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn { HeaderText = "选择", Name = "check" };

            dataGridView1.Columns.Insert(0, checkBoxColumn);
            dataGridView1.DataSource = views;

            Type type = dataGridView1.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(dataGridView1, true, null);
            }

            dataGridView1.Columns[0].Width = 50; // 选择框列
            dataGridView1.Columns[1].Width = 500;
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.Columns[6].DefaultCellStyle.ForeColor = Color.Blue;
            dataGridView1.Columns[7].DefaultCellStyle.ForeColor = Color.Blue;

            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.DefaultCellStyle.Font = new Font("UTF-8", 10);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }

        private static string GetApplicationTypeVersion(string filePath)
        {
            XmlDocument xml = LoadXmlDoc(filePath);
            string applicationTypeVersion = xml.SelectSingleNode("//*[local-name()='ApplicationManifest']")?.Attributes?["ApplicationTypeVersion"].Value;
            return applicationTypeVersion;
        }

        private static XmlDocument LoadXmlDoc(string filePath)
        {
            XmlDocument xml = new XmlDocument();

            //XmlTextReader reader = new XmlTextReader(filePath) { Namespaces = false };
            //xml.Load(reader);
            //reader.Close();

            xml.Load(filePath);

            return xml;
        }

        private Dictionary<string, string> GetActors()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_projectPath);
            FileInfo[] files = directoryInfo.GetFiles();

            FileInfo file = files.FirstOrDefault(t => t.Extension == ".sfproj");
            if (file == null)
            {
                MessageBox.Show(@"无法找到 .sfproj 文件", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return null;
            }

            XmlDocument xml = LoadXmlDoc(file.FullName);

            XmlNodeList nodes = xml.SelectNodes("//*[local-name()='ItemGroup']/*[local-name()='ProjectReference']");

            Dictionary<string, string> actorInfos = new Dictionary<string, string>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string actorPath = node.Attributes?["Include"].Value;
                    if (string.IsNullOrEmpty(actorPath))
                    {
                        continue;
                    }

                    string[] s = actorPath.Split('\\');
                    actorInfos.Add(s[1], actorPath);
                }
            }

            return actorInfos;
        }

        /// <summary>
        ///     预览
        /// </summary>
        private void btn_Preview_Click(object sender, EventArgs e)
        {
            //if (_applicationTypeVersion == txb_ApplicationTypeVersion.Text)
            //{
            //    MessageBox.Show(@"未修改应用程序版本号，无法预览", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            //    return;
            //}

            List<ActorInfo> updatedActorInfos = new List<ActorInfo>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["check"];
                bool flag = Convert.ToBoolean(checkBoxCell.Value);
                if (flag)
                {
                    for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LightSkyBlue;
                    }

                    if (chb_PkgVersion.Checked)
                    {
                        dataGridView1.Rows[i].Cells["NextPkgVersion"].Value = txb_ApplicationTypeVersion.Text;
                        dataGridView1.Rows[i].Cells["NextPkgVersion"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["NextPkgVersion"].Value = _actorVersions[dataGridView1.Rows[i].Cells["ActorName"].Value.ToString()][0];
                        dataGridView1.Rows[i].Cells["NextPkgVersion"].Style.ForeColor = DefaultForeColor;
                    }

                    if (chb_CodeVersion.Checked)
                    {
                        dataGridView1.Rows[i].Cells["NextCodeVersion"].Value = txb_ApplicationTypeVersion.Text;
                        dataGridView1.Rows[i].Cells["NextCodeVersion"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["NextCodeVersion"].Value = _actorVersions[dataGridView1.Rows[i].Cells["ActorName"].Value.ToString()][1];
                        dataGridView1.Rows[i].Cells["NextCodeVersion"].Style.ForeColor = DefaultForeColor;
                    }

                    if (chb_ConfigVersion.Checked)
                    {
                        dataGridView1.Rows[i].Cells["NextConfigVersion"].Value = txb_ApplicationTypeVersion.Text;
                        dataGridView1.Rows[i].Cells["NextConfigVersion"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["NextConfigVersion"].Value = _actorVersions[dataGridView1.Rows[i].Cells["ActorName"].Value.ToString()][2];
                        dataGridView1.Rows[i].Cells["NextConfigVersion"].Style.ForeColor = DefaultForeColor;
                    }

                    ActorInfo actorInfo = new ActorInfo
                    {
                        ActorName = dataGridView1.Rows[i].Cells["ActorName"].Value.ToString(),
                        CurrentPkgVersion = dataGridView1.Rows[i].Cells["CurrentPkgVersion"].Value.ToString(),
                        CurrentCodeVersion = dataGridView1.Rows[i].Cells["CurrentCodeVersion"].Value.ToString(),
                        CurrentConfigVersion = dataGridView1.Rows[i].Cells["CurrentConfigVersion"].Value.ToString(),
                        NextPkgVersion = dataGridView1.Rows[i].Cells["NextPkgVersion"].Value.ToString(),
                        NextCodeVersion = dataGridView1.Rows[i].Cells["NextCodeVersion"].Value.ToString(),
                        NextConfigVersion = dataGridView1.Rows[i].Cells["NextConfigVersion"].Value.ToString()
                    };

                    updatedActorInfos.Add(actorInfo);
                }
            }

            _updatedActorInfos = updatedActorInfos;
            _applicationTypeVersion = txb_ApplicationTypeVersion.Text;
        }

        /// <summary>
        ///     保存
        /// </summary>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (_updatedActorInfos == null || _updatedActorInfos.Count == 0)
            {
                MessageBox.Show(@"请先预览后再保存", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                return;
            }

            // 保存 ApplicationManifest.xml

            // 应用程序版本号
            string mainPath = $"{_projectPath}\\ApplicationPackageRoot\\ApplicationManifest.xml";
            XmlDocument xml = LoadXmlDoc(mainPath);

            XmlElement mainXmlElement = (XmlElement)xml.SelectSingleNode("//*[local-name()='ApplicationManifest']");
            mainXmlElement?.SetAttribute("ApplicationTypeVersion", _applicationTypeVersion);

            foreach (ActorInfo updatedActorInfo in _updatedActorInfos)
            {
                XmlElement xmlElement = (XmlElement)xml.SelectSingleNode(
                    $"//*[local-name()='ApplicationManifest']/*[local-name()='ServiceManifestImport']/*[local-name()='ServiceManifestRef' and @ServiceManifestName='{updatedActorInfo.ActorName}Pkg']");
                xmlElement?.SetAttribute("ServiceManifestVersion", updatedActorInfo.NextPkgVersion);

                string path = _projectPath.Replace(_projectPath.Split('\\').Last(), "") + $"{updatedActorInfo.ActorName}\\PackageRoot\\ServiceManifest.xml";
                XmlDocument actorXml = LoadXmlDoc(path);

                XmlElement actorElement = (XmlElement)actorXml.SelectSingleNode("//*[local-name()='ServiceManifest']");
                actorElement?.SetAttribute("Version", updatedActorInfo.NextPkgVersion);

                XmlElement codeElement = (XmlElement)actorXml.SelectSingleNode("//*[local-name()='ServiceManifest']/*[local-name()='CodePackage']");
                codeElement?.SetAttribute("Version", updatedActorInfo.NextCodeVersion);

                XmlElement configElement = (XmlElement)actorXml.SelectSingleNode("//*[local-name()='ServiceManifest']/*[local-name()='ConfigPackage']");
                configElement?.SetAttribute("Version", updatedActorInfo.NextConfigVersion);

                // 保存 Actor ServiceManifest.xml

                actorXml.Save(path);
            }

            xml.Save(mainPath);

            _updatedActorInfos = null;
            lbl_ChoosedActorCount.Text = "0";

            InitDataGridView();
        }

        /// <summary>
        ///     单选
        /// </summary>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["check"];
                bool flag = Convert.ToBoolean(checkBoxCell.Value);
                checkBoxCell.Value = !flag;

                if (!flag)
                {
                    _choosedActorCount++;

                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _choosedActorCount--;

                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = DefaultBackColor;
                }

                lbl_ChoosedActorCount.Text = _choosedActorCount.ToString();
            }
        }

        /// <summary>
        ///     全选
        /// </summary>
        private void chb_chooseAll_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["check"];
                bool flag = Convert.ToBoolean(chb_chooseAll.Checked);
                checkBoxCell.Value = flag;

                if (flag)
                {
                    _choosedActorCount = Convert.ToInt32(lbl_ActorCount.Text);

                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    _choosedActorCount = 0;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = DefaultBackColor;
                }

                lbl_ChoosedActorCount.Text = _choosedActorCount.ToString();
            }
        }

        /// <summary>
        ///     撤销
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_projectPath))
            {
                ResetForm();
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_projectPath))
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            lbl_ChoosedActorCount.Text = "0";
            chb_chooseAll.Checked = false;
            _updatedActorInfos = new List<ActorInfo>();
            InitDataGridView();
        }
    }
}