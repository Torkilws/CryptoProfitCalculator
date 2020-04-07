namespace CryptoProfitCalc {
  partial class CryptoProfitCalc {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CryptoProfitCalc));
      this.totTextBox = new System.Windows.Forms.TextBox();
      this.totProfitLabel = new System.Windows.Forms.Label();
      this.cryptoLabel = new System.Windows.Forms.Label();
      this.currentPriceLabel = new System.Windows.Forms.Label();
      this.profitLabel = new System.Windows.Forms.Label();
      this.fiatLabel = new System.Windows.Forms.Label();
      this.profit_button = new System.Windows.Forms.Button();
      this.tlp = new System.Windows.Forms.TableLayoutPanel();
      this.coinLabel = new System.Windows.Forms.Label();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.searchBox = new System.Windows.Forms.TextBox();
      this.languageMenu = new System.Windows.Forms.MenuStrip();
      this.currencyDropDown = new System.Windows.Forms.ToolStripMenuItem();
      this.currentCurrencyLabel = new System.Windows.Forms.Label();
      this.progressSpinner = new MetroFramework.Controls.MetroProgressSpinner();
      this.addCryptoButton = new System.Windows.Forms.Button();
      this.progressLabel = new System.Windows.Forms.Label();
      this.removeLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox_ownedValue = new System.Windows.Forms.TextBox();
      this.languageMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // totTextBox
      // 
      resources.ApplyResources(this.totTextBox, "totTextBox");
      this.totTextBox.Name = "totTextBox";
      this.totTextBox.ReadOnly = true;
      // 
      // totProfitLabel
      // 
      resources.ApplyResources(this.totProfitLabel, "totProfitLabel");
      this.totProfitLabel.Name = "totProfitLabel";
      // 
      // cryptoLabel
      // 
      resources.ApplyResources(this.cryptoLabel, "cryptoLabel");
      this.cryptoLabel.Name = "cryptoLabel";
      // 
      // currentPriceLabel
      // 
      resources.ApplyResources(this.currentPriceLabel, "currentPriceLabel");
      this.currentPriceLabel.Name = "currentPriceLabel";
      // 
      // profitLabel
      // 
      resources.ApplyResources(this.profitLabel, "profitLabel");
      this.profitLabel.Name = "profitLabel";
      // 
      // fiatLabel
      // 
      resources.ApplyResources(this.fiatLabel, "fiatLabel");
      this.fiatLabel.Name = "fiatLabel";
      // 
      // profit_button
      // 
      this.profit_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(207)))), ((int)(((byte)(245)))));
      resources.ApplyResources(this.profit_button, "profit_button");
      this.profit_button.Name = "profit_button";
      this.profit_button.UseVisualStyleBackColor = false;
      this.profit_button.Click += new System.EventHandler(this.button1_Click);
      // 
      // tlp
      // 
      resources.ApplyResources(this.tlp, "tlp");
      this.tlp.Name = "tlp";
      // 
      // coinLabel
      // 
      resources.ApplyResources(this.coinLabel, "coinLabel");
      this.coinLabel.Name = "coinLabel";
      // 
      // splitter1
      // 
      this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.splitter1, "splitter1");
      this.splitter1.Name = "splitter1";
      this.splitter1.TabStop = false;
      // 
      // searchBox
      // 
      resources.ApplyResources(this.searchBox, "searchBox");
      this.searchBox.Name = "searchBox";
      this.searchBox.Enter += new System.EventHandler(this.searchBox_Enter);
      this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
      // 
      // languageMenu
      // 
      this.languageMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(207)))), ((int)(((byte)(245)))));
      resources.ApplyResources(this.languageMenu, "languageMenu");
      this.languageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currencyDropDown});
      this.languageMenu.Name = "languageMenu";
      this.languageMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      // 
      // currencyDropDown
      // 
      resources.ApplyResources(this.currencyDropDown, "currencyDropDown");
      this.currencyDropDown.Name = "currencyDropDown";
      // 
      // currentCurrencyLabel
      // 
      resources.ApplyResources(this.currentCurrencyLabel, "currentCurrencyLabel");
      this.currentCurrencyLabel.Name = "currentCurrencyLabel";
      // 
      // progressSpinner
      // 
      this.progressSpinner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(207)))), ((int)(((byte)(245)))));
      resources.ApplyResources(this.progressSpinner, "progressSpinner");
      this.progressSpinner.Maximum = 100;
      this.progressSpinner.Name = "progressSpinner";
      this.progressSpinner.Speed = 2F;
      this.progressSpinner.UseSelectable = true;
      // 
      // addCryptoButton
      // 
      this.addCryptoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(207)))), ((int)(((byte)(245)))));
      resources.ApplyResources(this.addCryptoButton, "addCryptoButton");
      this.addCryptoButton.Name = "addCryptoButton";
      this.addCryptoButton.UseVisualStyleBackColor = false;
      this.addCryptoButton.Click += new System.EventHandler(this.addCryptoButton_Click);
      // 
      // progressLabel
      // 
      resources.ApplyResources(this.progressLabel, "progressLabel");
      this.progressLabel.Name = "progressLabel";
      // 
      // removeLabel
      // 
      resources.ApplyResources(this.removeLabel, "removeLabel");
      this.removeLabel.Name = "removeLabel";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // textBox_ownedValue
      // 
      resources.ApplyResources(this.textBox_ownedValue, "textBox_ownedValue");
      this.textBox_ownedValue.Name = "textBox_ownedValue";
      this.textBox_ownedValue.ReadOnly = true;
      // 
      // CryptoProfitCalc
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.textBox_ownedValue);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.removeLabel);
      this.Controls.Add(this.progressLabel);
      this.Controls.Add(this.addCryptoButton);
      this.Controls.Add(this.progressSpinner);
      this.Controls.Add(this.currentCurrencyLabel);
      this.Controls.Add(this.searchBox);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.coinLabel);
      this.Controls.Add(this.tlp);
      this.Controls.Add(this.profit_button);
      this.Controls.Add(this.fiatLabel);
      this.Controls.Add(this.profitLabel);
      this.Controls.Add(this.currentPriceLabel);
      this.Controls.Add(this.cryptoLabel);
      this.Controls.Add(this.totProfitLabel);
      this.Controls.Add(this.totTextBox);
      this.Controls.Add(this.languageMenu);
      this.MainMenuStrip = this.languageMenu;
      this.MaximizeBox = false;
      this.Name = "CryptoProfitCalc";
      this.Resizable = false;
      this.Load += new System.EventHandler(this.CryptoProfitCalc_Load);
      this.languageMenu.ResumeLayout(false);
      this.languageMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox totTextBox;
    private System.Windows.Forms.Label totProfitLabel;
    private System.Windows.Forms.Label cryptoLabel;
    private System.Windows.Forms.Label currentPriceLabel;
    private System.Windows.Forms.Label profitLabel;
    private System.Windows.Forms.Label fiatLabel;
    private System.Windows.Forms.Button profit_button;
    private System.Windows.Forms.TableLayoutPanel tlp;
    private System.Windows.Forms.Label coinLabel;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.TextBox searchBox;
    private System.Windows.Forms.MenuStrip languageMenu;
    private System.Windows.Forms.ToolStripMenuItem currencyDropDown;
    private System.Windows.Forms.Label currentCurrencyLabel;
    private MetroFramework.Controls.MetroProgressSpinner progressSpinner;
    private System.Windows.Forms.Button addCryptoButton;
    private System.Windows.Forms.Label progressLabel;
    private System.Windows.Forms.Label removeLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox_ownedValue;
  }
}

