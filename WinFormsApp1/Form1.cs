using System.Windows.Forms;
using WinformLib;
using WinFormsApp1.Crud;
using WinFormsApp1.Enums;
using WinFormsApp1.Model;
using static WinFormsApp1.Enums.EnumHelper;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<EnumDetails<EnumStage>> enumStageList = EnumHelper.GetEnumDetails<EnumStage>();
        List<EnumDetails<EnumCompanyType>> enumTypeList = EnumHelper.GetEnumDetails<EnumCompanyType>();
        EasyCrud easyCrud = EasyCrudSingleton.Instance;
        const string channelurl = "./Doc/channel.txt";
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetCommon(new FormSettings
            {
                TitleText = "应聘分析App"
            });
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd");

            ////渲染类型
            //comboBox1.SetCommonWithEnum<EnumCompanyType>();

            ////渲染阶段
            //comboBox2.SetCommonWithEnum<EnumStage>();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


            //默认查询
            QueryInfos();

            //开始时间记录
            label6.Text = "普通时间档";
            label6.ForeColor = Color.DarkGray;
            TimerExtentions.RegisterTimer("TimeNow", 1000, () =>
            {
                label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //黄金时间档
                var nowWeek = DateTime.Now.DayOfWeek;
                if (!new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Sunday, DayOfWeek.Saturday }.Contains(nowWeek))
                {
                    bool b1 = DateTime.Now.TimeOfDay >= new TimeSpan(10, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(11, 0, 0);
                    bool b2 = DateTime.Now.TimeOfDay >= new TimeSpan(14, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(15, 30, 0) && nowWeek != DayOfWeek.Friday;
                    if (b1 || b2)
                    {
                        // 黄金时间档逻辑
                        label6.Text = "黄金时间档";
                        label6.ForeColor = Color.Red;
                    }
                    else
                    {
                        label6.Text = "普通时间档";
                        label6.ForeColor = Color.DarkGray;
                    }
                }
            }, true);
        }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var stages = enumStageList.Select(x => x.Description).ToList();
            var types = enumTypeList.Select(x => x.Description).ToList();
            List<string> channelList = File.ReadAllText(channelurl).Split(',').ToList();
            var result = this.SetCustomizeForms(new CustomizeFormsExtentions.CustomizeFormInput
            {
                FormTitle = "新增公司",
                inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                {
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="公司名称",
                    },
                    //new CustomizeFormsExtentions.CustomizeValueInput
                    //{
                    //    Label ="投递阶段",
                    //    FormControlType =  CustomizeFormsExtentions.FormControlType.DropDown,
                    //    Value = stages
                    //},
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="期望值",
                        DefaultValue = "60"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="薪资",
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="HR姓名",
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="公司地址",
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="应聘岗位",
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="投递渠道",
                        FormControlType =  CustomizeFormsExtentions.FormControlType.DropDown,
                        Value = channelList
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="公司类型",
                        FormControlType =  CustomizeFormsExtentions.FormControlType.DropDown,
                        Value = types,
                        DefaultValue = types[1]
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="公司要求"
                    },
                      new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="公司福利"
                    },
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="备注"
                    },
                }
            });
            if (result.Count != 0)//用户填写了内容
            {
                if (!int.TryParse(result["期望值"] ?? "aa", out _) || !int.TryParse(result["薪资"] ?? "aa", out _))
                {
                    this.PopUpTips("期望值或薪资必须为数字");
                    return;
                }
                var entity = new CompanyDetails
                {
                    CompanyName = result["公司名称"],
                    Status = null,
                    Stage = EnumStage.Delivered.GetHashCode(),
                    FirstTime = DateTime.Now,
                    LatestTime = DateTime.Now,
                    ExpectedValue = int.Parse(result["期望值"]),
                    Salary = int.Parse(result["薪资"]),
                    HRName = result["HR姓名"],
                    Address = result["公司地址"],
                    Position = result["应聘岗位"],
                    Channel = result["投递渠道"],
                    CompanyType = enumTypeList?.FirstOrDefault(x => x.Description.Equals(result["公司类型"]))?.Index ?? 0,
                    Requirement = result["公司要求"],
                    Welfare = result["公司福利"],
                    Remark = result["备注"]

                };

                var flag = easyCrud.Insert(entity);
                if (flag)
                {
                    QueryInfos();
                }
                else
                {
                    this.PopUpTips("新增失败");
                }
            }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void QueryInfos()
        {
            // 获取查询的信息
            var keyWord = textBox1.Text.Trim();
            var type = comboBox1.SelectedIndex;
            var stage = comboBox2.SelectedIndex;
            var feedback7Days = checkBox1.Checked;
            var noreject = checkBox2.Checked;

            //查库
            var fsql = easyCrud.GetFreeSql();
            var sourcelist = fsql.Select<CompanyDetails>()
                            .WhereIf(!string.IsNullOrEmpty(keyWord), x => x.CompanyName.Contains(keyWord) || x.Position.Contains(keyWord) || x.HRName.Contains(keyWord) || x.Remark.Contains(keyWord) || x.Channel.Contains(keyWord))
                            .WhereIf(type != 0, x => x.CompanyType == type - 1)
                            .WhereIf(stage != 0, x => x.Stage == stage - 1)
                            .WhereIf(feedback7Days, x => x.LatestTime >= DateTime.Now.AddDays(-7))
                            .WhereIf(noreject, x => x.Status == 1 || x.Status == null)
                            .OrderByDescending(x => x.LatestTime)
                            .ToList();

            // 转化为Dto
            var destictlist = new List<CompanyDetailsDto>();
            foreach (var item in sourcelist)
            {
                var entity = new CompanyDetailsDto();
                entity.ID = item.ID;//自增主键
                entity.CompanyName = item.CompanyName;//公司名称
                entity.Status = GetCompanyStatus(item.Status);
                entity.Stage = enumStageList.FirstOrDefault(x => item.Stage == x.Index)?.Description ?? "未知";//求职阶段
                entity.FirstTime = item.FirstTime;//初次投递时间
                entity.LatestTime = item.LatestTime?.ToString("yyyy-MM-dd HH:mm") ?? "未知";//最新反馈时间
                entity.ExpectedValue = item.ExpectedValue;//期望薪资
                entity.Salary = item.Salary;
                entity.HRName = item.HRName;//HR姓名
                entity.Address = item.Address;//公司地址
                entity.Position = item.Position;//应聘岗位
                entity.Channel = item.Channel;//投递渠道
                entity.CompanyType = enumTypeList.FirstOrDefault(x => item.CompanyType == x.Index)?.Description ?? "未知";//公司类型
                entity.Requirement = item.Requirement;//岗位要求
                entity.Welfare = item.Welfare;//公司福利
                entity.Remark = item.Remark;//备注
                entity.CreateTime = item.CreateTime;//记录创建时间
                entity.UpdateTime = item.UpdateTime;//记录更新时间
                destictlist.Add(entity);
            }

            //  渲染表格
            dataGridView1.SetCommonWithUI(new DataGridViewExtentions.DataDisplayEntity<CompanyDetailsDto>
            {
                ButtonList = new List<string> { "编辑", "删除", "拒绝", "推进流程" },
                headtextList = new List<(System.Linq.Expressions.Expression<Func<CompanyDetailsDto, object>> fields, string name, int width)>
                 {
                      (x => x.CompanyName, "公司名称", 200),
                      (x => x.Stage, "投递阶段", 100),
                      (x => x.Status, "状态", 100),
                      (x => x.Salary, "薪资", 80),
                      (x => x.ExpectedValue, "期望值", 70),
                      (x => x.Position, "岗位", 150),
                      (x => x.CompanyType, "公司类型", 90),
                      (x => x.LatestTime, "最新反馈时间", 150),
                 },
                DataList = destictlist,
                changeLineFuns = (feilds, values, cells) =>
                {
                    if (feilds.Equals("Status"))
                    {
                        if (values.Equals("已拒绝"))
                        {
                            cells.ForeColor = Color.Red;
                        }

                        else if (values.Equals("已录用"))
                        {
                            cells.ForeColor = Color.Green;
                        }

                    }

                    if (feilds.Equals("Stage"))
                    {
                        if (values.Equals("评估中"))
                        {
                            cells.ForeColor = Color.Orange;
                        }
                        else if (values.Equals("已面试"))
                        {
                            cells.ForeColor = Color.Blue;
                        }
                    }
                }
            });

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                if (item.HeaderText.Equals("操作"))
                {
                    item.Width = 80;
                }
            }

            //统计
            var stateownedenterpriseCount = sourcelist.Count(x => x.CompanyType == EnumCompanyType.StateownedEnterprise.GetHashCode());
            var privateEnterpriseCount = sourcelist.Count(x => x.CompanyType == EnumCompanyType.PrivateEnterprise.GetHashCode());
            var foreignEnterpriseCount = sourcelist.Count(x => x.CompanyType == EnumCompanyType.ForeignCompany.GetHashCode());
            var passedCount = sourcelist.Count(x => x.Status == 1);
            var rejectedCount = sourcelist.Count(x => x.Status == 0);
            var evaluatingCount = sourcelist.Count(x => x.Status == null);
            var todayCount = sourcelist.Count(x => x.FirstTime.Value.Date == DateTime.Now.Date);

            label2.Text = @$"国企：{stateownedenterpriseCount}

