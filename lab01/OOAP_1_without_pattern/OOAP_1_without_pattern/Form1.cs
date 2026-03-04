using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOAP_1_without_pattern
{
    public partial class Form1 : Form
    {
        private readonly DocumentRepository _repository = new DocumentRepository();

        public Form1()
        {
            InitializeComponent();
            InitializeData();
            SetupUI();
        }

        

        private void InitializeData()
        {
            _repository.Add(new Letter { Number = 1, Date = DateTime.Now, Content = "Запрос цен", LetterType = "Входящее", Correspondent = "ООО Ромашка" });
            _repository.Add(new Order { Number = 2, Date = DateTime.Now, Content = "Отчет", Department = "Бухгалтерия", DueDate = DateTime.Now.AddDays(7), Responsible = "Иванов" });
            _repository.Add(new BusinessTripOrder { Number = 3, Date = DateTime.Now, Content = "Командировка", Employee = "Петров", Period = "01.03-10.03", Destination = "Москва" });
        }


        private void SetupUI()
        {
            // Настройка ComboBox
            cmbDocType.Items.AddRange(new string[] { "Письмо", "Приказ", "Распоряжение о командировке" });
            cmbDocType.SelectedIndexChanged += cmbDocType_SelectedIndexChanged;

            // Привязка кнопки создания
            btnCreateDoc.Click += btnCreateDoc_Click;

            // Изначально скрываем все специфичные панели
            HideAllSpecificPanels();
        }

        private void cmbDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideAllSpecificPanels();

            // Показываем нужную панель в зависимости от выбора
            switch (cmbDocType.SelectedItem.ToString())
            {
                case "Письмо":
                    panelLetter.Visible = true;
                    panelLetter.BringToFront();
                    panelLetter.Refresh();
                    break;
                case "Приказ":
                    panelOrder.Visible = true;
                    panelOrder.BringToFront();
                    panelOrder.Refresh();
                    break;
                case "Распоряжение о командировке":
                    panelTrip.Visible = true;
                    panelTrip.BringToFront();
                    panelTrip.Refresh();
                    break;
            }
            this.Refresh();
        }

        

        private void HideAllSpecificPanels()
        {
            // Убедитесь, что эти панели созданы в конструкторе
            panelLetter.Visible = false;
            panelOrder.Visible = false;
            panelTrip.Visible = false;
        }

        private void txtNewNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNewDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNewContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelTrip_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLetter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtLetterType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorrespondent_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelOrder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpDueDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtResponsible_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmployee_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPeriod_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDestination_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateDoc_Click(object sender, EventArgs e)
        {
            if (cmbDocType.SelectedIndex == -1 || !int.TryParse(txtNewNumber.Text, out int number))
            {
                MessageBox.Show("Пожалуйста, выберите тип документа и введите корректный номер.");
                return;
            }

            // Проверка на уникальность номера
            if (_repository.GetByNumber(number) != null)
            {
                MessageBox.Show("Документ с таким номером уже существует!");
                return;
            }

            Document newDoc = null;

            // Создаем экземпляр нужного класса
            switch (cmbDocType.SelectedItem.ToString())
            {
                case "Письмо":
                    newDoc = new Letter
                    {
                        LetterType = txtLetterType.Text,
                        Correspondent = txtCorrespondent.Text
                    };
                    break;
                case "Приказ":
                    newDoc = new Order
                    {
                        Department = txtDepartment.Text,
                        DueDate = dtpDueDate.Value,
                        Responsible = txtResponsible.Text
                    };
                    break;
                case "Распоряжение о командировке":
                    newDoc = new BusinessTripOrder
                    {
                        Employee = txtEmployee.Text,
                        Period = txtPeriod.Text,
                        Destination = txtDestination.Text
                    };
                    break;
            }

            // Заполняем общие свойства
            if (newDoc != null)
            {
                newDoc.Number = number;
                newDoc.Date = dtpNewDate.Value;
                newDoc.Content = txtNewContent.Text;

                _repository.Add(newDoc);
                MessageBox.Show("Документ успешно создан!");

                // Опционально: сразу обновляем список
                button1_Click(null, null);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var doc in _repository.GetAll())
            {
                listBox1.Items.Add(doc);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int number))
            {
                var doc = _repository.GetByNumber(number);
                if (doc != null) MessageBox.Show(doc.GetFullInfo());
                else MessageBox.Show("Документ не найден.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            if (listBox1.SelectedItem is Document doc)
            {
                // Можно выводить информацию о выбранном элементе в текстовое поле
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    abstract class Document
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public virtual string GetInfo() => $"№{Number} от {Date.ToShortDateString()}";
        public abstract string GetFullInfo();
        public override string ToString() => GetInfo();
    }

    class Letter : Document
    {
        public string LetterType { get; set; }
        public string Correspondent { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nТип: {LetterType}\nОт: {Correspondent}\nСуть: {Content}";
    }

    class Order : Document
    {
        public string Department { get; set; }
        public DateTime DueDate { get; set; }
        public string Responsible { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nЦех: {Department}\nСрок: {DueDate.ToShortDateString()}\nОтв: {Responsible}";
    }

    class BusinessTripOrder : Document
    {
        public string Employee { get; set; }
        public string Period { get; set; }
        public string Destination { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nКто: {Employee}\nКуда: {Destination}\nСроки: {Period}";
    }

    class DocumentRepository
    {
        private List<Document> documents = new List<Document>();
        public void Add(Document doc) => documents.Add(doc);
        public List<Document> GetAll() => documents;
        public Document GetByNumber(int number) => documents.FirstOrDefault(d => d.Number == number);
    }

}
