namespace Geotag
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TXT_workPath = new System.Windows.Forms.TextBox();
            this.workingFolder = new System.Windows.Forms.Button();
            this.button_geotag = new System.Windows.Forms.Button();
            this.TXT_outputlog = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // TXT_workPath
            // 
            this.TXT_workPath.Location = new System.Drawing.Point(12, 12);
            this.TXT_workPath.Name = "TXT_workPath";
            this.TXT_workPath.Size = new System.Drawing.Size(360, 25);
            this.TXT_workPath.TabIndex = 3;
            this.TXT_workPath.Text = "C:\\Work\\Photo data\\geotag test";
            // 
            // workingFolder
            // 
            this.workingFolder.Location = new System.Drawing.Point(378, 14);
            this.workingFolder.Name = "workingFolder";
            this.workingFolder.Size = new System.Drawing.Size(144, 23);
            this.workingFolder.TabIndex = 4;
            this.workingFolder.Text = "Working folder";
            this.workingFolder.UseVisualStyleBackColor = true;
            this.workingFolder.Click += new System.EventHandler(this.workingFolder_Click);
            // 
            // button_geotag
            // 
            this.button_geotag.Location = new System.Drawing.Point(378, 43);
            this.button_geotag.Name = "button_geotag";
            this.button_geotag.Size = new System.Drawing.Size(144, 23);
            this.button_geotag.TabIndex = 5;
            this.button_geotag.Text = "Geotag images";
            this.button_geotag.UseVisualStyleBackColor = true;
            this.button_geotag.Click += new System.EventHandler(this.button_getFile_Click);
            // 
            // TXT_outputlog
            // 
            this.TXT_outputlog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_outputlog.Location = new System.Drawing.Point(1, 76);
            this.TXT_outputlog.Multiline = true;
            this.TXT_outputlog.Name = "TXT_outputlog";
            this.TXT_outputlog.ReadOnly = true;
            this.TXT_outputlog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TXT_outputlog.Size = new System.Drawing.Size(599, 220);
            this.TXT_outputlog.TabIndex = 7;
            this.TXT_outputlog.Text = "使用说明\r\n1. 工作路径下每个相机的照片单独存入一个子文件夹内；\r\n2. 照片请按名称排序，与POS信息一一对应；\r\n3. POS信息存在工作路径下的\"pos." +
    "txt\"；\r\n4. 仅支持零度飞控导出的pos文件。\r\n";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 303);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(576, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 327);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.TXT_outputlog);
            this.Controls.Add(this.button_geotag);
            this.Controls.Add(this.workingFolder);
            this.Controls.Add(this.TXT_workPath);
            this.Name = "Form1";
            this.Text = "Geotag images";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_workPath;
        private System.Windows.Forms.Button workingFolder;
        private System.Windows.Forms.Button button_geotag;
        private System.Windows.Forms.TextBox TXT_outputlog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

