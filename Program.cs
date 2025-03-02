using System;


namespace CaveJourner{

    static class GlobalStore{
        public static ConsoleKeyInfo keyPress;
        public static string selectedColour = "\u001b[32m";
        public static string escapeColour = "\u001b[0m";
        public static int score = 0;
    }

    class Display{
        
    }

    class Timer{
        public static DateTime getTime(){
            return DateTime.Now;
        }
        public static TimeSpan timeDiffT(DateTime start, DateTime end){    
            return end.Subtract(start);
        }
    }

    class Quiz{
        string[] questions = {"How was your weekend?", "What's your best talent?", "Who is your icon?"};
        string[,] answers = {{"awful...", "decent", "the best"}, {"My eloquence", "My athleticism", "My candour"}, {"Ru-Paul", "Myself", "Nobody"}};
        int[] correctAns = [2, 0, 0];
        int choices = 3;

        public void DisplayScore(){
            Console.WriteLine($"You've earned a score of {GlobalStore.score}");
        }

        public void DisplayQuestion(int question){
            Console.WriteLine(questions[question]);
        }
    

        public void DisplayAnswers(int question){
            for (int ans = 0; ans < 3; ans++){
                    Console.WriteLine(answers[question, ans]);
                }
        }

        public void DisplayAnswersWithUserInput(int question){
            int selectedOption = 0;
            Boolean thinkingOfAnswer = true;
            //after asking the question, get the cursor position
            (int x, int y) = Console.GetCursorPosition();


            while (thinkingOfAnswer){
                Console.SetCursorPosition(x, y);
                for (int i = 0; i < choices; i++){
                    if (selectedOption == i){
                        Console.WriteLine(GlobalStore.selectedColour + answers[question, i] + GlobalStore.escapeColour);
                    }
                    else Console.WriteLine(answers[question, i] + GlobalStore.escapeColour);
                }

                GlobalStore.keyPress = Console.ReadKey(true);

                switch (GlobalStore.keyPress.Key){
                    case ConsoleKey.DownArrow:
                        selectedOption++;
                        if(selectedOption > 2){
                            selectedOption = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        selectedOption--;
                        if(selectedOption < 0){
                            selectedOption = 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        thinkingOfAnswer = false;
                        break;
                }
            } 

            if(selectedOption == correctAns[question]){
                    GlobalStore.score++;
            }

        }
    }

    class Program{

        static void Main(string[] args){
            Program prog = new Program();

            Console.WriteLine("I heard you're bored?\n");

            DateTime startTime = Timer.getTime();
            (int x, int y) = Console.GetCursorPosition();
            Boolean startQuiz = true;
            Quiz myQuiz = new Quiz();

            DateTime endTime = Timer.getTime();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Game data took {Timer.timeDiffT(startTime, endTime).ToString(@"mm\:ss\.ff")} to load...");

            Console.ResetColor();       

            while (startQuiz){
                for (int quizQuestion = 0; quizQuestion < 3; quizQuestion ++){
                    myQuiz.DisplayQuestion(quizQuestion);

                    myQuiz.DisplayAnswersWithUserInput(quizQuestion);

                }
                startQuiz = false;
            }

            myQuiz.DisplayScore();

        }

    }
}



