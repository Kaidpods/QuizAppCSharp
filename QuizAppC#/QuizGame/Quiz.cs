using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using QuizAppC_.QuizGame.Questions;

namespace QuizAppC_.QuizGame
{
    public class Quiz
    {
        protected string DEFAULT_NAME = "Anon";
        protected List<List<string>> options = new List<List<string>>();
        protected List<int> qType = new List<int>();
        protected List<string> questions = new List<string>();
        protected List<string> answers = new List<string>();
        protected List<int> value = new List<int>();
        public List<Question> quizQuestions = new List<Question>();

        private readonly string myFile = "questions.csv";
        private readonly string myFileBackup = "QuizTask/questions.csv";
        //Throws an exception only if file cant be found


        public Quiz()
        {
            read(myFile);
            int numQ = questions.Count();
            for (int i = 0; i < numQ; i++)
            {
                switch (qType[i])
                {
                    case 1:
                        Question newQ = new TextQuestion(questions[i], answers[i], value[i]);
                        quizQuestions.Add(newQ);
                        break;
                    case 2:
                        TrueFalseQuestion newTFQ = new TrueFalseQuestion(questions[i], bool.Parse(answers[i]), value[i]);
                        quizQuestions.Add(newTFQ);
                        break;
                    case 3:
                        MultipleChoiceQuestion newMQ = new MultipleChoiceQuestion(questions[i], answers[i], options[i], value[i]);
                        quizQuestions.Add(newMQ);
                        break;
                }

            }
            Shuffle.Shuffle.ShuffleList(quizQuestions);
        }
        public void start()
        {
            bool rerunTest = true;
            while (rerunTest)
            {
                rerunTest = false;
                string input;

                //Gets user's name
                Console.WriteLine("What is your name?");
                string name = Console.ReadLine();
                string fileCheck = (name.ToLower() + ".csv");
                Player livePlayer;
                if (!File.Exists(fileCheck))
                {
                    livePlayer = new Player(name, "");
                }
                else
                {
                    livePlayer = new Player(name, "", returnRead(name));
                }

                //Ensures input isn't empty
                if (name.Count() < 2)
                {
                    name = DEFAULT_NAME;
                }

                Console.WriteLine("Welcome " + livePlayer.getFirstName() + " to our Quiz!");

                //Asks questions and keeps the score of them

                int total = 0;
                for (int i = 0; i < quizQuestions.Count(); i++)
                {
                    Console.WriteLine("Q" + (i + 1) + ": ");
                    total = total + askQuestion(quizQuestions[i]);
                }
                livePlayer.incAttempt();
                Console.WriteLine(name + " you scored " + total + "/" + getTotalValue());
                Console.WriteLine("You have attempted it " + livePlayer.getAttempts());
                livePlayer.recordScore(total);
                DialogResult dialogResult = MessageBox.Show("Do you want to redo the quiz?", "Rerun quiz?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    rerunTest = true;
                }
                else
                {
                    writeToFile(toString(livePlayer), livePlayer);
                    Console.WriteLine("Thank you for playing the quiz " + livePlayer.getFirstName());
                    Console.WriteLine("Last score is: " + livePlayer.getPreviousScores());
                    Console.WriteLine("Highest score is: " + livePlayer.getHighestScore());
                    Environment.Exit(0);
                }
            }
        }
        public int askQuestion(Question q)
        {

            int score = 0;

            Console.WriteLine(q.getQuestion());

            //Gets user input and assumes it would be String

            string answer = Console.ReadLine();

            //If user entered nothing then it results in a zero

            if (answer.Count() < 1)
            {
                Console.WriteLine("No answer supplied, 0 points rewarded.");
            }
            else
            {

                if (q.isCorrect(answer))
                {
                    Console.WriteLine(answer + " is the correct answer, added " + q.getValue() + " to score.");
                    score = q.getValue();
                }
                else
                {
                    Console.WriteLine(answer + " is the wrong answer, 0 points awarded.");
                }
            }
            return score;
        }

        public void read(string file)
        {
            //Reads each line of file into 3 arrayLists, 1 for each row
            int index = -1;
            string line;
            string splitBy = ",";
            string splitFalse = ";";


            qType.Clear();
            questions.Clear();
            answers.Clear();
            value.Clear();
            options.Clear();

            /* For example
                QuizGame.QuestionTypes.Question,Answer,3 would be split into
                (QuizGame.QuestionTypes.Question) (Answer) (3)
             */


            try
            {
                using StreamReader myBuffer = new StreamReader(file);
                while ((line = myBuffer.ReadLine()) != null)
                {
                    index++;
                    options.Add(new List<string>());
                    string[] temp = line.Split(new[] { splitBy }, StringSplitOptions.None);
                    qType.Add(int.Parse(temp[0]));
                    questions.Add(temp[1]);
                    answers.Add(temp[2]);
                    value.Add(int.Parse(temp[3]));

                    if (qType[index] == 3)
                    {
                        string[] tempFalse = line.Split(new[] { splitFalse }, StringSplitOptions.None);

                        for (int i = 1; i < tempFalse.Length; i++)
                        {
                            options[index].Add(tempFalse[i]);
                        }
                    }
                }
                //Catches errors
            }
            catch (FileNotFoundException fnfe)
            {
                //Checks if the file cant be found even with the backup location
                if (file == myFileBackup)
                {
                    Console.Error.WriteLine("File could not be found anywhere");
                    Console.Error.WriteLine("CSV should be placed where a 'src' folder is visible or if ran from the jar, it should be next to the Jar file");
                    Environment.Exit(1);
                }
                else
                {
                    Console.Error.WriteLine("Couldn't find the file, searching the backup location");
                    read(myFileBackup);
                }

            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine("Exception thrown: " + ioe.Message);
            }
        }
        public string returnRead(string name)
        {
            try
            {
                using (StreamReader myBuffer = new StreamReader(name + ".csv"))
                {
                    return myBuffer.ReadLine();
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.Error.WriteLine("Could not find the file specified");
                return null;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }
        public void writeToFile(string output, Player currentPlayer)
        {
            try
            {
                createFile(currentPlayer);
                File.WriteAllText(currentPlayer.getFirstName() + ".csv", output);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine("Exception thrown: " + ioe.Message);
            }
        }

        public void createFile(Player currentPlayer)
        {
            string fileName = (currentPlayer.getFirstName().ToLower() + ".csv");
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("File Created: " + fileName);
                }
                else
                {
                    MessageBox.Show("File already exists (" + fileName + ")");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred.");
                Console.WriteLine(e);
            }
        }


        public int getTotalValue()
        {
            int total = 0;
            foreach (int integer in value)
            {
                total += integer;
            }
            return total;
        }

        public string toString(Player currentPlayer)
        {
            StringBuilder output = new StringBuilder();
            output.Append(currentPlayer.getFirstName());
            int size = currentPlayer.getScoreSize();
            Stack<int> working = new Stack<int>(currentPlayer.getPreviousScores());

            for (int i = 0; i < size; i++)
            {
                output.Append("," + working.Pop());
            }
            return output.ToString();
        }
        static void Main(string[] args)
        {
            Quiz myQuiz = new Quiz();

            myQuiz.start();
        }
    }
}