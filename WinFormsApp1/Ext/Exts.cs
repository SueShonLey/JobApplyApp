using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformLib;
using static WinformLib.CustomizeFormsExtentions;

namespace WinFormsApp1.Ext
{
    public static class Exts
    {
        /// <summary>
        /// 自定义窗体方法
        /// </summary>
        public static Dictionary<string, string> SetCustomizeFormsNew(this Form form, CustomizeFormInput inputDto)
        {
            // 基础验证
            var labels = inputDto.inputs.Select(x => x.Label).ToList();
            var labels_Distinct = labels.Distinct().ToList();
            if (labels.Count != labels_Distinct.Count)
            {
                throw new Exception("Label不允许重名！");
            }

            var inputs = inputDto.inputs;
            // 创建自定义窗体
            Form inputForm = new Form
            {
                Text = inputDto.FormTitle,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Icon = form.Icon //继承原来的窗体的图标
            };
            if (true)
            {
                inputForm.BackColor = form.BackColor;
                inputForm.BackgroundImage = form.BackgroundImage;
                inputForm.BackgroundImageLayout = form.BackgroundImageLayout;
            }

            // 常量声明，统一维护
            int defualtHeight = 30;
            int currentY = 15;
            int FormPadding = 15;//窗体内边距
            int defualtControlWidth = 250;//默认控件宽度（textbox,combobox）
            int horizontalSpacing = 10; // 单选框/复选框横向间距（解决文本重叠）
            //int btnTopMargin = 10; // 按钮与上方控件的间距（解决高度不足）

            // 存储各控件组
            Dictionary<string, List<CheckBox>> checkBoxGroups = new Dictionary<string, List<CheckBox>>();
            Dictionary<string, List<RadioButton>> radioButtonGroups = new Dictionary<string, List<RadioButton>>();
            Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
            Dictionary<string, NumericUpDown> NumberBoxes = new Dictionary<string, NumericUpDown>();
            Dictionary<string, ComboBox> comboBoxes = new Dictionary<string, ComboBox>();

            // 算出最大标签宽度
            var maxLabel = inputs.MaxBy(x => x.Label.Length)?.Label;
            int maxLabelWidth = GetLabelWith(maxLabel);
            var LabelAndValuePadding = inputDto.LabelLocationX > 0 ? inputDto.LabelLocationX : maxLabelWidth + 10;

            //算出控件最大宽度
            int maxControlWidth = defualtControlWidth;

            //添加控件
            foreach (var input in inputs)
            {
                // 添加标签
                Label label = new Label
                {
                    Text = input.Label,
                    Location = new System.Drawing.Point(FormPadding, currentY + 2),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };
                inputForm.Controls.Add(label);

                switch (input.FormControlType)
                {
                    case FormControlType.InputBox:
                        TextBox textBox = new TextBox
                        {
                            Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY),
                            Width = defualtControlWidth,
                            Height = defualtHeight,
                            //Enabled = input.Enable
                        };
                        inputForm.Controls.Add(textBox);
                        textBoxes[input.Label] = textBox;
                        if (!string.IsNullOrEmpty(input.DefaultValue))
                        {
                            textBox.Text = input.DefaultValue;
                        }
                        break;
                    case FormControlType.NumberBox:
                        NumericUpDown number = new NumericUpDown
                        {
                            Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY),
                            Width = defualtControlWidth,
                            Height = defualtHeight,
                            //Enabled = input.Enable
                        };
                        inputForm.Controls.Add(number);
                        NumberBoxes[input.Label] = number;
                        if (!string.IsNullOrEmpty(input.DefaultValue))
                        {
                            number.Value = decimal.TryParse(input.DefaultValue, out var outDecimal) ? outDecimal : 0;
                        }
                        break;

                    case FormControlType.DropDown:
                        ComboBox comboBox = new ComboBox
                        {
                            Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY),
                            Width = defualtControlWidth,
                            Height = defualtHeight,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                           // Enabled = input.Enable
                        };
                        comboBox.Items.AddRange(input.Value.ToArray());
                        inputForm.Controls.Add(comboBox);
                        comboBoxes[input.Label] = comboBox;
                        if (!string.IsNullOrEmpty(input.DefaultValue))
                        {
                            comboBox.SetCommonItems(input.DefaultValue);
                        }
                        else
                        {
                            if (comboBox.Items != null && comboBox.Items.Count > 0)
                            {
                                comboBox.SelectedIndex = 0;
                            }
                        }
                        break;

