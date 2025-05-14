using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ucEchocardioscopy : UserControl
    {
        private string[,] exam_data ={ { "Heart", "правосформированное леворасположенное" }, { "UpDwnVeins", "впадают в правое предсердие" }, { "RightAtriumPlace", "расположено справа" }, { "RightAtriumSized", "не увеличено" }, { "CoronarySinus", "впадает в правое предсердие, не расширен" }, { "LaunghVeins", "впадают в левое предсердие, без патологии" }, { "LeftAtriumPlace", "расположено слева" }, 
                                     { "LeftAtriumSized", "не увеличено" }, { "TripiscularValveStvork", "тонкие, подвижные" }, { "TripiscularValvePapilMusciHord", "без особенностей" }, { "MitralValveStvor", "тонкие, подвижные" }, { "TripiscularValvePapilMusciHord", "без особенностей" }, { "TripiscularValveReg", "нет" },{ "MitralValvePapilMusciHord", "без особенностей" }, { "MitralValveReg", "нет" }, { "MidVentrFenceMove", "нормокинетическое" }, { "Aort", "отходит от левого желудочка, расположена сзади и справа от легочной артерии" }, 
                                     { "LaunghArtria", "отходит от правого желудочка, раcположена спереди и слева от аорты" }, { "LaunghValveStvor", "тонкие, подвижные" }, { "LaunghValveReg", "нет" }, { "PolostPericard", "без особенностей" }, {"RightVentr","расположен спереди и справа, сообщается через трикуспидальный клапан с правым предсердием"},{"MidAtriumFence", "интактна"},{"AortValveStvor", "тонкие, подвижные"},{"AortValveReg", "нет"},{"MidVentrFence", "интактна"},{"MidVentrFenceMove", "нормокинетическое"},{"LeftVentr","расположен сзади и слева, сообщается через митральный клапан с левым предсердием"}};
        private bool correction = false;
        private double bsa = 0.0;

        public ucEchocardioscopy()
        {
            InitializeComponent();
            tbMitralValveEdgeVelocity.TextChanged += new EventHandler(MitralValveEdgeActiveProportion);
            tbMitralValveActiveVelocity.TextChanged += new EventHandler(MitralValveEdgeActiveProportion);
            tbVeSept.TextChanged += new EventHandler(MitralVeAvgCalc);
            tbVeLater.TextChanged += new EventHandler(MitralVeAvgCalc);
            tbVeAvg.TextChanged += new EventHandler(MitralVEVeAvgProportion);

            BindDataSource();

            FormCheckedChanged(this, EventArgs.Empty);
        }

        private void BindDataSource()
        {           
            //if (male)
            //{
            //    lIKDORef.Text = "(34-74 мл/м2)";
            //    lIKSORef.Text = "(11-31 мл/м2)";
            //    lKDRRef.Text = "(4,2-5,8 cм)";
            //    lIKDRRef.Text = "(2,2-3 см/м2)";
            //    lIKSRRef.Text = "(1,3-2,1 см/м2)";
            //    label207.Text = "(52-72) %";
            //    lTMGPRef.Text = "(0,6-1 см)";
            //    lTZSRef.Text = "(0,6-1 см)";
            //    lASINValsalvRef.Text = "(2,8-4 см)";
            //    lISinValsalveRef.Text = "(1,3-2,1 см/м2)";
            //    lAVORef.Text = "(2,2-3,8 см)";
            //    lIAVORef.Text = "(1,1-1,9 см/м2)";
            //    lAFKRef.Text = "(2-3,2 см)";
            //    lIAFKRef.Text = "(1,1-1,5 см/м2)";
            //    lILPVolumeRef.Text = "(16-34 мл/м2)";
            //    label353.Text = "(88-224 г)";
            //    label133.Text = "(49-115 г/м2)";
            //    lRightAtriumSquareRef.Text = "(до 16 см2)";
            //    lRightAtriumVolumeIndexRef.Text = "(до 32 мл/м2)";
            //}
            //else
            //{
            //    lIKDORef.Text = "(29-61 мл/м2)";
            //    lIKSORef.Text = "(8-24 мл/м2)";
            //    lKDRRef.Text = "(3,8-5,2 cм)";
            //    lIKDRRef.Text = "(2,3-3,1 см/м2)";
            //    lIKSRRef.Text = "(1,3-2,1 см/м2)";
            //    label207.Text = "(54-74) %";
            //    lTMGPRef.Text = "(0,6-0,9 см)";
            //    lTZSRef.Text = "(0,6-0,9 см)";
            //    lASINValsalvRef.Text = "(2,4-3,6 см)";
            //    lISinValsalveRef.Text = "(1,4-2,2 см/м2)";
            //    lAVORef.Text = "(1,9-3,5 см)";
            //    lIAVORef.Text = "(1-2,2 см/м2)";
            //    lAFKRef.Text = "(1,9-2,7 см)";
            //    lIAFKRef.Text = "(1,1-1,5 см/м2)";
            //    lILPVolumeRef.Text = "(16-34 мл/м2)";
            //    label353.Text = "(67-162 г)";
            //    label133.Text = "(43-95 г/м2)";
            //    lRightAtriumSquareRef.Text = "(до 15 см2)";
            //    lRightAtriumVolumeIndexRef.Text = "(до 27 мл/м2)";
            //}
        }       

        
        private void MitralValveEdgeActiveProportion(object sender, EventArgs e)
        {
            double active, edge;
            if (double.TryParse(tbMitralValveEdgeVelocity.Text.Trim().Replace('.', ','), out edge) && double.TryParse(tbMitralValveActiveVelocity.Text.Trim().Replace('.', ','), out active) && active != 0)
                tbMitralValveEdgeActiveProportion.Text = (edge / active).ToString("N2");
            else
                tbMitralValveEdgeActiveProportion.Text = "";

            MitralVEVeAvgProportion(null, null);
        }

        private void MitralVEVeAvgProportion(object sender, EventArgs e)
        {
            double VE, VeAvg;
            if (double.TryParse(tbMitralValveEdgeVelocity.Text.Trim().Replace('.', ','), out VE) && double.TryParse(tbVeAvg.Text.Trim().Replace('.', ','), out VeAvg) && VeAvg != 0)
                tbVEVeAvgProportion.Text = ((VE * 100) / VeAvg).ToString("N1");
            else
                tbVEVeAvgProportion.Text = "";
        }

        private void MitralVeAvgCalc(object sender, EventArgs e)
        {
            double VeSept, VeLater;
            if (double.TryParse(tbVeSept.Text.Trim().Replace('.', ','), out VeSept) && double.TryParse(tbVeLater.Text.Trim().Replace('.', ','), out VeLater) && VeLater != 0)
                tbVeAvg.Text = VeSept > 0 ? ((VeSept + VeLater) / 2).ToString("N2") : "";
            else
                tbVeAvg.Text = "";
        }       

        private ToolStripMenuItem CreateToolStripMenuItem(string text, object tag)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Tag = tag;
            item.Image = null;
            item.Click += new EventHandler(cmsMenu_OnClick);
            return item;
        }

        #region Events

        private void FormCheckedChanged(object sender, EventArgs e)
        {
            if (rbAdult.Checked)
            {
                panelAdultFull.Visible = bnCreate.Visible = true;
                tabControl.Visible = panelChildShort.Visible = panelAdultShort.Visible = false;
            }
            if (rbAdultShort.Checked)
            {
                panelAdultShort.Visible = bnCreate.Visible = true;
                tabControl.Visible = panelChildShort.Visible = panelAdultFull.Visible = false;
            }
            if (rbChild.Checked)
            {
                tabControl.Visible = true;
                panelAdultFull.Visible = bnCreate.Visible = panelChildShort.Visible = panelAdultShort.Visible = false;
            }
            if (rbChildShort.Checked)
            {
                panelChildShort.Visible = bnCreate.Visible = true;
                tabControl.Visible = panelAdultFull.Visible = panelAdultShort.Visible = false;
            }
        }

        private void IntegerTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.KeyChar = char.MinValue;
        }

        private void FloatTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (e.KeyChar == 'б' || e.KeyChar == 'Б')
                e.KeyChar = ',';
            if (e.KeyChar == 'ю' || e.KeyChar == 'Ю')
                e.KeyChar = '.';
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != ',')
                e.KeyChar = char.MinValue;
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if ((sender as TextBox).Text.Contains(".") || (sender as TextBox).Text.Contains(","))
                    e.KeyChar = char.MinValue;
                if ((sender as TextBox).Text.Length == 0 || ((sender as TextBox).Text.Length > 0 && (sender as TextBox).SelectionStart == 0))
                    e.KeyChar = char.MinValue;
            }
        }

        private void TextBoxLeave(object sender, EventArgs e)
        {
            /*IsCorrectValue(sender);
            if (rules.ContainsKey(sender as TextBox) && (value < rules[sender as TextBox].X || value > rules[sender as TextBox].Y))
            {
                if (MessageBox.Show(this, "Введенное значение выходит за границы допустимых значения (" + rules[sender as TextBox].X.ToString("N2") + "-" + rules[sender as TextBox].Y.ToString("N2") + "). Продолжить ввод?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    (sender as TextBox).Focus();
                    return;
                }
            }*/
        }

        private void chbBackWall_CheckedChanged(object sender, EventArgs e)
        {
            lLevelBackWall.Visible = tbBackWallLevel.Visible = lSantimetersBackWall.Visible = chbBackWall.Checked;
            if (!chbBackWall.Checked)
                tbBackWallLevel.Text = "";
        }

        private void chbFrontWall_CheckedChanged(object sender, EventArgs e)
        {
            lLevelFrontWall.Visible = tbFrontWallLevel.Visible = lSantimetersFrontWall.Visible = chbFrontWall.Checked;
            if (!chbFrontWall.Checked)
                tbFrontWallLevel.Text = "";
        }

        private void chbTop_CheckedChanged(object sender, EventArgs e)
        {
            lLevelTop.Visible = tbTopLevel.Visible = lSantimetersTop.Visible = chbTop.Checked;
            if (!chbTop.Checked)
                tbTopLevel.Text = "";
        }

        private void llLocalContractile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkPrevResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {           

        }

        private void cmsMenu_OnClick(object sender, EventArgs e)
        {

        }

        private void tbRightAtriumSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox)
            {
                string text = (sender as TextBox).Text;
                if (e.KeyChar == 'б' || e.KeyChar == 'Б')
                    e.KeyChar = ',';
                if (e.KeyChar == 'ю' || e.KeyChar == 'Ю')
                    e.KeyChar = ',';
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '*')
                    e.KeyChar = char.MinValue;
                if (e.KeyChar == '*') if (!(sender as TextBox).Text.Contains("*")) e.KeyChar = '*'; else e.KeyChar = char.MinValue;
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    if ((sender as TextBox).Text.Contains(".") || (sender as TextBox).Text.Contains(","))
                        e.KeyChar = char.MinValue;
                    if ((sender as TextBox).Text.Length == 0 || ((sender as TextBox).Text.Length > 0 && (sender as TextBox).SelectionStart == 0))
                        e.KeyChar = char.MinValue;
                }
            }
        }

        private void tbCommonАtrium_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {                
                if ((sender as TextBox).Text.Trim() != "")
                {
                    foreach (Control tb in gbP4.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                            // (tb as TextBox).Text = "";
                        }
                    }
                    foreach (Control tb in gbP7.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                            // (tb as TextBox).Text = "";
                        }
                    }
                }
                else
                {
                    bool flag = true;
                    foreach (Control tb in gbP3.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP4.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                        foreach (Control tb in gbP7.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbRightAtriumPlace_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {
                if ((sender as TextBox).Text.Trim() != "")
                {
                    foreach (Control tb in gbP3.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                            // (tb as TextBox).Text = "";
                        }
                    }
                }
                else
                {
                    bool flag = true;
                    foreach (Control tb in gbP4.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                            if ((tb as TextBox).Tag.ToString() == "") flag = false;
                        }
                    }
                    foreach (Control tb in gbP7.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP3.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbCommonAntivicValve_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {
                if ((sender as TextBox).Text.Trim() != "")
                {
                    foreach (Control tb in gbP10.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }
                    foreach (Control tb in gbP12.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }
                }
                else
                {
                    bool flag = true;
                    foreach (Control tb in gbP9.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP10.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }

                        }
                        foreach (Control tb in gbP12.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbTripiscularValve_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {

                if ((sender as TextBox).Text.Trim() != "")
                {
                    //////// p 10
                    if ((sender as TextBox).Tag.ToString() == "TripiscularValve")
                    {
                        foreach (Control g10 in gbP10.Controls)
                        {
                            if (g10 is TextBox)

                                if ((g10 as TextBox).Tag.ToString() != "TripiscularValve") (g10 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP10")
                            foreach (Control g10 in gbP10.Controls)
                            {
                                if (g10 is TextBox)

                                    if ((g10 as TextBox).Tag.ToString() == "TripiscularValve") (g10 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////

                    //////// p 12
                    if ((sender as TextBox).Tag.ToString() == "MitralValve")
                    {
                        foreach (Control g12 in gbP12.Controls)
                        {
                            if (g12 is TextBox)

                                if ((g12 as TextBox).Tag.ToString() != "MitralValve") (g12 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP12")
                            foreach (Control g12 in gbP12.Controls)
                            {
                                if (g12 is TextBox)

                                    if ((g12 as TextBox).Tag.ToString() == "MitralValve") (g12 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////


                    foreach (Control tb in gbP9.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }

                }

                else
                {

                    //////// p 10
                    if ((sender as TextBox).Tag.ToString() == "TripiscularValve")
                    {
                        foreach (Control g10 in gbP10.Controls)
                        {
                            if (g10 is TextBox)
                                if ((g10 as TextBox).Tag.ToString() != "TripiscularValve") (g10 as TextBox).ReadOnly = false;
                        }

                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP10")
                        {
                            bool fl = true;
                            foreach (Control g10 in gbP10.Controls)
                            {
                                if (g10 is TextBox)

                                    if ((g10 as TextBox).Tag.ToString() != "TripiscularValve" && (g10 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbTripiscularValve.ReadOnly = false;
                        }
                    }
                    /////////////

                    //////// p 12
                    if ((sender as TextBox).Tag.ToString() == "MitralValve")
                    {
                        foreach (Control g12 in gbP12.Controls)
                        {
                            if (g12 is TextBox)
                                if ((g12 as TextBox).Tag.ToString() != "MitralValve") (g12 as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP12")
                        {
                            bool fl = true;
                            foreach (Control g12 in gbP12.Controls)
                            {
                                if (g12 is TextBox)

                                    if ((g12 as TextBox).Tag.ToString() != "MitralValve" && (g12 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbMitralValve.ReadOnly = false;
                        }

                    }

                    /////////////
                    bool flag = true;
                    foreach (Control tb in gbP10.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }

                    }
                    foreach (Control tb in gbP12.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }

                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP9.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbDubLeftVentr_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {

                if ((sender as TextBox).Text.Trim() != "")
                {                   
                    foreach (Control tb in gbP11.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }

                    }
                    foreach (Control tb in gbP13.Controls)
                    {
                        if (tb is TextBox)
                        {

                            (tb as TextBox).ReadOnly = true;

                        }

                    }
                    foreach (Control tb in gbP15.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }

                    }
                }

                else
                {                   
                    bool flag = true;
                    foreach (Control tb in gbP14.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }

                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP11.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }

                        }
                        foreach (Control tb in gbP13.Controls)
                        {
                            if (tb is TextBox)
                            {
                                if ((tb as TextBox).Tag.ToString() != "LeftVentrKDOInd")
                                    (tb as TextBox).ReadOnly = false;
                            }

                        }
                        foreach (Control tb in gbP15.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }

                        }
                    }


                }

            }
        }

        private void tbRightVentr_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {

                if ((sender as TextBox).Text.Trim() != "")
                {
                    foreach (Control tb in gbP14.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }                    
                }
                else
                {             
                    bool flag = true;
                    foreach (Control tb in gbP11.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    foreach (Control tb in gbP13.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "")
                            {
                                flag = false;
                            }
                        }
                    }
                    foreach (Control tb in gbP15.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP14.Controls)
                        {
                            if (tb is TextBox)
                            {
                                if ((tb as TextBox).Tag.ToString() != "DubLeftVentrKDOInd")
                                    (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbStvolValve_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {
                if ((sender as TextBox).Text.Trim() != "")
                {
                    //////// p 19
                    if ((sender as TextBox).Tag.ToString() == "AortValve")
                    {
                        foreach (Control g19 in gbP19.Controls)
                        {
                            if (g19 is TextBox)

                                if ((g19 as TextBox).Tag.ToString() != "AortValve") (g19 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP19")
                            foreach (Control g19 in gbP19.Controls)
                            {
                                if (g19 is TextBox)

                                    if ((g19 as TextBox).Tag.ToString() == "AortValve") (g19 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////


                    //////// p 22
                    if ((sender as TextBox).Tag.ToString() == "LaunghValve")
                    {
                        foreach (Control g22 in gbP22.Controls)
                        {
                            if (g22 is TextBox)

                                if ((g22 as TextBox).Tag.ToString() != "LaunghValve") (g22 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP22")
                            foreach (Control g22 in gbP22.Controls)
                            {
                                if (g22 is TextBox)

                                    if ((g22 as TextBox).Tag.ToString() == "LaunghValve") (g22 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////

                    foreach (Control tb in gbP19.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }
                    foreach (Control tb in gbP22.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }
                }
                else
                {

                    //////// p 19
                    if ((sender as TextBox).Tag.ToString() == "AortValve")
                    {
                        foreach (Control g19 in gbP19.Controls)
                        {
                            if (g19 is TextBox)
                                if ((g19 as TextBox).Tag.ToString() != "AortValve") (g19 as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP19")
                        {
                            bool fl = true;
                            foreach (Control g19 in gbP19.Controls)
                            {
                                if (g19 is TextBox)

                                    if ((g19 as TextBox).Tag.ToString() != "AortValve" && (g19 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbAortValve.ReadOnly = false;
                        }
                    }
                    /////////////
                    //////// p 22
                    if ((sender as TextBox).Tag.ToString() == "LaunghValve")
                    {
                        foreach (Control g22 in gbP22.Controls)
                        {
                            if (g22 is TextBox)
                                if ((g22 as TextBox).Tag.ToString() != "LaunghValve") (g22 as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP22")
                        {
                            bool fl = true;
                            foreach (Control g22 in gbP22.Controls)
                            {
                                if (g22 is TextBox)

                                    if ((g22 as TextBox).Tag.ToString() != "LaunghValve" && (g22 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbLaunghValve.ReadOnly = false;
                        }
                    }
                    /////////////

                    bool flag = true;

                    foreach (Control tb in gbP17.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP19.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                        foreach (Control tb in gbP22.Controls)
                        {
                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void tbAortValve_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).ReadOnly != true)
            {
                if ((sender as TextBox).Text.Trim() != "")
                {

                    //////// p 19
                    if ((sender as TextBox).Tag.ToString() == "AortValve")
                    {
                        foreach (Control g19 in gbP19.Controls)
                        {
                            if (g19 is TextBox)

                                if ((g19 as TextBox).Tag.ToString() != "AortValve") (g19 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP19")
                            foreach (Control g19 in gbP19.Controls)
                            {
                                if (g19 is TextBox)

                                    if ((g19 as TextBox).Tag.ToString() == "AortValve") (g19 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////


                    //////// p 22
                    if ((sender as TextBox).Tag.ToString() == "LaunghValve")
                    {
                        foreach (Control g22 in gbP22.Controls)
                        {
                            if (g22 is TextBox)

                                if ((g22 as TextBox).Tag.ToString() != "LaunghValve") (g22 as TextBox).ReadOnly = true;
                        }
                    }
                    else
                    {
                        if ((sender as TextBox).Parent.Name == "gbP22")
                            foreach (Control g22 in gbP22.Controls)
                            {
                                if (g22 is TextBox)

                                    if ((g22 as TextBox).Tag.ToString() == "LaunghValve") (g22 as TextBox).ReadOnly = true;
                            }
                    }
                    /////////////
                    foreach (Control tb in gbP17.Controls)
                    {
                        if (tb is TextBox)
                        {
                            (tb as TextBox).ReadOnly = true;
                        }
                    }
                }
                else
                {

                    //////// p 19
                    if ((sender as TextBox).Tag.ToString() == "AortValve")
                    {
                        foreach (Control g19 in gbP19.Controls)
                        {
                            if (g19 is TextBox)
                                if ((g19 as TextBox).Tag.ToString() != "AortValve") (g19 as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {

                        if ((sender as TextBox).Parent.Name == "gbP19")
                        {
                            bool fl = true;
                            foreach (Control g19 in gbP19.Controls)
                            {
                                if (g19 is TextBox)

                                    if ((g19 as TextBox).Tag.ToString() != "AortValve" && (g19 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbAortValve.ReadOnly = false;
                        }

                    }
                    /////////////

                    //////// p 22
                    if ((sender as TextBox).Tag.ToString() == "LaunghValve")
                    {
                        foreach (Control g22 in gbP22.Controls)
                        {
                            if (g22 is TextBox)
                                if ((g22 as TextBox).Tag.ToString() != "LaunghValve") (g22 as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {

                        if ((sender as TextBox).Parent.Name == "gbP22")
                        {
                            bool fl = true;
                            foreach (Control g22 in gbP22.Controls)
                            {
                                if (g22 is TextBox)

                                    if ((g22 as TextBox).Tag.ToString() != "LaunghValve" && (g22 as TextBox).Text != "") fl = false;
                            }
                            if (fl) tbLaunghValve.ReadOnly = false;
                        }

                    }
                    /////////////
                    bool flag = true;
                    foreach (Control tb in gbP19.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    foreach (Control tb in gbP22.Controls)
                    {
                        if (tb is TextBox)
                        {
                            if ((tb as TextBox).Text != "") flag = false;
                        }
                    }
                    if (flag)
                    {
                        foreach (Control tb in gbP17.Controls)
                        {

                            if (tb is TextBox)
                            {
                                (tb as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }
        }

        private void Filter1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox)
            {
                string text = (sender as TextBox).Text;
                if (e.KeyChar == '\'' || e.KeyChar == '\"' || e.KeyChar == '\\' || e.KeyChar == '/' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '%' || e.KeyChar == '@' || e.KeyChar == '*' || e.KeyChar == '&')
                    e.KeyChar = char.MinValue;
            }
        }

        private void Filter1_KeyPress_11_12_13(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox)
            {
                string text = (sender as TextBox).Text;
                if (e.KeyChar == '\'' || e.KeyChar == '\"' || e.KeyChar == '\\' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '@' || e.KeyChar == '*' || e.KeyChar == '&')
                    e.KeyChar = char.MinValue;
            }
        }

        private void tbLeftVentr_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        #endregion

        private void bnCreate_Click(object sender, EventArgs e)
        {

        }

        private void cbMitralValveRegurgitation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbMitralValveRegurgitation.SelectedItem.ToString(), out int res) && res > 1)
                label185.Visible = label343.Visible = label352.Visible = tbSMR.Visible = tbVc.Visible = tbRpisa.Visible = label180.Visible = label161.Visible = label351.Visible = lMVCRef.Visible = label462.Visible = label451.Visible = tbEROA.Visible = lEROAUnit.Visible = tbMitralRegVolume.Visible = lMitralRegVolumeUnit.Visible = true;
            else
            {
                label185.Visible = label343.Visible = label352.Visible = tbSMR.Visible = tbVc.Visible = tbRpisa.Visible = label180.Visible = label161.Visible = label351.Visible = lMVCRef.Visible = label462.Visible = label451.Visible = tbEROA.Visible = lEROAUnit.Visible = tbMitralRegVolume.Visible = lMitralRegVolumeUnit.Visible = false;
                tbSMR.Text = tbVc.Text = tbRpisa.Text = tbMitralRegVolume.Text = tbEROA.Text = string.Empty;
            }
        }

        private void tbLeftVentricleTerminalDiastolicVolume_Leave(object sender, EventArgs e)
        {

        }

        private void tbLeftVentricleTerminalSistolicVolume_Leave(object sender, EventArgs e)
        {

        }

        private void tbLinearDimensionsDiastolicVolume_Leave(object sender, EventArgs e)
        {

        }

        private void tbLinearDimensionsSistolicVolume_Leave(object sender, EventArgs e)
        {

        }

        private void tbLeftAtriumVolume_Leave(object sender, EventArgs e)
        {

        }

        private void tbAortaFibrousRing_Leave(object sender, EventArgs e)
        {

        }

        private void tbAortaSinusVaalsalve_Leave(object sender, EventArgs e)
        {

        }

        private void tbAortaAscending_Leave(object sender, EventArgs e)
        {
        }

        private void chbBackWall_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            lLevelBackWall_AdultShort.Visible = tbBackWallLevel_AdultShort.Visible = lSantimetersBackWall_AdultShort.Visible = chbBackWall_AdultShort.Checked;
            if (!chbBackWall_AdultShort.Checked)
                tbBackWallLevel_AdultShort.Text = "";
        }

        private void chbFrontWall_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            lLevelFrontWall_AdultShort.Visible = tbFrontWallLevel_AdultShort.Visible = lSantimetersFrontWall_AdultShort.Visible = chbFrontWall_AdultShort.Checked;
            if (!chbFrontWall_AdultShort.Checked)
                tbFrontWallLevel_AdultShort.Text = "";
        }

        private void chbTop_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            lLevelTop_AdultShort.Visible = tbTopLevel_AdultShort.Visible = lSantimetersTop_AdultShort.Visible = chbTop_AdultShort.Checked;
            if (!chbTop_AdultShort.Checked)
                tbTopLevel_AdultShort.Text = "";
        }

        private void cbMitralValveRegurgitation_AdultShort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbMitralValveRegurgitation_AdultShort.SelectedItem.ToString(), out int res) && res > 1)
                label442.Visible = label438.Visible = label422.Visible = tbSMR_AdultShort.Visible = tbVc_AdultShort.Visible = tbRpisa_AdultShort.Visible = label386.Visible = label440.Visible = label432.Visible = label415.Visible = true;
            else
            {
                label442.Visible = label438.Visible = label422.Visible = tbSMR_AdultShort.Visible = tbVc_AdultShort.Visible = tbRpisa_AdultShort.Visible = label386.Visible = label440.Visible = label432.Visible = label415.Visible = false;
                tbSMR_AdultShort.Text = string.Empty;
                tbVc_AdultShort.Text = string.Empty;
                tbRpisa_AdultShort.Text = string.Empty;
            }
        }

        private void chkSideWall_CheckedChanged(object sender, EventArgs e)
        {
            label425.Visible = tbSideWallLevel.Visible = label430.Visible = chkSideWall.Checked;
            if (!chkSideWall.Checked)
                tbSideWallLevel.Text = "";
        }

        private void chkRightVentr_CheckedChanged(object sender, EventArgs e)
        {
            label420.Visible = tbRightVentrLevel.Visible = label423.Visible = chkRightVentr.Checked;
            if (!chkRightVentr.Checked)
                tbRightVentrLevel.Text = "";
        }

        private void chkRightAtrial_CheckedChanged(object sender, EventArgs e)
        {
            label385.Visible = tbRightAtrialLevel.Visible = label390.Visible = chkRightAtrial.Checked;
            if (!chkRightAtrial.Checked)
                tbRightAtrialLevel.Text = "";
        }

        private void chkVascular_CheckedChanged(object sender, EventArgs e)
        {
            label381.Visible = tbVascularLevel.Visible = label384.Visible = chkVascular.Checked;
            if (!chkVascular.Checked)
                tbVascularLevel.Text = "";
        }

        private void chkSideWall_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            label355.Visible = tbSideWallLevel_AdultShort.Visible = label356.Visible = chkSideWall_AdultShort.Checked;
            if (!chkSideWall_AdultShort.Checked)
                tbSideWallLevel_AdultShort.Text = "";
        }

        private void chkRightVentr_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            label358.Visible = tbRightVentrLevel_AdultShort.Visible = label361.Visible = chkRightVentr_AdultShort.Checked;
            if (!chkRightVentr_AdultShort.Checked)
                tbRightVentrLevel_AdultShort.Text = "";
        }

        private void chkRightAtrial_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            label362.Visible = tbRightAtrialLevel_AdultShort.Visible = label377.Visible = chkRightAtrial_AdultShort.Checked;
            if (!chkRightAtrial_AdultShort.Checked)
                tbRightAtrialLevel_AdultShort.Text = "";
        }

        private void chkVascular_AdultShort_CheckedChanged(object sender, EventArgs e)
        {
            label378.Visible = tbVascularLevel_AdultShort.Visible = label379.Visible = chkVascular_AdultShort.Checked;
            if (!chkVascular_AdultShort.Checked)
                tbVascularLevel_AdultShort.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void tbLeftVentrKDO_Leave(object sender, EventArgs e)
        {

        }

        private void tbDubLeftVentrKDO_Leave(object sender, EventArgs e)
        {

        }

        private void tbAdditionalParametersHeartShorteningFrequency_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbAorticValveMaxGradient_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbMitralValveMaxGradient_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbTricuspidalValveMaxGradient_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbAorticValveMaxGradient_AdultShort_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbMitralValveMaxGradient_AdultShort_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbLinearDimensionBasalContractileAbility_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbLinearDimensionBasalContractileAbility_AdultShort_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbLeftVentricleStrokeVolume_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbLinearDimensionsShorteningFraction_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbAditionalParametersMiocardiumMassIndex_TextChanged(object sender, EventArgs e)
        {
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            wmtbConclusion.Text = "Исследование проводится при показателях ЧСС = " + tbAdditionalParametersHeartShorteningFrequency.Text + " уд/мин.\r\n" + wmtbConclusion.Text;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void tbOTS_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbMedEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void linkSensors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkAdditionalInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void tbRightAtriumVolume_Leave(object sender, EventArgs e)
        {
        }

        private void NeededSave(object sender, EventArgs e)
        {
        }

    }
}
