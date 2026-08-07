using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformLib;
using WinFormsApp1.Crud;
using WinFormsApp1.Model;
using static WinFormsApp1.Enums.EnumHelper;

namespace WinFormsApp1
{
    public partial class EditForm : Form
    {
        CompanyDetailsDto _edit;
        EasyCrud easyCrud = EasyCrudSingleton.Instance;
        Action _queryInfos = null;
        private ChromiumWebBrowser browser;
        public EditForm(Model.CompanyDetailsDto edit, Action queryInfos)
        {
            InitializeComponent();
            _edit = edit;
            _queryInfos = queryInfos;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.SetCommon(new FormSettings
            {
                isExitAsk = false
            });

            //combobox => dropdownlist 
            foreach (var item in this.Controls)
            {
                if (item is ComboBox comboBox)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }

            //datetimepicker
            dateTimePicker1.SetCommon(DateTimePickerExtentions.EnumEasyDateTimePicker.DateAndTime);
            dateTimePicker2.SetCommon(DateTimePickerExtentions.EnumEasyDateTimePicker.DateAndTime);

            //选项设置
            //渲染类型
            comboBox3.SetCommonWithEnum<EnumCompanyType>(isLazyLoading: false);

            //渲染阶段
            comboBox2.SetCommonWithEnum<EnumStage>(isLazyLoading: false);

            comboBox1.Items.AddRange(new List<string> { "未确定", "已拒绝", "已录用" }.ToArray());
            comboBox1.SelectedIndex = 0;


            //查库和渲染
            QueryAndShow();
        }

        private void QueryAndShow()
        {
            //查出它的实体
            var entity = easyCrud.GetFreeSql().Select<CompanyDetails>()
                                                .Where(x => x.ID == _edit.ID)
                                                .First();

            //渲染到控件中
            textBox1.Text = entity.CompanyName;
            comboBox1.SelectedIndex = QueryStatus(entity.Status);
            comboBox2.SelectedIndex = entity.Stage ?? 0;
            dateTimePicker1.Value = entity.FirstTime ?? DateTime.Now;
            dateTimePicker2.Value = entity.LatestTime ?? DateTime.Now;
            numericUpDown1.Value = entity.ExpectedValue ?? 0;
            numericUpDown2.Value = entity.Salary ?? 0;
            textBox2.Text = entity.HRName;
            textBox3.Text = entity.Position;
            textBox4.Text = entity.Address;
            comboBox3.SelectedIndex = entity.CompanyType ?? 0;
            textBox5.Text = entity.Channel;
            textBox6.Text = entity.Requirement;
            textBox7.Text = entity.Welfare;
            richTextBox1.Text = entity.Remark;

            //地图渲染
            var url = $"https://ditu.amap.com/ssr/search?query={entity.Address}";

            browser = new ChromiumWebBrowser(url);

            panel1.Controls.Add(browser);//在容器中渲染

        }

        /// <summary>
        /// 返回状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private int QueryStatus(int? status)
        {
            if (status == null)
            {
                return 0;
            }
            return status.HasValue ? status.Value + 1 : 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //将信息放进实体
            var newEntity = new CompanyDetails();
            newEntity.CompanyName = textBox1.Text;
            newEntity.Status = QueryStatusAdd(comboBox1.SelectedIndex);
            newEntity.Stage = comboBox2.SelectedIndex;
            newEntity.FirstTime = dateTimePicker1.Value;
            newEntity.LatestTime = dateTimePicker2.Value;
            newEntity.ExpectedValue = Convert.ToInt32(numericUpDown1.Value);
            newEntity.Salary = Convert.ToInt32(numericUpDown2.Value);
            newEntity.HRName = textBox2.Text;
            newEntity.Position = textBox3.Text;
            newEntity.Address = textBox4.Text;
            newEntity.CompanyType = comboBox3.SelectedIndex;
            newEntity.Channel = textBox5.Text;
            newEntity.Requirement = textBox6.Text;
            newEntity.Welfare = textBox7.Text;
            newEntity.Remark = richTextBox1.Text;
            newEntity.ID = _edit.ID;

            //入库
            var entity = easyCrud.GetFreeSql().Select<CompanyDetails>()
                                    .Where(x => x.ID == _edit.ID)
                                    .First();
            if (entity.Stage != newEntity.Stage)
            {
                //  如果阶段发生了变化，则更新阶段
                newEntity.LatestTime = DateTime.Now;
            }
            if (newEntity.Status != null)
            {
                newEntity.Stage = EnumStage.ResultsAlreadyAvailable.GetHashCode();
            }
            var flag = easyCrud.Update(newEntity);

            if (!flag)
            {
                this.PopUpTips("更新失败，请重试！");
            }

            if (checkBox1.Checked)
            {
                this.Close();
                _queryInfos();
            }
        }

        private int? QueryStatusAdd(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return null;
                case 1:
                    return 0;
                case 2:
                    return 1;
                default:
                    return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Reload(true);//CTRL+F5
        }
    }
}
