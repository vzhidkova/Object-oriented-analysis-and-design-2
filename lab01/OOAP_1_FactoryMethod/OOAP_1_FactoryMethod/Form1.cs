using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OOAP_1_FactoryMethod
{

    public partial class Form1 : Form
    {

        private readonly DocumentRepository _repository = new DocumentRepository();

        private Dictionary<string, IDocumentFactory> _factories;

        public Form1()
        {
            InitializeComponent();
            InitFactories();
            InitializeData();
            SetupUI();
        }

        private void InitializeData()
        {
            _repository.Add(new Letter { Number = 1, Date = DateTime.Now, Content = "Запрос цен", LetterType = "Входящее", Correspondent = "ООО Ромашка" });
            _repository.Add(new Order { Number = 2, Date = DateTime.Now, Content = "Отчет", Department = "Бухгалтерия", DueDate = DateTime.Now.AddDays(7), Responsible = "Иванов" });
            _repository.Add(new BusinessTripOrder { Number = 3, Date = DateTime.Now, Content = "Командировка", Employee = "Петров", Period = "01.03-10.03", Destination = "Москва" });
        }

        private void InitFactories()
        {
            _factories = new Dictionary<string, IDocumentFactory>
            {
                { "Письмо", new LetterFactory() },
                { "Приказ", new OrderFactory() },
                { "Распоряжение о командировке", new BusinessTripFactory() }
            };
        }

        private void SetupUI()
        {
            cmbDocType.Items.Clear();
            cmbDocType.Items.AddRange(_factories.Keys.ToArray());
            cmbDocType.SelectedIndexChanged += (s, e) => ShowPanel();
        }

        private void ShowPanel()
        {
            panelLetter.Visible = (cmbDocType.Text == "Письмо");
            panelOrder.Visible = (cmbDocType.Text == "Приказ");
            panelTrip.Visible = (cmbDocType.Text == "Распоряжение о командировке");

            // Выводим активную панель на передний план
            if (panelLetter.Visible) panelLetter.BringToFront();
            if (panelOrder.Visible) panelOrder.BringToFront();
            if (panelTrip.Visible) panelTrip.BringToFront();
        }

        private void btnCreateDoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDocType.Text)) return;

            try
            {
                var factory = _factories[cmbDocType.Text];
                Document newDoc = factory.CreateDocument(this);

                if (newDoc != null)
                {
                    _repository.Add(newDoc);
                    MessageBox.Show("Документ добавлен через фабрику!");
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании: " + ex.Message);
            }
        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(_repository.GetAll().ToArray());
        }

        private void button1_Click(object sender, EventArgs e) => RefreshList();

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int number))
            {
                var doc = _repository.GetByNumber(number);
                if (doc != null) MessageBox.Show(doc.GetFullInfo());
                else MessageBox.Show("Документ не найден.");
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {}
        private void textBox1_TextChanged(object sender, EventArgs e) {}

        public void txtNewNumber_TextChanged(object sender, EventArgs e)
        {

        }

        public void dtpNewDate_ValueChanged(object sender, EventArgs e)
        {

        }

        public void txtNewContent_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public interface IDocumentFactory
    {
        Document CreateDocument(Form1 form);
    }

    class LetterFactory : IDocumentFactory
    {
        public Document CreateDocument(Form1 form)
        {
            return new Letter
            {
                Number = int.Parse(form.txtNewNumber.Text),
                Date = form.dtpNewDate.Value,
                Content = form.txtNewContent.Text,
                LetterType = form.txtLetterType.Text,
                Correspondent = form.txtCorrespondent.Text
            };
        }
    }

    class OrderFactory : IDocumentFactory
    {
        public Document CreateDocument(Form1 form)
        {
            return new Order
            {
                Number = int.Parse(form.txtNewNumber.Text),
                Date = form.dtpNewDate.Value,
                Content = form.txtNewContent.Text,
                Department = form.txtDepartment.Text,
                DueDate = form.dtpDueDate.Value,
                Responsible = form.txtResponsible.Text
            };
        }
    }

    class BusinessTripFactory : IDocumentFactory
    {
        public Document CreateDocument(Form1 form)
        {
            return new BusinessTripOrder
            {
                Number = int.Parse(form.txtNewNumber.Text),
                Date = form.dtpNewDate.Value,
                Content = form.txtNewContent.Text,
                Employee = form.txtEmployee.Text,
                Period = form.txtPeriod.Text,
                Destination = form.txtDestination.Text
            };
        }
    }

    // --- МОДЕЛИ ДАННЫХ ---

    public abstract class Document
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public virtual string GetInfo() => $"№{Number} от {Date.ToShortDateString()}";
        public abstract string GetFullInfo();
        public override string ToString() => GetInfo();
    }

    public class Letter : Document
    {
        public string LetterType { get; set; }
        public string Correspondent { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nТип: {LetterType}\nОт: {Correspondent}\nСуть: {Content}";
    }

    public class Order : Document
    {
        public string Department { get; set; }
        public DateTime DueDate { get; set; }
        public string Responsible { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nОтдел: {Department}\nСрок: {DueDate.ToShortDateString()}\nОтв: {Responsible}";
    }

    public class BusinessTripOrder : Document
    {
        public string Employee { get; set; }
        public string Period { get; set; }
        public string Destination { get; set; }
        public override string GetFullInfo() => $"{GetInfo()}\nКто: {Employee}\nКуда: {Destination}\nСроки: {Period}";
    }

    public class DocumentRepository
    {
        private List<Document> documents = new List<Document>();
        public void Add(Document doc) => documents.Add(doc);
        public List<Document> GetAll() => documents;
        public Document GetByNumber(int number) => documents.FirstOrDefault(d => d.Number == number);
    }

}


