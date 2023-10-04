using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppC_.QuizGame
{
    public abstract class Question
    {
        private string question;
        private string answer;
        private int value;

        public Question(string question, string answer, int value)
        {
            this.question = question;
            this.answer = answer.ToLower();
            this.value = value;
        }
        public virtual string getQuestion()
        {
            return question + " (" + value + " pts) ?";
        }

        public void setQuestion(string question)
        {
            this.question = question;
        }

        public string getAnswer()
        {
            return answer;
        }

        public void setAnswer(string answer)
        {
            this.answer = answer;
        }

        public int getValue()
        {
            return value;
        }

        public void setValue(int value)
        {
            this.value = value;
        }

        public virtual bool isCorrect(string userSays)
        {
            //Checks to see if any answer was supplied
            if (userSays != null && userSays.Count() > 0)
            {
                //Sets the users response to lowercase and compares
                return userSays.ToLower().Equals(getAnswer());
            }
            return false;
        }

        public virtual string toString()
        {
            return "Question{" + "question='" + question + '\'' + ", answer ='" + answer + '\'' + ", points=" + value + '}';
        }
    }
}
