using QuizAppC_.QuizGame.PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppC_.QuizGame
{
    public class Player : Person
    {
        private Stack<int> previousScores = new Stack<int>();

        private int attempts = 0;

        public Player(String firstName, String surname)
        : base(firstName, surname)
        {
            
        }
        public Player(String firstName, String surname, String scores)
        :this(firstName, surname)
        {
            

            //Split the string by commas, parse to Integers and add to stack
            String[] csvScores = scores.Split(",");
            foreach (String val in csvScores)
            {
                try
                {
                    int iVal = int.Parse(val);
                    previousScores.Push(iVal);
                }
                catch (FormatException nfe)
                {
                    Console.WriteLine("Problem parsing previous score information.");
                }
            }
        }
        public Stack<int> getPreviousScores()
        {
            return previousScores;
        }

        public void recordScore(int score)
        {
            previousScores.Push(score);
        }

        public int getLastScore()
        {
            try
            {
                return previousScores.Peek();
            }
            catch (Exception e)
            {
                Console.WriteLine("previousScores is empty, there are no scores to show");
                return 0;
            }

        }

        public int getScoreSize()
        {
            return previousScores.Count();
        }

        public int getHighestScore()
        {
            if (previousScores.Count() > 0)
            {
                //clone the stack so we don't change the original
                Stack<int> working = (Stack<int>)previousScores;
                //pop every element in the stack to findnew maximum score.
                //use a find maximum algorithm to find the highest score
                //use the working.pop() method to get a value from the stack.
                int maxValue = working.Pop();
                int size = working.Count();
                for (int i = 0; i < size; i++)
                {
                    if (working.Peek() > maxValue)
                    {
                        maxValue = working.Pop();
                    }
                    else
                    {
                        working.Pop();
                    }
                }
                return maxValue;
            }
            else
                return 0;
        }

        public int getAttempts()
        {
            return attempts;
        }

        public void incAttempt()
        {
            attempts++;
        }

    public override String ToString()
        {
            return "Player: " + base.getFullName() +
                    " previousScores=" + previousScores;
        }
    }
}