                    case FormControlType.RadioButton:
                        #region 修复核心：为每组单选框创建独立Panel容器
                        int radioButtonX = 0;
                        // 创建Panel，承载当前组所有单选框，解决全局互斥问题
                        Panel radioPanel = new Panel
                        {
                            Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY),
                            Height = defualtHeight,
                            Width = 0,// 宽度动态计算
                        };
                        radioButtonGroups[input.Label] = new List<RadioButton>();
                        foreach (var value in input.Value)
                        {
                            RadioButton radioButton = new RadioButton
                            {
                                Text = value,
                                Location = new System.Drawing.Point(radioButtonX, 0), // Panel内X轴从0开始
                                AutoSize = true,
                                Height = defualtHeight,
                                //Enabled = input.Enable,
                            };
                            radioButtonGroups[input.Label].Add(radioButton);
                            if (!string.IsNullOrEmpty(input.DefaultValue) && value == input.DefaultValue)
                            {
                                radioButton.Checked = true;
                            }
                            radioPanel.Controls.Add(radioButton);
                            // 累加Panel内控件位置，加间距
                            radioButtonX += radioButton.Width + horizontalSpacing;
                        }
                        // 设置Panel的实际宽度，刚好包裹所有单选框
                        radioPanel.Width = radioButtonX + horizontalSpacing;
                        inputForm.Controls.Add(radioPanel);
                        // 更新最大控件宽度，适配窗体
                        maxControlWidth = Math.Max(maxControlWidth, radioPanel.Width);
                        #endregion
                        break;

