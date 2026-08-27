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
using WinFormsApp1.Ext;
using WinFormsApp1.Model;

namespace WinFormsApp1
{
    public partial class BlackListForm : Form
    {
        public BlackListForm()
        {
            InitializeComponent();
        }

        private void BlackListForm_Load(object sender, EventArgs e)
        {
            //设置透明
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;

            //查询
            QueryInfo();
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        private void QueryInfo()
        {
            var queryText = textBox1.Text.Trim().Replace("股份有限公司", "").Replace("有限公司","").Replace("公司", "");
            var list = EasyCrudSingleton.Instance.GetList<BlackListInfo>(x => x.Name.Contains(queryText));
            if (!list.Any())
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                return;
            }
            dataGridView1.SetCommonWithCell<BlackListInfo>(new DataGridViewExtentions.DataDisplayEntityCell<BlackListInfo>
            {
                DataList = list,
                HeadtextList = new List<(System.Linq.Expressions.Expression<Func<BlackListInfo, object>> Feild, string TitileName, int Width)>
                {
                    //(x => x.ID, "ID", 50),
                    (x => x.Name, "公司名称", 200),
                    (x => x.AvoidCount, "避雷指数", 100),
                    (x => x.AvoidReason, "避雷原因", 300),
                    (x => x.Remark, "备注", 150),
                },
                ButtonList = new List<(string ButtonName, string TitileName, int Width)>
                {
                    ("编辑", "编辑", 50),
                    ("删除", "删除", 50),
                }
            });
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            QueryInfo();
        }

        List<string> avoiddata = new List<string> { "被仲裁", "被执行", "拖工资", "风评差", "无双休", "加班多", "流动大", "无社保", "同事差", "不尊重劳动者" };
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var dict = this.SetCustomizeFormsNew(new CustomizeFormsExtentions.CustomizeFormInput
            {
                FormTitle = "新增黑名单公司",
                inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                {
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "公司名称",
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "避雷原因",
                        FormControlType = CustomizeFormsExtentions.FormControlType.CheckBox,
                        Value = avoiddata,
                        VertiPadding = 80
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "相关链接"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "更多备注",
                    },
                }
            });
            if (dict.Count != 0)
            {
                var reasonList = dict["避雷原因"].Split(',').OrderBy(x => x.Length).ToList();
                var entity = new BlackListInfo();
                entity.Name = dict["公司名称"];
                entity.AvoidReason = string.Join('、', reasonList);
                entity.AvoidCount = reasonList.Count;
                entity.RelatedLink = dict["相关链接"];
                entity.Remark = dict["更多备注"];
                EasyCrudSingleton.Instance.Insert(entity);
                QueryInfo();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var easy = EasyCrudSingleton.Instance;
            var entity1 = dataGridView1.GetCommonByButton<BlackListInfo>("编辑", e);
            var entity2 = dataGridView1.GetCommonByButton<BlackListInfo>("删除", e);
            var entity =    entity1 ??  entity2;
            var queryEntity = new BlackListInfo();
            if (entity != null) 
            {
                queryEntity = easy.FirstOrDefault<BlackListInfo>(x => x.ID == entity.ID);
            }
            if (entity1 != null)
            {
                var dict = this.SetCustomizeFormsNew(new CustomizeFormsExtentions.CustomizeFormInput
                {
                    FormTitle = "新增黑名单公司",
                    inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                {
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "公司名称",
                        DefaultValue = queryEntity.Name
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "避雷原因",
                        FormControlType = CustomizeFormsExtentions.FormControlType.CheckBox,
                        Value = avoiddata,
                        DefaultValue = queryEntity.AvoidReason.Replace("、",","),
                        VertiPadding = 80
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "相关链接",
                        DefaultValue = queryEntity.RelatedLink
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label = "更多备注",
                        DefaultValue = queryEntity.Remark
                    },
                }
                });
                if (dict.Count != 0)
                {
                    var reasonList = dict["避雷原因"].Split(',').OrderBy(x => x.Length).ToList();
                    var new_entity = new BlackListInfo();
                    new_entity.ID = queryEntity.ID;
                    new_entity.Name = dict["公司名称"];
                    new_entity.AvoidReason = string.Join('、', reasonList);
                    new_entity.AvoidCount = reasonList.Count;
                    new_entity.RelatedLink = dict["相关链接"];
                    new_entity.Remark = dict["更多备注"];
                    EasyCrudSingleton.Instance.Update(new_entity);
                }
                QueryInfo();
            }
            else if (entity2 != null)
            {
                if (this.PopUpDialog($"您确定要将【{queryEntity.Name}】移除黑名单吗?"))
                {
                    easy.DeleteByExp<BlackListInfo>(x => x.ID == queryEntity.ID);
                }
                QueryInfo();
            }

        }
    }
}
