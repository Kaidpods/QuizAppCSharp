using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppC_.QuizGame.Questions
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(String question, Boolean answer, int value)
        :base(question, answer.ToString(), value)
        { 
        
        }
        public override String getQuestion()
        {
            return base.getQuestion() + "\n(Answer true or false)";
        }
    }
}