                    case FormControlType.CheckBox:
                        #region 优化：为每组复选框创建独立Panel容器，解决横向溢出
                        int checkButtonX = 0;
                        var defaultValues = !string.IsNullOrEmpty(input.DefaultValue) ? input.DefaultValue.Split(',').ToList() : new List<string>();
                        // 创建Panel，承载当前组所有复选框
                        FlowLayoutPanel checkPanel = new FlowLayoutPanel
                        {
                            Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY),
                            Height = defualtHeight*2,
                            Width = 400,
                            WrapContents = true,
                            AutoSize = false,
                            BackColor = Color.Transparent
                        };
                        checkBoxGroups[input.Label] = new List<CheckBox>();
                        foreach (var value in input.Value)
                        {
                            CheckBox checkBox = new CheckBox
                            {
                                Text = value,
                                Location = new System.Drawing.Point(checkButtonX, 0), // Panel内X轴从0开始
                                AutoSize = true,
                                Height = defualtHeight,
                                //Margin = new Padding(0, 0, horizontalSpacing, 4)
                                //Enabled = input.Enable
                            };
                            checkBoxGroups[input.Label].Add(checkBox);
                            if (defaultValues.Contains(value))
                            {
                                checkBox.Checked = true;
                            }
                            checkPanel.Controls.Add(checkBox);
                            // 累加Panel内控件位置，加间距
                            checkButtonX += checkBox.Width + horizontalSpacing;
                        }
                        // 设置Panel的实际宽度，刚好包裹所有复选框
                        //checkPanel.Width = checkButtonX + horizontalSpacing;
                        inputForm.Controls.Add(checkPanel);
                        // 更新最大控件宽度，适配窗体
                        maxControlWidth = Math.Max(maxControlWidth, checkPanel.Width);
                        #endregion
                        break;
                }
                // 保底最小值，防止控件垂直重叠
                currentY += Math.Max(input.VertiPadding, defualtHeight + 5);
            }

            // 重新计算窗体宽度，确保所有控件都能完整显示
            var FormWidth = FormPadding * 3 + LabelAndValuePadding + maxControlWidth;
            inputForm.Width = FormWidth;

            // 设置所有控件同宽度
            SetControlWidthRecursive(inputForm, maxControlWidth - horizontalSpacing);

            // 创建一个任务以便返回选定的值
            var tcs = new Dictionary<string, string>();

            // 创建确认按钮
            Button btnOK = new Button
            {
                Text = "确定",
                Height = defualtHeight,
                Width = 80,
                BackColor = Color.White,
                Location = new System.Drawing.Point(FormPadding + LabelAndValuePadding, currentY)
            };
            btnOK.Click += (s, args) =>
            {
                tcs.Clear();
                // 获取输入框的值
                foreach (var textBox in textBoxes)
                {
                    tcs[textBox.Key] = textBox.Value.Text;
                }

                // 获取数字框的值
                foreach (var number in NumberBoxes)
                {
                    tcs[number.Key] = number.Value.Text;
                }

                // 获取下拉框的值
                foreach (var comboBox in comboBoxes)
                {
                    tcs[comboBox.Key] = comboBox.Value.SelectedItem?.ToString() ?? string.Empty;
                }

                // 获取复选框的值
                foreach (var group in checkBoxGroups)
                {
                    List<string> selectedCheckBoxValues = group.Value.Where(cb => cb.Checked).Select(cb => cb.Text).ToList();
                    tcs[group.Key] = string.Join(",", selectedCheckBoxValues);
                }

                // 获取单选框的值【完全正常：组内单选、组间独立】
                foreach (var group in radioButtonGroups)
                {
                    string selectedRadioButtonValue = group.Value.FirstOrDefault(rb => rb.Checked)?.Text ?? string.Empty;
                    tcs[group.Key] = selectedRadioButtonValue;
                }

                inputForm.Close();
            };

            // 创建取消按钮
            Button btnCancel = new Button
            {
                Text = "取消",
                Height = defualtHeight,
                Width = 80,
                BackColor = Color.White,
                Location = new System.Drawing.Point(btnOK.Right + 80, currentY)
            };

            btnCancel.Click += (s, args) =>
            {
                tcs.Clear();
                inputForm.Close();
            };

            // 将按钮添加到窗体
            inputForm.Controls.Add(btnOK);
            inputForm.Controls.Add(btnCancel);

            // 核心修复：设置客户区高度，按钮完整显示，无遮挡
            inputForm.ClientSize = new Size(inputForm.ClientSize.Width, btnOK.Bottom + FormPadding);

            // 用户自定义大小
            if (inputDto.Size.Width > 0 && inputDto.Size.Height > 0)
            {
                inputForm.Width = inputDto.Size.Width;
                inputForm.Height = inputDto.Size.Height;
            }
            //
            var a = inputForm.Controls[0];

            //调整按钮位置
            var btnOk_X = (inputForm.ClientSize.Width - 80 * 3) / 2;
            var btnCancel_X = btnOk_X + 80 * 2;
            btnOK.Location = new Point(btnOk_X, currentY);
            btnCancel.Location = new Point(btnCancel_X, currentY);

            //if (inputDto.funsForm != null)
            //{
            //    inputDto.funsForm(inputForm);
            //}

            // 显示窗体并等待用户操作
            inputForm.ShowDialog();

            // 返回用户输入的结果
            return tcs;
        }

        /// <summary>
        /// 递归设置指定类型控件宽度，跳过AutoSize控件、Panel容器、按钮
        /// </summary>
        private static void SetControlWidthRecursive(Control container, int targetWidth)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox || c is NumericUpDown || c is ComboBox)
                {
                    //这类控件AutoSize默认false，可以修改Width
                    c.Width = targetWidth;
                }
                //继续递归进入Panel等容器
                if (c.HasChildren)
                {
                    SetControlWidthRecursive(c, targetWidth);
                }
            }
        }

        private static int GetLabelWith(string? maxLabel)
        {
            Label lbl = new Label();
            lbl.Text = maxLabel;
            //lbl.Font = new System.Drawing.Font("宋体",15) ;//如果有字体格式还要设置好，可以用默认的
            Graphics g = Graphics.FromHwnd(lbl.Handle);
            SizeF size = g.MeasureString(lbl.Text, lbl.Font);//获取大小
            g.Dispose();
            return Convert.ToInt32(size.Width);
        }


        public static void SetCommonNew(this ComboBox comboBox, List<string> dataList, bool isSelectFirst = true, bool isLazyLoading = true, bool isSuggest = true)
        {
            ComboBox comboBox2 = comboBox;
            List<string> dataList2 = dataList;
            if (isLazyLoading)
            {
                Task.Run(delegate
                {
                    comboBox2.BeginInvoke(delegate
                    {
                        SetData(comboBox2, dataList2, isSelectFirst, isSuggest);
                    });
                });
            }
            else
            {
                SetData(comboBox2, dataList2, isSelectFirst, isSuggest);
            }
        }
        private static void SetData(ComboBox comboBox, List<string> dataList, bool isSelectFirst, bool isSuggest)
        {
            // 如果没有启用延迟加载，直接在 UI 线程更新 ComboBox
            comboBox.Items.Clear();
            comboBox.Items.AddRange(dataList.ToArray());

            // 设置自动完成功能（如果启用）
            if (isSuggest)
            {
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            // 设置默认选中第一个项（如果启用）
            if (isSelectFirst && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;  // 默认选中第一个项
            }
        }

    }
}
