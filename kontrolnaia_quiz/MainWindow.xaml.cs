using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kontrolnaia_quiz
{
    public class Quiz
    {
        [JsonInclude]
        int numberquestion;
        [JsonInclude]
        string questiontext;
        [JsonInclude]
        int answer1;
       // [JsonInclude]
       // int answer2;
      //  [JsonInclude]
       // int answer3;

        public Quiz(int numberquestion, string questiontext, int answer1)
        {
            this.numberquestion = numberquestion;
            this.questiontext = questiontext;
            this.answer1 = answer1;
           

        }

        public int GetNumberQuestion()
        {
            return numberquestion;  
        }
        public void SetNumberQuestion(int numberquestion)
        {
            this.numberquestion = numberquestion;
        }

        public string GetQuestionText()
        {
            return questiontext;
        }
        
        public void SetQuestionText(string questiontext)
        {
            this.questiontext = questiontext;
        }


        public int getAnswer1()
        {
            return answer1;
        }
      
    }

    
    public partial class MainWindow : Window
    {
        Quiz quiz;
        List<Quiz> quizzes = new List<Quiz>();
        int buttonStartClicked = 1;
        int score = 0;
       public int checkanswer;

        public void ScoreUp()
        {
            score++;
            TBScore.Text = score.ToString();
        }
        public void ButtonStartClicked()
        {
            buttonStartClicked++;
        }
        public void SaveJson(Quiz quiz)
        {
           // List<Quiz> quizzes = new List<Quiz>();
            quizzes.Add(quiz);
            // Сериализация списка в JSON
           // "Овечка это какое животное? \n 1. Насекомое \n 2. Ракообразное \n 3. Рогатый скот", 0, 0 , 1
            string jsonString = JsonSerializer.Serialize(quizzes);
            // Сохранение JSON в файл
            File.WriteAllText("quizzes.json", jsonString);
        }

        public void LoadJson()
        {
            // Чтение JSON из файла
            string jsonFromFile = File.ReadAllText("quizzes.json");
           // List<Quiz> quizzes = new List<Quiz>();
            // Парсинг JSON
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);
            //Добавление новой записи в список класса из json
            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                int numberquestion = element.GetProperty("numberquestion").GetInt32();
                string questiontext = element.GetProperty("questiontext").GetString();
                int answer1 = element.GetProperty("answer1").GetInt32();
                // Создание нового экземпляра класса Person с помощью конструктора
                
                quiz = new Quiz(numberquestion, questiontext, answer1);
               // Добавление объекта в список
                quizzes.Add(quiz);
            }

            

        }

        public void DisplayNextQuestion()
        {

            Quiz[] quizArray = new Quiz[quizzes.Count];


            quizArray = quizzes.ToArray();


            for (int i = 0; i < buttonStartClicked && i < quizArray.Length; i++)
            {

                Quiz quiz = quizArray[i];
                TBTextQuestion.Text = quiz.GetQuestionText();
                checkanswer = quiz.getAnswer1();



            }
        }

       
        public void DoNewQuiestion(int numberquestion, string questiontext, int answer1)
        {
            Quiz quiz = new Quiz(numberquestion, questiontext, answer1);
            //return quiz;
            SaveJson(quiz);

        }


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            DisplayNextQuestion();
            ButtonStartClicked();


        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            DoNewQuiestion(Convert.ToInt32(TBNumberQuestion.Text), TBTextQuestion.Text, Convert.ToInt32(TBuseranswer.Text));
            TBTextQuestion.Clear(); TBuseranswer.Clear(); TBNumberQuestion.Clear();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
           if (checkanswer == 1)
            {
                TBuseranswer.Text = "ОТВЕТ ВЕРНЫЙ!";
                ScoreUp();
            }
            else
            {
                TBuseranswer.Text = "ОТВЕТ НЕВЕРНЫЙ";
            }
        }

        private void ButtonClearJson_Click(object sender, RoutedEventArgs e)
        {
            quizzes.Clear();
            string jsonString = JsonSerializer.Serialize(quizzes);
            // Сохранение JSON в файл
            File.WriteAllText("quizzes.json", jsonString);
            TBNumberQuestion.Clear(); TBTextQuestion.Clear(); TBuseranswer.Clear();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (checkanswer == 2)
            {
                TBuseranswer.Text = "ОТВЕТ ВЕРНЫЙ!";
                ScoreUp();
            }
            else
            {
                TBuseranswer.Text = "ОТВЕТ НЕВЕРНЫЙ";
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (checkanswer == 3)
            {
                TBuseranswer.Text = "ОТВЕТ ВЕРНЫЙ!";
                ScoreUp();
            }
            else
            {
                TBuseranswer.Text = "ОТВЕТ НЕВЕРНЫЙ";
            }
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {

            LoadJson();
            ButtonStart.IsEnabled = true;
            TBuseranswer.IsEnabled = false;
           // ButtonStartClicked();
        }
    }
}
