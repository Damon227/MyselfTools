using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DamonHelper.Helper;
using DamonHelper.Models;
using DamonHelper.Settings;
using Dapper;
using Newtonsoft.Json;

namespace DamonHelper
{
    public partial class PenaltySettingForm : Form
    {
        public PenaltySettingForm()
        {
            InitializeComponent();
        }

        private void PenaltySettingForm_Load(object sender, EventArgs e)
        {
            List<SimpleTenancy> result = DataHelper.GetTenancies();

            cmb_Tenancies.DataSource = result;
            cmb_Tenancies.DisplayMember = "TenancyName";
            cmb_Tenancies.ValueMember = "TenancyId";

            rbtn_Tenancy.Checked = true;
            cmb_Apartments.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void rbtn_Tenancy_Click(object sender, EventArgs e)
        {
            rbtn_Tenancy.Checked = true;
            rbtn_Apartment.Checked = false;

            cmb_Apartments.DataSource = null;
        }

        private void rbtn_Apartment_MouseCaptureChanged(object sender, EventArgs e)
        {
            rbtn_Tenancy.Checked = false;
            rbtn_Apartment.Checked = true;

            InitApartments();
        }

        private void InitApartments()
        {
            string tenancyId = cmb_Tenancies.SelectedValue.ToString();
            List<SimpleApartment> apartments = DataHelper.GetApartmentsByTenancyId(tenancyId);

            cmb_Apartments.DataSource = apartments;
            cmb_Apartments.DisplayMember = "ApartmentName";
            cmb_Apartments.ValueMember = "ApartmentId";
        }

        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            string tenancyId = cmb_Tenancies.SelectedValue.ToString();
            string apartmentId = cmb_Apartments.SelectedValue?.ToString();
            string type = rbtn_Tenancy.Checked ? "Tenancy" : "Apartment";
            string rate = txb_Radio.Text.Trim();

            if (!int.TryParse(rate, out int rateInt) || rateInt < 0)
            {
                MessageBox.Show("费率只能是大于或等于0的数字");
                return;
            }

            List<PenaltySetting> penaltySettings = DataHelper.GetPenaltySettings();

            string logicId = type == "Tenancy" ? tenancyId : apartmentId;
           
            PenaltySetting setting = penaltySettings.Find(t => t.LogicId == logicId);
            if (setting != null)
            {
                if (setting.Rate == rateInt)
                {
                    MessageBox.Show("已存在相同配置");
                    return;
                }

                // 更新 rate
                setting.Rate = rateInt;

                penaltySettings.RemoveAll(t => t.LogicId == logicId);
                penaltySettings.Add(setting);
            }
            else
            {
                PenaltySetting newSetting = new PenaltySetting
                {
                    Type = type,
                    LogicId = logicId,
                    Rate = rateInt
                };
                penaltySettings.Add(newSetting);
            }

            // 保存
            string penaltyConfigString = JsonConvert.SerializeObject(penaltySettings);
            SavePenaltyConfig(penaltyConfigString);
        }

        private static void SavePenaltyConfig(string penaltyConfigString)
        {
            string sql = $"update dbo.[KC.Fengniaowu.Talos.Schedules] set PenaltyConfig = '{penaltyConfigString}' where ScheduleId = '5337DCC80FEE45D39EAB76ACD2BF20A8'";

            using (SqlConnection connection = SqlConnectionExtensions.GetConnection())
            {
                connection.Open();

                int row = connection.Execute(sql);
                if (row > 0)
                {
                    MessageBox.Show("成功");
                }
                else
                {
                    MessageBox.Show("失败");
                }
            }
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string tenancyId = cmb_Tenancies.SelectedValue.ToString();
            string apartmentId = cmb_Apartments.SelectedValue?.ToString();
            string type = rbtn_Tenancy.Checked ? "Tenancy" : "Apartment";

            List<PenaltySetting> penaltySettings = DataHelper.GetPenaltySettings();

            string logicId = type == "Tenancy" ? tenancyId : apartmentId;

            PenaltySetting setting = penaltySettings.Find(t => t.LogicId == logicId);
            if (setting == null)
            {
                MessageBox.Show("删除失败，配置不存在");
            }

            penaltySettings.Remove(setting);

            // 保存
            string penaltyConfigString = JsonConvert.SerializeObject(penaltySettings);
            SavePenaltyConfig(penaltyConfigString);
        }

        private void cmb_Tenancies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtn_Apartment.Checked)
            {
                InitApartments();
            }
        }
    }
}
