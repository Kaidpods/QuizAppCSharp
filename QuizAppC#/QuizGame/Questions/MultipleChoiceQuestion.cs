using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizAppC_.QuizGame.Shuffle;

namespace QuizAppC_.QuizGame.Questions
{
    public class MultipleChoiceQuestion : Question
    {
        protected List<string> options = new List<string>();

        private readonly int correctIndex;

        public MultipleChoiceQuestion(String question, String answer, List<string> options, int points)
        :base(question, answer, points)
        {
            

            //Populate the list with all options and the correct answer, then finally randomises the list
            foreach (String choice in options)
            {
                this.options.Add(choice);
            }
            this.options.Add(answer);
            Shuffle.Shuffle.ShuffleList(this.options);

            //find index of correct answer and store for later
            this.correctIndex = this.options.IndexOf(answer);
        }


        
    public override String toString()
        {
            return "MultipleChoiceQuestion{" +
                    base.toString() +
                    "options=" + options +
                    "correct index=" + correctIndex +
                    '}';
        }

    public override String getQuestion()
        {
            StringBuilder output = new StringBuilder(base.getQuestion() + "\n");

            for (int i = 0; i < this.options.Count(); i++)
            {
                output.Append(i + 1).Append(": ");
                output.Append(options[i]).Append("\n");
            }

            output.Append("(Answer 1,2,3,4)");

            return output.ToString();
        }

        
    public override bool isCorrect(String userSays)
        {
            bool isCorrect = false;
            try
            {
                //Change user answer to an int and subtract 1 as index starts at value 0
                int userIndex = int.Parse(userSays) - 1;
                if (correctIndex == userIndex)
                {
                    isCorrect = true;
                }
            }
            catch (Exception e)
            {
                isCorrect = false;
            }
            return isCorrect;
        }
    }
}