私企：{privateEnterpriseCount}

外企：{foreignEnterpriseCount}

通过：{passedCount}

拒绝：{rejectedCount}

未有结果：{evaluatingCount}

今日投递：{todayCount}

当前总数：{sourcelist.Count}";
        }

        private string GetCompanyStatus(int? status)
        {
            if (status == null)
            {
                return "流程进行中";
            }
            if (status == 0)
            {
                return "已拒绝";
            }
            return "已录用";
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            QueryInfos();
        }

        //点击按钮
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var delete = dataGridView1.GetCommonByButton<CompanyDetailsDto>("删除", e);
            var reject = dataGridView1.GetCommonByButton<CompanyDetailsDto>("拒绝", e);
            var push = dataGridView1.GetCommonByButton<CompanyDetailsDto>("推进流程", e);
            var edit = dataGridView1.GetCommonByButton<CompanyDetailsDto>("编辑", e);

            if (delete != null)
            {
                var flag = easyCrud.DeleteByExp<CompanyDetails>(x => x.ID == delete.ID);
                if (flag)
                {
                    QueryInfos();
                }
                else
                {
                    this.PopUpTips("删除失败");
                }
            }

            if (reject != null)
            {
                var id = reject.ID;
                var flag = easyCrud.UpdateSetWhere<CompanyDetails>(x => x.Status, 0, x => x.ID == id);
                var flag2 = easyCrud.UpdateSetWhere<CompanyDetails>(x => x.Stage, EnumStage.ResultsAlreadyAvailable.GetHashCode(), x => x.ID == id);
                if (flag)
                {
                    QueryInfos();
                }
                else
                {
                    this.PopUpTips("更新失败");
                }
            }

            if (push != null)
            {

                var id = push.ID;
                var oldEntity = easyCrud.FirstOrDefault<CompanyDetails>(x => x.ID == id);

                var oldStageName = enumStageList.FirstOrDefault(x => x.Index == oldEntity.Stage)?.Description ?? "未知";
                var newStageName = enumStageList.FirstOrDefault(x => x.Index == oldEntity.Stage + 1)?.Description ?? "未知";
                oldEntity.Stage = oldEntity.Stage + 1 ?? 0;
                oldEntity.LatestTime = DateTime.Now;
                if (push.Stage.Equals("已有结果"))
                {
                    this.PopUpTips("更新失败，该流程不能被推进，已有结果！");
                    return;
                }
                if (!this.PopUpDialog($"您确定要将流程从【{oldStageName}】推进到【{newStageName}】吗？"))
                {
                    return;
                }
                var flag = easyCrud.Update<CompanyDetails>(oldEntity);

                // 从已面试出发推进，询问结果
                if (oldEntity.Stage == EnumStage.ResultsAlreadyAvailable.GetHashCode())
                {
                    while (true)
                    {
                        var result = this.SetCustomizeForms(new CustomizeFormsExtentions.CustomizeFormInput
                        {
                            FormTitle = "更新结果",
                            inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                            {
                                new CustomizeFormsExtentions.CustomizeValueInput
                                {
                                    Label ="结果",
                                    FormControlType =  CustomizeFormsExtentions.FormControlType.DropDown,
                                    Value = new List<string> { "已录用", "已拒绝" }
                                },
                            }
                        });
                        if (result.Count == 0)//用户没有填写内容
                        {
                            continue;
                        }

                        var statusStr = result["结果"];
                        int status = statusStr == "已录用" ? 1 : 0;
                        oldEntity.Status = status;
                        oldEntity.LatestTime = DateTime.Now;
                        flag = easyCrud.Update<CompanyDetails>(oldEntity);
                        if (result.Count != 0)//用户填写了内容
                        {
                            break;
                        }
                    }
                }


                if (flag)
                {
                    QueryInfos();
                }
                else
                {
                    this.PopUpTips("更新失败");
                }
            }

            if (edit != null)
            {
                EditForm form = new EditForm(edit, QueryInfos);
                form.Show();
                QueryInfos();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.PopUpTipsRight("请在稍后弹出的文件中维护招聘渠道，英文逗号分隔，记得保存，否则无效！");
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileExtentions.OpenFile(System.IO.Path.Combine(currentDirectory, "Doc", "channel.txt"));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryInfos();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var password = DateTime.Now.ToString("MMdd");
            if (this.PopUpDialog($"请确认是否清空所有数据，清空后无法恢复！\n请输入清空密码:{password}"))
            {
                var res = this.SetCustomizeForms(new CustomizeFormsExtentions.CustomizeFormInput
                {
                    FormTitle = $"请输入清空密码:{password}",
                    inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                {
                    new CustomizeFormsExtentions.CustomizeValueInput
                    {
                        Label ="密码:",
                    },
                }
                });
                if (res.Count != 0)
                {
                    if (res["密码:"] == password)
                    {
                        var flag = easyCrud.DeleteByExp<CompanyDetails>(x => true);
                        if (flag)
                        {
                            QueryInfos();
                            this.PopUpTips("清空成功");
                        }
                        else
                        {
                            this.PopUpTips("清空失败");
                        }
                    }
                    else
                    {
                        this.PopUpTips("密码错误");
                    }
                }
            }

        }

        /// <summary>
        /// 黄金时间档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_Click(object sender, EventArgs e)
        {
            this.PopUpTips("以下时间为黄金时间档:\n" +
                "1.周二-周五 上午10:00-11:00\n" +
                "2.周二-周四 下午14:00-15:30");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.OpenLink("https://www.tianyancha.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.OpenLink("https://wenshu.court.gov.cn");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.OpenLink("https://zxgk.court.gov.cn/zhzxgk/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel4.OpenLink("https://ditu.amap.com/ssr/search");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel5.OpenLink("https://www.zhaopin.com/");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel6.OpenLink("https://www.liepin.com/");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel7.OpenLink("https://www.51job.com/");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel8.OpenLink("https://zhiq.zhaopin.com/gongsidianping/0-0-");
        }
    }
}