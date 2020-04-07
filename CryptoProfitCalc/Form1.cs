using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using CryptoProfitCalc.Models;
using System.Collections;
using MetroFramework.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace CryptoProfitCalc {
  public partial class CryptoProfitCalc : MetroForm {

    const string RATE_ENDPOINT = "https://v3.exchangerate-api.com/pair/6e963847a886f9dff38d8bbc/";
    const string TICKER_ENDPOINT = "https://api.coinmarketcap.com/v1/ticker/?limit=100";
    const string SAVE_FILE = @"C:\Temp\CryptoSave.txt";
    const string TEMP_FOLDER = @"C:\Temp";
    const string CMC_API_KEY = "8b371403-3746-4046-9148-e600ad95c20d";

    string[] currencies = { "USD", "NOK", "EUR", "BRL", "CAD", "CHF", "CLP", "CNY",
      "CZK", "DKK", "GBP", "HKD", "HUF", "IDR", "ILS", "INR",
      "JPY", "KRW", "MXN", "MYR", "AUD", "NZD", "PHP", "PKR", "PLN",
      "RUB", "SEK", "SGD", "THB", "TRY", "TWD", "ZAR" };

    ArrayList savedData = new ArrayList();
    List<CryptoTicker> tickers = new List<CryptoTicker>();
    string currentCurrency;
    string latestLetter = "";
    string textBeforeKey = "";
    bool gotComma = false;
    int rowsToAdd = 0;


    public CryptoProfitCalc() {
      InitializeComponent();
    }

    private async void CryptoProfitCalc_Load(object sender, EventArgs e) {
      FileInfo file = new FileInfo(SAVE_FILE);
      if (!file.Exists) {
        CreateFile();
        Application.Restart();
      }

      if (file.Exists) {
        if (!FileIsEmpty(SAVE_FILE)) {
          ReadEntriesFromFile();
          progressSpinner.Visible = true;
          DisableInput();
          ShowStatusMessage("Fetching saved data...\n(Speed depends on server traffic)");
          try {
            await FetchCryptoTicker(currentCurrency);
          } catch (Exception ex) {
            MessageBox.Show("Try restarting the application and try again.", "Could not load currency data");
            Console.WriteLine("{0} Exception caught.", ex);
          }
          progressSpinner.Visible = false;
          EnableInput();
          HideStatusMessage();
          AddEntriesFromFile();
          CalculateProfit();
          CalculateTotalProfit();

        } else {
          currentCurrency = "USD";
          currentPriceLabel.Text = "Current\nPrice\n(" + currentCurrency + ")";
        }

      }
      AddLanguages();
    }



    private void DisableInput() {
    addCryptoButton.Enabled = false;
    languageMenu.Enabled = false;
    searchBox.Enabled = false;
    }   

    private void EnableInput() {
      addCryptoButton.Enabled = true;
      languageMenu.Enabled = true;
      searchBox.Enabled = true;
    }

    private void ShowStatusMessage(string message) {
      progressLabel.Visible = true;
      progressLabel.Text = message;
    }

    private void HideStatusMessage() {
      progressLabel.Visible = false;
      progressLabel.Text = "";
    }

    private void CreateFile() {
      FileInfo file = new FileInfo(SAVE_FILE);
      DirectoryInfo dir = new DirectoryInfo(TEMP_FOLDER);
      Directory.CreateDirectory(TEMP_FOLDER);
      file.Create();
    }

    private void WriteToFile() {
      if (tlp.Controls.Count > 0) {
        ClearFileContent(SAVE_FILE);
        using (StreamWriter sw = File.AppendText(SAVE_FILE)) {
          sw.WriteLine(tlp.RowCount - 1);
          sw.WriteLine(currentCurrency);
          for (int i = 0; i < tlp.RowCount - 1; i++) {
            for (int j = 0; j < 3; j++) {
              Control control = tlp.GetControlFromPosition(j, i);
              sw.WriteLine(control.Text);
            }
          }
        }
      }
    }

    private void ReadEntriesFromFile() {
      savedData.Clear();
      rowsToAdd = 0;
      string line = string.Empty;
      bool gotHeaderInfo = false;
      using (StreamReader sr = File.OpenText(SAVE_FILE)) {
        while (sr.Peek() >= 0) {
          if (!gotHeaderInfo) {
            rowsToAdd = Int32.Parse(sr.ReadLine());
            currentCurrency = sr.ReadLine();
            currentPriceLabel.Text = "Current\nPrice\n(" + currentCurrency + ")";
            currentCurrencyLabel.Text = currentCurrency;
            gotHeaderInfo = true;
          }
          savedData.Add(sr.ReadLine());
        }
      }

    }

    private void AddEntriesFromFile() {
      int dataIndex = 0;
      string currLabel = "";
      for (int i = 0; i < rowsToAdd; i++) {
        AddCrypto(savedData[dataIndex++].ToString());
        currLabel = savedData[dataIndex].ToString();
        AddLabel(currLabel); dataIndex++;
        AddFiat(savedData[dataIndex++].ToString());

        for (int j = 0; j < tickers.Count; j++) {
          if (tickers[j].name == currLabel) {
            //AddPrice(Round(tickers[j].price_cur, 4));
            //AddPrice(Round(tickers[j].quote.USD.price.ToString(), 4));
            AddPrice(Round(tickers[j].quote[currentCurrency]?.price.ToString(), 4));
                    }
        }
        AddProfit();
        AddRemove();
        ChangeRowSize(tlp, 40);
        tlp.RowCount++;
      }
    }

    private bool FileIsEmpty(string file) {
      FileInfo thisFile = new FileInfo(file);
      if (thisFile.Length > 0) {
        return false;
      }
      return true;
    }

    private void ClearFileContent(string file) {
      File.WriteAllText(file, "");
    }

    double totalValueOwned = 0;
    private void CalculateProfit() {
      try
      {
        if (tlp.Controls.Count > 0)
        {
            for (int i = 0; i < tlp.RowCount - 1; i++)
            {
                Control cryptoControl = tlp.GetControlFromPosition(0, i);
                Control fiatControl = tlp.GetControlFromPosition(2, i);
                Control priceControl = tlp.GetControlFromPosition(3, i);
                Control profitControl = tlp.GetControlFromPosition(4, i);
                double crypto = FixDecimalDo(cryptoControl.Text);
                double price = FixDecimalDo(priceControl.Text);
                double fiat = FixDecimalDo(fiatControl.Text);
                double profit = (crypto * price) - fiat;
                totalValueOwned += (crypto * price);
                profitControl.Text = Round(Convert.ToString(profit), 2);
            }
        }

      }
      catch (Exception)
      {

      }
    }

    private void CalculateTotalProfit() {
      double totProf = 0;
      if (tlp.Controls.Count > 0) {
        if (tlp.RowCount > 0) {
          for (int i = 0; i < tlp.RowCount-1; i++) {
            Control control = tlp.GetControlFromPosition(4, i);
            totProf += Convert.ToDouble(control.Text);
          }
        }
        totTextBox.Text = Round(Convert.ToString(totProf), 2);
      }
      string ownedValue = Round(Convert.ToString(totalValueOwned), 2);
      totalValueOwned = 0;
      textBox_ownedValue.Text = ownedValue;
    }

    private string Round(string thisNumber, int amount) {
      double number = double.Parse(thisNumber);
      number = Math.Round(number, amount);
      return Convert.ToString(number);
    }

    private double FixDecimalDo(string textField) {
      if (textField.Length != 0) {
        if (textField.Contains(",")) {

          return double.Parse(textField.Replace(',', '.'));
        
        } else
          return double.Parse(textField);
      }
      return 0;
    }

    private async Task<Rate> FetchRate(string to, string from) {
      string responseValue = string.Empty;
      string requestURI = string.Empty;
      to = to.ToUpper();
      from = from.ToUpper();

      requestURI = RATE_ENDPOINT + from + "/" + to;
      //https://v3.exchangerate-api.com/pair/6e963847a886f9dff38d8bbc/USD/GBP

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURI);
      using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync()) {
        if (response.StatusCode != HttpStatusCode.OK) {
          throw new ApplicationException("Error code: " + response.StatusCode.ToString());
        }

        using (Stream responseStream = response.GetResponseStream()) {
          if (responseStream != null) {
            using (StreamReader reader = new StreamReader(responseStream)) {
              responseValue = reader.ReadToEnd();
            }
          }
        }
      }

      Rate rate = Newtonsoft.Json.JsonConvert.DeserializeObject<Rate>(responseValue);
      return rate;
    }


      private async Task<List<CryptoTicker>> FetchCryptoTicker(string currency) {


        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "200";
        queryString["convert"] = currency;

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", CMC_API_KEY);
        client.Headers.Add("Accepts", "application/json");
        string urlString = URL.ToString();

        //string downloadString = client.DownloadString(urlString);

        string downloadString = await client.DownloadStringTaskAsync(urlString);

        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(downloadString);

        tickers.Clear();
        foreach (var data in responseObject.data)
        {
           tickers.Add(data);
        }

        return tickers;
      }

    private async Task AddCoinFromSearch(string inputCrypto) {
      addCryptoButton.Enabled = false;
      tickers = await FetchCryptoTicker(currentCurrency);
      addCryptoButton.Enabled = true;
    
      bool exists = false;
      for (int i = 0; i < 3; i++) {
        if (i == 1 && !exists) { inputCrypto = FirstLetterUpper(inputCrypto); }
        if (i == 2 && !exists) { inputCrypto = AllUpper(inputCrypto); }

        for (int j = 0; j < tickers.Count; j++) {
          if (tickers[j].name == inputCrypto) {
            AddLabel(tickers[j].name);
            AddCrypto();
            AddFiat();
            //AddPrice(Round(tickers[j].price_cur, 4));
            //AddPrice(Round(tickers[j].quote.USD.price.ToString(), 4));
            AddPrice(Round(tickers[j].quote[currentCurrency]?.price.ToString(), 4));
            AddProfit();
            AddRemove();
            ChangeRowSize(tlp, 40);
            tlp.RowCount++;
            exists = true;
            return;
          }
        }
      }
      if (!exists) { MessageBox.Show("Try write the name exactly how it is written on coinmarketcap.com", "Did not find crypto currency"); }
    }

    private string FirstLetterUpper(string text) {
      text = text.ToLower();
      string firstLetter = text.Substring(0, 1);
      text = text.Remove(0, 1);
      text = firstLetter.ToUpper() + text;
      return text;
    }

    private string AllUpper(string text) {
      text = text.ToUpper();
      return text;
    }

    private void AddLanguages() {
      foreach (string curr in currencies) {
        ToolStripMenuItem tsi = new ToolStripMenuItem(curr);
        currencyDropDown.DropDownItems.Add(tsi);
        tsi.Click += new EventHandler(MenuItemClickHandler);
      }

    }

    private async void MenuItemClickHandler(object sender, EventArgs e) {
      ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
      currentCurrencyLabel.Text = clickedItem.Text;
      string prevCurrency = currentCurrency;
      currentCurrency = clickedItem.Text;
      currentPriceLabel.Text = "Current\nPrice\n(" + currentCurrency + ")";
      if (tlp.RowCount > 0) {
        progressSpinner.Visible = true;
        ShowStatusMessage("Changing currency...\n(Speed depends on server traffic)");
        currencyDropDown.Enabled = false;
        List<CryptoTicker> tickers = await FetchCryptoTicker(currentCurrency);
        Rate rate = await FetchRate(currentCurrency, prevCurrency);
        currencyDropDown.Enabled = true;
        for (int i = 0; i < tlp.RowCount-1; i++) {
          Control priceControl = tlp.GetControlFromPosition(3, i);
          Control labelControl = tlp.GetControlFromPosition(1, i);
          Control fiatControl = tlp.GetControlFromPosition(2, i);
          for (int j = 0; j < tickers.Count; j++) {
            if (tickers[j].name == labelControl.Text) {
              //priceControl.Text = Round(tickers[j].price_cur, 4);
              //priceControl.Text = Round(tickers[j].quote.USD.price.ToString(), 4);
              priceControl.Text = Round(tickers[j].quote[currentCurrency]?.price.ToString(), 4);
              fiatControl.Text = GetCurrencyFromRate(rate, FixDecimalDo(fiatControl.Text).ToString());
            }
          }
        }
        CalculateProfit();
        CalculateTotalProfit();
        WriteToFile();
        progressSpinner.Visible = false;
        HideStatusMessage();
      }
    }

    private string GetCurrencyFromRate(Rate rate, string amount) {
      decimal dAmount = Convert.ToDecimal(amount);
      decimal dRate = rate.rate;
      decimal result = dAmount * dRate;
      result = Math.Round(result, 1);
      string returnNumber = Convert.ToString(result).Replace('.', ',');
      return returnNumber;
    }

    private void button1_Click(object sender, EventArgs e) {      
      CalculateProfit();
      CalculateTotalProfit();
      WriteToFile();
    }

    private void AddCrypto(string savedValue = "") {
      MaskedTextBox mtb = new MaskedTextBox();
      mtb.Font = new Font("Century Gothic", 14);
      mtb.Size = new Size(130, 30);
      if (savedValue != "") {
        mtb.Text = savedValue;
      } else {
        mtb.Text = "0";
      }
      mtb.TextAlign = HorizontalAlignment.Center;
      mtb.Anchor = AnchorStyles.None;
      mtb.TextChanged += new EventHandler(TextChangedHandler);
      mtb.PreviewKeyDown += PreviewKeyDown_Event;
      tlp.Controls.Add(mtb, 0, tlp.RowCount-1);
    }

    private void AddLabel(string coin) {
      Label l = new Label();
      l.Text = coin;
      l.Font = new Font("Century Gothic", 20);
      l.AutoSize = true;
      l.Anchor = AnchorStyles.Left;
      tlp.Controls.Add(l, 1, tlp.RowCount-1);
    }

    private void AddFiat(string savedValue = "") {
      MaskedTextBox mtb = new MaskedTextBox();
      mtb.Font = new Font("Century Gothic", 14);
      mtb.Size = new Size(130, 30);
      if (savedValue != "") {
        mtb.Text = savedValue;
      } else {
        mtb.Text = "0";
      }
      mtb.TextAlign = HorizontalAlignment.Center;
      mtb.Anchor = AnchorStyles.None;
      mtb.TextChanged += new EventHandler(TextChangedHandler);
      mtb.PreviewKeyDown += PreviewKeyDown_Event;
      tlp.Controls.Add(mtb, 2, tlp.RowCount-1);      
    }

    private void TextChangedHandler(object sender, EventArgs e) {
      MaskedTextBox x = (MaskedTextBox)sender;
      string currText = x.Text;
      char[] legalChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ','};
      bool isLegal = false;
      gotComma = textBeforeKey.Contains(",") ? true : false;
      if (!(latestLetter == "," && gotComma)) {

        for (int i = 0; i < currText.Length; i++) {
          isLegal = false;
          string thisLetter = currText.Substring(i, 1);
          for (int j = 0; j < legalChars.Length; j++) {
            string legalLetter = legalChars[j].ToString();
            if (thisLetter == legalLetter) {
              isLegal = true;
            }
          }
          if (!isLegal) {
            currText = currText.Replace(thisLetter, "");
          }
        }
      } else {
        currText = textBeforeKey;
      }
      x.Text = currText;
    }


    private void PreviewKeyDown_Event(object sender, PreviewKeyDownEventArgs e) {
      MaskedTextBox mtb = (MaskedTextBox)sender;
      textBeforeKey = mtb.Text;
      if (e.KeyCode.ToString() == "Oemcomma") {
        latestLetter = ",";
      } else {
        latestLetter = e.KeyCode.ToString();
      }
      if (latestLetter == "Return") {
        CalculateProfit();
        CalculateTotalProfit();
      }
    }

    private void AddPrice(string price) {
      TextBox t = new TextBox();
      t.Font = new Font("Century Gothic", 14);
      t.Size = new Size(130, 30);
      t.Text = price;
      t.TextAlign = HorizontalAlignment.Center;
      t.Anchor = AnchorStyles.None;
      t.ReadOnly = true;
      tlp.Controls.Add(t, 3, tlp.RowCount-1);
    }

    private void AddProfit() {
      TextBox t = new TextBox();
      t.Font = new Font("Century Gothic", 14);
      t.Size = new Size(130, 30);
      t.Text = "0";
      t.TextAlign = HorizontalAlignment.Center;
      t.Anchor = AnchorStyles.None;
      t.ReadOnly = true;
      
      tlp.Controls.Add(t, 4, tlp.RowCount-1);
    }

    private void AddRemove() {
      Button b = new Button();
      b.Name = Convert.ToString(tlp.RowCount-1);
      b.Tag = "Remove";
      b.Size = new Size(30, 30);
      b.Anchor = AnchorStyles.None;
      b.BackgroundImage = Properties.Resources.blackX;
      b.BackgroundImageLayout = ImageLayout.Stretch;
      b.FlatStyle = FlatStyle.Flat;
      b.Click += new EventHandler(AddRemoveClickHandler);
      tlp.Controls.Add(b, 5, tlp.RowCount-1);
      
    }

    private void AddRemoveClickHandler(object sender, EventArgs e) {
      Button x = (Button)sender;
      RemoveRow(Convert.ToInt32(x.Name));
    }

    public void RemoveRow(int row_index_to_remove) {    

      for (int i = 0; i < tlp.ColumnCount; i++) {
        Control control = tlp.GetControlFromPosition(i, row_index_to_remove);
        tlp.Controls.Remove(control);
      }

      for (int i = row_index_to_remove; i < tlp.RowCount-1; i++) {
        for (int j = 0; j < 6; j++) {
          var control = tlp.GetControlFromPosition(j, i);
          if (control != null) {
            if (control.Tag.Equals("Remove")) {
              int rowID = Convert.ToInt32(control.Name);
              control.Name = Convert.ToString(--rowID);
            }
            tlp.SetRow(control, i - 1);            
          }
        }
      }

      tlp.RowCount--;
      ChangeRowSize(tlp, 40);

    }

    private void ChangeRowSize(TableLayoutPanel tlp, int size) {
      TableLayoutRowStyleCollection styles = tlp.RowStyles;
      foreach (RowStyle style in styles) {
        style.SizeType = SizeType.Absolute;
        style.Height = size;
      }
    }

    private async void searchBox_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        if (searchBox.Text != string.Empty) {
          progressSpinner.Visible = true;
          ShowStatusMessage("Adding coin...");
          searchBox.Enabled = false;
          await AddCoinFromSearch(searchBox.Text);
          searchBox.Text = "";
          progressSpinner.Visible = false;
          HideStatusMessage();
          searchBox.Enabled = true;
          searchBox.Focus();
        }
      }
    }

    private async void addCryptoButton_Click(object sender, EventArgs e) {
      if (searchBox.Text != string.Empty) {
        progressSpinner.Visible = true;
        ShowStatusMessage("Adding coin...");
        await AddCoinFromSearch(searchBox.Text);
        searchBox.Text = "";
        progressSpinner.Visible = false;
        HideStatusMessage();
      }
    }

    private void searchBox_Enter(object sender, EventArgs e) {
      searchBox.Text = "";
    }
  }
}
